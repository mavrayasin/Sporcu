using System;
using System.Collections.Generic;

namespace Sporcu.Entity;

public partial class TblSporcu
{
    public int Id { get; set; }

    public string TcKimlikNo { get; set; } = null!;

    public string AdSoyad { get; set; } = null!;

    public DateOnly DogumTarihi { get; set; }

    public string Cinsiyet { get; set; } = null!;

    public int IlId { get; set; }

    public int IlceId { get; set; }

    public DateOnly LisansBaslangic { get; set; }

    public DateOnly LisansBitis { get; set; }

    public bool AktifMi { get; set; }
}
