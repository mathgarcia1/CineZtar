// <copyright file="MercadoPagoModel.cs" company="CineZtarCompany">
// Copyright (c) CineZtarCompany. All rights reserved.
// </copyright>
/// <summary>
/// Description of the class or file.
/// </summary>
/// <author>mathgarcia1</author>
/// <created>2023-05-31 14:02:40</created>
/// <lastModified>2023-05-31 14:02:40</lastModified>
namespace Cine.Models
{
    using System;

    public class MercadoPagoModel
    {
        public string email { get; set; }

        public string nome { get; set; }

        public string cidade { get; set; }

        public string estado { get; set; }

        public string telefone { get; set; }

        public int idPagamento { get; set; }

        public string logradouro { get; set; }

        public string numero { get; set; }

        public string cep { get; set; }

        public string nomePlano { get; set; }

        public decimal valor { get; set; }
    }
}
