using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo3.Models
{
    public class ModifyMeasureType
    {
        public List<Measure_Of_Measure_Type_Published_Result> published_Measure_Types
        {
            get;
            set;
        }

        public List<Measure_Of_Measure_Type_Unpublished_Result> unpublished_Measure_Types
        {
            get;
            set;
        }
    }
}