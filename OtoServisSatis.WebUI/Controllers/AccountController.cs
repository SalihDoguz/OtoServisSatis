using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;
using OtoServisSatis.WebUI.Models;
using System.Security.Claims;

namespace OtoServisSatis.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _serviceKullanici;
        private readonly IService<Rol> _serviceRol;

        public AccountController(IUserService serviceKullanici, IService<Rol> serviceRol)
        {
            _serviceKullanici = serviceKullanici;
            _serviceRol = serviceRol;
        }
        [Authorize]
        public IActionResult Index()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var guid = User.FindFirst(ClaimTypes.UserData)?.Value;
           
            if (!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(guid))
            {
                var user = _serviceKullanici.Get(k => k.Email == email || k.UserGuid.ToString() == guid);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult UserUpdate(Kullanici kullanici)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var guid = User.FindFirst(ClaimTypes.UserData)?.Value;
                if (!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(guid))
                {
                    var user = _serviceKullanici.Get(k => k.Email == email || k.UserGuid.ToString() == guid);
                    if (user != null)
                    {
                        user.Adi = kullanici.Adi;
                        user.AktifMi = kullanici.AktifMi;
                        user.Email = kullanici.Email;
                        user.Sifre = kullanici.Sifre;
                        user.Soyadi = kullanici.Soyadi;
                        user.Telefon = kullanici.Telefon;
                        _serviceKullanici.Update(user);
                        _serviceKullanici.Save();
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var rol = _serviceRol.Get(x => x.Adi == "Customer");
                    if (rol == null)
                    {
                        ModelState.AddModelError("", "Rol Bulunamadı!");
                        return View();
                    }
                    kullanici.RolId = rol.Id;
                    kullanici.AktifMi = true;
                    await _serviceKullanici.AddAsync(kullanici);
                    await _serviceKullanici.SaveAsync();
                    return Redirect(nameof(Index));
                }
                catch
                {

                    ModelState.AddModelError("", "Müşteri Kaydı başarısız!");
                }
            }
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(CustomerLoginnViewModel customerLoginVM)
        {
            try
            {
                var account = await _serviceKullanici.GetAsync(k => k.Email == customerLoginVM.Email && k.Sifre == customerLoginVM.Sifre && k.AktifMi == true);
                if (account == null)
                {
                    ModelState.AddModelError("", "Müşteri Kaydı başarısız!");
                }
                else
                {
                    var rol = _serviceRol.Get(r => r.Id == account.RolId);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name,account.Adi)
                    };

                    if (rol is not null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, rol.Adi));
                    }
                    var userIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    if (rol.Adi == "Admin")
                    {
                        return Redirect("/Admin");
                    }
                    return Redirect("/Account");
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Müşteri Girişi başarısız!");

            }
            return Redirect("/Account");
        }

       
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(); // burayı düzenlememiz gerekecek;
            return Redirect("/Account/Login");
        }

    }
}
