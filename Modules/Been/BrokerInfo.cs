using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class BrokerInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Website { get; set;}
        public string Email { get; set; }
        public long CountryId{get;set;}
        public string Description { get; set; }
    }
}
