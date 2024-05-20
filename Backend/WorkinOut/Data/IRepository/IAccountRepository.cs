using WorkinOut.Models;

namespace WorkinOut.Data.IRepository
{
    public interface IAccountRepository
    {
        void Create(Account Account);
        void DeleteByID(Guid id);
        IEnumerable<Account> ReadAll();
        Account ReadByID(Guid id);
        void Update(Account uptodate);
    }
}