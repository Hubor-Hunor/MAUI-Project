using WorkinOut.Dtos;
using WorkinOut.Models;

namespace WorkinOut.Data.IRepository
{
    public interface IWorkoutRepository
    {
        Workout Create(WorkoutCreateRequest workout);
        void Delete(Guid workoutId);
        IEnumerable<Workout> ReadAll();
        Workout? ReadFromId(Guid workoutId);
        void Update(Workout workout);
    }
}