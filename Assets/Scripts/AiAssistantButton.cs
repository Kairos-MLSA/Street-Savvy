using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAssistantButton : MonoBehaviour
{
    public GameObject AiTextbox;
    public KeyCode toggleKey = KeyCode.Tab;

    //if tab button is pressed the AiTextbox will be active
    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            AiTextbox.SetActive(!AiTextbox.activeSelf);
        }
    }
}
