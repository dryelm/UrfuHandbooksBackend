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

	private static ColumnInfo[] GetColumnInfos(Handbook handbook, Context context)
	{
		return context.ColumnInfos.Where(x => x.HandbookId == handbook.Id).ToArray();
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<HandbookDto>>> GetHandbooks()
	{
		return await context.Handbooks.Select(h => h.ToDto(GetColumnInfos(h, context))).ToListAsync();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<HandbookDto>> GetHandbook(long id)
	{
		var handbook = await context.Handbooks.FindAsync(id);

		if (handbook == null)
		{
			return NotFound();
		}

		return handbook.ToDto(GetColumnInfos(handbook, context));
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
		var handbook = new Handbook
		{
			Header = handbookDto.Header,
			Description = handbookDto.Description,
			IsInherited = handbookDto.IsInherited,
		};

		var columnInfos = handbookDto.ColumnInfos.Select(async x =>
		{
			var columnType = await context.ColumnTypes.FindAsync(x.ColumnTypeId);
			return new ColumnInfo
			{
				Handbook = handbook,
				Header = x.Header,
				ColumnType = columnType,
				Index = x.Index,
				IsRequired = x.IsRequired
			};
		}).ToArray();

		await context.ColumnInfos.AddRangeAsync(columnInfos
			.Select(x => x.GetAwaiter().GetResult()));

		await context.Handbooks.AddAsync(handbook);

		await context.SaveChangesAsync();

		return CreatedAtAction("GetHandbook", new {id = handbook.Id},
			handbook.ToDto(GetColumnInfos(handbook, context)));
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