using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timeap.Common.Models.BindingModels;
using Timeap.Common.Models.ViewModels;

namespace Timeap.Services.Developer.Interfaces
{
    public interface IDeveloperSolutionsService
    {
        Task CreateAsync(SolutionCreateBindingModel model, int teamId);

        Task<SolutionDetailsViewModel> GetDetailsAsync(int id);
    }
}
