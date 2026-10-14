namespace LuxeSalon.Web.Services;

public sealed record IzmirDistrictGroup(string TitleTr, string TitleEn, bool ContainsSalon, IReadOnlyList<string> Names);

// İzmir Büyükşehir Belediyesi'nin 30 ilçesi. Yalnız Bornova'da salon vardır.
public static class IzmirDistricts
{
    public static readonly IReadOnlyList<IzmirDistrictGroup> Groups =
    [
        new("Salonun bulunduğu ilçe", "The district where the salon is", true, ["Bornova"]),
        new(
            "Kent içindeki diğer ilçeler",
            "Other districts in the city",
            false,
            ["Bayraklı", "Karşıyaka", "Çiğli", "Konak", "Buca", "Karabağlar", "Balçova", "Gaziemir", "Narlıdere", "Güzelbahçe", "Menemen", "Kemalpaşa"]),
        new(
            "İl merkezi dışındaki ilçeler",
            "Districts farther from the city centre",
            false,
            ["Aliağa", "Foça", "Dikili", "Bergama", "Kınık", "Bayındır", "Beydağ", "Kiraz", "Ödemiş", "Tire", "Torbalı", "Menderes", "Seferihisar", "Urla", "Çeşme", "Karaburun", "Selçuk"])
    ];

    public static IReadOnlyList<string> All { get; } = Groups.SelectMany(group => group.Names).ToArray();
}
