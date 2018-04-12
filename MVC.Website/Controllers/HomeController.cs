using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;

namespace MVC.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieServices _movieServices;

        public HomeController(IMovieServices movieServices)
        {
            _movieServices = movieServices;
        }

        public async Task<ActionResult> Index()
        {
            var actors = await _movieServices.GetActors();

            return View(actors);
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