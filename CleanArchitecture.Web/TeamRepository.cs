using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.DataContext;
using System.Collections.Generic;

namespace CleanArchitecture.Infrastructure.Repository {
    public class TeamRepository : RepositoryBase<Team>, ITeamRepository {
        public TeamRepository(ApplicationDataContext dbContext) 
            : base(dbContext) {            
        }

        public async Task<IEnumerable<Team>> GetTeamListAsync() {
            return await _dbContext.Set<Team>().ToListAsync();
        }

        public async Task<Team> GetTeamByNameAsync(string name) {
            return await _dbContext.Set<Team>().Where(x => x.Name == name).FirstOrDefaultAsync();
        }
    }
}
