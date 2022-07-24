using Moq;
using SIGEPRO.Services;
using SIGEPRO.Models;
using SIGEPRO.Context;
using Microsoft.EntityFrameworkCore;

namespace SIGEPRO.Tests
{
    public class FornecedorTestes
    {
        Mock<ApiContext> mockContext = new Mock<ApiContext>();
        Mock<DbSet<Fornecedor>> mockFornecedor = new Mock<DbSet<Fornecedor>>();

        Mock<IFornecedorService> service = new();


        [Theory]
        [InlineData("", "123456")]
        public async Task CadastraFornecedor_DadoFornecedorSemDescricao(string descricao, string cnpj)
        {
            //Arrange
            var fornecedor = new Fornecedor
            {
                DescricaoFornecedor = descricao,
                CnpjFornecedor = cnpj
            };           

            mockContext.Setup(x => x.Fornecedor).Returns(mockFornecedor.Object);

            mockFornecedor.Setup(x => x.Add(fornecedor)).Verifiable();

            mockContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                       .Returns(() => Task.Run(() => { return 1; })).Verifiable();

            IFornecedorService _service = new FornecedorService(mockContext.Object);          

            //Act
            var resultado = await _service.CadastraFornecedor(fornecedor);

            //Assert
            Assert.True(resultado != null);

        }

        [Theory]
        [InlineData(1, "teste", "123456")]
        public void RecuperaFornecedorPorId_DadoCodigoIncorreto(int codigo, string descricao, string cnpj)
        {
            //Arrange
            var fornecedor = new Fornecedor
            {
                CodigoFornecedor = codigo,
                DescricaoFornecedor = descricao,
                CnpjFornecedor = cnpj
            };


            service.Object.CadastraFornecedor(fornecedor);

            service.Setup(x => x.RecuperaFornecedorPorId(codigo))
                .Returns(Task.FromResult(fornecedor));

            //Act
            var resultado = service.Object.RecuperaFornecedorPorId(codigo);

            //Assert
            Assert.True(resultado != null);

        }

        //[TestClass]
        //public class LivroServiceTest
        //{
        //    ////noQuery test
        //    [TestMethod]
        //    public void CriarLivroSalvaOContext()
        //    {

        //        var mockSet = new Mock<DbSet<Livro>>();
        //        //mock dbcontext
        //        var mockContext = new Mock<LivroContext>();

        //        mockContext.Setup(m => m.Livros).Returns(mockSet.Object);

        //        var service = new LivroService(mockContext.Object);

        //        service.Salvar(new Livro() { Nome = "Livro de teste", Status = true });

        //        mockSet.Verify(m => m.Add(It.IsAny<Livro>()), Times.Once());

        //        mockContext.Verify(m => m.SaveChanges(), Times.Once());
        //    }
        //}


    }
}