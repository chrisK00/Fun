using MemorySignal.Server.Filters;
using MemorySignal.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MemorySignal.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CardCollectionsController : ControllerBase
{
    private readonly ICardCollectionQueries _cardCollectionQueries;

    public CardCollectionsController(ICardCollectionQueries cardCollectionQueries)
    {
        _cardCollectionQueries = cardCollectionQueries;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CardCollectionResponse>>> GetAll()
    {
        return Ok(await _cardCollectionQueries.GetAll());
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [ServiceFilter(typeof(TokenFilter))]
    [HttpPost]
    public async Task<ActionResult<CardCollectionResponse>> AddCardCollection([FromForm] AddCardCollectionRequest request,
        [FromServices] IAction<AddCardCollectionRequest, CardCollectionResponse> action)
    {
        var response = await action.Execute(request);
        if (action.HasErrors)
        {
            return BadRequest(action.ErrorsFormatted);
        }

        return response;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [ServiceFilter(typeof(TokenFilter))]
    [HttpDelete("{id}")]
    public async Task<ActionResult<RemoveCardCollectionResponse>> RemoveCardCollection(int id,
        [FromServices] IAction<RemoveCardCollectionRequest, RemoveCardCollectionResponse> action)
    {
        var response = await action.Execute(new RemoveCardCollectionRequest(id));
        if (action.HasErrors)
        {
            return BadRequest(action.ErrorsFormatted);
        }

        return response;
    }
}