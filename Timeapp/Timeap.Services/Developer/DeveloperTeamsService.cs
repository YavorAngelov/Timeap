using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Timeap.Common.Models.ViewModels;
using Timeap.Services.Developer.Interfaces;
using Timeap.Web.Data;
using Microsoft.EntityFrameworkCore;
using Timeap.Models;

namespace Timeap.Services.Developer
{
    public class DeveloperTeamsService : BaseService, IDeveloperTeamsService
    {
        public DeveloperTeamsService(TimeapContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }

        public Team GetTeam(int teamId)
        {
            return this.DbContext.Teams.FirstOrDefault(t => t.Id == teamId);
        }

        public async Task JoinTeamAsync(int teamId, string devId)
        {
            var userTeam = await this.DbContext.UsersTeams.FirstOrDefaultAsync(ut => ut.TeamId == teamId);

            if (userTeam == null)
            {
                await this.DbContext.UsersTeams.AddAsync(new UsersTeams()
                {
                    MemberId = devId,
                    TeamId = teamId
                });
                await this.DbContext.SaveChangesAsync();

                return;
            }

            userTeam.MemberId = devId;
            await this.DbContext.SaveChangesAsync();
        }
    }
}
