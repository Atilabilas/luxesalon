namespace LuxeSalon.Web.Services;

// Corrections for the bundled stock examples. Uploaded CMS images keep their own identity.
public static class ContentImageCatalog
{
    private static readonly Dictionary<string, string> Replacements = new(StringComparer.OrdinalIgnoreCase)
    {
        ["keratin"] = "fon", ["kaynak"] = "fon", ["pastel"] = "balyaj",
        ["kirpik"] = "makyaj", ["protein"] = "fon", ["kas"] = "agda", ["form"] = "fon"
    };

    private static readonly Dictionary<string, (string Tr, string En)> Descriptions = new()
    {
        ["kadin-kesim"] = ("Kesim koltukları ve aynalar bulunan temsili salon iç mekânı", "Illustrative salon interior with styling chairs and mirrors"),
        ["balyaj"] = ("Uzun, dalgalı ve mor tonlu saç modeli; temsili görsel", "Long wavy hair with purple tones; illustrative image"),
        ["fon"] = ("Arkadan görünen uzun, dalgalı saç modeli; temsili görsel", "Long wavy hair shown from behind; illustrative image"),
        ["rofle"] = ("Kuaförde saçına renk işlemi yapılan kadın; temsili görsel", "A woman having her hair coloured in a salon; illustrative image"),
        ["erkek-kesim"] = ("Berber koltuğunda erkek saç ve sakal bakımı; temsili görsel", "Men's hair and beard care in a barber chair; illustrative image"),
        ["erkek-boya"] = ("Berberde erkek saçına şekil verilmesi; temsili görsel", "Men's hair styling at a barber; illustrative image"),
        ["makyaj"] = ("Göz ve dudak makyajının yakın görünümü; temsili görsel", "A close view of eye and lip makeup; illustrative image"),
        ["agda"] = ("Kaş çevresinde bakım uygulaması; temsili görsel", "Brow grooming; illustrative image"),
        ["manikur"] = ("Koyu renk oje uygulanmış tırnaklar; temsili görsel", "Nails painted with dark polish; illustrative image"),
        ["pedikur"] = ("Pembe oje uygulanmış el tırnakları; temsili tırnak bakım görseli", "Hand nails with pink polish; illustrative nail care image"),
        ["gelin-sac"] = ("Düğün buketi taşıyan gelin; temsili görsel", "A bride holding a wedding bouquet; illustrative image"),
        ["cocuk"] = ("Açık havada bir çocuk; temsili görsel", "A child outdoors; illustrative image"),
        ["tek-renk"] = ("Salonda müşteriyle ilgilenen kuaför; temsili görsel", "A stylist attending to a client in a salon; illustrative image"),
        ["kahkul"] = ("Salonda bakım hazırlığı yapan kuaför; temsili görsel", "A stylist preparing a salon service; illustrative image"),
        ["sac-botoks"] = ("Birlikte poz veren uzun saçlı kişiler; temsili görsel", "People with long hair posing together; illustrative image"),
        ["beach-waves"] = ("Kıvırcık saç modeli; temsili görsel", "A curly hairstyle; illustrative image"),
        ["sakal"] = ("Saç ve sakal bakım araçları; temsili görsel", "Hair and beard grooming tools; illustrative image")
    };

    public static string Resolve(string source)
    {
        if (source.Contains("images.unsplash.com/photo-1560066984-138dadb4c035")) return "/images/services/kadin-kesim.jpg";
        if (source.Contains("images.unsplash.com/photo-1522337360788-8b13dee7a37e")) return "/images/services/fon.jpg";
        if (!source.StartsWith("/images/services/", StringComparison.OrdinalIgnoreCase)) return source;
        var queryAt = source.IndexOf('?');
        var path = queryAt >= 0 ? source[..queryAt] : source;
        var name = Path.GetFileNameWithoutExtension(path);
        return Replacements.TryGetValue(name, out var replacement)
            ? "/images/services/" + replacement + Path.GetExtension(path) + (queryAt >= 0 ? source[queryAt..] : "")
            : source;
    }

    public static string? Alt(string source, string culture)
    {
        var en = culture == "en";
        if (source.StartsWith("/images/services/", StringComparison.OrdinalIgnoreCase) &&
            Descriptions.TryGetValue(Path.GetFileNameWithoutExtension(source.Split('?')[0]), out var description))
            return en ? description.En : description.Tr;
        if (source.Contains("images.unsplash.com/photo-1560066984-138dadb4c035"))
            return en ? Descriptions["kadin-kesim"].En : Descriptions["kadin-kesim"].Tr;
        if (source.Contains("images.unsplash.com/photo-1522337360788-8b13dee7a37e"))
            return en ? Descriptions["fon"].En : Descriptions["fon"].Tr;
        return null;
    }
}
