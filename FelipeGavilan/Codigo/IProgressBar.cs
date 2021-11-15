using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FelipeGavilan.Codigo
{
    public interface IProgressBar
    {
        public void ReportarProgreso(int porcentaje);
    }
}
