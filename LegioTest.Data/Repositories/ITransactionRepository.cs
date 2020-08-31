using LegioTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegioTest.Data.Repositories
{
    public interface ITransactionRepository
    {
        Task AddRangeAsync(IEnumerable<Transaction> transactions);
        Task<bool> EditAsync(int id, Transaction transaction);
        Task<bool> DeleteAsync(int id);
        IQueryable<Transaction> GetQuery();
    }
}
