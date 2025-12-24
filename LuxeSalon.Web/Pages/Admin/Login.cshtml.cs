using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace LuxeSalon.Web.Pages.Admin;

public class LoginModel : PageModel
{
    private readonly IConfiguration _configuration;

    public LoginModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [BindProperty]
    public string Username { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    public string? ErrorMessage { get; set; }

    public void OnGet()
    {
        // Eğer zaten giriş yapmışsa admin paneline yönlendir
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // Admin bilgilerini appsettings.json'dan oku
        var adminUsername = _configuration["AdminSettings:Username"] ?? "admin";
        var adminPassword = _configuration["AdminSettings:Password"] ?? "luxe2025";

        if (Username == adminUsername && Password == adminPassword)
        {
            // Başarılı giriş - Cookie oluştur
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Tarayıcı kapatılsa bile oturum açık kalsın
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24) // 24 saat geçerli
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToPage("/Admin/Index");
        }

        ErrorMessage = "Kullanıcı adı veya şifre hatalı!";
        return Page();
    }
}
