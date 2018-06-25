using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demo3.Models
{
    [MetadataType(typeof(PagerMeta))]
    public partial class Pager_Result
    {
    }

    public class PagerMeta
    {
        public int Measure_ID { get; set; }

        [DisplayName("Abbreviation")]
        public string Measure_Abbreviation { get; set; }

        [DisplayName("Data Collection Method")]
        public string Data_Collection_Method { get; set; }

        public string Description { get; set; }

        [DisplayName("NQS Domain")]
        public Nullable<int> NQS_Domain { get; set; }

        [DisplayName("Measure Type")]
        public Nullable<int> Measure_Type { get; set; }

        public Nullable<int> Scope { get; set; }

        [DisplayName("Measure Summary")]
        public string Measure_Summary { get; set; }

        public string Inclusions { get; set; }

        public string Exclusions { get; set; }

        public string Success { get; set; }
   
        [DisplayFormat(DataFormatString ="{0:0.00}%")]
        public Nullable<decimal> Threshold { get; set; }

        [DisplayName("Responsible Provider")]
        public string Responsible_Provider { get; set; }

        [DisplayName("Risk Adjustment")]
        public string Risk_Adjustment { get; set; }

        public string Reference { get; set; }
    }
}