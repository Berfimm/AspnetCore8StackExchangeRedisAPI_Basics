using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPIApp.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPIApp.Web.Controllers
{
    public class SetTypesController : Controller
    {

        private readonly RedisService _rediceService;
        private readonly IDatabase _db;
        private string listKey = "names";

        //Sliding expiration yok ama timeout ile bu özelliği kazandırabiliyoruz
        public SetTypesController(RedisService rediceService)
        {
            _rediceService = rediceService;
            _db = _rediceService.GetDb(1);
        }

        public IActionResult Index()
        {
            HashSet<string> nameList = new HashSet<string>();
            if (_db.KeyExists(listKey))
            {
                _db.SetMembers(listKey).ToList().ForEach(x =>
                {
                    nameList.Add(x.ToString());
                });
            }
            return View(nameList);
        }


        [HttpPost]
        public IActionResult Add(string name)
        {
            //if(!_db.KeyExists(listKey)) Sliding özelliği vermek için kullanabiliriz
            //{
                _db.KeyExpire(listKey, DateTime.Now.AddMinutes(5));
            //}
            _db.SetAdd(listKey, name);


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteItem(string name)
        {
            await _db.SetRemoveAsync(listKey, name);
            return RedirectToAction("Index");
        }
    }
}
