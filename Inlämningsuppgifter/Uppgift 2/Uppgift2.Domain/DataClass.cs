using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uppgift2.Domain
{
    public class DataClass
    {
        public int NoTimesRead { get; set; } = 1;
        public string InitialMessage { get; init; } = string.Empty;
        public string? ResponseMessage { get; set; }
    }
}
