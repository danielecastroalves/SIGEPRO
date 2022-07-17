using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIGEPRO.Models;
using SIGEPRO.Services;

namespace SIGEPRO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produto;

        public ProdutosController(IProdutoService produto)
        {
            _produto = produto;
        }

        /// <summary>
        /// Retorna todos os produtos cadastrados
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Dados encontrados.</response>
        /// <response code="404">Dados não encontrados.</response>
        /// <response code="500">Serviço indisponível.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Produto>>> RecuperaProdutos()
        {
            var result = await _produto.RecuperaProdutos();

            if (result == null || !result.Any())
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Retorna o produto correspondente ao ID fornecido
        /// </summary>
        /// <param name="idProduto"></param>
        /// <returns></returns>
        /// <response code="200">Dados encontrados.</response>
        /// <response code="400">Dados de entrada incorretos.</response>
        /// <response code="404">Dados não encontrados.</response>
        /// <response code="500">Serviço indisponível.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Produto>> RecuperaProdutoPorId(int idProduto)
        {
            var result = await _produto.RecuperaProdutoPorId(idProduto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Cadastra um novo Produto
        /// </summary>        
        /// <param name="produto"></param>
        /// <returns></returns>
        /// <response code="200">Dados cadastrados.</response>
        /// <response code="400">Dados de entrada incorretos.</response>        
        /// <response code="500">Serviço indisponível.</response>
        [HttpPost("CadastraProduto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Produto>> CadastraProduto([FromForm] Produto produto)
        {
            var result = await _produto.CadastraProduto(produto);

            if (result == null)
                return NotFound();

            return CreatedAtAction("RecuperaProdutoPorId", new { id = produto.CodigoProduto }, produto);
        }

        /// <summary>
        /// Atualiza as informações do Produto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="produto"></param>
        /// <returns></returns>
        /// <response code="200">Dados Atualizados.</response>
        /// <response code="400">Dados de entrada incorretos.</response>
        /// <response code="404">Dados não encontrados.</response>
        /// <response code="500">Serviço indisponível.</response>
        [HttpPut("AlteraProduto/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AlteraProduto(int id, [FromForm] Produto produto)
        {
            var result = await _produto.AlteraProduto(id, produto);

            if (!result)
                return BadRequest();

            return NoContent();
        }

        /// <summary>
        /// Inativa o Produto
        /// </summary>
        /// <param name="id"></param>        
        /// <returns></returns>
        /// <response code="200">Dados Atualizados.</response>
        /// <response code="400">Dados de entrada incorretos.</response>
        /// <response code="404">Dados não encontrados.</response>
        /// <response code="500">Serviço indisponível.</response>
        [HttpPut("InativaProduto/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InativaProduto(int id)
        {
            var result = await _produto.InativaProduto(id);

            if (!result)
                return BadRequest();

            return NoContent();
        }

    }
}
