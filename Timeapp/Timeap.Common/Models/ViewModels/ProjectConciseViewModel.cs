using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timeap.Common.Models.ViewModels
{
    public class ProjectConciseViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TeamId { get; set; }
    }
}
