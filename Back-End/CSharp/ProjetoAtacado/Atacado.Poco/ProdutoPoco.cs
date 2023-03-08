using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atacado.Poco
{
    public class ProdutoPoco
    {
        public int CodigoProduto { get; set; }
        public int CodigoCategoria { get; set; }
        public string Descricao { get; set; } = null!;
        public bool? Ativo { get; set; }
        public DateTime? DataInclusao { get; set; }
    }
}
