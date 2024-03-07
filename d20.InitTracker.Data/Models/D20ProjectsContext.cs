using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace d20.InitTracker.Data.Models;

public partial class D20ProjectsContext : DbContext
{
    public D20ProjectsContext()
    {
    }

    public D20ProjectsContext(DbContextOptions<D20ProjectsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Combatant> Combatants { get; set; }

    public virtual DbSet<CombatantType> CombatantTypes { get; set; }

    public virtual DbSet<Encounter> Encounters { get; set; }

    public virtual DbSet<EncounterCombatant> EncounterCombatants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=tcp:jpaulson.database.windows.net,1433;initial catalog=d20Projects;user id=jpaulson@outlook.com@jpaulson;password=Jester123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Combatant>(entity =>
        {
            entity.HasKey(e => e.CombatantKey);

            entity.Property(e => e.CombatantKey).ValueGeneratedNever();
            entity.Property(e => e.ControlledBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Dmnotes)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DMNotes");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CombatantType>(entity =>
        {
            entity.HasKey(e => e.CombatantTypeKey);

            entity.ToTable("CombatantType");

            entity.Property(e => e.CombatantTypeKey).ValueGeneratedNever();
            entity.Property(e => e.CombatantTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Encounter>(entity =>
        {
            entity.HasKey(e => e.EncounterKey);

            entity.Property(e => e.EncounterKey).ValueGeneratedNever();
            entity.Property(e => e.EncounterName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EncounterStartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<EncounterCombatant>(entity =>
        {
            entity.HasKey(e => e.EncounterCombatantKey);

            entity.Property(e => e.EncounterCombatantKey).ValueGeneratedNever();

            entity.HasOne(d => d.CombatantKeyNavigation).WithMany(p => p.EncounterCombatants)
                .HasForeignKey(d => d.CombatantKey)
                .HasConstraintName("FK_EncounterCombatants_Combatants");

            entity.HasOne(d => d.EncounterKeyNavigation).WithMany(p => p.EncounterCombatants)
                .HasForeignKey(d => d.EncounterKey)
                .HasConstraintName("FK_EncounterCombatants_Encounters");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
