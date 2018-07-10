using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo3.Models
{
    public class ModifyDomain
    {
        public List<Measure_Of_Domain_Published_Result> published_Domains
        {
            get;
            set;
        }

        public List<Measure_Of_Domain_Unpublished_Result> unpublished_Domains
        {
            get;
            set;
        }
    }
}