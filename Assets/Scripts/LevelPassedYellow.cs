using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPassedYellow : MonoBehaviour
{
    [SerializeField] private float targetTime = 3.0f;
    private float timer;
    public GameObject levelPassedBox;
    public Material yellowLightMaterial;
    public Material redLightMaterial;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            yellowLightMaterial.DisableKeyword("_EMISSION");
            redLightMaterial.EnableKeyword("_EMISSION");
            timer = targetTime;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            timer = 0f;
            levelPassedBox.SetActive(false);
        }
    }
    private void Update() {
       if (timer > 0f) // Check if timer is active
        {
            timer -= Time.deltaTime; // Decrement timer each frame

            if (timer <= 0f) // Timer finished (reached 0 or below)
            {
                levelPassedBox.SetActive(true);
                GameObject.Find("Player Car").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                Invoke(nameof(loadNextScene), 3f);
                
            }
        }
    }

    void loadNextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}
