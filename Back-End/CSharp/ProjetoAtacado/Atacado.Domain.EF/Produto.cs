using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atacado.Domain.EF
{
    [Table("Produto", Schema = "dbo")]
    public partial class Produto
    {
        [Key]
        [Column(name: "IdProduto")]
        public int CodigoProduto { get; set; }

        [Column(name: "IdCategoria")]
        public int CodigoCategoria { get; set; }

        [Column(name: "Descricao")]
        [Unicode(false)]
        public string Descricao { get; set; } = null!;

        [Column(name: "Ativo")]
        public bool? Ativo { get; set; }

        [Column(name: "DataInclusao", TypeName = "datetime")]
        public DateTime? DataInclusao { get; set; }
    }
}
