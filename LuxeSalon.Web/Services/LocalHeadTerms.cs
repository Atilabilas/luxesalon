namespace LuxeSalon.Web.Services;

public static class LocalHeadTerms
{
    public const string HomeTitleTr = "Bornova Kadın Kuaförü, İzmir | Celil & Atilla";
    public const string HomeTitleEn = "Haircuts, Colour & Care in Bornova | Celil & Atilla";
    public const string HomeDescriptionTr = "Bornova kadın kuaförü Celil & Atilla, Kazımdirik Mah. Gediz Cd. 21 A, 35100 Bornova / İzmir. Kesim, renk ve bakım için randevu alın: 0534 794 43 93. Tek adres.";
    public const string HomeDescriptionEn = "Celil & Atilla is a Bornova hair salon at Gediz Cd. 21 A, Kazımdirik, İzmir. Book a cut, colour or care visit on 0534 794 43 93. There is no second branch.";
    public const string HomeSupportTr = "Bornova kuaför hizmetlerinin tamamı bu adrestedir. İzmir kadın kuaförü arayanlar da aynı salona gelir; başka ilçede şube yoktur.";
    public const string HomeSupportEn = "Every service is booked at this Bornova address. Guests looking for a women's salon in İzmir use the same chair; there is no second branch.";
    public const string HomeFocusTr = "Bornova kadın kuaförü";
    public const string HomeFocusEn = "Bornova hair salon";

    public static string WithBornova(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return "Bornova";
        return title.Contains("Bornova", StringComparison.OrdinalIgnoreCase)
            ? title.Trim()
            : title.Trim() + " Bornova";
    }
}
