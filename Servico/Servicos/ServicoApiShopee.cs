using AutoMapper;
using Dominio;
using Dominio.Entidades;
using Dominio.Interfaces;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Servico.Servicos
{
    public class ServicoApiShopee : ServicoBase<Produto>, IServicoShopee
    {

        const string AppId = "18345230041";
        const string Secret = "VT5BXKHLQ4RZENS7GPZHP4CCQACJKKHB";
        const string Endpoint = "https://open-api.affiliate.shopee.com.br/graphql";
        private readonly IMapper _mapper;

        public ServicoApiShopee(IRepositorioBase<Produto> baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
        }

        public TOutputModel ObterOfertas<TOutputModel>(List<Produto> produtos) where TOutputModel : class
        {
            var produto = RetornarProdutoEmOferta();

            TOutputModel outputModel = _mapper.Map<TOutputModel>(produto);

            return outputModel;
        }

        public TOutputModel ObterProdutoPorNome<TOutputModel>(string nome) where TOutputModel : class
        {
            throw new NotImplementedException();
        }

        public Produto RetornarProdutoEmOferta()
        {
            var listaProdutos = BuscarListaProdutosShopee().Result;
            return validarProdutoNaLista(listaProdutos);
        }

        public Produto validarProdutoNaLista(List<Produto> listaProdutos)
        {
            int contador = 1;
            Produto produtoParaEnviar = new Produto();
            bool continuarValidacao = true;
            do
            {
                foreach (var produto in listaProdutos)
                    if (!ProdutoExiste(produto) && ProdutoValido(produto))
                    {
                        produtoParaEnviar = produto;
                        AdicionarProdutoNoJson(produtoParaEnviar);
                        return produtoParaEnviar;
                    }


                contador++;
                listaProdutos = BuscarListaProdutosShopee(contador).Result;
            } while ((continuarValidacao == true));

            AdicionarProdutoNoJson(produtoParaEnviar);
            return produtoParaEnviar;
        }

        private bool ProdutoValido(Produto produto)
        {
            if (
                produto.PriceDiscountRate > 0 ||
                (produto.PriceMax != null || string.IsNullOrEmpty(produto.PriceMax)) ||
                (produto.PriceMin != null || string.IsNullOrEmpty(produto.PriceMin)) ||
                produto.ProductName != null ||
                (produto.OfferLink != null || string.IsNullOrEmpty(produto.OfferLink)) ||
                produto.PeriodEndTime != null ||
                produto.PeriodStartTime != null ||
                produto.RatingStar != null ||
                (produto.ImageUrl != null || string.IsNullOrEmpty(produto.ImageUrl))
            )
                return true;

            return false;
        }

        bool ProdutoExiste(Produto produto)
        {
            string caminhoJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProdutosEnviados.json");

            if (!File.Exists(caminhoJson))
            {
                File.WriteAllText(caminhoJson, "[]");
            }

            string produtosJson = File.ReadAllText(caminhoJson);
            List<Produto> produtos = JsonConvert.DeserializeObject<List<Produto>>(produtosJson);

            return produtos.Any(p => p.ProductName == produto.ProductName);
        }

        void AdicionarProdutoNoJson(Produto produto)
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProdutosEnviados.json");

            List<Produto> produtos;

            if (File.Exists(jsonFilePath))
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                produtos = JsonConvert.DeserializeObject<List<Produto>>(jsonContent);
            }
            else
            {
                produtos = new List<Produto>();
            }

            produtos.Add(produto);

            string updatedJsonContent = JsonConvert.SerializeObject(produtos, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(jsonFilePath, updatedJsonContent);
        }

        public async Task<List<Produto>> BuscarListaProdutosShopee(int numeroPagina = 0)
        {
            try
            {
                string payload = "{\"query\":\"query{productOfferV2(listType: 0, sortType: 5, page:" + numeroPagina + ", limit: 20){" +
                      "nodes{imageUrl, priceMax, priceMin, productName, " +
                      "offerLink,priceDiscountRate,ratingStar, periodStartTime, periodEndTime}" +
                      ",pageInfo{page, limit, hasNextPage}}}\"}";


                long timestamp = GetUnixTimestamp();

                string assinaturaApi = $"{AppId}{timestamp}{payload}{Secret}";

                string assinaturaCriptografada = ComputeSha256Hash(assinaturaApi);

                using (HttpClient apiClient = new HttpClient())
                {
                    HttpRequestMessage requisicao = new HttpRequestMessage(HttpMethod.Post, Endpoint);
                    requisicao.Content = new StringContent(payload, Encoding.UTF8, "application/json");

                    requisicao.Headers.Add("Authorization", $"SHA256 Credential={AppId}, Timestamp={timestamp}, Signature={assinaturaCriptografada}");

                    HttpResponseMessage response = await apiClient.SendAsync(requisicao);


                    string responseData = await response.Content.ReadAsStringAsync();
                    List<Produto>? listaProdutos = JsonConvert.DeserializeObject<ResponseApi>(responseData)
                         ?.Data?.ProductOfferV2?.Nodes
                         ?.Select(produto => new Produto
                         {
                             ImageUrl = produto.ImageUrl,
                             PriceMax = (Convert.ToDecimal(produto.PriceMax) / (1 - (Convert.ToDecimal(produto.PriceDiscountRate) / 100))).ToString("0.00"),
                             PriceMin = produto.PriceMin,
                             ProductName = produto.ProductName,
                             OfferLink = produto.OfferLink,
                             PeriodStartTime = produto.PeriodStartTime,
                             PeriodEndTime = produto.PeriodEndTime,
                             PriceDiscountRate = produto.PriceDiscountRate,
                             RatingStar = produto.RatingStar
                         })
                         ?.Where(x => x.PriceDiscountRate > 0).ToList();

                    return listaProdutos;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro Comunicacao API Shopee: {ex.Message}");
                return null;
            }
        }

        static long GetUnixTimestamp()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }

        static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
