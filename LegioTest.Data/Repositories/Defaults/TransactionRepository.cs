using LegioTest.Data.EntityFramerwork;
using LegioTest.Data.Models;
using Microsoft.Data.SqlClient;
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

        public async Task AddRangeAsync(IEnumerable<Transaction> transactions)
        {
          foreach(var t in transactions)
            {
                if (await _db.Transactions.AnyAsync(x => x.Id ==t.Id))
                {
                    _db.Entry(t).State = EntityState.Modified;
                }
                else
                {
                    _db.Entry(t).State = EntityState.Added;
                }
            }

           await _db.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existTransaction = await _db.Transactions
                .FirstOrDefaultAsync(x=>x.Id==id);

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

        public async Task<bool> EditAsync(int id, Transaction transaction)
        {
            var transactionFromDb = await _db.Transactions.FirstOrDefaultAsync(x=>x.Id==id);

            _db.Entry(transactionFromDb).CurrentValues.SetValues(transaction);

            bool result = await _db.SaveChangesAsync() > 0;

            return  result;
        }

        public IQueryable<Transaction> GetQuery()
        {
            return _db.Transactions.AsQueryable();
        }
    }
}
