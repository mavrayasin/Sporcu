using Sporcu.Repositories.Abstract;

namespace Sporcu.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        
        ITblSporcuRepository TblSporcu { get; }
        ITblSporDaliRepository TblSporDali { get; }
        ISporcuSporDaliRepository SporcuSporDali { get; }
        int Complete(bool state = true);
        Task<int> CompleteAsync(bool state = true);
    }
}
