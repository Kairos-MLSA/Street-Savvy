namespace DirectLine
{
    [System.Serializable]
    public class Activity
    {
        public string type;
        public string id;
        public string timestamp;
        public string localTimestamp;
        public string channelId;

        public ChannelAccount from;
        public Attachment[] attachments;

        public string text;
        public string summary;
    }

}