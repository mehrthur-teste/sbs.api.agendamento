using Newtonsoft.Json;
namespace sbs.api.agendamento.dominio.Models
{
    public class Agendamento
    {
        [JsonProperty(PropertyName = "id")]
        public string? id { get; set; } 

        [JsonProperty(PropertyName = "Documento")]
        public string? Documento { get; set; }

        [JsonProperty(PropertyName = "TipoDocumento")]
        public int? TipoDocumento { get; set; }

        [JsonProperty(PropertyName = "TipoAssunto")]
        public int? TipoAssunto { get; set; }

        [JsonProperty(PropertyName = "NomeAgendador")]
        public string? NomeAgendador { get; set; }

        [JsonProperty(PropertyName = "DataAgendamento")]
        public DateTime? DataAgendamento { get; set; }

        [JsonProperty(PropertyName = "EmailAgendador")]
        public string? EmailAgendador { get; set; }

        [JsonProperty(PropertyName = "TelefoneAgendador")]
        public string? TelefoneAgendador { get; set; }

        [JsonProperty(PropertyName = "EmailConsular")]
        public string? EmailConsular { get; set; }

        [JsonProperty(PropertyName = "TelefoneConsular")]
        public string? TelefoneConsular { get; set; }

        [JsonProperty(PropertyName = "LinkReuniao")]
        public string? LinkReuniao { get; set; }

        [JsonProperty(PropertyName = "Status")]
        public string? Status { get; set; }
    }
}
