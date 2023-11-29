using NewNerdStore.Catalogos.Application.AppServices.Interfaces;
using NewNerdStore.Catalogos.Application.AppServices;
using NewNerdStore.Catalogos.Domain.DomainServices.Interfaces;
using NewNerdStore.Catalogos.Domain.DomainServices;
using NewNerdStore.Catalogos.Domain.Interfaces.Repositories;
using NewNerdStore.Catalogos.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using NewNerdStore.Catalogos.Infra.Contexts;
using MediatR;
using NewNerdStore.Catalogos.Domain.Events;
using NewNerdStore.Catalogos.Domain.Events.Handlers;
using NewNerdStore.Vendas.Domain.Interfaces.Repositories;
using NerdStore.Vendas.Infra.Repository;
using NerdStore.Vendas.Infra;
using NewNerdStore.Vendas.Application.Comunication.Commands;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Comunications.Mediator.Implementations;
using NewNerdStore.Core.Messages.Commons.Notifications.Errors;
using NewNerdStore.Core.Messages.Commons.Notifications.Events;
using NewNerdStore.Vendas.Application.ComunicationBridge.Queries.Pedido;
using NewNerdStore.Vendas.Application.ComunicationBridge.Events.Domain;
using NerdStore.Vendas.Application.Events.Handler;
using NewNerdStore.Vendas.Application.ComunicationBridge.Commands.Handlers;
using NewNerdStore.Vendas.Application.ComunicationBridge.Commands;

namespace NewNerdStore.WebApp.MVC.Setup
{

    //TODO : REFACTORING : Extension Methods para as configs de dependencia do shared kernel e dos demais bounded contexts
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services, string connectionString)
        {
            #region Shared Kernel (Core)

            #region Mediator Strategies
            services.AddScoped<IDomainEventMediatorStrategy, DomainEventMediatorStrategy>();
            services.AddScoped<ICommandMediatorStrategy, CommandMediatorStrategy>();
            services.AddScoped<INotificationMediatorStrategy, NotificationMediatorStrategy>();
            services.AddScoped<INotificationEventManager, NotificationEventManager>();

            #region Notifications
            services.AddScoped<INotificationHandler<DomainErrorNotifications>, DomainNotificationErrorsHandler>();
            #endregion

            #endregion

            #endregion

            #region Bounded Context Catalogos

            #region Domain Events Handlers (Mediator)
            services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueDomainEvent>, ProdutoAbaixoEstoqueDomainEventHandler>();
            #endregion
           
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddDbContext<CatalogoContext>(options =>
             options.UseSqlServer(connectionString));
            #endregion

            #region Bounded Context Vendas

            #region Commands/Queries Handlers (Mediator)
            services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoAdicionarItemCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarItemPedidoCommand, bool>, PedidoAtualizarItemCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverItemPedidoCommand, bool>, PedidoRemoverItemCommandHandler>();
            services.AddScoped<IRequestHandler<AplicarVoucherPedidoCommand, bool>, PedidoAplicarVoucherCommandHandler>();
            #endregion

            #region Domain Events Handlers (Mediator)
            services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoDomainEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoRascunhoAtualizadoDomainEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoItemAdicionadoDomainEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoProdutoRemovidoEvent>, PedidoEventHandler>();

            #endregion

            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoQuery, PedidoQuery>();
            services.AddDbContext<VendasContext>(options =>
             options.UseSqlServer(connectionString));
            #endregion

        }
    }
}
