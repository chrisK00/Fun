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
}