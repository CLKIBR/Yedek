using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums;
public enum LinkRoleType // Bağlantı rolleri
{
    Unknown = 0, // Belirtilmemiş
    MainTeacher = 1, // Ana öğretmen
    AssistantTeacher = 2, // Yardımcı öğretmen
    Counselor = 3, // Rehber öğretmen
    SupportStaff = 4 // Destek personeli
}

