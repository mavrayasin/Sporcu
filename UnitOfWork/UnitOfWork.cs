using Microsoft.EntityFrameworkCore.Storage;
using Sporcu.Data;
using Sporcu.Entity;
using Sporcu.Repositories.Abstract;
using Sporcu.Repositories.Concrete;

namespace Sporcu.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SporcuTakipDbContext _context;

        private IDbContextTransaction? transaction = null;

        public UnitOfWork(SporcuTakipDbContext context)
        {
            _context = context;
            TblSporcu = new TblSporcuRepository(_context);
            TblSporDali = new TblSporDaliRepository(_context);
            SporcuSporDali = new SporcuSporDaliRepository(_context);
        }
        public ITblSporcuRepository TblSporcu { get; private set; }
        public ITblSporDaliRepository TblSporDali { get; private set; }
        public ISporcuSporDaliRepository SporcuSporDali { get; private set; }

        public int Complete(bool state = true)
        {
            transaction = _context.Database.BeginTransaction();
            var result = _context.SaveChanges();
            if (state)
                transaction.Commit();
            else
                transaction.Rollback();

            return result;
        }

        public async Task<int> CompleteAsync(bool state = true)
        {
            transaction = _context.Database.BeginTransaction();
            var result = await _context.SaveChangesAsync();
            if (state)
                transaction.Commit();
            else
                transaction.Rollback();

            return result;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
