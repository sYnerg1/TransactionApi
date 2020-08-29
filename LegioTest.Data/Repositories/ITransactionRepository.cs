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
        Task AddAsync(Transaction value);
        Task AddRangeAsync(IEnumerable<Transaction> value);
        Task<Transaction> GetByIdAsync(int id);
        Task<bool> EditAsync(int id, Transaction value);
        Task<bool> DeleteAsync(int id);
        IQueryable<Transaction> GetQuery();
    }
}
