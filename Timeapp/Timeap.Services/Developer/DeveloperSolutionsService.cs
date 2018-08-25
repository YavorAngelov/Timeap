using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Timeap.Services.Developer.Interfaces;
using Timeap.Common.Models.BindingModels;
using Timeap.Common.Models.ViewModels;
using Timeap.Web.Data;
using Timeap.Models;
using Microsoft.EntityFrameworkCore;
using Timeap.Common.Validation;
using System.Linq;

namespace Timeap.Services.Developer
{
    public class DeveloperSolutionsService : BaseService, IDeveloperSolutionsService
    {
        public DeveloperSolutionsService(TimeapContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task CreateAsync(SolutionCreateBindingModel model, int teamId)
        {
            var solution = this.Mapper.Map<Solution>(model);
            solution.TeamId = teamId;

            var statusWithType = await this.AddStatusForSolutionAsync(solution);
            solution.StatusId = statusWithType.Id;

            this.DbContext.SaveChanges();
        }

        private async Task<Status> AddStatusForSolutionAsync(Solution solution)
        {
            var status = new Status()
            {
                Progress = 0M,
            };

            await this.DbContext.Statuses.AddAsync(status);

            var statusWithType = await this.AddTypeForStatusAsync(status);
            return statusWithType;
        }

        private async Task<Status> AddTypeForStatusAsync(Status status)
        {
            var statusType = new StatusType()
            {
                Name = ValidationConstants.StatusTypeInProgress
            };

            await this.DbContext.StatusTypes.AddAsync(statusType);

            status.StatusTypeId = statusType.Id;

            return status;
        }

        public async Task<SolutionDetailsViewModel> GetDetailsAsync(int id)
        {
            var solution = await this.DbContext.Solutions
                .Include(s => s.Status)
                .FirstOrDefaultAsync(s => s.Id == id);

            var model = this.Mapper.Map<SolutionDetailsViewModel>(solution);
            return model;
        }
    }
}
