//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjeKulubu.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Team
    {
        public Team()
        {
            this.Office = new HashSet<Office>();
            this.Project = new HashSet<Project>();
        }
    
        public int ID { get; set; }
        public Nullable<int> MemberID { get; set; }
        public string MemberName { get; set; }
        public string MemberPozision { get; set; }
        public string MemberExperience { get; set; }
        public string MemberBiografi { get; set; }
        public Nullable<int> MemberAge { get; set; }
        public string MemberPictureURL { get; set; }
        public string MemberFacebookURL { get; set; }
        public string MemberTwitterURL { get; set; }
        public string MemberGoogleURL { get; set; }
        public string MemberLinkedinURL { get; set; }
    
        public virtual ICollection<Office> Office { get; set; }
        public virtual ICollection<Project> Project { get; set; }
    }
}
