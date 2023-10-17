using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HandbooksBackend.Domain;
using HandbooksBackend.Domain.Entities;
using HandbooksBackend.Domain.Models;

[Route("api/[controller]")]
[ApiController]
public class HandbooksController : ControllerBase
{
	private readonly Context context;

	public HandbooksController(Context context)
	{
		this.context = context;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<HandbookDto>>> GetHandbooks()
	{
		return await context.Handbooks.Select(h => h.ToDto()).ToListAsync();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<HandbookDto>> GetHandbook(long id)
	{
		var handbook = await context.Handbooks.FindAsync(id);

		if (handbook == null)
		{
			return NotFound();
		}

		return handbook.ToDto();
	}


	// GET: api/Handbooks/5/Records
	[HttpGet("{id}/Records")]
	public async Task<ActionResult<IEnumerable<HandbookRecordDto>>> GetHandbookRecords(long id)
	{
		var handbook = await context.Handbooks.FindAsync(id);

		if (handbook == null)
		{
			return NotFound();
		}

		return await context.HandbookRecords
			.Where(r => r.Handbook.Id == id)
			.Select(r => r.ToDto())
			.ToListAsync();
	}

	[HttpPut]
	public async Task<IActionResult> PutHandbook(HandbookDto handbookDto)
	{
		var handbook = await context.Handbooks.FindAsync(handbookDto.Id);
		if (handbook == null)
		{
			return NotFound();
		}

		handbook.Header = handbookDto.Header;
		handbook.Description = handbookDto.Description;
		handbook.IsInherited = handbookDto.IsInherited;

		try
		{
			await context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!HandbookExists(handbookDto.Id))
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
	public async Task<ActionResult<HandbookDto>> PostHandbook(HandbookCreationDto handbookDto)
	{
		var columnInfos = handbookDto.ColumnInfos.Select(x => new ColumnInfo
		{
			Header = x.Header,
			ColumnType = context.ColumnTypes.FindAsync(x.ColumnTypeId).GetAwaiter().GetResult(),
			Index = x.Index,
			IsRequired = x.IsRequired
		}).ToArray();

		await context.ColumnInfos.AddRangeAsync(columnInfos);

		var handbook = new Handbook
		{
			Header = handbookDto.Header,
			Description = handbookDto.Description,
			IsInherited = handbookDto.IsInherited,
			ColumnInfos = columnInfos
		};

		await context.Handbooks.AddAsync(handbook);
		await context.SaveChangesAsync();

		return CreatedAtAction("GetHandbook", new {id = handbook.Id}, handbook.ToDto());
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteHandbook(long id)
	{
		var handbook = await context.Handbooks.FindAsync(id);
		if (handbook == null)
		{
			return NotFound();
		}

		context.Handbooks.Remove(handbook);
		await context.SaveChangesAsync();

		return NoContent();
	}

	private bool HandbookExists(long id)
	{
		return context.Handbooks.Any(e => e.Id == id);
	}
}