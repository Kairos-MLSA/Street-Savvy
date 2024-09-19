using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAssistantButton : MonoBehaviour
{
    public GameObject AiTextbox;
    public GameObject AiTextbox2;
    public KeyCode toggleKey = KeyCode.Tab;
    public KeyCode toggleKey2 = KeyCode.Q;

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            AiTextbox.SetActive(!AiTextbox.activeSelf);
            AiTextbox2.SetActive(false);
        }

        if (Input.GetKeyDown(toggleKey2))
        {
            AiTextbox2.SetActive(!AiTextbox2.activeSelf);
            AiTextbox.SetActive(false);
        }
    }
}
