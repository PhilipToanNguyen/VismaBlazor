
using VismaBlazor.Models;

namespace VismaBlazor;

    public static class BrukerController
    {
        public static List<BrukerRespons> brukere = new List<BrukerRespons>();

    public static void LeggTilBruker(BrukerRespons bruker)
    {
        brukere.Add(bruker);
    }
    public static BrukerRespons[] HentBrukere()
    {
        return brukere.ToArray();
    }
}

