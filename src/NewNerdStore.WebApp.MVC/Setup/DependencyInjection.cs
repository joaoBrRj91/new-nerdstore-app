using NewNerdStore.Catalogos.Application.AppServices.Interfaces;
using NewNerdStore.Catalogos.Application.AppServices;
using NewNerdStore.Catalogos.Domain.DomainServices.Interfaces;
using NewNerdStore.Catalogos.Domain.DomainServices;
using NewNerdStore.Catalogos.Domain.Interfaces.Repositories;
using NewNerdStore.Catalogos.Infra.Repositories;
using NewNerdStore.Core.Bus;
using Microsoft.EntityFrameworkCore;
using NewNerdStore.Catalogos.Infra.Contexts;

namespace NewNerdStore.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services, string connectionString)
        {
            #region Bus (Mediator)
            services.AddScoped<IDomainMediatorHandler, DomainMediatorHandler>();
            #endregion

            #region Bounded Context Catalogos
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddDbContext<CatalogoContext>(options =>
             options.UseSqlServer(connectionString));
            #endregion


        }
    }
}
