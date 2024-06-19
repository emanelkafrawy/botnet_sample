using System;
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
	}
}

