using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ApiTestWeb.Services;


namespace ApiTestWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiService _apiService;

        public HomeController()
        {
            _apiService = new ApiService();
        }

        // ...existing code...
        public async Task<ActionResult> Index()
        {
            var data = await _apiService.GetDataAsync();
            ViewBag.Data = data;
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}



