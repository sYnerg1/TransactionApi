﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace LegioTest.Domain.ModelsDTO
{
    public class FilterDTO
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeDTO Type { get; set; } = TypeDTO.All;

        [JsonConverter(typeof(StringEnumConverter))]
        public StatusDTO Status { get; set; } = StatusDTO.All;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }

    public enum StatusDTO
    {
        Pending ,
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
