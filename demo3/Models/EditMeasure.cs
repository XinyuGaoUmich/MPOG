using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo3.Models
{
    public class EditMeasure
    {
        public IEnumerable<Details_All_Result> Details_All_Results
        {
            get;
            set;
        }

        public IEnumerable<Status_Type> Status_Types
        {
            get;
            set;
        }

        public IEnumerable<Enumeration> Enumerations
        {
            get;
            set;
        }
    }
}