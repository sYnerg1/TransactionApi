using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace LegioTest.Domain.ModelsDTO
{
    public class ExcelFilterDTO
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeDTO Type { get; set; } = TypeDTO.All;

        [JsonConverter(typeof(StringEnumConverter))]
        public StatusDTO Status { get; set; } = StatusDTO.All;
    }
}
