using System;
using System.Collections.Generic;
using System.Text;

namespace LegioTest.Domain.ModelsDTO
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public Type Type { get; set; }
        public string ClientName { get; set; }
        public decimal Amount { get; set; }
    }
}
