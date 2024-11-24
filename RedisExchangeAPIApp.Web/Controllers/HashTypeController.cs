using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPIApp.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPIApp.Web.Controllers
{
    public class HashTypeController : Controller
    {
        private readonly RedisService _rediceService;
        private readonly IDatabase _db;
        private string hashKey = "hashdicnames";
        public HashTypeController(RedisService rediceService)
        {
            _rediceService = rediceService;
            _db = _rediceService.GetDb(3);
        }


        public IActionResult Index()
        {
            Dictionary<string,string> list = new Dictionary<string,string>();
            if(_db.KeyExists(hashKey))
            {
                _db.HashGetAll(hashKey).ToList().ForEach(x=>
                {
                    list.Add(x.Name, x.Value);
                });
            }


            return View(list);
        }

        [HttpPost]
        public IActionResult Add(string name, string val)
        {
            _db.HashSet(hashKey, name, val);

            return RedirectToAction("Index");
        }
        public IActionResult DeleteItem(string name)
        {
            _db.HashDelete(hashKey, name);

            return RedirectToAction("Index");
        }
    }
}
