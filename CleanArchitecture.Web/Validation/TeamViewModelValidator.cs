using System;
using CleanArchitecture.Web.ViewModels;
using FluentValidation;

namespace Web.InfrastructureServices.Validation.ViewModel {
	public class TeamViewModelValidator : AbstractValidator<TeamViewModel> {
		public TeamViewModelValidator() {
			RuleFor(x => x.Id).NotNull();
			RuleFor(x => x.Name).NotEmpty()
				.WithMessage("The name is required");
			RuleFor(x => x.Name)
				.Matches(@"^[0-9a-zA-Z ]+$")
				.WithMessage("Only numbers and letters are allowed.");
			RuleFor(x => x.FoundationYear)
				.NotEmpty().WithMessage("The year of foundation is required");
			RuleFor(x => x.Name).Length(0, 50);
		    RuleFor(x => x.FoundationYear).InclusiveBetween(1946, DateTime.Today.Year);
			RuleFor(x => x.Wins).InclusiveBetween(0, 76);
		}
	}
}
