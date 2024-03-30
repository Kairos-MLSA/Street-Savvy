using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPassed : MonoBehaviour
{
    [SerializeField] private float targetTime = 3.0f;
    private float timer;
    public GameObject levelPassedBox;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            timer = targetTime;
        }
    }
    private void Update() {
       if (timer > 0f) // Check if timer is active
        {
            timer -= Time.deltaTime; // Decrement timer each frame

            if (timer <= 0f) // Timer finished (reached 0 or below)
            {
                levelPassedBox.SetActive(true);
            }
        }
    }
    
}
