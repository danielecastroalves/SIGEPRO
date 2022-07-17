using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPRO.Models
{
    [Table("PRODUTO")]
    public class Produto
    {
        [Key, Column("COD_PRODUTO")]
        public int CodigoProduto { get; set; }
        [Column("DESC_PRODUTO")]
        public string DescricaoProduto { get; set; }
        [Column("SIT_PRODUTO")]
        public string SituacaoProduto { get; set; }
        [Column("DATA_FABRICACAO")]
        public DateTime DataFabricacao { get; set; }
        [Column("DATA_VALIDADE")]
        public DateTime DataValidade { get; set; }

        public Fornecedor Fornecedor { get; set; }
       

    }



}
