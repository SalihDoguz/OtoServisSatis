using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;
using OtoServisSatis.WebUI.Utils;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers
{
 //   [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    [Area("Admin"), Authorize]
    public class CarsController : Controller
    {
        private readonly ICarService _service;
        private readonly IService<Marka> _serviceMarka;

        public CarsController(ICarService service, IService<Marka> serviceMarka)
        {
            _service = service;
            _serviceMarka = serviceMarka;
        }


        // GET: CarsController
        public async Task<ActionResult> IndexAsync()
        {
            var model =await  _service.GetCustomCarList();
            return View(model);
        }

        // GET: CarsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarsController/Create
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.MarkaId = new SelectList(await _serviceMarka.GetAllAsync(),"Id","Adi");
            return View();
        }

        // POST: CarsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Arac arac, IFormFile? resim1,IFormFile? resim2,IFormFile? resim3)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    arac.Resim1 = await FileHelper.FileLoaderAsync(resim1, "/Img/Cars/");
                    arac.Resim2 = await FileHelper.FileLoaderAsync(resim2, "/Img/Cars/");
                    arac.Resim3 = await FileHelper.FileLoaderAsync(resim3, "/Img/Cars/");
                    await _service.AddAsync(arac);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("","Araç Eklenemedi!");
                }
            }
            ViewBag.MarkaId = new SelectList(await _serviceMarka.GetAllAsync(), "Id", "Adi");

            return View(arac);
        }

        // GET: CarsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            ViewBag.MarkaId = new SelectList(await _serviceMarka.GetAllAsync(), "Id", "Adi");

            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: CarsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Arac arac, IFormFile? resim1, IFormFile? resim2, IFormFile? resim3)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    if(resim1 is not null)
                    {
                        arac.Resim1 = await FileHelper.FileLoaderAsync(resim1, "/Img/Cars/");
                    }
                    if (resim2 is not null)
                    {
                        arac.Resim2 = await FileHelper.FileLoaderAsync(resim2, "/Img/Cars/");
                    }
                    if (resim3 is not null)
                    {
                        arac.Resim3 = await FileHelper.FileLoaderAsync(resim3, "/Img/Cars/");
                    }
                    
                    _service.Update(arac);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Güncellenecek Araç Bulunamadı!");
                }
            }
            
            ViewBag.MarkaId = new SelectList(await _serviceMarka.GetAllAsync(), "Id", "Adi");

            return View(arac);  
        }

        // GET: CarsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: CarsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Arac arac)
        {
            try
            {
                _service.Delete(arac);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Silinecek Araç Bulunamadı!");
            }
            return View();
        }
    }
}
