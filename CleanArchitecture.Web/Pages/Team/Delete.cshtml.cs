using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CleanArchitecture.Web.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Web.Pages.Team {

    [Authorize]
    public class DeleteModel : PageModel {

        [BindProperty]
        public TeamViewModel Team { get; set; }

        [TempData]
        public string FormResult { get; set; }

        private readonly ITeamPageService _teamPageService;

        public DeleteModel(ITeamPageService teamPageService) {
            _teamPageService = teamPageService;
        }

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            Team = await _teamPageService.GetTeamByIdAsync(id.Value);
            if (Team == null) {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            await _teamPageService.DeleteTeamAsync(Team.Id);
            return RedirectToPage("./Index");
        }
    }
}
