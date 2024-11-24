using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPIApp.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPIApp.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _rediceService;
        private readonly IDatabase _db;

        public StringTypeController(RedisService rediceService)
        {
            _rediceService = rediceService;
            _db = _rediceService.GetDb(0);
        }
        public IActionResult Index()
        {
            _db.StringSet("name", "Berfim Korkmaz");
            _db.StringSet("ziyaretci", 100);
            return View();
        }
        public IActionResult ShowStringType()
        {
            //ASYNC METOT
            //_db.StringIncrement("ziyaretci", 100);
            //var result = _db.StringIncrementAsync("ziyaretci", 100).Result; Sonuç lazım ise 
            //_db.StringIncrementAsync("ziyaretci",100).Wait(); // Sadece bu metodu asenkron bir şekilde yap

            //GET
            //var nameval = _db.StringGet("name");
            //var ziyval = _db.StringGet("ziyaretci");
            //if (nameval.HasValue)
            //{
            //    ViewBag.nameval = nameval;
            //    ViewBag.ziyval = ziyval;
            //}
            //RANGE
            //var rangevalue = _db.StringGetRange("name", 0, 6);
            //if (rangevalue.HasValue)
            //{
            //    ViewBag.rangevalue = rangevalue;
            //}  
            
            //LENGTH
            var lengthvalue = _db.StringLength("name");
            ViewBag.lengthvalue = lengthvalue;

            return View();
        }
    }
}
