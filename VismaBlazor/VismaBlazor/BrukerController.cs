
using VismaBlazor.Models;

namespace VismaBlazor;

    public static class BrukerController
    {
        public static List<BrukerResponse> brukere = new List<BrukerResponse>();

    public static void LeggTilBruker(BrukerResponse bruker)
    {
        brukere.Add(bruker);
    }
    public static BrukerResponse[] HentBrukere()
    {
        return brukere.ToArray();
    }
}

