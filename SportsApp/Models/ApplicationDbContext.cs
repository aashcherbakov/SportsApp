﻿using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models
{
    public class ApplicationDbContext : DbContext
    {
		public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./SportsApp.sqlite");
        }
    }
}