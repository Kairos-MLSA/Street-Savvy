using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : MonoBehaviour
{
    [SerializeField] private InputField inputFieldMessage;
    [SerializeField] private Button btnPost;
    [SerializeField] private Text textChat;
    public Text botReplyText;
    public TTS tts;

    private bool isFirstResponse = true;



    private void Start()
    {
        btnPost.onClick.AddListener(delegate {
            string message = inputFieldMessage.text;
            DirectLine.DirectLineConnection.instance.SendMessage(message);
            textChat.text += "\n> You said: " + message;
        });

        DirectLine.DirectLineConnection.instance.OnReceivedMessage += AppendBotChatMessage;
    }

    /*private void AppendBotChatMessage(string message)
    {
        textChat.text += "\n> Bot replied: " + message;
        botReplyText.text = message;
        tts.ButtonClick();
    }*/

    private void AppendBotChatMessage(string message)
    {
        //tts.ButtonClick();
        if (!isFirstResponse)
        {
            textChat.text += "\n> Bot replied: " + message;
            botReplyText.text = message;
            tts.ButtonClick();
        }
        else
        {
            isFirstResponse = false;
            //tts.ButtonClick();
        }
    }
}

//First our TTS service will input a text which will be spoken.
//The Bot's reply one by one text, will be sent to the TTS.
//Once the bot receives reply from the TTS, it will speak the text.
