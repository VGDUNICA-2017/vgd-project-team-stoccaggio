using UnityEngine;
using System.Collections;

public class doorOpenandClose : MonoBehaviour {

	
	public bool T_ActivatedOpen = true;
	public bool T_ActivatedClose = false;
	public bool activateTrigger = false;

	public GameObject textO;
	public GameObject textC;

	
	Animator animator;
	bool doorOpen;
	
	public GameObject doorOpenSound;
	public GameObject doorCloseSound;

	void Start ()
    { 
		textC.SetActive (false);
		textO.SetActive (false);
		T_ActivatedOpen = true;
		T_ActivatedClose = false;

		animator = GetComponent<Animator> ();
		doorOpen = false;


		doorCloseSound.SetActive (false);
		doorOpenSound.SetActive (false);
	}

	void Update ()
    { 

		if (T_ActivatedOpen == true)
			T_ActivatedClose = false;

		else if (T_ActivatedClose == true)
			T_ActivatedOpen = false;

        if (Input.GetKeyDown(KeyCode.E) && activateTrigger == true)
        {
            if (T_ActivatedOpen) // Se la porta è chiusa
            {
                textO.SetActive(false);
                textC.SetActive(false);
                T_ActivatedOpen = false;
                T_ActivatedClose = true;
                textO.SetActive(false);
                textC.SetActive(true);
                doorOpen = true;

                doorOpenSound.SetActive(true);
                doorCloseSound.SetActive(false);

                if (doorOpen) //Se la porta è chiusa, la setta aperta e fa l'animazione per aprirla
                {
                    doorOpen = true;
                    doorController("Open");
                }

            }
            else if (T_ActivatedClose) //Se la porta è aperta
            {
                T_ActivatedOpen = true;
                T_ActivatedClose = false;
                textO.SetActive(true);
                textC.SetActive(false);

                doorCloseSound.SetActive(true);
                doorOpenSound.SetActive(false);

                if (doorOpen) //Se la porta è aperta, la setta chiusa e fa l'animazione per chiuderla
                {
                    doorOpen = false;
                    doorController("Close");
                }
            }
        }
	}
														
	void OnTriggerEnter(Collider col) //Se entri nel raggio della porta
	{
		if(col.gameObject.tag == "player")
		{

				activateTrigger = true;
			if((T_ActivatedOpen == true))
			textO.SetActive (true);

			if((T_ActivatedClose == true))
				textC.SetActive (true);
		}
		
	}

	void OnTriggerExit(Collider col) //Se esci dal raggio della porta
	{
		if(col.gameObject.tag == "player")
		{
			textO.SetActive (false);
			textC.SetActive (false);
			activateTrigger = false;
		}

	}

	void doorController(string direction) //Setta l'animazione a seconda della "direzione"
	{
		animator.SetTrigger(direction);
	}
		
}
