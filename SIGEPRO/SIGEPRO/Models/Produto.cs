using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPRO.Models
{
    [Table("PRODUTO")]
    public class Produto
    {
        [Column("COD_PRODUTO"), Key]        
        public int CodigoProduto { get; set; }
        [Column("DESC_PRODUTO")]
        public string DescricaoProduto { get; set; }
        [Column("SIT_PRODUTO")]
        public string SituacaoProduto
        {
            get { return _situacao.ToString(); }
            set
            {
                value = value.ToUpper();

                if (!Enum.IsDefined(typeof(SITUACAO), value))
                    _situacao = SITUACAO.ATIVO;
                else
                    _situacao = (SITUACAO)Enum.Parse(typeof(SITUACAO), value);
            }
        }
        private SITUACAO _situacao { get; set; }

        [Column("DATA_FABRICACAO")]
        public DateTime DataFabricacao { get; set; }

        [Column("DATA_VALIDADE")]
        public DateTime DataValidade { get; set; }

        [ForeignKey("COD_FORNECEDOR")]
        public int CodigoFornecedor { get; set; }
       

    }

    public enum SITUACAO
    {
        ATIVO,
        INATIVO
    }

    public static partial class MapTool
    {
        public static ModelBuilder ProdutoMap(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(tabela =>
            {
                tabela.ToTable("PRODUTO");

                tabela.HasKey(p => new { p.CodigoProduto });

                tabela.Property(p => p.CodigoProduto).HasColumnName("COD_PRODUTO");
                tabela.Property(p => p.DescricaoProduto).HasColumnName("DESC_PRODUTO");
                tabela.Property(p => p.SituacaoProduto).HasColumnName("SIT_PRODUTO");
                tabela.Property(p => p.DataFabricacao).HasColumnName("DATA_FABRICACAO");
                tabela.Property(p => p.DataValidade).HasColumnName("DATA_VALIDADE");
                tabela.Property(p => p.CodigoFornecedor).HasColumnName("COD_FORNECEDOR");

            });

            return modelBuilder;
        }

    }


}
