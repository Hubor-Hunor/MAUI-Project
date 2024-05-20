using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WorkinOut.Data.Database;
using WorkinOut.Data.IRepository;
using WorkinOut.Dtos;
using WorkinOut.Models;

namespace WorkinOut.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IPersonRepository _personRepository;
        private readonly WorkoutDbContext _context;
        private readonly UserManager<Account> _userManager;

        public WorkoutController(IWorkoutRepository workoutRepository, IPersonRepository personRepository, WorkoutDbContext workoutDbContext, UserManager<Account> userManager)
        {
            _workoutRepository = workoutRepository;
            _personRepository = personRepository;
            _context = workoutDbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<Workout> GetWorkouts()
        {
            return _workoutRepository.ReadAll();
        }

        [HttpGet("/{id}")]
        public async Task<ActionResult<Workout>> GetWorkoutById(Guid id)
        {
            var workout = _workoutRepository.ReadFromId(id);
            if (workout == null)
            {
                return NotFound();
            }
            return Ok(workout);
        }   
        
        [HttpGet("person/{personId}")]  
        public async Task<ActionResult<Workout>> GetPersonWorkoutById(Guid personId)
        {
            var person = _personRepository.ReadFromId(personId);
            if (person == null)
            {
                return NotFound();
            }
            var workoutByPerson = await _context.Workouts
                .Where(w => w.Person.PersonId == personId).ToListAsync();

            return Ok(workoutByPerson);
        }
        
        [HttpPost]
        public async Task<ActionResult<Workout>> CreateWorkout([FromBody]WorkoutCreateRequest workout)
        {
            var username = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var account = await _userManager.FindByNameAsync(username);
            if (account == null)
            {
                return NotFound("User not found");

            }


            //var person = account.Person.FirstOrDefault(p => p.PersonName == workout.Username);
            var currentPerson = _context.People.FirstOrDefault(p => p.PersonName == workout.Username);
            var savedworkout = _workoutRepository.Create(workout);
            if (currentPerson == null)
            {
                return NotFound("Profile not found");
            }


            savedworkout.Person = currentPerson;
            _context.SaveChanges();
            return Ok(savedworkout);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateWorkout(Guid id, Workout workout)
        {
            if (id != workout.WorkoutId)
            {
                return BadRequest();
            }

            var existingWorkout = _workoutRepository.ReadFromId(id);
            if (existingWorkout == null)
            {
                return NotFound();
            }

            _workoutRepository.Update(workout);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWorkout(Guid id)
        {
            var existingWorkout = _workoutRepository.ReadFromId(id);
            if (existingWorkout == null)
            {
                return NotFound();
            }

            _workoutRepository.Delete(id);

            return NoContent();
        }
    }
}
