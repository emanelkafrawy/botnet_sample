﻿using System;
using firstProject.Models;
using Microsoft.EntityFrameworkCore;

namespace firstProject.Data
{
	public class AppDbContext: DbContext
    { //constructor
		//here to initialize your DBdb connection
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
		{

		}
		public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                  new Category() { Id = 1, Name = "Select Category" },
                  new Category() { Id = 2, Name = "Computers" },
                  new Category() { Id = 3, Name = "Mobiles" },
                  new Category() { Id = 4, Name = "Electric machines" }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}

