using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using WorkinOut.Data.Database;
using WorkinOut.Data.IRepository;
using WorkinOut.Dtos;
using WorkinOut.Identity;
using WorkinOut.Models;

namespace WorkinOut.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class PersonController : ControllerBase
    {
        IPersonRepository _personRepository;
        IAccountRepository _accountRepository;
        private readonly WorkoutDbContext _context;
        private readonly UserManager<Account> _userManager;


        public PersonController(IPersonRepository personRepository, WorkoutDbContext context, UserManager<Account> userManager, IAccountRepository accountRepository)
        {
            _personRepository = personRepository;
            _context = context;
            _userManager = userManager;
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IEnumerable<Person> GetPeople()
        {
            return _personRepository.Read();

        }

        [HttpGet("person/{id}")]
        public ActionResult<Person> GetPerson(Guid id)
        {
            var person = _personRepository.ReadFromId(id);
            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpGet("pe/{personId}")]
        public async Task<ActionResult<IEnumerable<Person>>> GetUserPerson(Guid personId)
        {
            var user = _accountRepository.ReadByID(personId);   
            if (user == null)
            {
                return NotFound();
            }
            var peopleWithThisUserId = await _context.People
                .Where(p => p.Account.Email == user!.Email)
                .ToListAsync();
                return Ok(peopleWithThisUserId);
        }

        [HttpGet("withWorkouts")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeopleWithWorkouts()
        {

            var peopleWithWorkouts = await _context.People.Include(p => p.Workouts).ToListAsync();
            return Ok(peopleWithWorkouts);
        }
         
        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody]PersonCreateRequest person)
        {
            var username = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var account = await _userManager.FindByNameAsync(username);
            var savedPerson = _personRepository.Create(person);
            if (account == null)
            {
                _context.SaveChanges();
                return Ok("Profile has been created, but log in for more functions");
            }

            savedPerson.Account = account;
            _context.SaveChanges();
            return Ok(savedPerson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(Guid id, Person updatedPerson)
        {
            var existingPerson = _personRepository.ReadFromId(id);
            if (existingPerson == null)
                return NotFound();

            updatedPerson.PersonId = existingPerson.PersonId;
            _personRepository.Update(updatedPerson);

            return NoContent();
        }
         
        [HttpDelete]
        public async Task<IActionResult> DeleteFirstPerson()
        {
            var existingPerson = _personRepository.Read();
            if (existingPerson != null)
            {
                _personRepository.DeleteFirst(existingPerson.First());
                return Ok(existingPerson);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            var existingPerson = _personRepository.ReadFromId(id);
            if (existingPerson != null)
            {
                _personRepository.Delete(id);
                return Ok(existingPerson);
            }
                return NotFound();
        }

        //*--------------------------------------------------------------------------------------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------------------------------------------------//
        //-------------------------------------------------------------ORDER API REQUESTS--------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------------------------------------------------*/

        //[HttpPost]
        //public async Task<IActionResult> OrderPeopleByName()
        //{
        //    return Ok();
        //}

        //[HttpPost]
        //public async Task<IActionResult> OrderPeopleByTrainer()
        //{
        //    return Ok();
        //}

        //[HttpPost]
        //public async Task<IActionResult> OrderPeopleByMostWorkout()
        //{
        //    return Ok();
        //}
























    }
}
