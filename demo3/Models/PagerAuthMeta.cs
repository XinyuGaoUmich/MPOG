using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demo3.Models
{
    [MetadataType(typeof(PagerAuthMeta))]
    public partial class Pager_Auth_Result
    {
    }

    public class PagerAuthMeta
    {
        [DisplayName("NQS Domain")]
        public string NQS_Domain { get; set; }

        public string Responsible_Provider { get; set; }

        public Nullable<int> Status_ID { get; set; }

        public string Status_Name { get; set; }

        [DisplayName("Measure Type")]
        public string Measure_Type { get; set; }

        public string Scope { get; set; }
        public int Measure_ID { get; set; }
        public string Measure_Abbreviation { get; set; }
        public string Data_Collection_Method { get; set; }
        public string Description { get; set; }
        public string Measure_Summary { get; set; }
        public string Inclusions { get; set; }
        public string Exclusions { get; set; }
        public string Success { get; set; }
        public Nullable<decimal> Threshold { get; set; }
        public string Risk_Adjustment { get; set; }
        public string Reference { get; set; }

        [DisplayName("NQS Domain")]
        public Nullable<int> NQS_Domain_ID { get; set; }

        public Nullable<int> Responsible_Provider_ID { get; set; }

        [DisplayName("Measure Type")]
        public Nullable<int> Measure_Type_ID { get; set; }
        public Nullable<int> Measure_Scope_ID { get; set; }
    }
}