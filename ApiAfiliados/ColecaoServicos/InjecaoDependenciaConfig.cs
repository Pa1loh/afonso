using AutoMapper;
using Dominio;
using Dominio.Entidades;
using Dominio.Interfaces;
using InfraEstrutura;
using Servico;
using Servico.Servicos;

namespace ApiAfiliados.ColecaoServicos
{
    public static class InjecaoDependenciaConfig
    {
        public static void RegistrarServices(this IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<PostgreSqlContext>(options =>
            //{
            //    options.UseNpgsql(connectionString);
            //}
            // );

            //services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));
            //services.AddScoped<JwtConfig>();

            #region Repositorios

            services.AddScoped(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));
            services.AddScoped<IContaTelegramRepositorio, ContaTelegramRepositorio>();
            services.AddScoped<ILeadTelegramRepositorio, LeadTelegramRepositorio>();

            //services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
            //services.AddScoped<IEventoRepositorio, EventoRepositorio>();
            //services.AddScoped<IRepositorioBase<Missao>, MissaoRepositorio>();
            //services.AddScoped<IMissaoRepositorio, MissaoRepositorio>();
            //services.AddScoped<INivelRepositorio, NivelRepositorio>();
            //services.AddScoped<IProdutoImagemRepositorio, ProdutoImagemRepositorio>();
            //services.AddScoped<IMissaoUsuarioRepositorio, MissaoUsuarioRepositorio>();
            //services.AddScoped<ICidadeRepositorio, CidadeRepositorio>();
            //services.AddScoped<IProdutoUsuarioRepositorio, ProdutoUsuarioRepositorio>();
            //services.AddScoped<IRecompensaResgateUsuarioRepositorio, RecompensaResgateUsuarioRepositorio>();
            //services.AddScoped<IRecompensaRepositorio, RecompensaRepositorio>();

            #endregion

            #region Servicos

            services.AddScoped(typeof(IServicoBase<>), typeof(ServicoBase<>));
            services.AddScoped<IServicoShopee, ServicoApiShopee>();
            services.AddScoped<IServicoAwin, ServicoAwin>();
            services.AddScoped<IServicoSocialSoul, servicoSocialSoul>();
            services.AddScoped<IServicoLeadTelegram, ServicoLeadTelegram>();
            services.AddScoped<IServicoContaTelegram, ServicoContaTelegram>();

            #endregion

        }

        public static IServiceCollection AdicionarModelos(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMapper>(new MapperConfiguration(config =>
            {
                //Produto
                config.CreateMap<Produto, Produto>();

                //Usuário
                config.CreateMap<ModeloEntradaContaTelegram, ContaTelegram>();
                config.CreateMap<ModeloAtualizacaoContaTelegram, ContaTelegram>();
                config.CreateMap<ContaTelegram, ModeloSaidaContaTelegram>();

                //Produto
                config.CreateMap<LeadTelegram, ModeloSaidaLeadTelegram>();
                config.CreateMap<ModeloEntradaLeadTelegram, LeadTelegram>();
                config.CreateMap<ModeloAtualizacaoLeadTelegram, LeadTelegram>();



            }).CreateMapper());

            return services;

        }
    }
}