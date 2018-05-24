using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demo3.Models
{
    [MetadataType(typeof(PublicMeta))]
    public partial class Public_Measure_Result
    {

    }

    public class PublicMeta
    {
        public int Measure_ID { get; set; }

        [DisplayName("Measure Abbreviation")]
        public string Measure_Abbreviation { get; set; }

        [DisplayName("Measure Title")]
        public string Measure_Title { get; set; }
    }
}