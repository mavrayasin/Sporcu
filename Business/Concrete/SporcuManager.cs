using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Sporcu.Business.Abstract;
using Sporcu.Data;
using Sporcu.Dtos;
using Sporcu.Entity;
using Sporcu.UnitOfWork;

namespace Sporcu.Business.Concrete
{
    public class SporcuManager(IUnitOfWork _unitOfWork, SporcuTakipDbContext _dbContext) : ISporcuService
    {
        public async Task<List<TblSporcu>> GetAllSporcuAsync()
        {
            var sporcular = await _unitOfWork.TblSporcu.GetAllAsync();
            if (sporcular == null || !sporcular.Any())
            {
                throw new KeyNotFoundException("No sporcu found");
            }
            return sporcular.ToList();
        }
        public async Task<int> AddSporcuAsync(TblSporcu tblSporcu)
        {
            if (tblSporcu == null)
            {
                throw new ArgumentNullException(nameof(tblSporcu), "Sporcu cannot be null");
            }
            await _unitOfWork.TblSporcu.AddAsync(tblSporcu);
            return await _unitOfWork.CompleteAsync();
        }
        public async Task<int> UpdateSporcuAsync(TblSporcu tblSporcu)
        {
            if (tblSporcu == null)
            {
                throw new ArgumentNullException(nameof(tblSporcu), "Sporcu cannot be null");
            }
            _unitOfWork.TblSporcu.Update(tblSporcu);
            return await _unitOfWork.CompleteAsync();
        }
        public async Task<TblSporcu> GetSporcuByIdAsync(int id)
        {
            var sporcu = await _unitOfWork.TblSporcu.GetByIdAsync(id);
            if (sporcu == null)
            {
                throw new KeyNotFoundException("Sporcu not found");
            }
            return sporcu;
        }
        public async Task<int> DeleteSporcuAsync(int id)
        {
            var sporcu = await _unitOfWork.TblSporcu.GetByIdAsync(id);
            if (sporcu == null)
            {
                throw new KeyNotFoundException("Sporcu not found");
            }
            _unitOfWork.TblSporcu.Delete(sporcu);
            return await _unitOfWork.CompleteAsync();
        }

        public  async Task<List<SporcuSporDaliDTO>> GetSporcuRapor()
        {
            var liste = new List<SporcuSporDaliDTO>();
            liste = (from sporcuSporDali in _dbContext.TblSporcuSporDalis
                    join sporcu in _dbContext.TblSporcus on sporcuSporDali.SporcuId equals sporcu.Id
                    join sporDali in _dbContext.TblSporDalis on sporcuSporDali.SporDaliId equals sporDali.Id
               
                    select new SporcuSporDaliDTO
                    {
                        SporcuId = sporcu.Id,
                        SporcuAdSoyad = sporcu.AdSoyad,
                        SporDaliAd = sporDali.Ad,
                       
                    }
                ).ToList();
            return liste;
        }
        public async Task<List<SporDetayCountDTO>> GetSporcuSayisiRapor()
        {
            var liste = new List<SporDetayCountDTO>();
            liste = (from sporDali in _dbContext.TblSporDalis
                     join sporcuSporDali in _dbContext.TblSporcuSporDalis on sporDali.Id equals sporcuSporDali.SporDaliId
                     into joinSporcu
                     from sporcuSporDali in joinSporcu.DefaultIfEmpty()

                     select new SporDetayCountDTO
                     {
                         id = sporDali.Id,
                         sporName = sporDali.Ad,

                     }).ToList();


            var sonucListe = (from a in liste
                              group a by a.id into sporDallar
                              select new SporDetayCountDTO
                              {
                                  id = sporDallar.FirstOrDefault().id,
                                  sporName = sporDallar.FirstOrDefault().sporName,
                                  count = _dbContext.TblSporcuSporDalis.Count(x => x.SporDaliId == sporDallar.FirstOrDefault().id)
                              }
                             ).ToList();

            return sonucListe;
        }

        //public async Task<List<SporDetayCountDTO>> GetSporcuSayisiRapor()
        //{
        //    var sonucListe = await (from sporDali in _dbContext.TblSporDalis

        //                            join sporcuSporDali in _dbContext.TblSporcuSporDalis
        //                            on sporDali.Id equals sporcuSporDali.SporDaliId into joinSporcu

        //                            from sporcuSporDali in joinSporcu.DefaultIfEmpty()

        //                            group sporcuSporDali by new { sporDali.Id, sporDali.Ad } into grouped

        //                            select new SporDetayCountDTO
        //                            {
        //                                id = grouped.Key.Id,
        //                                sporName = grouped.Key.Ad,
        //                                count = grouped.Count(x => x != null) // null olanları sayma, sadece sporcusu olanları say
        //                            }).ToListAsync();

        //    return sonucListe;
        //}



    }
}
