using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelipeGavilan.Codigo
{
    public abstract class AbstractProgress
    {
        public Progress<int> reportarProgreso { get; set; }
        public AbstractProgress()
        {
             new Progress<int>(ReportarProgreso);
        }



        public abstract void ReportarProgreso(int porcentaje);
    }
}
