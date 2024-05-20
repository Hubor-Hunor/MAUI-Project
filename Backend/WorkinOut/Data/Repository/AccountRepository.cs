using WorkinOut.Data.Database;
using WorkinOut.Data.IRepository;
using WorkinOut.Models;

namespace WorkinOut.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        WorkoutDbContext dbContext;
        public AccountRepository(WorkoutDbContext db)
        {
            this.dbContext = db;
        }

        public void Create(Account Account)
        {
            dbContext.Accounts.Add(Account);
            dbContext.SaveChanges();
        }

        public void DeleteByID(Guid id)
        {
            var Account = ReadByID(id);
            dbContext.Accounts.Remove(Account);
            dbContext.SaveChanges();
        }

        public IEnumerable<Account> ReadAll()
        {
            return dbContext.Accounts;
        }

        public Account ReadByID(Guid id)
        {
            return dbContext.Accounts.FirstOrDefault(a => a.AccountId == id);
        }

        public void Update(Account update)
        {
            DeleteByID(update.AccountId);
            dbContext.Accounts.Add(update);
            dbContext.SaveChanges();
        }
    }
}
