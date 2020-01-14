using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ReportMgmt.CommonLayer.Utility.IUtilityLayer;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportMgmt.CommonLayer.Utility.UtilityLayer
{
    public class RedisManager : IRedisManager
    {
        private readonly Lazy<ConnectionMultiplexer> _connection;
        private readonly IDatabase _db;

        private readonly IOptions<RedisSettings> _redisSettings;

        public RedisManager(IOptions<RedisSettings> redis)
        {
            _redisSettings = redis;
            this._connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(_redisSettings.Value.Host));
            _db = _connection.Value.GetDatabase(_redisSettings.Value.Database);
        }

        public ConnectionMultiplexer GetConnection()
        {
            return this._connection.Value;
        }

        public IEnumerable<RedisKey> GetAllKeys()
        {
            return this._connection.Value.GetServer(_redisSettings.Value.Host + ":" + _redisSettings.Value.Port).Keys();
        }

        public void Set(string key, object value)
        {
            try
            {
                _db.StringSet(key, JsonConvert.SerializeObject(value));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T Get<T>(string key) where T : class
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(_db.StringGet(key));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsExists(string key)
        {
            try
            {
                return _db.KeyExists(key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(string key)
        {
            try
            {
                _db.KeyDelete(key);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
