using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisSatis.Entities
{
    public class Musteri : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Araç")]
        public int AracId { get; set; }
        [StringLength(50)]
        [Display(Name = "Adı"), Required(ErrorMessage ="{0} Boş Bırakılamaz!")]
        public string Adi { get; set; }
        [Display(Name = "Soyadı"), Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public string Soyadi { get; set; }
        [Display(Name = "TC Numarası")]
        [StringLength(11)]
        public string? TcNo { get; set; }
        public string Email { get; set; }
        [StringLength(200)]
        public string? Adres { get; set; }
        public string? Telefon { get; set; }
        public string? Notlar { get; set; }
        [Display(Name ="Araç")]
        public Arac?  Arac { get; set; }


    }
}
