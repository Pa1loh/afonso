using AutoMapper;
using Dominio;
using Dominio.Entidades;
using Dominio.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Servico.Servicos
{
    public class ServicoAwin : ServicoBase<Produto>, IServicoAwin
    {
        const string IdLoja = "17729";
        const string ChaveApi = "5234a9b3-05b9-4811-bb91-d6e532fc03fe";
        private readonly IMapper _mapper;

        public ServicoAwin(IRepositorioBase<Produto> baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
        }

        public TOutputModel ObterOfertas<TOutputModel>(List<Produto> produtos) where TOutputModel : class
        {
            var produto = RetornarProdutoEmOferta();

            TOutputModel outputModel = _mapper.Map<TOutputModel>(produto);

            return outputModel;
        }

        private async Task<object> RetornarProdutoEmOferta()
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"https://api.awin.com/publisher/1576488/promotions";

                string requestBody = "{\"filters\": {\"membership\": \"joined\", \"status\": \"active\", \"type\": \"promotion\", \"updatedSince\": \"2022-05-06\"}, \"pagination\": {\"page\": 1}}";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ChaveApi);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                // Verificando se a requisição foi bem-sucedida
                if (response.IsSuccessStatusCode)
                {
                    // Lendo e exibindo os dados da resposta

                    string responseBody = await response.Content.ReadAsStringAsync();
                    PromoData promoData = JsonSerializer.Deserialize<PromoData>(responseBody);

                    foreach (var promo in promoData.Data)
                    {
                        var x = promo.PromotionId;


                    }


                }
                else
                {
                    // Exibindo o código de status caso a requisição falhe
                    Console.WriteLine($"Erro: {response.StatusCode}");
                }
                return null;
            }
        }
    }
}
public class PromoData
{
    public List<Promotion> Data { get; set; }
}

public class Promotion
{
    public int PromotionId { get; set; }
    public string Title { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string Url { get; set; }
    // Adicione outras propriedades conforme necessário
}