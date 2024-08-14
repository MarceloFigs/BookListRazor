using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookListRazor.Controllers
{
	[Route("api/Book")]
	[ApiController]
	public class BookController : Controller
	{
		private readonly ApplicationDbContext _context;

		public BookController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Json(new { data = await _context.Book.ToListAsync() });
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			var book = await _context.Book.FirstOrDefaultAsync(b => b.Id == id);
			if (book == null)
			{
				return Json(new { success = false, message = "Error while Deleting" });
			}
			_context.Book.Remove(book);
			await _context.SaveChangesAsync();
			return Json(new { success = true, message = "Delete successful" });
		}
	}
}
