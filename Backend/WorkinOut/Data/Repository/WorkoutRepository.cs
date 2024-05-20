
using AutoMapper;
using WorkinOut.Data.Database;
using WorkinOut.Data.IRepository;
using WorkinOut.Dtos;
using WorkinOut.Models;

namespace WorkinOut.Data.Repository
{
    public class WorkoutRepository : IWorkoutRepository
    {
        WorkoutDbContext _context;
        private readonly IMapper _mapper;
        public WorkoutRepository(WorkoutDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public Workout Create(WorkoutCreateRequest newworkout)
        {
            var workout = _mapper.Map<Workout>(newworkout);
            _context.Workouts.Add(workout);
            //_context.SaveChanges();
            return workout;


            //context.Workouts.Add(workout);
            //if (context.People.FirstOrDefault(p => p.PersonId == workout.PersonId) != null)
            //{
            //    context.People.FirstOrDefault(p => p.PersonId == workout.PersonId).Workouts.Add(workout);
            //    Person person = contex    t.People.FirstOrDefault(p => p.PersonId == workout.PersonId);
            //    person.Workouts.Add(workout);
            //}
            //context.SaveChanges();
            ////context.Workouts.Add(workout);
            ////context.SaveChanges();
        }

        public IEnumerable<Workout> ReadAll()
        {
            return _context.Workouts;
        }
        public Workout? ReadFromId(Guid workoutId)
        {
            return _context.Workouts.FirstOrDefault(w => w.WorkoutId == workoutId);
        }

        public void Delete(Guid workoutId)
        {
            var workout = ReadFromId(workoutId);
            _context.Workouts.Remove(workout);
            _context.SaveChanges();
        }

        public void Update(Workout workout)
        {
            var old = ReadFromId(workout.WorkoutId);
            old.MuscleType = workout.MuscleType;
            old.WorkoutTime_Weights = workout.WorkoutTime_Weights;
            old.WorkoutTime_Cardio = workout.WorkoutTime_Cardio;
            old.WorkoutDifficulty = workout.WorkoutDifficulty;

            _context.SaveChanges();
        }   
    }
}
