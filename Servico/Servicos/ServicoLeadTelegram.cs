using AutoMapper;
using Dominio;
using Servico.Servicos;

namespace Servico;

public class ServicoLeadTelegram : ServicoBase<LeadTelegram>, IServicoLeadTelegram
{
    public ServicoLeadTelegram(IRepositorioBase<LeadTelegram> baseRepository, IMapper mapper) : base(baseRepository, mapper)
    {
    }
}
