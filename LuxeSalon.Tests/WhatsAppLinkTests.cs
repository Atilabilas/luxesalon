using LuxeSalon.Web.Services;
using Xunit;

namespace LuxeSalon.Tests;

public class WhatsAppLinkTests
{
    [Fact]
    public void Chat_UsesDigitsAndEncodesMessage()
    {
        var url = WhatsAppLink.Chat("0534 794 43 93", "Merhaba, randevu");
        Assert.StartsWith("https://wa.me/05347944393?text=", url);
        Assert.Contains("Merhaba%2C%20randevu", url);
    }

    [Fact]
    public void AppointmentMessage_IncludesCoreFields()
    {
        var text = WhatsAppLink.AppointmentMessage(
            "tr",
            "Ayşe Yılmaz",
            "05347944393",
            "ayse@example.com",
            new DateTime(2026, 9, 12, 14, 0, 0),
            "Kadın saç kesimi",
            "Kahkül kısa olsun");

        Assert.Contains("Ad Soyad: Ayşe Yılmaz", text);
        Assert.Contains("Telefon: 05347944393", text);
        Assert.Contains("E-posta: ayse@example.com", text);
        Assert.Contains("Tarih: 12.09.2026", text);
        Assert.Contains("Saat: 14:00", text);
        Assert.Contains("Hizmet: Kadın saç kesimi", text);
        Assert.Contains("Not: Kahkül kısa olsun", text);
    }
}
