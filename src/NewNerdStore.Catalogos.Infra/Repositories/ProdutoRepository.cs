using Microsoft.EntityFrameworkCore;
using NewNerdStore.Catalogos.Domain.Entities;
using NewNerdStore.Catalogos.Domain.Interfaces.Repositories;
using NewNerdStore.Catalogos.Infra.Contexts;
using NewNerdStore.Core.Data;

namespace NewNerdStore.Catalogos.Infra.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalogoContext _context;

        #region Constructor
        public ProdutoRepository(CatalogoContext context) => _context = context;
        #endregion

        #region UOW
        public IUnitOfWork UnitOfWork => _context;
        #endregion

        #region Queries
        public async Task<Produto> ObterPorId(Guid id)
            => await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Produto>> ObterTodos()
            => await _context.Produtos.AsNoTracking().ToListAsync();

        public async Task<IEnumerable<Categoria>> ObterCategorias()
            => await _context.Categorias.AsNoTracking().ToListAsync();

        public async Task<IEnumerable<Produto>> ObterPorCategoria(int codigo)
            => await _context.Produtos.AsNoTracking().Include(p => p.Categoria)
            .Where(c => c.Categoria.Codigo == codigo).ToListAsync();
        #endregion

        #region Commands
        public void Adicionar(Produto produto) => _context.Produtos.Add(produto);

        public void Adicionar(Categoria categoria) => _context.Categorias.Add(categoria);

        public void Atualizar(Produto produto) => _context.Produtos.Update(produto);

        public void Atualizar(Categoria categoria) => _context.Categorias.Update(categoria);
        #endregion

        public void Dispose() => _context.Dispose();
    }
}
