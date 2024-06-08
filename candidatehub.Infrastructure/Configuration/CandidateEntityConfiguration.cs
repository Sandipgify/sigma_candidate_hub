using candidatehub.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace candidatehub.Infrastructure.Configuration
{
    public class CandidateEntityConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName)
                .HasMaxLength(100).IsRequired(true);

            builder.Property(c => c.LastName)
                .HasMaxLength(100).IsRequired(true);

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(c => c.Email)
                .IsUnique();

            builder.Property(c => c.CallTimeInterval)
                .HasMaxLength(100);

            builder.Property(c => c.LinkedInUrl)
                .HasMaxLength(200);

            builder.Property(c => c.GitHubUrl)
                .HasMaxLength(200);

            builder.Property(c => c.Comment)
                .IsRequired();
        }
    }
}
