using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class SignalPopUp : MonoBehaviour{
    public GameObject redSignal;
    public GameObject signalMessage;
    void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == "Player") {
                redSignal.SetActive(true);
                signalMessage.SetActive(true);
                Invoke(nameof(SetActiveFalse), 3f);
            }
    }
        public void SetActiveFalse() {
            redSignal.SetActive(false);
            signalMessage.SetActive(false);
        }
}

