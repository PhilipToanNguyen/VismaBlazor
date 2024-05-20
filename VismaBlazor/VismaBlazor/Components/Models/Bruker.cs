

namespace VismaBlazor.Models
{
    //Data som hentes fra API
    public class BrukerRespons
    {
        public string NyId { get; set; } = string.Empty;
        public string Fornavn { get; set; } = string.Empty;
        public string Mellomnavn { get; set; } = string.Empty;
        public string Etternavn { get; set; } = string.Empty;
        public string Tlf { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Epost { get; set; } = string.Empty;
        public string Brukernavn { get; set; } = string.Empty;
        public string Passord { get; set; } = string.Empty;


    }
    //Error melding fra api
    public class ErrorMelding
    {
        public string Melding { get; set; } = string.Empty;
    }

}
