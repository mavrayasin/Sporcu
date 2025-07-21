namespace Sporcu.Dtos
{
    public class SporcuSporDaliDTO
    {
        public int Id { get; set; }
        public int SporcuId { get; set; }
        public int SporDaliId { get; set; }
        public string SporcuAdSoyad { get; set; }
        public string SporDaliAd { get; set; }
        public DateTime KayitTarihi { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
        public bool AktifMi { get; set; }
    }
}
