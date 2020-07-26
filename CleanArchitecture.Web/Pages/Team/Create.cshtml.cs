using System.Threading.Tasks;
using CleanArchitecture.Web.Interfaces;
using CleanArchitecture.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Team {

    [Authorize]
    public class CreateModel : PageModel {

        [BindProperty]
        public TeamViewModel Team { get; set; }

        [TempData]
        public string FormResult { get; set; }

        private readonly ITeamPageService _teamPageService;

        public CreateModel(ITeamPageService teamPageService) {
            _teamPageService = teamPageService;
        }

        public IActionResult OnGet() {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }
            
            await _teamPageService.CreateTeamAsync(Team);
            return RedirectToPage("./Index");
        }
    }
}