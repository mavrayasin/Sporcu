using System;
using System.Collections.Generic;

namespace Sporcu.Entity;

using System.ComponentModel.DataAnnotations;

public class TblSporcu
{
    public int Id { get; set; }

    [Required(ErrorMessage = "TC Kimlik No zorunludur.")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır.")]
    public string TcKimlikNo { get; set; }

    [Required(ErrorMessage = "Ad Soyad zorunludur.")]
    [StringLength(100, ErrorMessage = "Ad Soyad en fazla 100 karakter olabilir.")]
    public string AdSoyad { get; set; }

    [Required(ErrorMessage = "Doğum tarihi zorunludur.")]
    [DataType(DataType.Date)]
    public DateTime DogumTarihi { get; set; }

    [Required(ErrorMessage = "Cinsiyet zorunludur.")]
    [StringLength(1, ErrorMessage = "Cinsiyet E veya K olmalıdır.")]
    public string Cinsiyet { get; set; }

    [Required(ErrorMessage = "İl ID zorunludur.")]
    public int IlId { get; set; }

    [Required(ErrorMessage = "İlçe ID zorunludur.")]
    public int IlceId { get; set; }

    [Required(ErrorMessage = "Lisans başlangıç tarihi zorunludur.")]
    [DataType(DataType.Date)]
    public DateTime LisansBaslangic { get; set; }

    [Required(ErrorMessage = "Lisans bitiş tarihi zorunludur.")]
    [DataType(DataType.Date)]
    public DateTime LisansBitis { get; set; }

    public bool AktifMi { get; set; }
}
