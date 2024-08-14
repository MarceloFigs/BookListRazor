using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookListRazor.Pages.BookList
{
	public class UpsertModel : PageModel
	{
		private ApplicationDbContext _context;

		public UpsertModel(ApplicationDbContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Book Book { get; set; }
		public async Task<IActionResult> OnGet(int? id)
		{
			Book = new Book();
			if (id == null)
			{
				return Page();
			}
			Book = await _context.Book.FirstOrDefaultAsync(x => x.Id == id);

			if (Book == null)
			{
				return NotFound();
			}
			return Page();
		}

		public async Task<IActionResult> OnPost()
		{
			if (ModelState.IsValid)
			{
				if (Book.Id == 0)
				{
					_context.Book.Add(Book);
				}
				else
				{
					_context.Book.Update(Book);
				}

				await _context.SaveChangesAsync();
				return RedirectToPage("Index");
			}
			return RedirectToPage();
		}
	}
}
