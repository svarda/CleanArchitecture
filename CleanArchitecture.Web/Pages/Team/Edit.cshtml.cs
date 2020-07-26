using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Web.ViewModels;
using CleanArchitecture.Web.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Web.Pages.Team {

    [Authorize]
    public class EditModel : PageModel {
        
        [BindProperty]
        public TeamViewModel Team { get; set; }

        [TempData]
        public string FormResult { get; set; }

        private readonly ITeamPageService _teamPageService;

        public EditModel(ITeamPageService teamPageService) {
            _teamPageService = teamPageService;
        }

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (!ModelState.IsValid) {
                return Page();
            }
            if (id == null) {
                return NotFound();
            }

            Team = await _teamPageService.GetTeamByIdAsync(id.Value);
            if (Team == null) {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }
            try {
                await _teamPageService.UpdateTeamAsync(Team);
            } catch (DbUpdateConcurrencyException) {
                if (!TeamExists(Team.Id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }
            return RedirectToPage("./Index");
        }

        private bool TeamExists(int id) {
            var team = _teamPageService.GetTeamByIdAsync(id);
            return team != null;
        }
    }
}
