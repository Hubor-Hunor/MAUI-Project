using WorkinOut.Data.Database;
using WorkinOut.Data.IRepository;
using WorkinOut.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkinOut.Dtos;
using AutoMapper;

namespace WorkinOut.Data.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly WorkoutDbContext _context;
        private readonly IMapper _mapper;
        public PersonRepository(WorkoutDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Person Create(PersonCreateRequest newperson)
        {
            var person = _mapper.Map<Person>(newperson);
            _context.People.Add(person);
            _context.SaveChanges();
            return person;
        }

        public IEnumerable<Person> Read()
        {
            return _context.People.ToList();
        }

        public Person ReadFromId(Guid id)
        {
            return _context.People.FirstOrDefault(p => p.PersonId == id);
        }

        public void DeleteFirst(Person person)
        {
            if (person != null)
            {
                _context.People.Remove(person);
                _context.SaveChanges();
            }
            
        }
        public void Delete(Guid id)
        {
            var person = _context.People.FirstOrDefault(p => p.PersonId == id);
            var deletingPerson = ReadFromId(id);
            if (deletingPerson != null)
            {
                _context.People.Remove(person);
                _context.SaveChanges();
            }
        }

        public void Update(Person person)
        {
            var existingPerson = _context.People.FirstOrDefault(p => p.PersonId == person.PersonId);
            if (existingPerson != null)
            {
                existingPerson.PersonName = person.PersonName;
                existingPerson.PersonAge = person.PersonAge;
                existingPerson.PersonGender = person.PersonGender;
                existingPerson.PersonIdentity = person.PersonIdentity;
                _context.SaveChanges();
            }
        }
    }
}
