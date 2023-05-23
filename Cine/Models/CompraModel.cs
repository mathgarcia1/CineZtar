using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Repositorio.Models;
using Repositorio.Repositorios;
using MercadoPago.Client.Common;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using System.Collections.Generic;

namespace Cine.Models
{
    
    public class CompraModel
    {

        public int IdCompra { get; set; }
        public DateTime? Data { get; set; }
        public int? IdStatus { get; set; }
        public decimal? Valor { get; set; }

        public String IdPreferencia { get; set; }
        public String Url { get; set; }

        
        public CompraModel selecionar(int id)
        {
            CompraModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                CompraRepositorio repositorio = new CompraRepositorio(contexto);
                Compra compra = repositorio.Recuperar(c => c.IdCompra == id);
                model = mapper.Map<CompraModel>(compra);
            }
            return model;
        }

        public CompraModel selecionarIdPreferencia(String IdPreferencia)
        {
            CompraModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                CompraRepositorio repositorio = new CompraRepositorio(contexto);
                Compra compra = repositorio.Recuperar(c => c.IdPreferencia== IdPreferencia);
                model = mapper.Map<CompraModel>(compra);
            }
            return model;
        }

        public CompraModel salvar(CompraModel model)
        {

            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Compra compra = mapper.Map<Compra>(model);

            using (DB_Ingressos2Context contexto = new DB_Ingressos2Context())
            {
                CompraRepositorio repositorio = new CompraRepositorio(contexto);

                if (model.IdCompra == 0)
                    repositorio.Inserir(compra);
                else
                    repositorio.Alterar(compra);

                contexto.SaveChanges();
            }
            model.IdCompra = compra.IdCompra;
            return model;


        }



        public async Task<RetornoMercadoPago> gerarPagamentoMercadoPago(MercadoPagoModel model)
        {

            RetornoMercadoPago ret = new RetornoMercadoPago();
            try
            {

                string cidade = model.cidade;
                string estado = model.estado;



                // Adicione as credenciais
                MercadoPagoConfig.AccessToken = "TEST-5818482197201188-122717-7f6bef44575fde6b43ddb8c8ee872495-168845261";


                String[] split = model.nome.Split(' ');
                // ...
                var payer = new PreferencePayerRequest
                {
                    Name = split[0],
                    Surname = split[split.Length - 1],
                    Email = model.email,
                    Phone = new PhoneRequest
                    {
                        AreaCode = "",
                        Number = model.telefone,
                    },

                    Identification = new IdentificationRequest
                    {
                        Type = "DNI",
                        Number = model.idPagamento.ToString(),
                    },

                    Address = new AddressRequest
                    {
                        StreetName = model.logradouro,
                        StreetNumber = model.numero,
                        ZipCode = model.cep,
                    },
                };
                // ...


                // ...
                var item = new PreferenceItemRequest
                {
                    Id = model.idPagamento.ToString(),
                    Title = "Venda Cinema Toledo",
                    Description = "Compra de ingressos Cinema Toledo",
                    CategoryId = "Cinema",
                    Quantity = 1,
                    CurrencyId = "BRL",
                    UnitPrice = model.valor,
                };
                // ...


                var request = new PreferenceRequest
                {
                    // ...
                    BackUrls = new PreferenceBackUrlsRequest
                    {
                        Success = "/Cine/Views/Finalizacao.cshtml",
                        Failure = "ENDPOINT_Retorno_falha",
                        Pending = "ENDPOINT_Retorno_pendencias",
                    },
                    AutoReturn = "approved",
                    Payer = payer,
                    Items = new List<PreferenceItemRequest>()
                };
                request.Items.Add(item);

                // Cria a preferência usando o client
                var client = new PreferenceClient();
                Preference preference = await client.CreateAsync(request);
                ret.Url = preference.InitPoint;
                ret.IdPreferencia = preference.Id;
                ret.status = "SUCESSO";
                // preference.
                return ret;

            }
            catch (Exception ex)
            {
                ret.status = "ERRO";
                ret.erro = ex.Message;
                return ret;
            }
        }
    }
}
