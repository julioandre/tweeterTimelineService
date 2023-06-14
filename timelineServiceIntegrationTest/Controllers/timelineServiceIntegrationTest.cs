using System.Net;

namespace timelineServiceIntegrationTest.Controllers;

public class timelineServiceIntegrationTest:IDisposable
{
    private CustomWebApplicationFactory _factory;
    private HttpClient _client;

    public timelineServiceIntegrationTest()
    {
        _factory = new CustomWebApplicationFactory();
        _client = new HttpClient();
    }

    [Fact]
    public async Task GetTimeline_returnsOk()
    {
        var id = "4d2e935e-98c7-42e7-a9f3-3061ebc6475c";
        var response = await _client.GetAsync($"http://localhost:5242/api/timeline/{id}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    public void Dispose()
    {
        _factory.Dispose();
        _client.Dispose();
    }
}