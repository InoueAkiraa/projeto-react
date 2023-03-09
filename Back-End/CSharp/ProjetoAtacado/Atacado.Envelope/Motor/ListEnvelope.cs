namespace Atacado.Envelope.Motor
{
    public class ListEnvelope<T>
        where T : class
    {
        private EnvelopeGenerico<T> etapa;

        public EnvelopeGenerico<T> Etapa 
        { 
            get { return etapa; } 
        }

        private ListEnvelope()
        {
            etapa = new EnvelopeGenerico<T>();
        }

        public ListEnvelope(List<T> items, int? codigo, string mensagem, string linkCreate, string versao) : this()
        {
            if (items == null)
                etapa.Dados = new List<T>();
            else
                etapa.Dados.AddRange(items);

            etapa.Status.Codigo = codigo;
            etapa.Status.Mensagem = mensagem;

            etapa.Paginacao.TotalReg = etapa.Dados.Count();

            etapa.LinkCreate = linkCreate;
            etapa.Versao = versao;
        }

        public ListEnvelope(List<T> items, int? codigo, string mensagem, string linkCreate, string versao, int? totalReg)
            : this(items, codigo, mensagem, linkCreate, versao)
        {
            this.etapa.Paginacao.TotalReg = totalReg.Value;
        }

        public ListEnvelope(List<T> items, int? codigo, string mensagem, string linkCreate, string versao,
            string urlServidor, int? salto, int? limite) : this()
        {
            if (items == null)
                etapa.Dados = new List<T>();
            else
                etapa.Dados.AddRange(items);

            etapa.Status.Codigo = codigo;
            etapa.Status.Mensagem = mensagem;

            int tamanhoPagina = limite.Value;
            if (salto.HasValue)
            {
                string urlAnterior = string.Empty;
                if (salto.Value != 0)
                {
                    int anterior = salto.Value - limite.Value;
                    if (anterior < 0)
                        anterior = 0;

                    urlAnterior = urlServidor + "?limite=" + tamanhoPagina.ToString() + "&salto=" + anterior.ToString();
                }

                int proximo = salto.Value + limite.Value;
                string urlProximo = urlServidor + "?limite=" + tamanhoPagina.ToString() + "&salto=" + proximo.ToString();

                etapa.Paginacao.PageNumber = proximo / tamanhoPagina;
                etapa.Paginacao.HasPrev = urlAnterior;
                etapa.Paginacao.HasNext = urlProximo;
            }

            etapa.LinkCreate = linkCreate;
            etapa.Versao = versao;
        }

        public ListEnvelope(List<T> items, int? codigo, string mensagem, string linkCreate, string versao,
            string urlServidor, int? salto, int? limite, int? totalReg)
            : this(items, codigo, mensagem, linkCreate, versao, urlServidor, salto, limite)
        {
            this.etapa.Paginacao.TotalReg = totalReg.Value;
            if (totalReg.Value % limite.Value != 0)
            {
                this.etapa.Paginacao.TotalPage = (totalReg.Value / limite.Value) + 1;
            }
            else
            {
                this.etapa.Paginacao.TotalPage = (totalReg.Value / limite.Value);
            }
        }
    }
}
