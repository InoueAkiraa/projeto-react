using Atacado.Poco;

namespace Atacado.Envelope.Modelo
{
    public class ProdutoEnvelope : BaseEnvelope
    {
        public int CodigoProduto { get; set; }
        public int CodigoCategoria { get; set; }
        public string Descricao { get; set; } = null!;
        public bool? Ativo { get; set; }
        public DateTime? DataInclusao { get; set; }

        public ProdutoEnvelope(ProdutoPoco poco)
        {
            CodigoProduto = poco.CodigoProduto;
            CodigoCategoria = poco.CodigoCategoria;
            Descricao = poco.Descricao;
            Ativo = poco.Ativo;
            DataInclusao = poco.DataInclusao;
        }

        public override void SetLinks()
        {
            Links.List = "GET /produto";
            Links.Self = "GET /produto/" + CodigoProduto.ToString();
            Links.Exclude = "DELETE /produto/" + CodigoProduto.ToString();
            Links.Update = "PUT /produto";
        }
    }
}
