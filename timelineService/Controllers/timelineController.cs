using KafkaFlow.Producers;
using Microsoft.AspNetCore.Mvc;
using timelineService.Cache;
using timelineService.Messaging;
using timelineService.Models;

namespace timelineService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class timelineController:ControllerBase
{
    private ICacheService _cacheService;
    private IProducerAccessor _producer;
    
    public timelineController(ICacheService cacheService,IProducerAccessor producer)
    {
        _cacheService = cacheService;
        _producer = producer;
        
    }

    [HttpGet]
    public ActionResult<IEnumerable<Tweet>> GetAsync(string Id)
    {
        string topic = "timelineTopic";
        // if (!ModelState.IsValid)
        // {
        //     return BadRequest(ModelState);
        // }
        //
        // var result =  await _cacheService.GetData<IEnumerable<Tweet>>(Id);
        // if (result != null)
        // {
        //     return Ok(result);
        // }

        var producer = _producer.GetProducer("pull-timeline");
        producer.ProduceAsync("key",Id);
        //await _producers.GenerateTimeline("timelineTopic", Id);
        var result =  _cacheService.GetData(Id);
        if (result == null)
        {
            return NotFound("Error getting timeline");
        }
        return Ok(result);

    }

}