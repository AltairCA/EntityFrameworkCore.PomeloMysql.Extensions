using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Functions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            ////_dbContext.Database.Migrate();
            _dbContext.TModels.Add(new TestModel
            {
                Name = "Hello",
                Name2 = "Hello"
            });
            _dbContext.TModels.Add(new TestModel
            {
                Name = "test lol",
                Name2 = "test lol"
            });
            _dbContext.TModels.Add(new TestModel
            {
                Name = "hahha",
                Name2 = "hahha"
            });
            _dbContext.SaveChanges();
            var model = _dbContext.TModels.First();
            //model.Name = "test category";
            //_dbContext.SaveChanges();

            var test = _dbContext.TModels.Select(x => x.Name).ToList();
            var searchTest = _dbContext.TModels.Where(x => x.Name2.Contains("lol") ).ToList();
            var searchTest2 = _dbContext.TModels.Where(x => x.Name.MySqlDecrypt().Contains("lol") ).ToList();
            _dbContext.TModels.Add(new TestModel
            {
                Name = "hahha"
            });
             _dbContext.SaveChanges();
             var keys = _dbContext.TModels.Where(x=> x.Name.MySqlDecrypt().Contains("ha") || x.Name.MySqlDecrypt().Contains("lol")).GroupBy(x => x.Name).Select(x => x.Key).ToList();
            
             return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
