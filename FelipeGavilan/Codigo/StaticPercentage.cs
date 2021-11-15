using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelipeGavilan.Codigo
{
    public static class StaticPercentage
    {
        public static int PercentageProgress(double indice , double total)
        {
            var porcentaje = (double)indice / total;
            porcentaje = porcentaje * 100;

            var porcentajeInt = (int)Math.Round(porcentaje, 0);
            return porcentajeInt;
        }
    }
}
