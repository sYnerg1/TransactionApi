using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Text;

namespace LegioTest.Data.Models
{
    public class Transaction
    {
        [Name("TransactionId")]
        public int Id { get; set; }
        public Status Status { get; set; }
        public Type Type { get; set; }
        public string ClientName { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Amount { get; set; }
    }
}


public enum Status
{
    Pending,
    Completed,
    Cancelled
}

public enum Type
{
    Refill,
    Withdrawal
}
