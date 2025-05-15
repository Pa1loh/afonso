using AutoMapper;
using Dominio;
using Dominio.Entidades;
using Dominio.Interfaces;

namespace Servico.Servicos
{
    public class servicoSocialSoul : ServicoBase<Produto>, IServicoSocialSoul
    {
        string appToken = "1709105999187c909237a";
        string categoryId = "6";
        string sourceId = "23320049";
        string baseUrl = "http://sandbox-api.lomadee.com/v3/";


        private readonly IMapper _mapper;

        public servicoSocialSoul(IRepositorioBase<Produto> baseRepository, IMapper mapper) : base(baseRepository, mapper)
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
                try
                {
                    string url = $"{baseUrl}{appToken}/offer/_category/{categoryId}?sourceId={sourceId}";

                    // Fazendo a requisição GET
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Verificando se a requisição foi bem-sucedida (código 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Lendo o conteúdo da resposta
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);
                    }
                    else
                    {
                        Console.WriteLine($"Erro na requisição: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
                return null;
            }
        }
    }
}