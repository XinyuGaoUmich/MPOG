using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo3.Models
{
    public class ModifyScope
    {
        public List<Measure_Of_Scope_Published_Result> published_Scopes
        {
            get;
            set;
        }

        public List<Measure_Of_Scope_Unpublished_Result> unpublished_Scopes
        {
            get;
            set;
        }
    }
}