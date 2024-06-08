using candidatehub.Application.DTO;
using candidatehub.Domain;
using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace candidatehub.Application.Validation
{
    public class CandidateValidator : AbstractValidator<CandidateDTO>
    {
        public CandidateValidator()
        {
            RuleFor(candidate => candidate.FirstName)
              .NotEmpty().WithMessage("First name is required.");

            RuleFor(candidate => candidate.LastName)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(candidate => candidate.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(candidate => candidate.Comment)
                .NotEmpty().WithMessage("Comment is required.");


            When(x => x.PhoneNumber is not null, () =>
            RuleFor(candidate => candidate.PhoneNumber)
                 .Must(IsValidateMobileNo).WithMessage("Invalid phone number"));

            When(x=>x.LinkedInUrl is not null,() =>
            RuleFor(candidate => candidate.LinkedInUrl)
                 .Must(IsValidUrl).WithMessage("Invalid LinkedIn profile URL format."));

            When(x => x.GitHubUrl is not null, () =>
            RuleFor(candidate => candidate.GitHubUrl)
                 .Must(IsValidUrl).WithMessage("Invalid GitHub profile URL format."));
        }

        private bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }

        private bool IsValidateMobileNo(string? mobileNo)
        {
            return !string.IsNullOrEmpty(mobileNo) && Regex.IsMatch(mobileNo, @"^\d{10}$") ? true : false;
        }

    }
}
