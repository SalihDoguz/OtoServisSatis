using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;
using OtoServisSatis.WebUI.Utils;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers
{
    // [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    [Area("Admin"), Authorize]
    public class SlidersController : Controller
    {
        private readonly IService<Slider> _service;

        public SlidersController(IService<Slider> service)
        {
            _service = service;
        }

        // GET: SlidersController
        public async Task<ActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: SlidersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SlidersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SlidersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Slider collection, IFormFile Resim)
        {
            try
            {
                collection.Resim = await FileHelper.FileLoaderAsync(Resim, "/Img/Slider/");
                await _service.AddAsync(collection);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: SlidersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            // Önce modeli almak zorudnayız
            // Modelimizi almaz isek veriyi nereye çekeceğiz
            var model = await _service.FindAsync(id);
            // Modelimizi aldıktan sonra view kısmına gönderiyoruz.
            return View(model);
        }

        // POST: SlidersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Slider collection, IFormFile? Resim)
        {
            try
            {
                if (Resim is not null)
                    collection.Resim = await FileHelper.FileLoaderAsync(Resim, "/Img/Slider/");

                _service.Update(collection);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SlidersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id) // Her zaman oldugu gibi burada da slide silme işlemi uyguluyoruz.
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: SlidersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Slider collection)
        {
            try
            {
                _service.Delete(collection);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
