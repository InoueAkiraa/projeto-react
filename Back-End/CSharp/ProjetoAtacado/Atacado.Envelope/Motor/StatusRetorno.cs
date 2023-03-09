using Newtonsoft.Json;

namespace Atacado.Envelope.Motor
{
    public class StatusRetorno
    {
        [JsonProperty(propertyName: "codigo")]
        public int? Codigo { get; set; }

        [JsonProperty(propertyName: "mensagem")]
        public string Mensagem { get; set; }
    }
}
