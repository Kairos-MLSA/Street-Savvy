using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPassedGreen : MonoBehaviour
{
   
    public GameObject levelPassedBox;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            levelPassedBox.SetActive(true);
            Invoke(nameof(loadNextScene), 3f);
            
        }
    }
     void loadNextScene(){
        SceneManager.LoadScene(0);
    }
    
}
