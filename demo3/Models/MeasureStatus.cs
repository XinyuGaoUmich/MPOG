using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demo3.Models;

namespace demo3.Models
{
    public class MeasureStatus
    {
        public IEnumerable<Measure_List_Result> Measure_List_Results
        {
            get;
            set;
        }

        public IEnumerable<Status_Type> Status_Types
        {
            get;
            set;
        }
      
    }
}