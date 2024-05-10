using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAssistantButton : MonoBehaviour
{
    public GameObject AiTextbox;
    public GameObject AiTextbox2; // Add this line
    public KeyCode toggleKey = KeyCode.Tab;
    public KeyCode toggleKey2 = KeyCode.Q; // Add this line

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            AiTextbox.SetActive(!AiTextbox.activeSelf);
            AiTextbox2.SetActive(false); // Add this line
        }

        if (Input.GetKeyDown(toggleKey2)) // Add this block
        {
            AiTextbox2.SetActive(!AiTextbox2.activeSelf);
            AiTextbox.SetActive(false);
        }
    }
}
