using System;
using System.Collections.Generic;
using System.Text;

namespace LegioTest.Domain.Options
{
    public class JwtOption
    {
        public string Key { get; set; }
        public int Expires { get; set; }
    }
}
