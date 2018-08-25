using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Timeap.Services.Developer.Interfaces;
using Timeap.Common.Models.ViewModels;
using Timeap.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace Timeap.Services.Developer
{
    public class DeveloperProjectsService : BaseService, IDeveloperProjectsService
    {
        public DeveloperProjectsService(TimeapContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<IEnumerable<ProjectConciseViewModel>> GetLatestProjectsAsync()
        {
            var latestProjects = await this.DbContext.Projects
                .Include(p => p.Team)
                .OrderByDescending(p => p.StartDate)
                .Take(6)
                .ToListAsync();

            var modelProjects = this.Mapper.Map<IEnumerable<ProjectConciseViewModel>>(latestProjects);

            return modelProjects;
        }
    }
}
