﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers
{
  //  [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    [Area("Admin"), Authorize]
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
