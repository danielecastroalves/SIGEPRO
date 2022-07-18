namespace SIGEPRO.Models
{
    public class Resultado
    {
        public string Codigo { get; set; }

        public string Mensagem { get; set; }
    }

    public class ResultadoListaFornecedores : Resultado
    {
        public List<Fornecedor> Fornecedor { get; set; }
    }
    public class ResultadoFornecedor : Resultado
    {
        public Fornecedor Fornecedor { get; set; }
    }

    public class ResultadoListaProdutos : Resultado
    {
        public List<Produto> Produto { get; set; }        
    }
    public class ResultadoProduto : Resultado
    {
        public Produto Produto { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}
