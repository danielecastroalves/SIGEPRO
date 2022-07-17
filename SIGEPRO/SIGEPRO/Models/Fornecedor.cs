using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPRO.Models
{
    [Table("FORNECEDOR")]
    public class Fornecedor
    {        
        [Column("COD_FORNECEDOR"), Key, Required]
        public int CodigoFornecedor { get; set; }
        [Column("DESC_FORNECEDOR"), Required]
        public string DescFornecedor { get; set; }
        [Column("CNPJ_FORNECEDOR")]
        public string CnpjFornecedor { get; set; }        
    }
}
