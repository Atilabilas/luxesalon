using LuxeSalon.Core.Entities;
using LuxeSalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuxeSalon.Web.Pages.Admin.Services;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Service Service { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service == null) return NotFound();
        Service = service;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service != null)
        {
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage("Index");
    }
}
