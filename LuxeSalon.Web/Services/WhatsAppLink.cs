using System.Globalization;
using System.Text;

namespace LuxeSalon.Web.Services;

public static class WhatsAppLink
{
    public static string Chat(string? phone, string text)
    {
        var digits = Digits(phone);
        if (string.IsNullOrEmpty(digits))
            digits = "905347944393";

        return $"https://wa.me/{digits}?text={Uri.EscapeDataString(text)}";
    }

    public static string AppointmentMessage(
        string culture,
        string name,
        string customerPhone,
        string? email,
        DateTime when,
        string? serviceName,
        string? notes)
    {
        var en = string.Equals(culture, "en", StringComparison.OrdinalIgnoreCase);
        var date = when.ToString(en ? "d MMMM yyyy" : "dd.MM.yyyy", en ? CultureInfo.GetCultureInfo("en-GB") : CultureInfo.GetCultureInfo("tr-TR"));
        var time = when.ToString("HH:mm");

        var sb = new StringBuilder();
        if (en)
        {
            sb.AppendLine("Hello, I would like to book at Celil & Atilla.");
            sb.AppendLine();
            sb.AppendLine($"Name: {name}");
            sb.AppendLine($"Phone: {customerPhone}");
            if (!string.IsNullOrWhiteSpace(email))
                sb.AppendLine($"Email: {email}");
            sb.AppendLine($"Date: {date}");
            sb.AppendLine($"Time: {time}");
            if (!string.IsNullOrWhiteSpace(serviceName))
                sb.AppendLine($"Service: {serviceName}");
            if (!string.IsNullOrWhiteSpace(notes))
                sb.AppendLine($"Notes: {notes}");
        }
        else
        {
            sb.AppendLine("Merhaba, Celil & Atilla için randevu almak istiyorum.");
            sb.AppendLine();
            sb.AppendLine($"Ad Soyad: {name}");
            sb.AppendLine($"Telefon: {customerPhone}");
            if (!string.IsNullOrWhiteSpace(email))
                sb.AppendLine($"E-posta: {email}");
            sb.AppendLine($"Tarih: {date}");
            sb.AppendLine($"Saat: {time}");
            if (!string.IsNullOrWhiteSpace(serviceName))
                sb.AppendLine($"Hizmet: {serviceName}");
            if (!string.IsNullOrWhiteSpace(notes))
                sb.AppendLine($"Not: {notes}");
        }

        return sb.ToString().Trim();
    }

    public static string Digits(string? phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return "";

        var chars = phone.Where(char.IsDigit).ToArray();
        return new string(chars);
    }
}
