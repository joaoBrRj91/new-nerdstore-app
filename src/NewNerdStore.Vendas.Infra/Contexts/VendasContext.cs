using Microsoft.EntityFrameworkCore;
using NewNerdStore.Core.Comunications.Mediator.Interfaces;
using NewNerdStore.Core.Data;
using NewNerdStore.Core.Messages.Abstracts;
using NewNerdStore.Vendas.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Infra
{
    public class VendasContext : DbContext, IUnitOfWork
    {
        //private readonly IDomainMediatorHandler _domainMediatorHandler;

        public VendasContext(DbContextOptions<VendasContext> options/*, IDomainMediatorHandler domainMediatorHandler*/)
            : base(options)
        {
            //_domainMediatorHandler = domainMediatorHandler;
        }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItems { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }


        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }
            
            var sucesso = await base.SaveChangesAsync() > 0;
            //if (sucesso) _domainMediatorHandler.PublishEvents(this);

            return sucesso;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            //modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VendasContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.HasSequence<int>("MinhaSequencia").StartsAt(1000).IncrementsBy(1);
            base.OnModelCreating(modelBuilder);
        }
    }
}
