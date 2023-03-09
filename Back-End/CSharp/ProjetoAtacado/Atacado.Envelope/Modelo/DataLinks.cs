using Newtonsoft.Json;

namespace Atacado.Envelope.Modelo
{
    public class DataLinks
    {
        [JsonProperty(propertyName: "lista")]
        public string List { get; set; }

        [JsonProperty(propertyName: "self")]
        public string Self { get; set; }

        [JsonProperty(propertyName: "excluir")]
        public string Exclude { get; set; }

        [JsonProperty(propertyName: "atualizar")]
        public string Update { get; set; }

        public bool ShouldSerializeList()
        {
            return !string.IsNullOrEmpty(List);
        }

        public bool ShouldSerializeSelf()
        {
            return !string.IsNullOrEmpty(Self);
        }

        public bool ShouldSerializeExclude()
        {
            return !string.IsNullOrEmpty(Exclude);
        }

        public bool ShouldSerializeUpdate()
        {
            return !string.IsNullOrEmpty(Update);
        }
    }
}
