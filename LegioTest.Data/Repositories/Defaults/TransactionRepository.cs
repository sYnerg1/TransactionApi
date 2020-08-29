using LegioTest.Data.EntityFramerwork;
using LegioTest.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LegioTest.Data.Repositories.Defaults
{
    internal class TransactionRepository : ITransactionRepository
    {
        private readonly LegioContext _db;

        public TransactionRepository(LegioContext db)
        {
            _db = db;
        }
        public async Task AddAsync(Transaction value)
        {
            await _db.Transactions.AddAsync(value);
        }

        public async Task AddRangeAsync(IEnumerable<Transaction> value)
        {
            await _db.Transactions.AddRangeAsync(value);
            
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existTransaction = await GetByIdAsync(id);

            if (existTransaction != null)
            {
                _db.Remove<Transaction>(existTransaction);
                return (await _db.SaveChangesAsync()) > 0;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> EditAsync(int id, Transaction value)
        {
            var transactionFromDb = await _db.Transactions.FirstOrDefaultAsync(x=>x.Id==id);

            _db.Entry(transactionFromDb).CurrentValues.SetValues(value);
            await _db.SaveChangesAsync();

            bool result = await _db.SaveChangesAsync() > 0;

            return  result;
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            return await _db.Transactions.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public IQueryable<Transaction> GetQuery()
        {
            return _db.Transactions.AsQueryable();
        }
    }
}
