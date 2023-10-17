using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HandbooksBackend.Domain;
using HandbooksBackend.Domain.Entities;


[Route("api/[controller]")]
[ApiController]
public class HandbookRecordsController : ControllerBase
{
    private readonly Context _context;

    public HandbookRecordsController(Context context)
    {
        _context = context;
    }

    // GET: api/HandbookRecords
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HandbookRecordDto>>> GetHandbookRecords()
    {
        return await _context.HandbookRecords.Select(h => h.ToDto()).ToListAsync();
    }

    // GET: api/HandbookRecords/5
    [HttpGet("{id}")]
    public async Task<ActionResult<HandbookRecordDto>> GetHandbookRecord(long id)
    {
        var handbookRecord = await _context.HandbookRecords.FindAsync(id);

        if (handbookRecord == null)
        {
            return NotFound();
        }

        return handbookRecord.ToDto();
    }

    // PUT: api/HandbookRecords/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHandbookRecord(long id, HandbookRecordDto handbookRecordDto)
    {
        var handbookRecord = await _context.HandbookRecords.FindAsync(id);
        if (handbookRecord == null)
        {
            return NotFound();
        }

        // Update the properties of the existing record from the DTO
        handbookRecord.Handbook.Id = handbookRecordDto.HandbookId;
        handbookRecord.HandbookRecordContent.Id = handbookRecordDto.HandbookRecordContentId;
        handbookRecord.Parent = handbookRecordDto.ParentId.HasValue ? new HandbookRecord { Id = handbookRecordDto.ParentId.Value } : null;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!HandbookRecordExists(id))
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

    // POST: api/HandbookRecords
    [HttpPost]
    public async Task<ActionResult<HandbookRecordDto>> PostHandbookRecord(HandbookRecordDto handbookRecordDto)
    {
        var handbookRecord = new HandbookRecord
        {
            Handbook = new Handbook { Id = handbookRecordDto.HandbookId },
            HandbookRecordContent = handbookRecordDto.HandbookRecordContent,
            Parent = handbookRecordDto.ParentId.HasValue ? new HandbookRecord { Id = handbookRecordDto.ParentId.Value } : null
        };

        _context.HandbookRecords.Add(handbookRecord);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetHandbookRecord", new { id = handbookRecord.Id }, handbookRecord.ToDto());
    }

    // DELETE: api/Handbooks/5
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

    private bool HandbookRecordExists(long id)
    {
        return _context.HandbookRecords.Any(e => e.Id == id);
    }
}
