namespace VismaBlazor.AuthSync
{
    public class LoginStatus
    {
        public List<string> loggetbruker = new List<string>();

        //for å legge til status for logget inn
        public void Addid(string id)
        {
            if (!loggetbruker.Contains(id)) loggetbruker.Add(id);
        }

        public void Fjernid(string id)
        {
            if (loggetbruker.Contains(id)) loggetbruker.Remove(id);
        }

        public bool brukerloggetinn(string id)
        {
            return loggetbruker.Contains(id);
        }


    }
}
