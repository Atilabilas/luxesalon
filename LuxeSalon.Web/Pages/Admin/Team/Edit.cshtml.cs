using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Core.Entities;

namespace LuxeSalon.Web.Pages.Admin.Team;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public EditModel(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [BindProperty]
    public TeamMember TeamMember { get; set; } = new() { IsActive = true };

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id.HasValue)
        {
            var member = await _context.TeamMembers.FindAsync(id.Value);
            if (member == null) return NotFound();
            TeamMember = member;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(IFormFile? ImageFile)
    {
        if (ImageFile != null && ImageFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "team");
            Directory.CreateDirectory(uploadsFolder);
            
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(stream);
            }
            
            TeamMember.ImageUrl = "/uploads/team/" + uniqueFileName;
        }

        if (TeamMember.Id == 0)
        {
            _context.TeamMembers.Add(TeamMember);
        }
        else
        {
            _context.TeamMembers.Update(TeamMember);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
