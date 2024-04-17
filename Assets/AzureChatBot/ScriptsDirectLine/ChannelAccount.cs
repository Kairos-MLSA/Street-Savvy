namespace DirectLine
{
    [System.Serializable]
    public class ChannelAccount
    {
        public string id;
        public string name;

        public ChannelAccount(string id, string name = null)
        {
            this.id = id;
            this.name = name;
        }
    }
}