// DTO: MEB JSON satýrý için veri transfer nesnesi
namespace Infrastructure.Import;

public class MebSchoolRow
{
    public string OKUL_ADI { get; set; } = string.Empty;
    public string HOST { get; set; } = string.Empty;
    public string YOL { get; set; } = string.Empty;
}
