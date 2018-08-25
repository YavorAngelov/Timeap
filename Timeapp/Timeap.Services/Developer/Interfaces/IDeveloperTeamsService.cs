using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timeap.Common.Models.ViewModels;
using Timeap.Models;

namespace Timeap.Services.Developer.Interfaces
{
    public interface IDeveloperTeamsService
    {
        Team GetTeam(int id);

        Task JoinTeamAsync(int teamId, string devId);
    }
}
