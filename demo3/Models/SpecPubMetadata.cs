using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace demo3.Models
{
    [MetadataType(typeof(SpecPubMetadata))]
    public partial class Spec_Published_Result{}

    public class SpecPubMetadata
    {
        [DisplayName("Measure")]
        public string Measure_Abbreviation { get; set; }

        [DisplayName("NQS Domain")]
        public string NQS_Domain { get; set; }

        [DisplayName("Measure Type")]
        public string Measure_Type { get; set; }
    }
}