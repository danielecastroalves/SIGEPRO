using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIGEPRO.Context;
using SIGEPRO.Models;
using SIGEPRO.Services;

namespace SIGEPRO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FornecedoresController : ControllerBase
    {
        private readonly IFornecedorService _fornecedor;

        public FornecedoresController(IFornecedorService fornecedor)
        {
            _fornecedor = fornecedor;
        }

        /// <summary>
        /// Retorna todos os fornecedores cadastrados
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Dados encontrados.</response>
        /// <response code="404">Dados não encontrados.</response>
        /// <response code="500">Serviço indisponível.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> RecuperaFornecedores()
        {
            var result = await _fornecedor.RecuperaFornecedores();  
            
            if(result == null || !result.Any())
                          return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Retorna o Fornecedor correspondente ao ID fornecido
        /// </summary>
        /// <param name="idFornecedor"></param>
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
        public async Task<ActionResult<Fornecedor>> RecuperaFornecedorPorId(int idFornecedor)
        {
            var result = await _fornecedor.RecuperaFornecedorPorId(idFornecedor);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Cadastra um novo Fornecedor
        /// </summary>        
        /// <param name="fornecedor"></param>
        /// <returns></returns>
        /// <response code="200">Dados cadastrados.</response>
        /// <response code="400">Dados de entrada incorretos.</response>        
        /// <response code="500">Serviço indisponível.</response>
        [HttpPost("CadastraFornecedor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Fornecedor>> CadastraFornecedor([FromForm] Fornecedor fornecedor)
        {            
            var result = await _fornecedor.CadastraFornecedor(fornecedor);

            if (!result)
                return BadRequest();

            return CreatedAtAction("RecuperaFornecedorPorId", new { id = fornecedor.CodigoFornecedor}, fornecedor);
        }

        /// <summary>
        /// Atualiza as informações do Fornecedor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fornecedor"></param>
        /// <returns></returns>
        /// <response code="200">Dados Atualizados.</response>
        /// <response code="400">Dados de entrada incorretos.</response>
        /// <response code="404">Dados não encontrados.</response>
        /// <response code="500">Serviço indisponível.</response>
        [HttpPut("AlteraFornecedor/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AlteraFornecedor(int id, [FromForm] Fornecedor fornecedor)
        {
            var result = await _fornecedor.AlteraFornecedor(id, fornecedor);

            if (!result)
                return BadRequest();

            return NoContent();
        }
               

        /// <summary>
        /// Deleta o Fornecedor da base de dados
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fornecedor"></param>
        /// <returns></returns>
        /// <response code="200">Dados excluídos.</response>
        /// <response code="400">Dados de entrada incorretos.</response>
        /// <response code="404">Dados não encontrados.</response>
        /// <response code="500">Serviço indisponível.</response>       
        [HttpDelete("DeletaFornecedor/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletaFornecedor(int id, [FromForm] Fornecedor fornecedor)
        {
            var result = await _fornecedor.DeletaFornecedor(id);

            if (!result)
                return BadRequest();

            return NoContent();
        }
    }
}
