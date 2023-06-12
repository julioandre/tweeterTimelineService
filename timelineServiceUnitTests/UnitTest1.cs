using FakeItEasy;
using FluentAssertions;
using KafkaFlow.Producers;
using Microsoft.AspNetCore.Mvc;
using timelineService.Cache;
using timelineService.Controllers;

namespace timelineServiceUnitTests;

public class timelineServiceUnitTests
{
    private readonly ICacheService _cacheService;
    private readonly IProducerAccessor _producer;
    
    
    public timelineServiceUnitTests()
    {
        _cacheService = A.Fake<ICacheService>();
        _producer = A.Fake<IProducerAccessor>();
    }

    [Fact]
    public void GetTimeline_ReturnOkResult()
    {
        //Arrange
        var id = "58f81189-05b0-4765-987d-d6ebb87cc8cd";

        var controller = new timelineController(_cacheService, _producer);
        
        //Act

        var result = controller.GetAsync(id);
        result.Should().BeOfType(typeof(OkObjectResult));
        
    }
}