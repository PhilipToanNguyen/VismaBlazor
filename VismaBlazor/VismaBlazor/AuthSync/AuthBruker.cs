namespace VismaBlazor.AuthSync
{
    //brukerinfo for å lagre i state
    public class AuthBruker
    {
        public string BrukerId { get; set; } = string.Empty;
        public string BrukerNavn { get; set; } = string.Empty;
        public string BrukerEpost { get; set; } = string.Empty;
        public string BrukerPassord { get; set; } = string.Empty;
    }
}
