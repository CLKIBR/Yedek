using Domain.Entities;
using Domain.Enums;
using Infrastructure.Import;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Infrastructure.Services;

// Okul verilerini JSON'dan alýp veritabanýna ekleyen/güncelleyen servis
public class SchoolImportService
{
    private readonly BaseDbContext _context; // Doðru DbContext türü

    public SchoolImportService(BaseDbContext context)
    {
        _context = context;
    }

    // JSON'dan okullarý yükler ve upsert iþlemi yapar
    public async Task ImportAsync(string jsonPath)
    {
        var rows = ImportHelpers.LoadFromJson(jsonPath);
        var schools = _context.Set<School>();

        foreach (var row in rows)
        {
            // OKUL_ADI: "Ýl - Ýlçe - Okul Adý" formatýnda
            var parts = row.OKUL_ADI.Split(" - ", StringSplitOptions.None);
            string city = parts.Length > 0 ? parts[0].Trim() : string.Empty;
            string district = parts.Length > 1 ? parts[1].Trim() : string.Empty;
            string name = parts.Length > 2 ? string.Join(" - ", parts.Skip(2)).Trim() : string.Empty;

            // Ayný isim, þehir ve ilçe ile okul var mý?
            var existing = await schools.FirstOrDefaultAsync(s => s.Name == name && s.City == city && s.District == district);
            if (existing == null)
            {
                // Yeni okul ekle
                var school = new School
                {
                    Name = name,
                    City = city,
                    District = district,
                    Country = "Türkiye",
                    Notes = $"HOST: {row.HOST}, YOL: {row.YOL}",
                    Type = SchoolType.Unknown
                };
                schools.Add(school);
            }
            else
            {
                // Mevcut okulu güncelle
                existing.Notes = $"HOST: {row.HOST}, YOL: {row.YOL}";
                existing.Country = "Türkiye";
                existing.Type = SchoolType.Unknown;
            }
        }
        await _context.SaveChangesAsync();
    }
}
