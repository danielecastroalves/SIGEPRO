using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
           
            if (result == null || !result.Any())
            {
                return StatusCode(StatusCodes.Status404NotFound, new Resultado()
                {
                    Codigo = "ERRO",
                    Mensagem = "Nenhum Fornecedor foi encontrado."
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResultadoListaFornecedores()
            {
                Codigo = "OK",
                Mensagem = $"Solicitação realizada com sucesso, segue lista de Fornecedores",
                Fornecedor = result
            });

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
        [HttpGet("RecuperaFornecedorPorId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Fornecedor>> RecuperaFornecedorPorId(int idFornecedor)
        {
            var result = await _fornecedor.RecuperaFornecedorPorId(idFornecedor);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Resultado()
                {
                    Codigo = "ERRO",
                    Mensagem = "O Fornecedor não foi encontrado."
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResultadoFornecedor()
            {
                Codigo = "OK",
                Mensagem = $"Solicitação realizada com sucesso",
                Fornecedor = result
            });
        }

        /// <summary>
        /// Cadastra um novo Fornecedor
        /// </summary>        
        /// <param name="descricao"></param>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        /// <response code="200">Dados cadastrados.</response>
        /// <response code="400">Dados de entrada incorretos.</response>        
        /// <response code="500">Serviço indisponível.</response>
        [HttpPost("CadastraFornecedor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Fornecedor>> CadastraFornecedor(
            [Required] string descricao,
            [Required] string cnpj)
        {
            var fornecedor = new Fornecedor
            {
                DescricaoFornecedor = descricao,
                CnpjFornecedor = cnpj
            };

            var result = await _fornecedor.CadastraFornecedor(fornecedor);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Resultado()
                {
                    Codigo = "ERRO",
                    Mensagem = "Não foi possível cadastrar o fornecedor, verifique os dados digitados."
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResultadoFornecedor()
            {
                Codigo = "OK",
                Mensagem = $"Fornecedor de código {fornecedor.CodigoFornecedor} foi cadastrado com sucesso",
                Fornecedor = result
            });
            
        }

        /// <summary>
        /// Atualiza as informações do Fornecedor
        /// </summary>       
        /// <param name="fornecedor"></param>
        /// <returns></returns>
        /// <response code="200">Dados Atualizados.</response>
        /// <response code="400">Dados de entrada incorretos.</response>
        /// <response code="404">Dados não encontrados.</response>
        /// <response code="500">Serviço indisponível.</response>
        [HttpPut("AlteraFornecedor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AlteraFornecedor([FromForm] Fornecedor fornecedor)
        {
            var result = await _fornecedor.AlteraFornecedor(fornecedor);

            if (!result)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Resultado()
                {
                    Codigo = "ERRO",
                    Mensagem = "Não foi possível atualizar o fornecedor, verifique os dados digitados."
                });
            }                

            return StatusCode(StatusCodes.Status200OK, new Resultado()
            {
                Codigo = "OK",
                Mensagem = $"Fornecedor de código {fornecedor.CodigoFornecedor} foi alterado com sucesso"
            });
        }
               

        /// <summary>
        /// Deleta o Fornecedor da base de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Dados excluídos.</response>
        /// <response code="400">Dados de entrada incorretos.</response>
        /// <response code="500">Serviço indisponível.</response>       
        [HttpDelete("DeletaFornecedor/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletaFornecedor(int id)
        {
            var result = await _fornecedor.DeletaFornecedor(id);

            if (!result)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Resultado()
                {
                    Codigo = "ERRO",
                    Mensagem = "Não foi possível deletar o fornecedor, verifique os dados digitados."
                });
            }

            return StatusCode(StatusCodes.Status200OK, new Resultado()
            {
                Codigo = "OK",
                Mensagem = $"Fornecedor de código {id} foi deletado com sucesso"
            });
        }
    }
}
