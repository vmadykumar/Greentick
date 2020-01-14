using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportMgmt.CommonLayer.Utility.IUtilityLayer
{
    public interface IRedisManager
    {
        ConnectionMultiplexer GetConnection();
        void Set(string key, object value);
        T Get<T>(string key) where T :class;
       
        bool IsExists(string key);
        void Delete(string key);

        IEnumerable<RedisKey> GetAllKeys();
    }
}
