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
    }
}
