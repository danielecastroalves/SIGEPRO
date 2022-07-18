using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPRO.Models
{
    [Table("FORNECEDOR")]
    public class Fornecedor
    {
        [Column("COD_FORNECEDOR"), Key]        
        public int CodigoFornecedor { get; set; }
        [Column("DESC_FORNECEDOR")]
        public string DescricaoFornecedor { get; set; }
        [Column("CNPJ_FORNECEDOR")]
        public string CnpjFornecedor { get; set; }
    }

    public static partial class MapTool
    {
        public static ModelBuilder FornecedorMap(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fornecedor>(tabela =>
            {
                tabela.ToTable("FORNECEDOR");

                tabela.HasKey(p => new { p.CodigoFornecedor });

                tabela.Property(p => p.CodigoFornecedor).HasColumnName("COD_FORNECEDOR");
                tabela.Property(p => p.DescricaoFornecedor).HasColumnName("DESC_FORNECEDOR");
                tabela.Property(p => p.CnpjFornecedor).HasColumnName("CNPJ_FORNECEDOR");

            });

            return modelBuilder;
        }
    } 

}
