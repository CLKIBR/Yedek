using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums;
public enum RelationshipType // Veli-öğrenci yakınlık türleri
{
    Unknown = 0, // Belirtilmemiş
    Mother = 1, // Anne
    Father = 2, // Baba
    Guardian = 3, // Vasi
    Relative = 4 // Akraba
}
