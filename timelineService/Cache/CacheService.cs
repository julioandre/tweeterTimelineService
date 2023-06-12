using Newtonsoft.Json;
using StackExchange.Redis;
using timelineService.Config;
using timelineService.Models;

namespace timelineService.Cache;

public class CacheService:ICacheService
{
    private IDatabase _db;

    public CacheService()
    {
        ConfigureRedis();
    }
    private void ConfigureRedis() {
        _db = ConnectionHelper.Connection.GetDatabase();
    }
    public IEnumerable<Tweet> GetData(string key)
    {
        var value = _db.ListRange(key);
        if (value.Length>0)
        {
            var timeline = value.Select(r => JsonConvert.DeserializeObject<Tweet>(r));
            return timeline;
        }
        return default;
    }
    

    public bool SetData<T>(string key, T value, TimeSpan expirationTime)
    {
        if (CheckData(key) == true)
        {
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expirationTime);
            return isSet; 
        }
        return false;
    }

    public object RemoveData(string key)
    {
        bool _isKeyExist = _db.KeyExists(key);
        if (_isKeyExist == true) {
            return _db.KeyDelete(key);
        }
        return false;
    }
    public bool CheckData(string key)
    {
        var _data = _db.StringGet(key);
        return _data.IsNullOrEmpty;
    }
    public long SetTimeline(string key, Tweet tweet)
    {
        var pushToRedis = _db.ListRightPush(key, JsonConvert.SerializeObject(tweet));
        return pushToRedis;
    }
}