namespace Dominio.Entidades
{
    public class InfoPaginacao
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public bool HasNextPage { get; set; }
    }
}
