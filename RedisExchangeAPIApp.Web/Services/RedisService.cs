using StackExchange.Redis;

namespace RedisExchangeAPIApp.Web.Services
{
    public class RedisService
    {
        private readonly string _redisHost;
        private readonly string _redisPort;
        private ConnectionMultiplexer? _redis;
        public IDatabase? db { get; private set; }

        public RedisService(IConfiguration configuration)
        {
            _redisHost = configuration["Redis:Host"]!;
            _redisPort = configuration["Redis:Port"]!;
        }

        public void Connect()
        {
            // Redis sunucusuna bağlan
            var configString = $"{_redisHost}:{_redisPort}";
            _redis = ConnectionMultiplexer.Connect(configString);
        }

        public IDatabase GetDb(int db)
        {
            // Belirli bir veritabanına bağlan
            if (_redis == null)
                throw new InvalidOperationException("Redis bağlantısı henüz kurulmadı. 'Connect()' metodunu çağırmalısınız.");

            return _redis.GetDatabase(db);
        }
    }
}
