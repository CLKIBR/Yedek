using System.Text.Json;
using System.Text;

namespace Infrastructure.Import;

public static class ImportHelpers
{
    // JSON dosyasýndan MebSchoolRow listesini okur
    public static List<MebSchoolRow> LoadFromJson(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"JSON dosyasý bulunamadý: {path}");

        var json = File.ReadAllText(path, Encoding.UTF8);
        var list = JsonSerializer.Deserialize<List<MebSchoolRow>>(json);
        return list ?? new List<MebSchoolRow>();
    }
}
