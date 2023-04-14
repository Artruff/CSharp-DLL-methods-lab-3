using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManageProgram.Access
{
    /// <summary>
    /// Перечисление с уровнями доступности меню
    /// </summary>
    public enum AccessEnum
    {
        VisibleAndAccessible,
        VisibleAndNotAccessible,
        NotVisible
    }
}
