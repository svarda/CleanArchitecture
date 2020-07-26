using AutoMapper;
using CleanArchitecture.Web.ViewModels;
using CleanArchitecture.Core.Entities;

namespace Web.Models.Mapping {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile() {
            CreateMap<Team, TeamViewModel>().ReverseMap();
        }
    }
}
 