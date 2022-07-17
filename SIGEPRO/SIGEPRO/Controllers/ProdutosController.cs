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
    public class ProdutosController : ControllerBase
    {
        private readonly ApiContext _context;

        public ProdutosController(ApiContext context)
        {
            _context = context;
        }

        //GET: /Produto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> RecuperaProdutos()
        {
            return NoContent();
        }

        //GET: /Produto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> RecuperaProdutosPorId(int idProduto)
        {
            return NoContent();
        }

        //PUT: /Produto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> AlteraProduto(int id, [FromForm] Produto produto)
        {
            return NoContent();
        }

        // POST: /Produto        
        [HttpPost]
        public async Task<ActionResult<Produto>> CadastraProduto([FromForm] Produto produto)
        {
            return NoContent();
        }

        //PUT: /Produto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> InativaProduto(int id, [FromForm] Produto produto)
        {
            return NoContent();
        }
       
        private bool ProdutoExists(int id)
        {
          return (_context.Produto?.Any(e => e.CodigoProduto == id)).GetValueOrDefault();
        }
    }
}
