using LuxeSalon.Core.Entities;

namespace LuxeSalon.Web.Services;

/// <summary>
/// Search titles and descriptions for the eight real landing services.
/// Rendered from code so an existing database keeps its slugs and body text.
/// </summary>
public static class ServiceSearchCopy
{
    public static readonly string[] Slugs =
    [
        "kadin-sac-kesimi",
        "balyaj-ombre-sombre",
        "keratin-sac-botoksu",
        "rofle-kok-rotusu",
        "gelin-saci",
        "sac-kaynagi",
        "erkek-kesim-fade",
        "cocuk-kesimi"
    ];

    public const string DirectoryTitleTr = "Bornova Kuaför Hizmetleri, İzmir | Celil & Atilla";
    public const string DirectoryTitleEn = "Bornova Hair Services | Celil & Atilla";
    public const string DirectoryMetaTr = "Bornova kuaför hizmetleri Kazımdirik Mah. Gediz Cd. 21 A, 35100 Bornova / İzmir adresindedir. Kesim, balyaj, keratin ve gelin saçı. Randevu: 0534 794 43 93.";
    public const string DirectoryMetaEn = "Hair salon services in Bornova, at Gediz Cd. 21 A, Kazımdirik. Cuts, balayage, keratin and bridal hair. Book on 0534 794 43 93. There is no second branch.";

    public readonly record struct Snippet(string TitleTr, string TitleEn, string MetaTr, string MetaEn);

    static readonly Dictionary<string, Snippet> Known = new(StringComparer.Ordinal)
    {
        ["kadin-sac-kesimi"] = new(
            "Bornova kadın kuaförü | Celil & Atilla",
            "Bornova women's haircut | Celil & Atilla",
            "Bornova kadın saç kesimi Kazımdirik Mah. Gediz Cd. 21 A’da yapılır. Yüz hattı ve saç yoğunluğuna göre planlanır. Randevu alın: 0534 794 43 93. Tek adres.",
            "Women's haircuts in Bornova, at Gediz Cd. 21 A, Kazımdirik. The cut follows your face and hair density. Book on 0534 794 43 93. There is no second branch."),
        ["balyaj-ombre-sombre"] = new(
            "Bornova balyaj | Celil & Atilla",
            "Bornova balayage | Celil & Atilla",
            "Bornova balyaj, ombre ve sombre Kazımdirik Mah. Gediz Cd. 21 A’da uygulanır. Geçiş, saçın geçmişine göre seçilir. Randevu alın: 0534 794 43 93. Tek adres.",
            "Balayage, ombre and sombre in Bornova, at Gediz Cd. 21 A, Kazımdirik. The transition follows your colour history. Book: 0534 794 43 93. No second branch."),
        ["keratin-sac-botoksu"] = new(
            "Bornova keratin bakımı | Celil & Atilla",
            "Bornova keratin care | Celil & Atilla",
            "Bornova keratin bakımı ve saç botoksu Kazımdirik Mah. Gediz Cd. 21 A’da yapılır. Kabarma ve yıpranma ayrı değerlendirilir. Randevu: 0534 794 43 93. Tek adres.",
            "Keratin care and hair botox in Bornova, at Gediz Cd. 21 A, Kazımdirik. Chosen for frizz or tired ends. Book on 0534 794 43 93. There is no second branch."),
        ["rofle-kok-rotusu"] = new(
            "Bornova röfle | Celil & Atilla",
            "Bornova highlights | Celil & Atilla",
            "Bornova röfle ve kök rötuşu Kazımdirik Mah. Gediz Cd. 21 A’da yapılır. Işık ve dip uzaması aynı günde karıştırılmaz. Randevu: 0534 794 43 93. Tek adres.",
            "Highlights and root touch-ups in Bornova, at Gediz Cd. 21 A, Kazımdirik. Light and regrowth are planned apart. Book: 0534 794 43 93. There is no second branch."),
        ["gelin-saci"] = new(
            "Bornova gelin saçı | Celil & Atilla",
            "Bornova bridal hair | Celil & Atilla",
            "Bornova gelin saçı Kazımdirik Mah. Gediz Cd. 21 A’da prova ile planlanır. Tek salon bu adrestedir. Randevu için arayın: 0534 794 43 93. Tek adres, Bornova.",
            "Bridal hair in Bornova, at Gediz Cd. 21 A, Kazımdirik. A trial sets the style before the day. Book the salon: 0534 794 43 93. There is no second branch."),
        ["sac-kaynagi"] = new(
            "Bornova saç kaynağı | Celil & Atilla",
            "Bornova hair extensions | Celil & Atilla",
            "Bornova saç kaynağı Kazımdirik Mah. Gediz Cd. 21 A’da uygulanır. Uzunluk ve yoğunluk ön görüşmede netleşir. Randevu: 0534 794 43 93. Tek adres, Bornova.",
            "Hair extensions in Bornova, at Gediz Cd. 21 A, Kazımdirik. Length and density are agreed before the visit. Book: 0534 794 43 93. There is no second branch."),
        ["erkek-kesim-fade"] = new(
            "Bornova erkek kuaför | Celil & Atilla",
            "Bornova men's haircut | Celil & Atilla",
            "Bornova erkek kesim ve fade Kazımdirik Mah. Gediz Cd. 21 A’da yapılır. Sakal hattı aynı randevuda konuşulabilir. Arayın: 0534 794 43 93. Tek adres, Bornova.",
            "Men's cuts and fades in Bornova, at Gediz Cd. 21 A, Kazımdirik. Beard lines can be planned in the same visit. Book: 0534 794 43 93. There is no second branch."),
        ["cocuk-kesimi"] = new(
            "Bornova çocuk saç kesimi | Celil & Atilla",
            "Bornova kids' haircut | Celil & Atilla",
            "Bornova çocuk saç kesimi Kazımdirik Mah. Gediz Cd. 21 A’da, sakin bir randevuda yapılır. Ebeveyn yanında durabilir. Arayın: 0534 794 43 93. Tek adres, Bornova.",
            "Kids' haircuts in Bornova, at Gediz Cd. 21 A, Kazımdirik. A calm visit, with a parent beside the chair. Book: 0534 794 43 93. There is no second branch.")
    };

    static readonly Dictionary<string, (string Tr, string En)> Queries = new(StringComparer.Ordinal)
    {
        ["kadin-sac-kesimi"] = ("Bornova kadın kuaförü", "Bornova women's haircut"),
        ["balyaj-ombre-sombre"] = ("Bornova balyaj", "Bornova balayage"),
        ["keratin-sac-botoksu"] = ("Bornova keratin bakımı", "Bornova keratin care"),
        ["rofle-kok-rotusu"] = ("Bornova röfle", "Bornova highlights"),
        ["gelin-saci"] = ("Bornova gelin saçı", "Bornova bridal hair"),
        ["sac-kaynagi"] = ("Bornova saç kaynağı", "Bornova hair extensions"),
        ["erkek-kesim-fade"] = ("Bornova erkek kuaför", "Bornova men's haircut"),
        ["cocuk-kesimi"] = ("Bornova çocuk saç kesimi", "Bornova kids' haircut")
    };

    public static bool TryGet(string? slug, out Snippet snippet) =>
        Known.TryGetValue(slug ?? "", out snippet);

    public static string Query(string? slug, string culture)
    {
        if (!Queries.TryGetValue(slug ?? "", out var pair))
            return "";
        return culture.StartsWith("en", StringComparison.OrdinalIgnoreCase) ? pair.En : pair.Tr;
    }

    public static string VisibleName(Service service, string culture)
    {
        var query = Query(service.Slug, culture);
        return query.Length > 0 ? query : service.LocalizedName(culture);
    }

    public const string ServiceLeadTr = "Bornova kuaför Celil & Atilla, Kazımdirik Mah. Gediz Cd. 21 A. İzmir kadın kuaförü arayanlar bu salona gelir.";
    public const string ServiceLeadEn = "Bornova hair salon Celil & Atilla, Gediz Cd. 21 A, Kazımdirik. One address in İzmir.";

    public static string WithQueryHeadings(string? html, string? slug, string culture)
    {
        if (string.IsNullOrEmpty(html))
            return "";
        var query = Query(slug, culture);
        if (query.Length == 0)
            return html;

        var english = culture.StartsWith("en", StringComparison.OrdinalIgnoreCase);
        (string From, string To)[] map = english
            ?
            [
                ("What it's for", $"{query}: what it's for"),
                ("How it works", $"{query}: how it works"),
                ("Who it's for", $"{query}: who it's for"),
                ("Aftercare", $"{query}: aftercare"),
                ("FAQ", $"{query}: questions")
            ]
            :
            [
                ("Ne için?", $"{query}: ne için?"),
                ("Nasıl ilerler?", $"{query}: nasıl ilerler?"),
                ("Kimler için uygun?", $"{query}: kimler için?"),
                ("Sonrasında bakım", $"{query}: sonrası bakım"),
                ("Sık sorulanlar", $"{query}: sık sorulanlar")
            ];

        foreach (var (from, to) in map)
            html = html.Replace($"<h2>{from}</h2>", $"<h2>{to}</h2>", StringComparison.Ordinal);
        return html;
    }

    public static string Title(Service service, string culture)
    {
        if (TryGet(service.Slug, out var snippet))
            return culture.StartsWith("en", StringComparison.OrdinalIgnoreCase) ? snippet.TitleEn : snippet.TitleTr;
        return LocalHeadTerms.WithBornova(service.LocalizedSeoTitle(culture));
    }

    public static string Meta(Service service, string culture)
    {
        if (TryGet(service.Slug, out var snippet))
            return culture.StartsWith("en", StringComparison.OrdinalIgnoreCase) ? snippet.MetaEn : snippet.MetaTr;
        return service.LocalizedMetaDescription(culture);
    }

    public static List<Service> Sort(IEnumerable<Service> services) =>
        services
            .OrderBy(service =>
            {
                var index = Array.IndexOf(Slugs, service.Slug);
                return index < 0 ? Slugs.Length : index;
            })
            .ThenBy(service => service.Name)
            .ToList();
}
