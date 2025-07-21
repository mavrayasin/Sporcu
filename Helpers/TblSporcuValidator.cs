using FluentValidation;
using Sporcu.Entity;

public class TblSporcuValidator : AbstractValidator<TblSporcu>
{
    public TblSporcuValidator()
    {
        RuleFor(x => x.TcKimlikNo).NotEmpty().WithMessage("TC Kimlik No zorunludur.");
        RuleFor(x => x.AdSoyad).NotEmpty().WithMessage("Ad Soyad zorunludur.");
        RuleFor(x => x.DogumTarihi).NotEmpty().WithMessage("Doğum tarihi zorunludur.");
        RuleFor(x => x.Cinsiyet).NotEmpty().WithMessage("Cinsiyet zorunludur.");
        RuleFor(x => x.IlId).NotEmpty().WithMessage("İl seçimi zorunludur.");
        RuleFor(x => x.IlceId).NotEmpty().WithMessage("İlçe seçimi zorunludur.");
        RuleFor(x => x.LisansBaslangic).NotEmpty().WithMessage("Lisans başlangıç tarihi zorunludur.");
        RuleFor(x => x.LisansBitis).NotEmpty().WithMessage("Lisans bitiş tarihi zorunludur.");
    }
}

