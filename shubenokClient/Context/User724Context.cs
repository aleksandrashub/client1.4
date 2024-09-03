using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using shubenokClient.Models;

namespace shubenokClient.Context;

public partial class User724Context : DbContext
{
    public User724Context()
    {
    }

    public User724Context(DbContextOptions<User724Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientFile> ClientFiles { get; set; }

    public virtual DbSet<ClientTag> ClientTags { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLazyLoadingProxies().UseNpgsql("Host=192.168.2.159;Database=user724;Username=user724;password=68202");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("clients_pk");

            entity.ToTable("clients");

            entity.Property(e => e.IdClient)
                .ValueGeneratedNever()
                .HasColumnName("id_client");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.DateReg).HasColumnName("date_reg");
            entity.Property(e => e.IdGender).HasColumnName("id_gender");
            entity.Property(e => e.Mail)
                .HasColumnType("character varying")
                .HasColumnName("mail");
            entity.Property(e => e.NameClient)
                .HasColumnType("character varying")
                .HasColumnName("name_client");
            entity.Property(e => e.OtchestvoCl)
                .HasColumnType("character varying")
                .HasColumnName("otchestvo_cl");
            entity.Property(e => e.Phone)
                .HasColumnType("character varying")
                .HasColumnName("phone");
            entity.Property(e => e.Photo)
                .HasColumnType("character varying")
                .HasColumnName("photo");
            entity.Property(e => e.SurnameCl)
                .HasColumnType("character varying")
                .HasColumnName("surname_cl");

            entity.HasOne(d => d.IdGenderNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdGender)
                .HasConstraintName("clients_gender_fk");
        });

        modelBuilder.Entity<ClientFile>(entity =>
        {
            entity.HasKey(e => e.IdClientFile).HasName("client_file_pk");

            entity.ToTable("client_file");

            entity.Property(e => e.IdClientFile)
                .ValueGeneratedNever()
                .HasColumnName("id_client_file");
            entity.Property(e => e.Filename)
                .HasColumnType("character varying")
                .HasColumnName("filename");
            entity.Property(e => e.IdClient).HasColumnName("id_client");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.ClientFiles)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("client_file_clients_fk");
        });

        modelBuilder.Entity<ClientTag>(entity =>
        {
            entity.HasKey(e => e.IdClientTag).HasName("client_tag_pk");

            entity.ToTable("client_tag");

            entity.Property(e => e.IdClientTag)
                .ValueGeneratedNever()
                .HasColumnName("id_client_tag");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.IdTag).HasColumnName("id_tag");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.ClientTags)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("client_tag_clients_fk");

            entity.HasOne(d => d.IdTagNavigation).WithMany(p => p.ClientTags)
                .HasForeignKey(d => d.IdTag)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("client_tag_tags_fk");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.IdGender).HasName("gender_pk");

            entity.ToTable("gender");

            entity.Property(e => e.IdGender)
                .ValueGeneratedNever()
                .HasColumnName("id_gender");
            entity.Property(e => e.NameGender)
                .HasColumnType("character varying")
                .HasColumnName("name_gender");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.IdTag).HasName("tags_pk");

            entity.ToTable("tags");

            entity.Property(e => e.IdTag)
                .ValueGeneratedNever()
                .HasColumnName("id_tag");
            entity.Property(e => e.NameTag)
                .HasColumnType("character varying")
                .HasColumnName("name_tag");
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasKey(e => e.IdClientVisit).HasName("visits_pk");

            entity.ToTable("visits");

            entity.Property(e => e.IdClientVisit)
                .ValueGeneratedNever()
                .HasColumnName("id_client_visit");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.TimedateVisit)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timedate_visit");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Visits)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("visits_clients_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
