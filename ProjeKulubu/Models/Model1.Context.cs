﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class db2299D218BEEntities8 : DbContext
    {
        public db2299D218BEEntities8()
            : base("name=db2299D218BEEntities8")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Admin> Admin { get; set; }
        public DbSet<AskedQuestions> AskedQuestions { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<CustomerComments> CustomerComments { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<EducationType> EducationType { get; set; }
        public DbSet<LoginList> LoginList { get; set; }
        public DbSet<Office> Office { get; set; }
        public DbSet<OfficePictures> OfficePictures { get; set; }
        public DbSet<OurPictures> OurPictures { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectPicture> ProjectPicture { get; set; }
        public DbSet<ProjectType> ProjectType { get; set; }
        public DbSet<Reference> Reference { get; set; }
        public DbSet<sysdiagrams> sysdiagrams { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Team> Team { get; set; }
    }
}
