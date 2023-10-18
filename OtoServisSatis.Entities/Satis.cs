using System.ComponentModel.DataAnnotations;

namespace OtoServisSatis.Entities
{
    public class Satis : IEntity
    {
        public int Id { get; set; }
        [Display(Name ="Araç Adı")]
        public int AracId { get; set; }
        public int MusteriId { get; set; }
        public decimal SatisFiyati { get; set; }
        public DateTime SatisTarih { get; set; }
        public virtual Arac? Arac { get; set; }
        public virtual Musteri? Musteri { get; set; }


    }
}
