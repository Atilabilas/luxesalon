using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Core.Entities;

namespace LuxeSalon.Web.Pages.Admin.Team;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<TeamMember> TeamMembers { get; set; } = new();

    public async Task OnGetAsync()
    {
        TeamMembers = await _context.TeamMembers.OrderBy(t => t.DisplayOrder).ToListAsync();
    }
}
