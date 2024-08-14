using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookListRazor.Pages.BookList
{
	public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		public IndexModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Book> Books { get; set; }

		public async Task OnGet()
		{
			Books = await _context.Book.ToListAsync();
		}

		public async Task<IActionResult> OnPostDelete(int id)
		{
			var book = await _context.Book.FindAsync(id);

			if (book == null) 
			{
				return NotFound();
			}

			_context.Book.Remove(book);
			await _context.SaveChangesAsync();

			return RedirectToPage("Index");
		}
	}
}
