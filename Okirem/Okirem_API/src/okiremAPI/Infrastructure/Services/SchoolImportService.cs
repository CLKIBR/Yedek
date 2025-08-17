using Domain.Entities;
using Domain.Enums;
using Infrastructure.Import;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Infrastructure.Services;

// Okul verilerini JSON'dan al�p veritaban�na ekleyen/g�ncelleyen servis
public class SchoolImportService
{
    private readonly BaseDbContext _context; // Do�ru DbContext t�r�

    public SchoolImportService(BaseDbContext context)
    {
        _context = context;
    }

    // JSON'dan okullar� y�kler ve upsert i�lemi yapar
    public async Task ImportAsync(string jsonPath)
    {
        var rows = ImportHelpers.LoadFromJson(jsonPath);
        var schools = _context.Set<School>();

        foreach (var row in rows)
        {
            // OKUL_ADI: "�l - �l�e - Okul Ad�" format�nda
            var parts = row.OKUL_ADI.Split(" - ", StringSplitOptions.None);
            string city = parts.Length > 0 ? parts[0].Trim() : string.Empty;
            string district = parts.Length > 1 ? parts[1].Trim() : string.Empty;
            string name = parts.Length > 2 ? string.Join(" - ", parts.Skip(2)).Trim() : string.Empty;

            // Ayn� isim, �ehir ve il�e ile okul var m�?
            var existing = await schools.FirstOrDefaultAsync(s => s.Name == name && s.City == city && s.District == district);
            if (existing == null)
            {
                // Yeni okul ekle
                var school = new School
                {
                    Name = name,
                    City = city,
                    District = district,
                    Country = "T�rkiye",
                    Notes = $"HOST: {row.HOST}, YOL: {row.YOL}",
                    Type = SchoolType.Unknown
                };
                schools.Add(school);
            }
            else
            {
                // Mevcut okulu g�ncelle
                existing.Notes = $"HOST: {row.HOST}, YOL: {row.YOL}";
                existing.Country = "T�rkiye";
                existing.Type = SchoolType.Unknown;
            }
        }
        await _context.SaveChangesAsync();
    }
}
