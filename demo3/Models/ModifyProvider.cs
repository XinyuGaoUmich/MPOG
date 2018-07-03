using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo3.Models
{
    public class ModifyProvider
    {
        public List<Measure_Of_Provider_Published_Result> published_Providers
        {
            get;
            set;
        }

        public List<Measure_Of_Provider_Unpublished_Result> unpublished_Providers
        {
            get;
            set;
        }
    }
}