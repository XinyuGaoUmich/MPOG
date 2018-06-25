using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo3.Models
{
    public class EditPager
    {
        public IEnumerable<Pager_Auth_Result> pager_Auth_Results
        {
            get;
            set;
        }

        public IEnumerable<Pager_Result> pager_Results
        {
            get;
            set;
        }

        public IEnumerable<Enumeration_NQS_Domain> nQS_Domain
        {
            get;
            set;
        }

        public IEnumerable<Enumeration_Measure_Type> measure_Type
        {
            get;
            set;
        }

        public IEnumerable<Enumeration_Scope> scope
        {
            get;
            set;
        }

        public IEnumerable<Enumeration_Responsible_Provider> responsible_Provider
        {
            get;
            set;
        }

        public IEnumerable<Responsible_Provider_Unpublished> responsible_Provider_id
        {
            get;
            set;
        }

        public IEnumerable<Responsible_Provider_Published> responsible_Provider_id_published
        {
            get;
            set;
        }
    }
}