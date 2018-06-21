using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace demo3.Models
{
    [MetadataType(typeof(DetailsMetaData))]
    public partial class Details_All_Result
    {

    }

    public class DetailsMetaData
    {
        public int Measure_ID { get; set; }

        [DisplayName("Measure Abbreviation")]
        public string Measure_Abbreviation { get; set; }

        [DisplayName("Measure Title")]
        public string Measure_Title { get; set; }

        [DisplayName("NQS Domain")]
        public string NQS_Domain { get; set; }

        [DisplayName("QCDR Measure Name")]
        public string QCDR_Measure_Name { get; set; }

        public Nullable<bool> VBR { get; set; }

        [DisplayName("Clinical Lead")]
        public string Clinical_Lead { get; set; }

        public string Developer { get; set; }

        [DisplayName("Measure Spec Completed")]
        public Nullable<bool> Measure_Spec_Completed { get; set; }

        [DisplayName("Date Published (mm/dd/yyyy)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: MM/dd/yyyy}")]
        public Nullable<System.DateTime> Date_Published { get; set; }

        [DisplayName("Status")]
        public Nullable<int> Status_ID { get; set; }

        [DisplayName("NQS Domain")]
        public string NQS_Domain_Name { get; set; }

        [DisplayName("Status")]
        public string Status_Name { get; set; }
    }
}