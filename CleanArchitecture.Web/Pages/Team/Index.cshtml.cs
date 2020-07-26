using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Web.ViewModels;
using CleanArchitecture.Web.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System.Linq;

namespace Web.Pages.Team {

    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
    public class IndexModel : PageModel {
        public List<TeamViewModel> TeamList { get; set; } = new List<TeamViewModel>();

        private readonly ITeamPageService _teamPageService;

        public IndexModel(ITeamPageService teamPageService) {
            _teamPageService = teamPageService;
        }

        public async Task<IActionResult> OnGetAsync() {
            var teams = (await _teamPageService.GetAllTeamsAsync());
            if (teams != null) {
                TeamList = teams.ToList();
            }
            return Page();
        }
    }
}