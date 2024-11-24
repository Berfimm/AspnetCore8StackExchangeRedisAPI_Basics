using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPIApp.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPIApp.Web.Controllers
{
    public class SortedSetsController : Controller
    {
        private readonly RedisService _rediceService;
        private readonly IDatabase _db;
        private string listKey = "sortedsetnames";
        public SortedSetsController(RedisService rediceService)
        {
            _rediceService = rediceService;
            _db = _rediceService.GetDb(2);
        }
        public IActionResult Index()
        {
            HashSet<string> list = new HashSet<string>();

            if(_db.KeyExists(listKey))
            {
                //DB deki hali ile sıralama/ score değeri geliyor
                //_db.SortedSetScan(listKey).ToList().ForEach(x =>
                //{
                //    list.Add(x.ToString());
                //});
                //büyükten küçüğe sıralama /  score değeri gelmiyor
                _db.SortedSetRangeByRank(listKey, order: Order.Descending).ToList().ForEach(x => 
                {
                    list.Add(x.ToString());
                });
            }
            return View(list);
        }
        [HttpPost]
        public IActionResult Add(string name,int score)
        {
            _db.SortedSetAdd(listKey,name,score);

            _db.KeyExpire(listKey, DateTime.Now.AddMinutes(1));

            return RedirectToAction("Index");
        }
        public IActionResult DeleteItem(string name)
        {
            _db.SortedSetRemove(listKey,name);

            return RedirectToAction("Index");
        }
    }
}
