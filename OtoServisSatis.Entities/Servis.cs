using System.ComponentModel.DataAnnotations;

namespace OtoServisSatis.Entities
{
    public  class Servis : IEntity
    {
        public int  Id { get; set; }
        [Display(Name = "Servis Geliş Tarihi")]
        public DateTime ServiseGelisTarihi { get; set; }
        [Display(Name = "Arac Sorunu")]
        public string AracSorunu { get; set; }
        [Display(Name = "Servis Ücreti")]
        public decimal ServisUcreti { get; set; }
        [Display(Name = "Servisten Çıkış Tarihi")]
        public DateTime ServistenCikisTarihi { get; set; }
        [Display(Name = "Yapılan İşlemler")]
        public string YapilanIslemler { get; set; }
        [Display(Name = "Garanti Kapsamında mı?")]
        public string GarantiKapsamindaMi { get; set; }
        [Display(Name ="Araç Plaka"),Required(ErrorMessage ="{0} Boş Bırakılamaz!")]
        public string AracPlaka { get; set; }
        public string Marka { get; set; }
        public string? Model { get; set; }
        [Display(Name ="Kasa Tipi")]
        public string? KasaTipi { get; set; }
        [Display(Name ="Şase No")]
        public string? SaseNo { get; set; }
        [Required(ErrorMessage ="{0} Boş Bırakılamaz!")]
        public string Notlar { get; set; }
    }
}
