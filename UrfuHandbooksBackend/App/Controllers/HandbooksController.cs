using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HandbooksBackend.Domain;
using HandbooksBackend.Domain.Entities;

[Route("api/[controller]")]
[ApiController]
public class HandbooksController : ControllerBase
{
    private readonly Context _context;

    public HandbooksController(Context context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HandbookDto>>> GetHandbooks()
    {
        return await _context.Handbooks.Select(h => h.ToDto()).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HandbookDto>> GetHandbook(long id)
    {
        var handbook = await _context.Handbooks.FindAsync(id);

        if (handbook == null)
        {
            return NotFound();
        }

        return handbook.ToDto();
    }

    // GET: api/Handbooks/5/FieldNames
    [HttpGet("{id}/FieldNames")]
    public async Task<ActionResult<IEnumerable<string>>> GetHandbookFieldNames(long id)
    {
        var handbook = await _context.Handbooks.FindAsync(id);

        if (handbook == null)
        {
            return NotFound();
        }

        return await _context.ColumnInfos
            .Where(c => c.HandbookId == id)
            .Select(c => c.Header)
            .ToListAsync();
    }

    // GET: api/Handbooks/5/Records
    [HttpGet("{id}/Records")]
    public async Task<ActionResult<IEnumerable<HandbookRecordDto>>> GetHandbookRecords(long id)
    {
        var handbook = await _context.Handbooks.FindAsync(id);

        if (handbook == null)
        {
            return NotFound();
        }

        return await _context.HandbookRecords
            .Where(r => r.Handbook.Id == id)
            .Select(r => r.ToDto())
            .ToListAsync();
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> PutHandbook(long id, HandbookDto handbookDto)
    {
        var handbook = await _context.Handbooks.FindAsync(id);
        if (handbook == null)
        {
            return NotFound();
        }

        handbook.Header = handbookDto.Header;
        handbook.Description = handbookDto.Description;
        handbook.IsInherited = handbookDto.IsInherited;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!HandbookExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<HandbookDto>> PostHandbook(HandbookDto handbookDto)
    {
        var handbook = new Handbook
        {
            Header = handbookDto.Header,
            Description = handbookDto.Description,
            IsInherited = handbookDto.IsInherited
        };

        _context.Handbooks.Add(handbook);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetHandbook", new { id = handbook.Id }, handbook.ToDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHandbook(long id)
    {
        var handbook = await _context.Handbooks.FindAsync(id);
        if (handbook == null)
        {
            return NotFound();
        }

        _context.Handbooks.Remove(handbook);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool HandbookExists(long id)
    {
        return _context.Handbooks.Any(e => e.Id == id);
    }
}
