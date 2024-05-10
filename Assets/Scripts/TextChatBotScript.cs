using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChatBotScript : MonoBehaviour
{
    [SerializeField] private InputField inputFieldMessage;
    [SerializeField] private Button btnPost;
    [SerializeField] private Text textChat;


    private void Start()
    {
        btnPost.onClick.AddListener(delegate {
            string message = inputFieldMessage.text;
            DirectLine.DirectLineConnection.instance.SendMessage(message);
            textChat.text += "\n> You said: " + message;
        });

        DirectLine.DirectLineConnection.instance.OnReceivedMessage += AppendBotChatMessage;
    }

    private void AppendBotChatMessage(string message)
    {
        textChat.text += "\n> Bot replied: " + message;
    }
}
