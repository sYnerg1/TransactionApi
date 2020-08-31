using LegioTest.Domain.ModelsDTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LegioTest.Domain.Services
{
    public interface ITransactionService
    {
        Task AddRangeAsync(IEnumerable<TransactionDTO> transactionDTOs);
        Task<bool> EditAsync(int id, TransactionDTO transactionDTO);
        Task<bool> DeleteAsync(int id);
        Task<PagedTransactionsDTO> Find(FilterDTO filter);
        Task<bool> ReadFile(IFormFile file);
        Task<byte[]> CreateCSV(ExcelFilterDTO filter);
    }
}
