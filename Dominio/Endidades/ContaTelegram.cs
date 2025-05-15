using Dominio.Entidades;

namespace Dominio;

public class ContaTelegram : EntidadeBase
{
    public string? ApiId { get; set; }
    public string? ApiHash { get; set; }
    public string? Numero { get; set; }
    public string? CodigoAuth { get; set; }
}
