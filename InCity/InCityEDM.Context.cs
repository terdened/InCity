﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InCity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class InCityDBEntities1 : DbContext
    {
        public InCityDBEntities1()
            : base("name=InCityDBEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Pictures> Pictures { get; set; }
        public virtual DbSet<Place> Place { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<Type> Type { get; set; }
        public virtual DbSet<EventPlace> EventPlace { get; set; }
        public virtual DbSet<EventTag> EventTag { get; set; }
    }
}
