using VismaBlazor.Client.Models;

namespace VismaBlazor.Client;

    public static class BrukerController
    {
        private static readonly List<Bruker> brukere = new()
    {
        new Bruker()
        {
            Fornavn = "Ola",
            Etternavn = "Nordmann",
            Epost = "OlaNordmann@hotmail.no",
            Telefon = 12345678,
            Adresse = "Nordmannveien 1",
            NøkkelID = "16A4N84753",

        },
        new Bruker()
        {
            Fornavn = "Kari",
            Etternavn = "Nordmann",
            Epost = "KonatilOla@hotmail.no",
            Telefon = 87654321,
            Adresse = "Nordmannveien 2",
            NøkkelID = "16A4N84754",
        }
    };

    public static Bruker[] HentBrukere()
    {
        return brukere.ToArray();
    }
}

