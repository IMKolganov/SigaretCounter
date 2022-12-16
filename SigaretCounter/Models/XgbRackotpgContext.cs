using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace SigaretCounter.Models;

public partial class XgbRackotpgContext : DbContext
{
    private readonly string _connectionString;
    public XgbRackotpgContext()
    {
    }

    public XgbRackotpgContext(DbContextOptions<XgbRackotpgContext> options)
        : base(options)
    {
    }

    public XgbRackotpgContext(IOptions<DbConnectionInfo> dbConnectionInfo)
    {
        _connectionString = dbConnectionInfo.Value.WebApiDatabase;
    }


    public virtual DbSet<CounterSigaret> CounterSigarets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

            optionsBuilder.UseNpgsql(_connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CounterSigaret>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CounterSigarets_pkey");

            entity.ToTable("CounterSigarets", "xgb_rackotpg");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
