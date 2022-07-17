using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SIGEPRO.Context;
using SIGEPRO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIGEPRO.Services
{
    public interface IProdutoService
    {
        Task<List<Produto>> RecuperaProdutos();
        Task<Produto> RecuperaProdutoPorId(int id);
        Task<bool> AlteraProduto(int id, Produto Produto);
        Task<Produto> CadastraProduto(Produto produto);
        Task<bool> InativaProduto(int id);
    }

    public class ProdutoService : IProdutoService
    {
        private readonly ILogger _logger;
        private readonly ApiContext _context;

        public ProdutoService(ApiContext context, ILogger<ApiContext> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<bool> AlteraProduto(int id, Produto Produto)
        {
            throw new NotImplementedException();
        }

        public Task<Produto> CadastraProduto(Produto produto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InativaProduto(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Produto> RecuperaProdutoPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Produto>> RecuperaProdutos()
        {
            throw new NotImplementedException();
        }

        private bool ProdutoExists(int id)
        {
            return (_context.Produto?.Any(e => e.CodigoProduto == id)).GetValueOrDefault();
        }
    }
}
