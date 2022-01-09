﻿using Microsoft.EntityFrameworkCore;
using ReportService.Models;

namespace ReportService.Data
{
    public class ReportContext : DbContext
    {
        public ReportContext(DbContextOptions<ReportContext> options) : base(options)
        {

        }

        public DbSet<ReportRequest> ReportRequests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReportRequest>(entity =>
            {
                entity.HasKey(x => x.UUID);
            });
        }
    }
}
