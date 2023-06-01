/// <summary>
/// Description of the class or file.
/// </summary>
/// <author>mathgarcia1</author>
/// <created>2023-05-31 14:03:04</created>
/// <lastModified>2023-05-31 14:03:04</lastModified>
/// <copyright>
/// Copyright (c) 2023 mathgarcia1
/// </copyright>
namespace Cine.Models
{
    using System;

    public class RetornoMercadoPago
    {
        public string IdPreferencia { get; set; }

        public string Url { get; set; }

        public string Status { get; set; }

        public string Erro { get; set; }
    }
}
