using System;
using System.Collections;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

namespace DirectLine
{
    public class DirectLineConnection : MonoBehaviour
    {

        [DllImport("__Internal")]
        private static extern void ConnectToWebSocket(string str);

        public static DirectLineConnection instance;

        [SerializeField] private string directLineSecret = "Your secret here";
        [SerializeField] private string fromUser = "Dev";

        public delegate void ReceivedMessage(string message);
        public event ReceivedMessage OnReceivedMessage;

        private Conversation currentConversation;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            StartCoroutine(Connect());
        }


        public void SendMessage(string text, string type = "message")
        {
            StartCoroutine(PostActivity(text, type));
        }

        // This method gets called when an activity is received through the web socket (also WebGL → see .jslib)
        public void OnMessageReceived(string data)
        {
            ActivitySet activitySet = JsonUtility.FromJson<ActivitySet>(data);

            if (activitySet == null)
                return;

            for (int i = 0; i < activitySet.activities.Length; i++)
            {
                if (activitySet.activities[i].from.id == fromUser)
                {
                    // Your posted activity
                }
                else
                {
                    // Bots response activity
                    Debug.Log("Bot response: " + activitySet.activities[i].text);
                    if (OnReceivedMessage != null)
                        OnReceivedMessage.Invoke(activitySet.activities[i].text);
                }
            }
        }

        private IEnumerator Connect()
        {
            UnityWebRequest webRequest = new UnityWebRequest("https://directline.botframework.com/v3/directline/conversations");
            webRequest.SetRequestHeader("Authorization", "Bearer " + directLineSecret);
            webRequest.method = "POST";

            webRequest.downloadHandler = new DownloadHandlerBuffer();

            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError("Can't start conversation: " + webRequest.error);
            }
            else
            {
                string responseAsString = webRequest.downloadHandler.text;
                currentConversation = JsonUtility.FromJson<Conversation>(responseAsString);

                Debug.Log("Conversation started: " + currentConversation.conversationId);

#if UNITY_WEBGL && !UNITY_EDITOR
            ConnectToWebSocket(currentConversation.streamUrl);
#else
            SetupWebSocket(currentConversation.streamUrl);
#endif
            }
        }

        // Setup websocket (if not webgl)
        private async void SetupWebSocket(string stream)
        {
            Uri uri = new Uri(stream);
            ClientWebSocket webSocket = new ClientWebSocket();

            await webSocket.ConnectAsync(uri, CancellationToken.None);

            Debug.Log("WebSocket state: " + webSocket.State);

            ArraySegment<byte> buffer = WebSocket.CreateClientBuffer(1024, 1024);
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);


            while (!webSocket.CloseStatus.HasValue)
            {
                string message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);

                if (result != null && result.EndOfMessage)
                {
                    OnMessageReceived(message);
                }

                result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
            }

            await webSocket.CloseAsync(webSocket.CloseStatus.Value, webSocket.CloseStatusDescription, CancellationToken.None);
        }

        private IEnumerator PostActivity(string text, string type = "message")
        {
            Debug.Log("Started activity post... ");

            UnityWebRequest webRequest = new UnityWebRequest(
                "https://directline.botframework.com/v3/directline/conversations/" + currentConversation.conversationId + "/activities"
                );

            webRequest.SetRequestHeader("Authorization", "Bearer " + directLineSecret);
            webRequest.SetRequestHeader("Content-Type", "application/json");

            webRequest.method = "POST";

            webRequest.downloadHandler = new DownloadHandlerBuffer();

            Activity activitiy = new Activity();
            activitiy.from = new ChannelAccount(fromUser);
            activitiy.type = type;
            activitiy.text = text;

            string activitityString = JsonUtility.ToJson(activitiy);
            webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(activitityString));

            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError("Can't post " + type + " activitiy: " + webRequest.error);
            }
            else
            {
                string responseAsString = webRequest.downloadHandler.text;
                Debug.Log("Posted activity of type: " + type + " " + responseAsString);
            }
        }
    }
}
