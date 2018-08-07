using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demo3.Models
{
    [MetadataType(typeof(SpecMetadata))]
    public partial class Spec_Result { }

    public class SpecMetadata
    {
        [DisplayName("Measure")]
        public string Measure_Abbreviation { get; set; }

        [DisplayName("NQS Domain")]
        public string NQS_Domain { get; set; }

        [DisplayName("Measure Type")]
        public string Measure_Type { get; set; }
    }

    

}