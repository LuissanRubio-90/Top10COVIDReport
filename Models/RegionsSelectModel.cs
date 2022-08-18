using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopCovidCases.Models
{
    public class RegionData
    {
        public RegionsSelectModel[] data { get; set; }
    }

    public class RegionsSelectModel
    {
        public string iso { get; set; }
        public string name { get; set; }
        public bool selected { get; set; }
    }
}