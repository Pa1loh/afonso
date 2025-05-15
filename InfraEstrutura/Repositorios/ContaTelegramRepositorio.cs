using Dominio;
using InfraEstrutura.BancoDeDados;

namespace InfraEstrutura;

public class ContaTelegramRepositorio : RepositorioBase<ContaTelegram>, IContaTelegramRepositorio
{
    public ContaTelegramRepositorio(ApiDBContext context) : base(context)
    {
    }
}
