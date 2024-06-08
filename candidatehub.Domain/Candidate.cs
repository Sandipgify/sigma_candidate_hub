using System.ComponentModel.DataAnnotations;

namespace candidatehub.Domain
{
    public class Candidate
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string? CallTimeInterval { get; set; }
        [Url]
        public string? LinkedInUrl { get; set; }
        [Url]
        public string? GitHubUrl { get; set; }
        public string Comment { get; set; }
    }
}
