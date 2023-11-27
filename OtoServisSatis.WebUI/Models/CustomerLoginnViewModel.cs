using System.ComponentModel.DataAnnotations;

namespace OtoServisSatis.WebUI.Models
{
    public class CustomerLoginnViewModel
    {
        [StringLength(100),Required(ErrorMessage ="{0} Boş Bırakılamaz!")]
        public string Email { get; set; }
        [Display(Name = "Şifre"),Required(ErrorMessage ="{0} Boş Bırakılamaz!")]
        public string  Sifre { get; set; }

    }
}
