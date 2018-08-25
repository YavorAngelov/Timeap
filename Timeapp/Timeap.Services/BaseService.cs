using AutoMapper;
using Timeap.Web.Data;

namespace Timeap.Services
{
    public class BaseService
    {
        protected BaseService(
            TimeapContext dbContext,
            IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        protected TimeapContext DbContext { get; private set; }

        protected IMapper Mapper { get; private set; }
    }
}
