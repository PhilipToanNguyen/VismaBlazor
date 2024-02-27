using System.Net;

namespace VismaBlazor.Models
{
    public static class IdData
    {
        public static readonly List<IdList> IdListe = new()
        {


        };

        public static void LeggTilId(string id)
        {
            IdListe.Add(new IdList() { Ids = id });
        }

        public static IdList[] HentIdListe()
        {
            return IdListe.ToArray();
        }

    }
}
