using Microsoft.EntityFrameworkCore;
using Sporcu.Data;
using Sporcu.Entity;
using Sporcu.Repositories.Abstract;

namespace Sporcu.Repositories.Concrete
{
    public class TblSporcuRepository : GenericRepository<TblSporcu>, ITblSporcuRepository
    {
        public TblSporcuRepository(DbContext context) : base(context)
        {
        }

    }
}
