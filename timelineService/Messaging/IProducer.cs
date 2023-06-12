namespace timelineService.Messaging;

public interface IProducer
{
    Task<bool> GenerateTimeline(string topic, string userId);
}