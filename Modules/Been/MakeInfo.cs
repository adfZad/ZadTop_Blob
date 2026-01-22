using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class MakeInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AltDescription { get; set; }
        public bool Active { get; set; }
        public long CreatedId { get; set; }
        public string CreatedName { get; set; }
        public DateTime CreatedTime { get; set; }
        public long UpdatedId { get; set; }
        public string UpdatedName { get; set; }
        public DateTime UpdatedTime { get; set; }

        public string NameDescription
        {
            get
            {
                return string.Format("{0}-{1}", Name, Description);
            }
        }
        public string NameAltDescription
        {
            get
            {
                return string.Format("{0}-{1}", Name, AltDescription);
            }
        }
    }
}
