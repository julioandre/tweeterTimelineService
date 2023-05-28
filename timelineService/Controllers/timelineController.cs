using Microsoft.AspNetCore.Mvc;
using timelineService.Cache;
using timelineService.Models;

namespace timelineService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class timelineController:ControllerBase
{
    private ICacheService _cacheService;

    public timelineController(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tweet>>> GetAsync([FromBody]string Id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result =  _cacheService.GetData<IEnumerable<Tweet>>(Id);
        return Ok(result);

    }

}