using Microsoft.EntityFrameworkCore;
using SIGEPRO.Context;
using SIGEPRO.Models;
using System.Collections.Generic;

namespace SIGEPRO.Services
{
    public interface IFornecedorService
    {
        Task<List<Fornecedor>> RecuperaFornecedores();
        Task<Fornecedor> RecuperaFornecedorPorId(int id);
        Task<Fornecedor> CadastraFornecedor(Fornecedor Fornecedor);
        Task<bool> AlteraFornecedor(Fornecedor Fornecedor);
        Task<bool> DeletaFornecedor(int id);
    }

    public class FornecedorService : IFornecedorService
    {
        private readonly ILogger _logger;
        private readonly ApiContext _context;

        public FornecedorService(ApiContext context, ILogger<ApiContext> logger)
        {
            _context = context;
            _logger = logger;
        }

        public FornecedorService(ApiContext context)
        {
            _context = context;
        }

        public async Task<List<Fornecedor>> RecuperaFornecedores()
        {
            try
            {
                return await _context.Fornecedor.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Fornecedor> RecuperaFornecedorPorId(int id)
        {
            try
            {
                if (FornecedorExiste(id))
                    return await _context.Fornecedor.FindAsync(id);
                else
                    return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }            
        }

        public async Task<bool> AlteraFornecedor(Fornecedor fornecedor)
        {
            try
            {
                _context.Entry(fornecedor).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Fornecedor> CadastraFornecedor(Fornecedor fornecedor)
        {
            try
            {                
                _context.Fornecedor.Add(fornecedor);
                await _context.SaveChangesAsync();

                return fornecedor;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> DeletaFornecedor(int id)
        {
            try
            {
                if (!FornecedorEstaSendoUsado(id))
                {
                    var fornecedor = await _context.Fornecedor.FindAsync(id);

                    if (fornecedor == null)
                    {
                        return false;
                    }

                    _context.Fornecedor.Remove(fornecedor);
                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                    return false;
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        private bool FornecedorEstaSendoUsado(int id)
        {
            return (_context.Produto?.Any(e => e.CodigoFornecedor == id)).GetValueOrDefault();
        }

        private bool FornecedorExiste(int id)
        {
            return (_context.Fornecedor?.Any(e => e.CodigoFornecedor == id)).GetValueOrDefault();
        }
    }
}
