using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces.Repositories {
    public interface ITeamRepository : IRepositoryBase<Team> {
        Task<IEnumerable<Team>> GetTeamListAsync();
        Task<Team> GetTeamByNameAsync(string teamName);
    }
}
