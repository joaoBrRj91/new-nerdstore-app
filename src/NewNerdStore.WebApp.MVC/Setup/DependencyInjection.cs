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
using NewNerdStore.Core.Messages.Commons.IntegrationEvents;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Pagamentos.AntiCorruption;
using NerdStore.Pagamentos.Business;
using NerdStore.Pagamentos.Data.Repository;
using NerdStore.Pagamentos.Data;
using EventSourcing.Interfaces;
using EventSourcing;
using IConfigurationManager = NerdStore.Pagamentos.AntiCorruption.IConfigurationManager;
using ConfigurationManager = NerdStore.Pagamentos.AntiCorruption.ConfigurationManager;
using NewNerdStore.Core.Data.EventSourcing;

namespace NewNerdStore.WebApp.MVC.Setup
{

    //TODO : REFACTORING : Extension Methods para as configs de dependencia do shared kernel e dos demais bounded contexts
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services, string connectionString)
        {
            #region Shared Kernel (Core)

            #region Mediator Strategies
            services.AddScoped<IEventMediatorStrategy, EventMediatorStrategy>();
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
            services.AddScoped<INotificationHandler<PedidoIniciadoEvent>, PedidoIniciadoIntegrationEventHandler>();
            services.AddScoped<INotificationHandler<PedidoProcessamentoCanceladoEvent>, PedidoProcessamentoCanceladoIntegrationEventHandler>();

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
            services.AddScoped<IRequestHandler<IniciarPedidoCommand, bool>, PedidoIniciadoCommandHandler>();
            services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoCommand, bool>, PedidoCancelarProcessamentoCommandHandler>();
            services.AddScoped<IRequestHandler<FinalizarPedidoCommand, bool>, PedidoFinalizadoCommandHandler>();
            services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoEstornarEstoqueCommand, bool>, PedidoCancelarProcessamentoEstornarEstoqueCommandHandler>();
            #endregion

            #region Domain Events Handlers (Mediator)
            services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoDomainEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoRascunhoAtualizadoDomainEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoItemAdicionadoDomainEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoProdutoRemovidoDomainEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<VoucherAplicadoPedidoDomainEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoEstoqueRejeitadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PagamentoRealizadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PagamentoRecusadoEvent>, PedidoEventHandler>();

            #endregion

            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoQuery, PedidoQuery>();
            services.AddDbContext<VendasContext>(options =>
             options.UseSqlServer(connectionString));
            #endregion

            #region Bounded Context Pagamentos
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            services.AddScoped<IPagamentoService, PagamentoService>();
            services.AddScoped<IPagamentoCartaoCreditoFacade, PagamentoCartaoCreditoFacade>();
            services.AddScoped<IPayPalGateway, PayPalGateway>();
            services.AddScoped<IConfigurationManager, ConfigurationManager>();
            services.AddDbContext<PagamentoContext>(options =>
             options.UseSqlServer(connectionString));
            #endregion

            #region Event Sourcing
            services.AddSingleton<IEventStoreService, EventStoreService>();
            services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();

            #endregion
        }
    }
}
