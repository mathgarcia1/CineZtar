using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cine.Models
{
    public class RetornoMercadoPago
    {
        public string IdPreferencia { get; set; }
        public string Url { get; set; }

        public String status { get; set; }
        public String erro { get; set; }

    }
}
