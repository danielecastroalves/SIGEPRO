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
    public interface IFornecedorService
    {
        Task<List<Fornecedor>> RecuperaFornecedores();
        Task<Fornecedor> RecuperaFornecedorPorId(int id);
        Task<bool> CadastraFornecedor(Fornecedor Fornecedor);
        Task<bool> AlteraFornecedor(int id, Fornecedor Fornecedor);
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
        public async Task<List<Fornecedor>> RecuperaFornecedores()
        {
            return await _context.Fornecedor.ToListAsync();
        }

        public async Task<Fornecedor> RecuperaFornecedorPorId(int id)
        {
            if (FornecedorExists(id))
                return await _context.Fornecedor.FindAsync(id);
            else
                return null;
        }

        public async Task<bool> AlteraFornecedor(int id, Fornecedor fornecedor)
        {
            try
            {
                _context.Entry(fornecedor).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FornecedorExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

        }

        public async Task<bool> CadastraFornecedor(Fornecedor fornecedor)
        {         
            try
            {
                _context.Fornecedor.Add(fornecedor);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<bool> DeletaFornecedor(int id)
        {
            try
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
            catch (Exception)
            {
                throw;
            }            
        }



        private bool FornecedorExists(int id)
        {
            return (_context.Fornecedor?.Any(e => e.CodigoFornecedor == id)).GetValueOrDefault();
        }
    }
}
