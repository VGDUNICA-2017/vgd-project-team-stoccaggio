using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTrigger : MonoBehaviour {

    public string infoText1;
    public string infoText2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            // disattiva gameobject per evitare ulteriori trigger
            gameObject.SetActive(false);

            // speaktoself
            if(infoText1 != "")
                SceneController.CurrentScene.SpeakToSelf(infoText1);

            if (infoText2 != "")
                SceneController.CurrentScene.SpeakToSelf(infoText2);
        }
    }
}
