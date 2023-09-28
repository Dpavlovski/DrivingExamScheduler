using DrivingExamScheduler.Domain.Models.Domain;
using DrivingExamScheduler.Domain.Models.Identity;
using DrivingExamScheduler.Domain.Models.Realation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DrivingExamScheduler.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<Candidate>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<RequirementForCategory>()
                .HasKey(key => new { key.RequirementId, key.CategoryId });

            builder.Entity<RequirementForCategory>()
                .HasOne(z => z.Requirement)
                .WithMany(z => z.RequirementsForCategory)
                .HasForeignKey(z => z.RequirementId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<RequirementForCategory>()
                .HasOne(z => z.Category)
                .WithMany(z => z.RequirementsForCategory)
                .HasForeignKey(z => z.CategoryId);

            builder.Entity<Requirement>()
               .HasOne(z => z.DocumentType)
               .WithMany(z => z.Requirements)
               .HasForeignKey(z => z.DocumentTypeId);

            builder.Entity<TimeSlot>()
                .HasOne(z => z.Location)
                .WithMany(z => z.TimeSlots)
                .HasForeignKey(z => z.LocationId);

            builder.Entity<Appointment>()
               .HasOne(z => z.Exam)
               .WithMany(z => z.AppointmentsForExam)
               .HasForeignKey(z => z.ExamId);

            builder.Entity<Appointment>()
               .HasOne(z => z.Candidate)
               .WithOne(z => z.Appointment)
               .HasForeignKey<Appointment>(z => z.CandidateId);

            builder.Entity<Document>()
               .HasOne(z => z.DocumentType)
               .WithMany(z => z.Documents)
               .HasForeignKey(z => z.DocumentTypeId);

            builder.Entity<Document>()
              .HasOne(z => z.Candidate)
              .WithMany(z => z.Documents)
              .HasForeignKey(z => z.CandidateId);

            builder.Entity<Requirement>()
                .HasOne(z => z.RequiredCategory)
                .WithMany(z => z.Requirements)
                .HasForeignKey(z => z.RequiredCategoryId);

            builder.Entity<Exam>()
                .HasOne(z => z.TimeSlot)
                .WithOne(z => z.Exam)
                .HasForeignKey<Exam>(z => z.TimeSlotId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Exam>()
               .HasOne(z => z.Category)
               .WithMany(z => z.Exams)
               .HasForeignKey(z => z.CategoryId);

            builder.Entity<Category>()
              .HasMany(z => z.Candidates)
              .WithOne(z => z.CurrentCategory)
              .HasForeignKey(z => z.CategoryId)
              .OnDelete(DeleteBehavior.NoAction);

        }

        public DbSet<Category> Category { get; set; } = default!;

        public DbSet<Location> Location { get; set; } = default!;

        public DbSet<Requirement> Requirement { get; set; } = default!;

        public DbSet<TimeSlot> TimeSlot { get; set; } = default!;

        public DbSet<DocumentType> DocumentType { get; set; } = default!;

        public DbSet<Document> Document { get; set; } = default!;

        public DbSet<Exam> Exam { get; set; } = default!;

        public DbSet<Appointment> Appointment { get; set; } = default!;

        public DbSet<RequirementForCategory> RequirementsForCategory { get; set; } = default!;
    }
}