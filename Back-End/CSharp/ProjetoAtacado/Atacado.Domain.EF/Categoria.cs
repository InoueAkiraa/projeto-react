using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Atacado.Domain.EF
{
    [Table("Categoria", Schema = "dbo")]
    public partial class Categoria
    {
        [Key]
        [Column(name: "IdCategoria")]
        public int CodigoCategoria { get; set; }

        [Column(name: "Descricao")]
        [Unicode(false)]
        public string Descricao { get; set; } = null!;

        [Column(name: "DataInsert", TypeName = "datetime")]
        public DateTime? DataInclusao { get; set; }

        [Column(name: "Ativo")]
        public bool? Ativo { get; set; }
    }
}
