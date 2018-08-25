using AutoMapper;
using Timeap.Models;
using Timeap.Common.Models.ViewModels;
using Timeap.Common.Models.BindingModels;

namespace Timeap.Web.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<Project, ProjectConciseViewModel>();
            this.CreateMap<Team, TeamConciseViewModel>();
            this.CreateMap<SolutionCreateBindingModel, Solution>();
            this.CreateMap<Solution, SolutionDetailsViewModel>();
        }
    }
}
