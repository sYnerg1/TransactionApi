using System;
using System.Collections.Generic;
using System.Text;

namespace LegioTest.Domain.ModelsDTO
{
    public class PagedTransactionsDTO
    {
        public IEnumerable<TransactionDTO> TransactionDTOs { get; set; }
        public int PageNumber { get; set; }
    }
}
