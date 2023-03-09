using Atacado.Poco;

namespace Atacado.Envelope.Modelo
{
    public class CategoriaEnvelope : BaseEnvelope
    {
        public int CodigoCategoria { get; set; }
        public string Descricao { get; set; } = null!;
        public DateTime? DataInclusao { get; set; }
        public bool? Ativo { get; set; }

        public CategoriaEnvelope(CategoriaPoco poco)
        {
            CodigoCategoria = poco.CodigoCategoria;
            Descricao = poco.Descricao;
            DataInclusao = poco.DataInclusao;
            Ativo = poco.Ativo;
        }

        public override void SetLinks()
        {
            Links.List = "GET /categoria";
            Links.Self = "GET /categoria/" + CodigoCategoria.ToString();
            Links.Exclude = "DELETE /categoria/" + CodigoCategoria.ToString();
            Links.Update = "PUT /categoria";
        }
    }
}
