using Dominio;
using InfraEstrutura.BancoDeDados;

namespace InfraEstrutura;

public class LeadTelegramRepositorio : RepositorioBase<LeadTelegram>, ILeadTelegramRepositorio
{
    public LeadTelegramRepositorio(ApiDBContext context) : base(context)
    {
    }
}
