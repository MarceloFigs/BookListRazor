using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BookListRazor.Pages.BookList
{
	public class CreateModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		public CreateModel(ApplicationDbContext context)
		{
			_context = context;
		}
		[BindProperty]
		public Book Book { get; set; }

		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPost()
		{
			if (ModelState.IsValid)
			{
				await _context.AddAsync(Book);
				await _context.SaveChangesAsync();
				return RedirectToPage("Index");
			}
			else
			{
				return Page();
			}
		}
	}
}
