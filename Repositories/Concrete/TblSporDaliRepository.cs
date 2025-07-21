using Microsoft.EntityFrameworkCore;
using Sporcu.Entity;
using Sporcu.Repositories.Abstract;

namespace Sporcu.Repositories.Concrete
{
    public class TblSporDaliRepository : GenericRepository<TblSporDali>, ITblSporDaliRepository
    {
        public TblSporDaliRepository(DbContext context) : base(context)
        {
        }
    }
}
