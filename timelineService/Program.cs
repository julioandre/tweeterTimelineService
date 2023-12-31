using KafkaFlow;
using KafkaFlow.Serializer;
using timelineService.Cache;
using timelineService.Messaging;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

        builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<ICacheService, CacheService>();
        builder.Services.AddScoped<IProducer, Producer>();
        builder.Services.AddKafka(kafka => kafka.AddCluster(cluster =>
        {
            const string topicname = "timelineTopic";
            Console.WriteLine("Creating Topic"); 
            cluster.WithBrokers(new[] { "127.0.0.1:9092" }).CreateTopicIfNotExists(topicname,2,4)
                .AddProducer("pull-timeline", producer => producer.DefaultTopic(topicname)
                    .AddMiddlewares(middlewares => middlewares.AddSerializer<JsonCoreSerializer>()));
        }));
        builder.Services.AddStackExchangeRedisCache(options =>
        {
    
            options.Configuration = "localhost:6377";
            options.InstanceName = "timeline-redis-cache";
   
        });

        var app = builder.Build();

// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}