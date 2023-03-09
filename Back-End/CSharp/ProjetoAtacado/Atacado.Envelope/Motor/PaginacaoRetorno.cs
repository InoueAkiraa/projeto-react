using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;

namespace Atacado.Envelope.Motor
{
    public class PaginacaoRetorno
    {
        [JsonProperty(propertyName: "has_prev")]
        public string HasPrev { get; set; }

        [JsonProperty(propertyName: "has_next")]
        public string HasNext { get; set; }

        [JsonProperty(propertyName: "page_count")]
        public int? PageCount { get; set; }

        [JsonProperty(propertyName: "page_number")]
        public int? PageNumber { get; set; }

        [JsonProperty(propertyName: "total_reg")]
        public int? TotalReg { get; set; }

        [JsonProperty(propertyName: "total_page")]
        public int? TotalPage { get; set; }

        public bool ShouldSerializeHasPrev()
        {
            return !string.IsNullOrEmpty(HasPrev);
        }

        public bool ShouldSerializeHasNext()
        {
            return !string.IsNullOrEmpty(HasNext);
        }

        public bool ShouldSerializePageCount()
        {
            return PageCount.HasValue;
        }

        public bool ShouldSerializePageNumber()
        {
            return PageNumber.HasValue;
        }

        public bool ShouldSerializeTotalReg()
        {
            if (TotalReg.HasValue)
            {
                return TotalReg.Value != 0 ? true : false;
            }
            else
            {
                return false;
            }
        }

        public bool ShouldSerializeTotalPage()
        {
            if (TotalPage.HasValue)
            {
                return TotalPage.Value != 0 ? true : false;
            }
            else
            {
                return false;
            }
        }
    }
}
