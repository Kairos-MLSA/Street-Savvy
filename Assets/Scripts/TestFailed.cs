using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFailed : MonoBehaviour
{
    public GameObject warningMessage;
    public GameObject gameOver;
    public GameObject panel;
    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player"){
            warningMessage.SetActive(true);
            gameOver.SetActive(true);
            panel.SetActive(true);
            Debug.Log("Game Over");
        }
    }
}
