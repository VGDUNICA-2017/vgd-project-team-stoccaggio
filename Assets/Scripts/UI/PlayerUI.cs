using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    // parametri
    public GameObject itemsPanel;
    public GameObject itemPrefab;
    public Image equippedImage;
    public Text equippedText;

    public GameObject missionsPanel;
    public GameObject missionPrefab;

    public Text actionText;
    public Text actionSubtext;

    public GameObject dialogsBox;
    public GameObject dialogLinePrefab;

    // countdown
    public Text countdownText;

    // exit menu
    public GameObject exitMenu;

    // modal panel
    public GameObject modalPanel;
    public Text modalText;

    // transition
    public GameObject transitionPanel;
    public delegate void OnTransitionEnd();

    // items
    private List<GameObject> itemsGameObjects = new List<GameObject>();

    // timer
    private Countdown countdown = null;

    // missions
    private Dictionary<string, GameObject> missions = new Dictionary<string, GameObject>();

    void Start()
    {
        // inizializzazione exit menu
        exitMenuSetup();
    }

    void Update()
    {
        exitMenuController();
        modalPanelController();
        equipController();
        countdownUpdate();
    }

    #region Exit Menu

    private void exitMenuSetup()
    {
        // setup del menu exit
        exitMenu.transform.Find("No").GetComponent<Button>().onClick.AddListener(() =>
        {
            // nascondi il cursore
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // sblocca il gioco
            Time.timeScale = 1;

            // chiudi il menu di uscita
            exitMenu.SetActive(false);

            // riattiva audio
            AudioListener.pause = false;
        });

        exitMenu.transform.Find("Yes").GetComponent<Button>().onClick.AddListener(() =>
        {
            // sblocca il gioco
            Time.timeScale = 1;

            // chiudi il menu di uscita
            exitMenu.SetActive(false);

            // riattiva audio
            AudioListener.pause = false;

            // carica il menu principale
            GameObject.FindWithTag("GameController").GetComponent<GameController>().LoadScene("Scenes/MainMenu");
        });
    }

    private void exitMenuController()
    {
        // tasto di uscita dal gioco
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // chiudi menu
            if(exitMenu.activeSelf)
            {
                // nascondi il cursore
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                // riattiva audio
                AudioListener.pause = false;

                // sblocca il gioco
                Time.timeScale = 1;

                // chiudi il menu di uscita
                exitMenu.SetActive(false);
            }
            // apri menu
            else
            {
                // disattiva l'audio
                AudioListener.pause = true;

                // pausa il gioco
                Time.timeScale = 0;

                // attiva il menu di uscita
                exitMenu.SetActive(true);

                // mostra il cursore
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    #endregion

    #region Modal Panel

    public void ActivateModal(string richText)
    {
        modalText.text = richText;
        modalPanel.SetActive(true);
    }

    private void modalPanelController()
    {
        if(modalPanel.activeSelf && Input.GetKeyDown(KeyCode.E))
            modalPanel.SetActive(false);
    }

    #endregion

    #region Equip

    private void loadItemDetails(GameObject itemGameObject, Item item)
    {
        itemGameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/" + item.Name);
    }
    private void loadItemDetails(Image itemImage, Text itemText, Item item)
    {
        itemImage.sprite = Resources.Load<Sprite>("Items/" + item.Name);
        itemText.text = item.DisplayName;
    }

    private void equipController()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            SceneController.CurrentScene.EquipPrevItem();

        if (Input.GetKeyDown(KeyCode.E))
            SceneController.CurrentScene.EquipNextItem();
    }

    public void RefreshInventory(Equipment updatedEquip)
    {
        // elimina i vecchi gameObject
        foreach (GameObject itemGameObject in itemsGameObjects)
            Destroy(itemGameObject);
        itemsGameObjects.Clear();

        // posiziona i nuovi
        foreach (Item item in updatedEquip.GetOrderedList())
        {
            if (updatedEquip.CurrentItem() != item)
            {
                // item nello zaino
                GameObject newItem = Instantiate(itemPrefab, itemsPanel.transform);
                loadItemDetails(newItem, item);
                itemsGameObjects.Add(newItem);
            }
            else
            {
                // item equippato
                loadItemDetails(equippedImage, equippedText, item);
            }
        }
    }

    #endregion

    #region ActionText

    public void ActionText(string text, string subtext)
    {
        if (text != null)
            actionText.text = text;
        if (subtext != null)
            actionSubtext.text = subtext;
    }

    #endregion

    #region Dialog

    public void ConversationMsg(string message)
    {
        // aggiunge il testo
        Instantiate(dialogLinePrefab, dialogsBox.transform);
    }

    public void ConversationMsg(string message, int durationInSeconds)
    {
        // aggiunge il testo
        GameObject newText = Instantiate(dialogLinePrefab, dialogsBox.transform);
        newText.GetComponent<Text>().text = message;

        // coroutine per la pulizia del testo
        StartCoroutine(dialogClear(newText, durationInSeconds));
    }

    public void ConversationMsg(string message, int durationInSeconds, int delayInSeconds)
    {
        // couroutine per il lancio con delay
        StartCoroutine(dialogDelay(message, durationInSeconds, delayInSeconds));
    }

    private IEnumerator dialogClear(GameObject textToClear, int durationInSeconds)
    {
        yield return new WaitForSeconds(durationInSeconds);
        Destroy(textToClear);
    }

    private IEnumerator dialogDelay(string message, int durationInSeconds, int delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        ConversationMsg(message, durationInSeconds);
    }

    #endregion

    #region Timer

    private void countdownUpdate()
    {
        if(countdown != null)
            countdownText.text = countdown.NiceString();
    }

    public void CountdownSet(Countdown countdown)
    {
        this.countdown = countdown;
    }

    public void CountdownReset()
    {
        countdown = null;
        countdownText.text = "";
    }

    #endregion

    #region Missions

    private void loadMissionDetails(GameObject missionGameObject, string title, string description)
    {
        missionGameObject.transform.Find("Title").GetComponent<Text>().text = title;
        missionGameObject.transform.Find("Description").GetComponent<Text>().text = description;
    }

    public void AddMission(string id, string title, string description)
    {
        GameObject newMission = Instantiate(missionPrefab, missionsPanel.transform);
        loadMissionDetails(newMission, title, description);
        missions.Add(id, newMission);
        missionsPanel.GetComponent<AudioSource>().Play();
    }

    public void RemoveMission(string name)
    {
        if(missions.ContainsKey(name))
            missions[name].SetActive(false);
    }

    #endregion

    #region Transition

    public void OpenTransition()
    {
        transitionPanel.GetComponent<Animation>().Play("openTransition");
    }

    public void OpenTransition(OnTransitionEnd onTransitionEnd)
    {
        OpenTransition();
        StartCoroutine(transitionEndHandler(onTransitionEnd));
    }

    public void CloseTransition()
    {
        transitionPanel.GetComponent<Animation>().Play("closeTransition");
    }

    public void CloseTransition(OnTransitionEnd onTransitionEnd)
    {
        CloseTransition();
        StartCoroutine(transitionEndHandler(onTransitionEnd));
    }

    private IEnumerator transitionEndHandler(OnTransitionEnd onTransitionEnd)
    {
        yield return new WaitForSeconds(4);

        onTransitionEnd();
    }

    #endregion
}
