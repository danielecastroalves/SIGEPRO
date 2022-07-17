using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIGEPRO.Context;
using SIGEPRO.Models;

namespace SIGEPRO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FornecedoresController : ControllerBase
    {
        private readonly ApiContext _context;

        public FornecedoresController(ApiContext context)
        {
            _context = context;
        }

        //GET: /Fornecedor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> RecuperaFornecedores()
        {
            return NoContent();
        }

        //GET: /Fornecedor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fornecedor>> RecuperaFornecedoresPorId(int idFornecedor)
        {
            return NoContent();
        }

        //PUT: /Fornecedor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> AlteraFornecedor(int id, [FromForm] Fornecedor fornecedor)
        {
            return NoContent();
        }

        // POST: /Fornecedor        
        [HttpPost]
        public async Task<ActionResult<Fornecedor>> CadastraFornecedor([FromForm] Fornecedor fornecedor)
        {
            return NoContent();
        }

        //PUT: /Fornecedor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaFornecedor(int id, [FromForm] Fornecedor fornecedor)
        {
            return NoContent();
        }
    }
}
