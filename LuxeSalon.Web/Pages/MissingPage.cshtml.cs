using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LuxeSalon.Web.Pages;

public class MissingPageModel : PageModel
{
    public void OnGet()
    {
        if (Response.StatusCode == StatusCodes.Status200OK)
            Response.StatusCode = StatusCodes.Status404NotFound;
        ViewData["Title"] = "Sayfa bulunamadı";
        ViewData["MetaDescription"] = "Aradığınız sayfa bulunamadı. Celil & Atilla’nın hizmetlerini inceleyebilir veya randevu talebi oluşturabilirsiniz.";
        ViewData["Robots"] = "noindex, follow";
    }
}
