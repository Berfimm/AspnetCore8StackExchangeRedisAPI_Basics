using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPIApp.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPIApp.Web.Controllers
{
    public class ListTypesController : Controller
    {

        private readonly RedisService _rediceService;
        private readonly IDatabase _db;
        private string listKey = "names";

        public ListTypesController(RedisService rediceService)
        {
            _rediceService = rediceService;
            _db = _rediceService.GetDb(0);
        }

        public IActionResult Index()
        {
            List<string> namelist = new List<string>();
            if(_db.KeyExists(listKey))
            {
                _db.ListRange(listKey).ToList().ForEach(x =>
                {
                    namelist.Add(x.ToString());
                });
            }
            return View(namelist);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            _db.ListRightPush(listKey, name);


            return RedirectToAction("Index");
        }
        public IActionResult DeleteItem(string name)
        {
            _db.ListRemoveAsync(listKey, name).Wait();
            return RedirectToAction("Index");
        }
        public IActionResult DeleteFirstItem()
        {
            _db.ListLeftPop(listKey);
            return RedirectToAction("Index");
        }
    }
}
