using Microsoft.EntityFrameworkCore;
using Sporcu.Entity;
using Sporcu.Repositories.Abstract;

namespace Sporcu.Repositories.Concrete
{
    public class SporcuSporDaliRepository : GenericRepository<TblSporcuSporDali>, ISporcuSporDaliRepository
    {
        public SporcuSporDaliRepository(DbContext context) : base(context)
        {
        }
    }
}
