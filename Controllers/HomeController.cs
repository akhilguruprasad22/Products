using Microsoft.AspNetCore.Mvc;
using Products.Data;
using Products.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace Products.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductsContext _context;

        public HomeController(ILogger<HomeController> logger, ProductsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProductQuery requestParams)
        {   //TODO: Fix date validation and uncomment this state check

            //List<Product> searchResult = new();

            //if (ModelState.IsValid)
            //{
            //    searchResult = await _context.ExecuteSearchSP(requestParams);
            //}

            List<Product> searchResult = await _context.ExecuteSearchSP(requestParams);

            return View(searchResult);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}