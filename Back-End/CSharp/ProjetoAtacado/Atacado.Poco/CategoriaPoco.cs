using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atacado.Poco
{
    public class CategoriaPoco
    {
        public int CodigoCategoria { get; set; }
        public string Descricao { get; set; } = null!;
        public DateTime? DataInclusao { get; set; }
        public bool? Ativo { get; set; }
    }
}
