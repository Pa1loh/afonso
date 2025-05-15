using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio.Entidades
{
    public class ResponseApi : EntidadeBase
    {
        public ResponseShopeeData Data { get; set; }
        public List<Error> Errors { get; set; }
    }
}
