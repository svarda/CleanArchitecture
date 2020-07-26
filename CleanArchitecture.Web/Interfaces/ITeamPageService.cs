using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Web.ViewModels;

namespace CleanArchitecture.Web.Interfaces {
    public interface ITeamPageService {
        Task<IEnumerable<TeamViewModel>> GetAllTeamsAsync();
        Task<TeamViewModel> GetTeamByIdAsync(int id);
        Task<TeamViewModel> CreateTeamAsync(TeamViewModel teamViewModel);
        Task UpdateTeamAsync(TeamViewModel teamViewModel);
        Task DeleteTeamAsync(int id);
    }
}
