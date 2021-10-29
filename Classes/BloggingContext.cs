using System;
using System.Collections.Generic;
using EFGetStarted.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFGetStarted.Classes
{
    public class BloggingContext : DbContext
    {
      

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        
        public string DbPath { get; private set; }

        public BloggingContext()
        {
            //*
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}blogging.db";
            /*/
            DbPath = "C:\\Users\\minhk\\AppData\\Local\\blogging.db"; 
            /**/
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
             options.UseSqlite($"Data Source={DbPath}");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            //one blog has many post
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Blog)
                .WithMany(b => b.Posts);

            modelBuilder.Entity<Blog>()
                .Property(b => b.Url)
                .HasMaxLength(25) // maximum length
                .IsRequired();
                

            modelBuilder.Entity<Blog>()
                .HasIndex(b => b.Url)
                .IsUnique();

            /*
            modelBuilder.Entity<Blog>().Property<string>("_tenantId").HasColumnName("TenantId");

            // Configure entity filters
            #region FilterConfiguration
            modelBuilder.Entity<Blog>().HasQueryFilter(b => EF.Property<string>(b, "_tenantId") == _tenantId);
            
            #endregion
            */

            modelBuilder.Entity<Post>().HasQueryFilter(p => !p.IsDeleted);
            
            modelBuilder.Entity<Blog>().HasMany(b => b.Posts).WithOne(p => p.Blog).IsRequired();
            modelBuilder.Entity<Blog>().HasQueryFilter(b => b.Url.Contains("com"));
            modelBuilder.Entity<Post>().HasQueryFilter(p => p.Blog.Url.Contains("com"));
            /*/
            /**/
        }

    }

}