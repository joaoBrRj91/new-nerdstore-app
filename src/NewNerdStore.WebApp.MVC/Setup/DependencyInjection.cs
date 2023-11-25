﻿using NewNerdStore.Catalogos.Application.AppServices.Interfaces;
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

namespace NewNerdStore.WebApp.MVC.Setup
{
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
            services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
            #endregion

            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddDbContext<VendasContext>(options =>
             options.UseSqlServer(connectionString));
            #endregion

        }
    }
}
