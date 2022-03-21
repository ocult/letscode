#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LetsCode.Models;
using LetsCode.DTO;
using LetsCode.Validators;
using FluentValidation.Results;
using LetsCode.Customs;
using Microsoft.AspNetCore.Authorization;

namespace LetsCode.Controllers;

[Route("cards")]
[ApiController]
[Authorize(MyJwtConstants.DEFAULT_POLICY)]
public class CardsController : ControllerBase
{
    private readonly CardContext _context;

    private readonly ILogger<CardsController> _logger;

    public CardsController(CardContext context, ILogger<CardsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CardDTO>>> GetCards()
    {
        return await _context.Cards.Select(card => ItemToDTO(card)).ToListAsync();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CardDTO>> GetCard(Guid id)
    {
        var card = await _context.Cards.FindAsync(id);

        if (card == null)
        {
            return NotFound();
        }

        return ItemToDTO(card);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CardDTO>> PutCard(Guid id, CardDTO cardDTO)
    {
        if (id != cardDTO.Id)
        {
            return BadRequest();
        }
        var validator = new CardDtoValidator();

        ValidationResult results = validator.Validate(cardDTO);
        if (!results.IsValid)
        {
            return BadRequest(results.Errors);
        }

        var card = await _context.Cards.FindAsync(id);
        if (card == null)
        {
            return NotFound();
        }
        card.Update(cardDTO.Title, cardDTO.Content, Enum.Parse<KanbanListEnum>(cardDTO.List.ToUpperInvariant()));

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!CardExists(id))
        {
            return NotFound();
        }
        _logger.LogCardChanged(card);

        return ItemToDTO(card);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CardDTO>> PostCard(CreateCardDTO createCardDTO)
    {
        var validator = new CreateCardDtoValidator();

        ValidationResult results = validator.Validate(createCardDTO);

        if (!results.IsValid)
        {
            return BadRequest(results.Errors);
        }

        var card = new Card(createCardDTO.Title, createCardDTO.Content);
        _context.Cards.Add(card);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCard), new { id = card.Id }, ItemToDTO(card));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCard(Guid id)
    {
        var card = await _context.Cards.FindAsync(id);
        if (card == null)
        {
            return NotFound();
        }

        _context.Cards.Remove(card);
        await _context.SaveChangesAsync();
        _logger.LogCardDeleted(card);

        return NoContent();
    }

    private bool CardExists(Guid id)
    {
        return _context.Cards.Any(e => e.Id == id);
    }

    private CardDTO ItemToDTO(Card card)
    {
        return new CardDTO
        {
            Id = card.Id,
            Title = card.Title,
            Content = card.Content,
            List = card.List.ToString()
        };
    }
}
