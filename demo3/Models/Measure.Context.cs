﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace demo3.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MPOG_XinyuEntities3 : DbContext
    {
        public MPOG_XinyuEntities3()
            : base("name=MPOG_XinyuEntities3")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ASPIRE_Measures> ASPIRE_Measures { get; set; }
        public virtual DbSet<MPOG_Concept_Types> MPOG_Concept_Types { get; set; }
        public virtual DbSet<MPOG_Concepts> MPOG_Concepts { get; set; }
        public virtual DbSet<Diagnostic_BarCharts> Diagnostic_BarCharts { get; set; }
        public virtual DbSet<Diagnostic_LineCharts> Diagnostic_LineCharts { get; set; }
        public virtual DbSet<Section_fields> Section_fields { get; set; }
        public virtual DbSet<Measure_Site> Measure_Site { get; set; }
        public virtual DbSet<Measure_Status> Measure_Status { get; set; }
    }
}
