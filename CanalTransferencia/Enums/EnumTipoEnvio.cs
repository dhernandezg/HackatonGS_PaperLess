using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanalTransferencia.Enums
{
    [Flags]
    public enum EnumTipoEnvio
    {
        None = 0,
        Email = 2,
        SMS = 4,
        WhatsApp = 8
    }
}
