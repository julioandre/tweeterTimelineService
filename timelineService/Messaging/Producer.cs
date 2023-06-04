using System.Net;
using Confluent.Kafka;

namespace timelineService.Messaging;

public class Producer:IProducer
{
    private readonly string bootstrapServers = "127.0.0.1:9092";
    public async Task<bool> GenerateTimeline(string topic, string userId)
    {
        ProducerConfig config = new ProducerConfig
        {
            BootstrapServers = bootstrapServers, ClientId = Dns.GetHostName(),
        };
        try
        {
            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                Console.WriteLine($"Connected to Topic"+ topic);
                var result = await producer.ProduceAsync(topic, new Message<Null, string> { Value = userId });
                Console.WriteLine($"Delivery Timestamp:{result.Timestamp.UtcDateTime}");
                
                return await Task.FromResult(true);

            }
        }catch (Exception ex) {
            Console.WriteLine($"Error occured: {ex.Message}");
        }
        
        return await Task.FromResult(false);
    }
}