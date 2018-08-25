using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timeap.Common.Models.ViewModels;

namespace Timeap.Services.Developer.Interfaces
{
    public interface IDeveloperProjectsService
    {
        Task<IEnumerable<ProjectConciseViewModel>> GetLatestProjectsAsync();
    }
}
