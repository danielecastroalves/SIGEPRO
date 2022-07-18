using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
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
        private readonly IFornecedorService _fornecedor;

        public ProdutosController(IProdutoService produto, IFornecedorService fornecedor)
        {
            _produto = produto;
            _fornecedor = fornecedor;
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
            {
                return StatusCode(StatusCodes.Status404NotFound, new Resultado()
                {
                    Codigo = "ERRO",
                    Mensagem = "Nenhum Produto foi encontrado."
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResultadoListaProdutos()
            {
                Codigo = "OK",
                Mensagem = $"Solicitação realizada com sucesso, segue lista de Produtos",
                Produto = result
            });
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
        [HttpGet("RecuperaProdutoPorId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Produto>> RecuperaProdutoPorId([Required] int idProduto)
        {
            var result = await _produto.RecuperaProdutoPorId(idProduto);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Resultado()
                {
                    Codigo = "ERRO",
                    Mensagem = $"Não foi encontrado o produto de código {idProduto}. Verifique os dados digitados."
                });
            }
            var fornecedor = await _fornecedor.RecuperaFornecedorPorId(result.CodigoFornecedor);

            return StatusCode(StatusCodes.Status200OK, new ResultadoProduto()
            {
                Codigo = "OK",
                Mensagem = $"Solicitação realizada com sucesso",
                Produto = result,
                Fornecedor = fornecedor
            });
        }

        /// <summary>
        /// Cadastra um novo Produto
        /// </summary>        
        /// <param name="Descricao"></param>
        /// <param name="Situacao"></param>
        /// <param name="DataFabricacao"></param>
        /// <param name="DataValidade"></param>
        /// <param name="CodFornecedor"></param>
        /// <returns>Retorna mensagem com Status Code</returns>
        /// <remarks>
        /// Situacao = ATIVO ou INATIVO
        /// </remarks>
        /// <response code="200">Dados cadastrados.</response>
        /// <response code="400">Dados de entrada incorretos.</response>        
        /// <response code="500">Serviço indisponível.</response>
        [HttpPost("CadastraProduto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Produto>> CadastraProduto(
           [Required] string Descricao,
           string Situacao,
           string DataFabricacao,
           string DataValidade,
           int CodFornecedor
            )
        {
            var produto = new Produto
            {
                DescricaoProduto = Descricao,
                SituacaoProduto = Situacao,
                DataFabricacao = DateTime.Parse(DataFabricacao, new CultureInfo("pt-BR")),
                DataValidade = DateTime.Parse(DataValidade, new CultureInfo("pt-BR")),
                CodigoFornecedor = CodFornecedor
            };

            var result = await _produto.CadastraProduto(produto);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Resultado()
                {
                    Codigo = "ERRO",
                    Mensagem = "Não foi possível cadastrar o Produto, verifique os dados digitados."
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResultadoProduto()
            {
                Codigo = "OK",
                Mensagem = $"Solicitação realizada com sucesso, segue Produto cadastrado",
                Produto = result
            });
        }

        /// <summary>
        /// Atualiza as informações do Produto
        /// </summary>     
        /// <param name="CodProduto"></param>
        /// <param name="Descricao"></param>
        /// <param name="Situacao"></param>
        /// <param name="DataFabricacao"></param>
        /// <param name="DataValidade"></param>
        /// <param name="CodFornecedor"></param>
        /// <returns></returns>
        /// <response code="200">Dados Atualizados.</response>
        /// <response code="400">Dados de entrada incorretos.</response>
        /// <response code="404">Dados não encontrados.</response>
        /// <response code="500">Serviço indisponível.</response>
        [HttpPut("AlteraProduto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AlteraProduto(
           [Required] int CodProduto,
           [Required] string Descricao,
           string Situacao,
           string DataFabricacao,
           string DataValidade,
           int CodFornecedor)
        {
            var produto = new Produto
            {
                CodigoProduto = CodProduto,
                DescricaoProduto = Descricao,
                SituacaoProduto = Situacao,
                DataFabricacao = DateTime.Parse(DataFabricacao, new CultureInfo("pt-BR")),
                DataValidade = DateTime.Parse(DataValidade, new CultureInfo("pt-BR")),
                CodigoFornecedor = CodFornecedor
            };
            var result = await _produto.AlteraProduto(produto);

            if (!result)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Resultado()
                {
                    Codigo = "ERRO",
                    Mensagem = "Não foi possível atualizar o produto, verifique os dados digitados."
                });
            }

            return StatusCode(StatusCodes.Status200OK, new Resultado()
            {
                Codigo = "OK",
                Mensagem = $"Produto de código {produto.CodigoProduto} foi alterado com sucesso"
            });
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
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Resultado()
                {
                    Codigo = "ERRO",
                    Mensagem = "Não foi possível inativar o produto, verifique os dados digitados."
                });
            }

            return StatusCode(StatusCodes.Status200OK, new Resultado()
            {
                Codigo = "OK",
                Mensagem = $"Produto de código {id} foi inativado com sucesso"
            });
        }

    }
}
