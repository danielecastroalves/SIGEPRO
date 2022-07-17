using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPRO.Models
{
    [Table("FORNECEDOR")]
    public class Fornecedor
    {
        [Key, Column("COD_FORNECEDOR")]
        public int CodigoFornecedor { get; set; }

        [Column("DESC_FORNECEDOR")]
        public string DescFornecedor { get; set; }
        [Column("CNPJ_FORNECEDOR")]
        public string CnpjFornecedor { get; set; }        
    }
}
