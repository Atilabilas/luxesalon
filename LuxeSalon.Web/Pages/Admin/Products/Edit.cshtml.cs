using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LuxeSalon.Infrastructure.Data;
using LuxeSalon.Core.Entities;

namespace LuxeSalon.Web.Pages.Admin.Products;

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
    public Product Product { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        Product = product;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(IFormFile? ProductImage)
    {
        var existingProduct = await _context.Products.FindAsync(Product.Id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        // Handle image upload
        if (ProductImage != null && ProductImage.Length > 0)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "products");
            Directory.CreateDirectory(uploadsFolder);
            
            var uniqueFileName = $"{Guid.NewGuid()}_{ProductImage.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ProductImage.CopyToAsync(stream);
            }
            
            Product.ImageUrl = $"/uploads/products/{uniqueFileName}";
        }

        // Update fields
        existingProduct.Name = Product.Name;
        existingProduct.Category = Product.Category;
        existingProduct.Price = Product.Price;
        existingProduct.StockQuantity = Product.StockQuantity;
        existingProduct.Description = Product.Description;
        existingProduct.ImageUrl = Product.ImageUrl;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
