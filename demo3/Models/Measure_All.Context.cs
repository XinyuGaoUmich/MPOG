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
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MPOG_XinyuEntities4 : DbContext
    {
        public MPOG_XinyuEntities4()
            : base("name=MPOG_XinyuEntities4")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Status_Type> Status_Type { get; set; }
    
        public virtual ObjectResult<Details_All_Result> Details_All(Nullable<int> measure_ID)
        {
            var measure_IDParameter = measure_ID.HasValue ?
                new ObjectParameter("Measure_ID", measure_ID) :
                new ObjectParameter("Measure_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Details_All_Result>("Details_All", measure_IDParameter);
        }
    
        public virtual int Add_Measure(string measure_Abbreviation, string measure_Title, string nQS_Domain, string qCDR_Measure_Name, Nullable<bool> vBR, string clinical_Lead, string developer, Nullable<bool> measure_Spec_Completed, Nullable<System.DateTime> date_Published)
        {
            var measure_AbbreviationParameter = measure_Abbreviation != null ?
                new ObjectParameter("Measure_Abbreviation", measure_Abbreviation) :
                new ObjectParameter("Measure_Abbreviation", typeof(string));
    
            var measure_TitleParameter = measure_Title != null ?
                new ObjectParameter("Measure_Title", measure_Title) :
                new ObjectParameter("Measure_Title", typeof(string));
    
            var nQS_DomainParameter = nQS_Domain != null ?
                new ObjectParameter("NQS_Domain", nQS_Domain) :
                new ObjectParameter("NQS_Domain", typeof(string));
    
            var qCDR_Measure_NameParameter = qCDR_Measure_Name != null ?
                new ObjectParameter("QCDR_Measure_Name", qCDR_Measure_Name) :
                new ObjectParameter("QCDR_Measure_Name", typeof(string));
    
            var vBRParameter = vBR.HasValue ?
                new ObjectParameter("VBR", vBR) :
                new ObjectParameter("VBR", typeof(bool));
    
            var clinical_LeadParameter = clinical_Lead != null ?
                new ObjectParameter("Clinical_Lead", clinical_Lead) :
                new ObjectParameter("Clinical_Lead", typeof(string));
    
            var developerParameter = developer != null ?
                new ObjectParameter("Developer", developer) :
                new ObjectParameter("Developer", typeof(string));
    
            var measure_Spec_CompletedParameter = measure_Spec_Completed.HasValue ?
                new ObjectParameter("Measure_Spec_Completed", measure_Spec_Completed) :
                new ObjectParameter("Measure_Spec_Completed", typeof(bool));
    
            var date_PublishedParameter = date_Published.HasValue ?
                new ObjectParameter("Date_Published", date_Published) :
                new ObjectParameter("Date_Published", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Add_Measure", measure_AbbreviationParameter, measure_TitleParameter, nQS_DomainParameter, qCDR_Measure_NameParameter, vBRParameter, clinical_LeadParameter, developerParameter, measure_Spec_CompletedParameter, date_PublishedParameter);
        }
    
        public virtual int Delete_Measure(Nullable<int> measure_ID)
        {
            var measure_IDParameter = measure_ID.HasValue ?
                new ObjectParameter("Measure_ID", measure_ID) :
                new ObjectParameter("Measure_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Measure", measure_IDParameter);
        }
    
        public virtual int Edit_Measure(Nullable<int> measure_ID, string measure_Abbreviation, string measure_Title, string nQS_Domain, string qCDR_Measure_Name, Nullable<bool> vBR, string clinical_Lead, string developer, Nullable<bool> measure_Spec_Completed, Nullable<System.DateTime> date_Published)
        {
            var measure_IDParameter = measure_ID.HasValue ?
                new ObjectParameter("Measure_ID", measure_ID) :
                new ObjectParameter("Measure_ID", typeof(int));
    
            var measure_AbbreviationParameter = measure_Abbreviation != null ?
                new ObjectParameter("Measure_Abbreviation", measure_Abbreviation) :
                new ObjectParameter("Measure_Abbreviation", typeof(string));
    
            var measure_TitleParameter = measure_Title != null ?
                new ObjectParameter("Measure_Title", measure_Title) :
                new ObjectParameter("Measure_Title", typeof(string));
    
            var nQS_DomainParameter = nQS_Domain != null ?
                new ObjectParameter("NQS_Domain", nQS_Domain) :
                new ObjectParameter("NQS_Domain", typeof(string));
    
            var qCDR_Measure_NameParameter = qCDR_Measure_Name != null ?
                new ObjectParameter("QCDR_Measure_Name", qCDR_Measure_Name) :
                new ObjectParameter("QCDR_Measure_Name", typeof(string));
    
            var vBRParameter = vBR.HasValue ?
                new ObjectParameter("VBR", vBR) :
                new ObjectParameter("VBR", typeof(bool));
    
            var clinical_LeadParameter = clinical_Lead != null ?
                new ObjectParameter("Clinical_Lead", clinical_Lead) :
                new ObjectParameter("Clinical_Lead", typeof(string));
    
            var developerParameter = developer != null ?
                new ObjectParameter("Developer", developer) :
                new ObjectParameter("Developer", typeof(string));
    
            var measure_Spec_CompletedParameter = measure_Spec_Completed.HasValue ?
                new ObjectParameter("Measure_Spec_Completed", measure_Spec_Completed) :
                new ObjectParameter("Measure_Spec_Completed", typeof(bool));
    
            var date_PublishedParameter = date_Published.HasValue ?
                new ObjectParameter("Date_Published", date_Published) :
                new ObjectParameter("Date_Published", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Edit_Measure", measure_IDParameter, measure_AbbreviationParameter, measure_TitleParameter, nQS_DomainParameter, qCDR_Measure_NameParameter, vBRParameter, clinical_LeadParameter, developerParameter, measure_Spec_CompletedParameter, date_PublishedParameter);
        }
    
        public virtual ObjectResult<Collations_Result> Collations(Nullable<int> measure_ID)
        {
            var measure_IDParameter = measure_ID.HasValue ?
                new ObjectParameter("Measure_ID", measure_ID) :
                new ObjectParameter("Measure_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Collations_Result>("Collations", measure_IDParameter);
        }
    
        public virtual ObjectResult<Data_Diagnostics_Affected_Result> Data_Diagnostics_Affected(Nullable<int> measure_ID)
        {
            var measure_IDParameter = measure_ID.HasValue ?
                new ObjectParameter("Measure_ID", measure_ID) :
                new ObjectParameter("Measure_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Data_Diagnostics_Affected_Result>("Data_Diagnostics_Affected", measure_IDParameter);
        }
    
        public virtual ObjectResult<MPOG_Concept_ID_Required_Result> MPOG_Concept_ID_Required(Nullable<int> measure_ID)
        {
            var measure_IDParameter = measure_ID.HasValue ?
                new ObjectParameter("Measure_ID", measure_ID) :
                new ObjectParameter("Measure_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MPOG_Concept_ID_Required_Result>("MPOG_Concept_ID_Required", measure_IDParameter);
        }
    
        public virtual ObjectResult<Pager_Result> Pager(Nullable<int> measure_ID)
        {
            var measure_IDParameter = measure_ID.HasValue ?
                new ObjectParameter("Measure_ID", measure_ID) :
                new ObjectParameter("Measure_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pager_Result>("Pager", measure_IDParameter);
        }
    
        public virtual ObjectResult<Spec_Result> Spec(Nullable<int> measure_ID)
        {
            var measure_IDParameter = measure_ID.HasValue ?
                new ObjectParameter("Measure_ID", measure_ID) :
                new ObjectParameter("Measure_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Spec_Result>("Spec", measure_IDParameter);
        }
    
        public virtual ObjectResult<Public_Measure_Result> Public_Measure()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Public_Measure_Result>("Public_Measure");
        }
    
        public virtual ObjectResult<Pager_Auth_Result> Pager_Auth(Nullable<int> measure_ID)
        {
            var measure_IDParameter = measure_ID.HasValue ?
                new ObjectParameter("Measure_ID", measure_ID) :
                new ObjectParameter("Measure_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pager_Auth_Result>("Pager_Auth", measure_IDParameter);
        }
    
        public virtual ObjectResult<Measure_List_Result> Measure_List()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Measure_List_Result>("Measure_List");
        }
    }
}
