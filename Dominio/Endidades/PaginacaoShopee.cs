using Dominio.Entidades;

namespace Dominio
{
    public class PaginacaoShopee : EntidadeBase
    {
        public List<Produto> Nodes { get; set; }
        public InfoPaginacao PageInfo { get; set; }
    }
}
