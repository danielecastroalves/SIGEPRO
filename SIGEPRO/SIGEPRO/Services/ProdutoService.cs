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
        Task<Produto> CadastraProduto(Produto produto);
        Task<bool> AlteraProduto(Produto Produto);        
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

        public async Task<List<Produto>> RecuperaProdutos()
        {
            try
            {
                
                return await _context.Produto.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
           
        }

        public async Task<Produto> RecuperaProdutoPorId(int id)
        {
            try
            {
                if (ProdutoExiste(id))
                {
                   return await _context.Produto.FindAsync(id);                    
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Produto> CadastraProduto(Produto produto)
        {
            try
            {
                if (VerificaDatas(produto) && VerificaFornecedor(produto))
                {
                    _context.Produto.Add(produto);
                    await _context.SaveChangesAsync();
                    return produto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> AlteraProduto(Produto produto)
        {
            try
            {
                if (VerificaDatas(produto) && VerificaFornecedor(produto))
                {
                    _context.Entry(produto).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> InativaProduto(int id)
        {
            try
            {
                var produto = await _context.Produto.FindAsync(id);
                if (produto != null)
                {
                    produto.SituacaoProduto = SITUACAO.INATIVO.ToString();
                    _context.Entry(produto).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        private bool VerificaDatas(Produto produto)
        {
            //Verifica se a Data de Fabricação é menor do que a Data de Validade
            var r = produto.DataFabricacao.CompareTo(produto.DataValidade) < 0;
            return (r);
        }

        private bool VerificaFornecedor(Produto produto)
        {
            //Verifica se o Fornecedor existe
            return (_context.Fornecedor?.Any(e => e.CodigoFornecedor == produto.CodigoFornecedor)).GetValueOrDefault();
        }

        private bool ProdutoExiste(int id)
        {
            return (_context.Produto?.Any(e => e.CodigoProduto == id)).GetValueOrDefault();
        }
    }
}
