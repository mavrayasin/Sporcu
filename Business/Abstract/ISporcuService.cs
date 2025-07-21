using Sporcu.Dtos;
using Sporcu.Entity;

namespace Sporcu.Business.Abstract
{
    public interface ISporcuService
    {
        Task<List<TblSporcu>> GetAllSporcuAsync();
        Task<int> AddSporcuAsync(TblSporcu tblSporcu);
        Task<TblSporcu> GetSporcuByIdAsync(int id);
        Task<int> UpdateSporcuAsync(TblSporcu tblSporcu);
        Task<int> DeleteSporcuAsync(int id);
        Task<List<SporcuSporDaliDTO>> GetSporcuRapor();
        Task<List<SporDetayCountDTO>> GetSporcuSayisiRapor();
    }
}
