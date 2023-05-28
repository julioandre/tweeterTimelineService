using StackExchange.Redis;

namespace timelineService.Config;

public class ConnectionHelper
{
    static ConnectionHelper() {
        ConnectionHelper.lazyConnection = new Lazy <ConnectionMultiplexer> (() => {
            return ConnectionMultiplexer.Connect(ConfigurationManager.AppSetting["RedisUrl"]);
        });
    }
    private static Lazy <ConnectionMultiplexer> lazyConnection;
    public static ConnectionMultiplexer Connection {
        get {
            return lazyConnection.Value;
        }
    }
}