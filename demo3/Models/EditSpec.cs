using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo3.Models
{
    public class EditSpec
    {
        public IEnumerable<Spec_Result> spec_Results
        {
            get;
            set;
        }

        public IEnumerable<Spec_Published_Result> spec_Published_Results
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

        public IEnumerable<Collations_Result> collations_Results
        {
            get;
            set;
        }

        public IEnumerable<Data_Diagnostics_Affected_Result> data_Diagnostics_Affected_Results
        {
            get;
            set;
        }

        public IEnumerable<MPOG_Concept_ID_Required_Result> concept_ID_Required_Results
        {
            get;
            set;
        }

        public IEnumerable<Concept_Each_Header> all_Concept_ids
        {
            get;
            set;
        }

        public IEnumerable<MPOG_Concepts> all_Concepts
        {
            get;
            set;
        }

    }
}