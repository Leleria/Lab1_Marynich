﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace testing_lab1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class testingDBEntities : DbContext
    {
        public testingDBEntities()
            : base("name=testingDBEntities")
        {
        }

        private static testingDBEntities context;
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public static testingDBEntities GetContext()
        {
            if (context == null)
                context = new testingDBEntities();
            return context;
        }
        public virtual DbSet<Users> Users { get; set; }
    }
}