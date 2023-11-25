using NerdStore.Vendas.Infra;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewNerdStore.Vendas.Infra.Extensions
{
    public static class MediatorExtension
    {
        public static async Task PublishEvents(this IDomainEventMediatorStrategy mediator, VendasContext ctx)
        {
            //var domainEntities = ctx.ChangeTracker
            //    .Entries<Entity>()
            //    .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

            //var domainEvents = domainEntities
            //    .SelectMany(x => x.Entity.Notificacoes)
            //    .ToList();

            //domainEntities.ToList()
            //    .ForEach(entity => entity.Entity.LimparEventos());

            //var tasks = domainEvents
            //    .Select(async (domainEvent) => {
            //        await mediator.PublicarEventos(domainEvent);
            //    });

            //await Task.WhenAll(tasks);
        }
    }
}
