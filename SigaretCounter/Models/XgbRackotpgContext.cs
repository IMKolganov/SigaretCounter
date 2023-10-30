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
                .HasDefaultValueSql("nextval('countersigaretsid_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.CurrentDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        modelBuilder.Entity<Sigaret>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sigarets_pkey");

            entity.ToTable("Sigarets", "xgb_rackotpg");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('sigarets_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Brand)
                .HasMaxLength(255)
                .HasColumnName("brand");
            entity.Property(e => e.CountryOfOrigin)
                .HasMaxLength(255)
                .HasColumnName("country_of_origin");
            entity.Property(e => e.NicotineContent)
                .HasPrecision(5, 2)
                .HasColumnName("nicotine_content");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.ProductionDate).HasColumnName("production_date");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Type).WithMany(p => p.Sigarets)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("sigarets_type_id_fkey");
        });

        modelBuilder.Entity<SigaretsType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("sigaretstypes_pkey");

            entity.ToTable("SigaretsTypes", "xgb_rackotpg");

            entity.Property(e => e.TypeId)
                .HasDefaultValueSql("nextval('sigaretstypes_type_id_seq'::regclass)")
                .HasColumnName("type_id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("Users", "xgb_rackotpg");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('usersid_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Dateofbirth)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateofbirth");
            entity.Property(e => e.Displayname)
                .HasMaxLength(255)
                .HasColumnName("displayname");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Emailverified).HasColumnName("emailverified");
            entity.Property(e => e.Languageuser)
                .HasMaxLength(255)
                .HasColumnName("languageuser");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(50)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Photourl)
                .HasMaxLength(255)
                .HasColumnName("photourl");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });
        modelBuilder.HasSequence("countersigaretsid_seq", "xgb_rackotpg");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
