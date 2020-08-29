using CsvHelper;
using LegioTest.Data.Models;
using LegioTest.Data.Repositories;
using LegioTest.Domain.ModelsDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegioTest.Domain.Services.Defaults
{
    public class TransctionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepo;

        public TransctionService(ITransactionRepository transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public async Task AddAsync(TransactionDTO transactionDTO)
        {
            Transaction transaction = new Transaction()
            {
                Type = transactionDTO.Type,
                Status = transactionDTO.Status,
                ClientName = transactionDTO.ClientName,
                Amount = transactionDTO.Amount
            };

            await _transactionRepo.AddAsync(transaction);
        }

        public Task AddRangeAsync(IEnumerable<TransactionDTO> transactionDTOs)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _transactionRepo.DeleteAsync(id);

            return result;
        }

        public async Task<bool> EditAsync(int id, TransactionDTO transactionDTO)
        {
            Transaction transaction = new Transaction()
            {
                Type = transactionDTO.Type,
                Status = transactionDTO.Status,
                ClientName = transactionDTO.ClientName,
                Amount = transactionDTO.Amount
            };

            return await _transactionRepo.EditAsync(id,transaction);
        }

        public async Task<PagedTransactionsDTO> Find(FilterDTO filter)
        {
            var transactionQuery = _transactionRepo.GetQuery();

            if (filter.Status != StatusDTO.All)
            {
                transactionQuery = transactionQuery
                    .Where(x => x.Status == (Status)filter.Status);
            }

            if (filter.Type != TypeDTO.All)
            {
                transactionQuery = transactionQuery
                    .Where(x => x.Type == (Type)filter.Type);
            }

            transactionQuery = transactionQuery
                .Skip((filter.PageNumber - 1) * 10)
                .Take(10);

            var transactions = await transactionQuery.ToListAsync();

            List<TransactionDTO> transactionDTOs = new List<TransactionDTO>();

            foreach (Transaction tr in transactions)
            {
                transactionDTOs.Add(new TransactionDTO()
                { 
                    Id = tr.Id,
                    ClientName = tr.ClientName,
                    Status = tr.Status,
                    Type = tr.Type,
                    Amount = tr.Amount
                });
            }

            PagedTransactionsDTO pagedResult = new PagedTransactionsDTO()
            {
                TransactionDTOs = transactionDTOs,
                PageNumber = filter.PageNumber
            };

            return pagedResult;
        }

        public Task<TransactionDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ReadFile(IFormFile file)
        {
            List<Transaction> transactions = new List<Transaction>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.GetCultureInfo("en-US")))
            {
                csv.Configuration.TypeConverterOptionsCache.GetOptions(typeof(decimal))
                    .NumberStyle = NumberStyles.AllowCurrencySymbol |
                     NumberStyles.AllowDecimalPoint;

                transactions = csv.GetRecords<Transaction>().ToList();
            }

            await _transactionRepo.AddRangeAsync(transactions);

            return true;
        }
    }
}
