using Microsoft.AspNetCore.Mvc.Rendering;
using Sporcu.Entity;

namespace Sporcu.Dtos
{
    public class SporcuSporDaliAddDTO
    {
        public int SporcuId { get; set; }
        public int SporDaliId { get; set; }
        public List<SelectListItem> SporcuListesi { get; set; }

        public List<SelectListItem> SporDaliListesi { get; set; }

    }
}
