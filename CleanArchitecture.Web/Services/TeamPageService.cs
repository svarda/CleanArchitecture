using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Web.Interfaces;
using CleanArchitecture.Web.ViewModels;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Web.Services {
    public class TeamPageService : ITeamPageService {

        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public TeamPageService(ITeamRepository teamRepository, IMapper mapper) {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamViewModel>> GetAllTeamsAsync() {
            var teams = await _teamRepository.GetAllAsync();
            var mapped = _mapper.Map<IEnumerable<TeamViewModel>>(teams);
            return mapped;
        }

        public async Task<TeamViewModel> GetTeamByIdAsync(int id) {
            var team = await _teamRepository.GetByIdAsync(id);
            var mapped = _mapper.Map<TeamViewModel>(team);
            return mapped;
        }

        public async Task<TeamViewModel> CreateTeamAsync(TeamViewModel teamViewModel) {
            var mapped = _mapper.Map<Team>(teamViewModel);
            if (mapped == null)
                throw new Exception($"TeamModel entity could not be mapped");

            var team = await _teamRepository.AddAsync(mapped);
            var mappedViewModel = _mapper.Map<TeamViewModel>(team);
            return mappedViewModel;
        }

        public async Task UpdateTeamAsync(TeamViewModel teamViewModel) {
            var mapped = _mapper.Map<Team>(teamViewModel);
            if (mapped == null)
                throw new Exception($"TeamModel entity could not be mapped");

            await _teamRepository.UpdateAsync(mapped);
        }

        public async Task DeleteTeamAsync(int id) {
            var team = await _teamRepository.GetByIdAsync(id);
            await _teamRepository.DeleteAsync(team);
        }
    }
}
