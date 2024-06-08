using candidatehub.Domain;
using candidatehub.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace candidatehub.Infrastructure
{
    public class CandidateHubContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public CandidateHubContext(DbContextOptions<CandidateHubContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new CandidateEntityConfiguration());
        }

    }
}
