using System;
using System.Collections.Generic;
using System.Text;

namespace LegioTest.Domain.ModelsDTO
{
    public class FilterDTO
    {
        public TypeDTO Type { get; set; }
        public StatusDTO Status { get; set; }
        public int PageNumber { get; set; }
    }

    public enum StatusDTO
    {
        Pending,
        Completed,
        Cancelled,
        All
    }

    public enum TypeDTO
    {
        Refill,
        Withdrawal,
        All
    }
}
