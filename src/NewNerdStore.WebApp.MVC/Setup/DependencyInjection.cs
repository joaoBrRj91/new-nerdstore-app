using NewNerdStore.Catalogos.Application.AppServices.Interfaces;
using NewNerdStore.Catalogos.Application.AppServices;
using NewNerdStore.Catalogos.Domain.DomainServices.Interfaces;
using NewNerdStore.Catalogos.Domain.DomainServices;
using NewNerdStore.Catalogos.Domain.Interfaces.Repositories;
using NewNerdStore.Catalogos.Infra.Repositories;
using NewNerdStore.Core.Bus;
using Microsoft.EntityFrameworkCore;
using NewNerdStore.Catalogos.Infra.Contexts;
using MediatR;
using NewNerdStore.Catalogos.Domain.Events;
using NewNerdStore.Catalogos.Domain.Events.Handlers;
using NewNerdStore.Vendas.Application.CQRS.Commands;
using NewNerdStore.Vendas.Domain.Interfaces.Repositories;
using NerdStore.Vendas.Infra.Repository;
using NerdStore.Vendas.Infra;

namespace NewNerdStore.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services, string connectionString)
        {
            #region Bus (Mediator)
            services.AddScoped<IDomainMediatorHandler, DomainMediatorHandler>();
            services.AddScoped<ICommandMediatorHandler, CommandMediatorHandler>();
            #endregion

            #region Bounded Context Catalogos

            #region Domain Events Notifications Handlers (Mediator)
            services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoAbaixoEstoqueEventHandler>();
            #endregion
           
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddDbContext<CatalogoContext>(options =>
             options.UseSqlServer(connectionString));
            #endregion

            #region Bounded Context Vendas

            #region Commands/Queries Handlers (Mediator)
            services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
            #endregion

            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddDbContext<VendasContext>(options =>
             options.UseSqlServer(connectionString));
            #endregion

        }
    }
}
