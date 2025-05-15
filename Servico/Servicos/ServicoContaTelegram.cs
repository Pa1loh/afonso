using AutoMapper;
using Dominio;
using Dominio.Interfaces;
using Servico.Servicos;

namespace Servico;

public class ServicoContaTelegram : ServicoBase<ContaTelegram>, IServicoContaTelegram
{
    public ServicoContaTelegram(IRepositorioBase<ContaTelegram> baseRepository, IMapper mapper) : base(baseRepository, mapper)
    {
    }
}
