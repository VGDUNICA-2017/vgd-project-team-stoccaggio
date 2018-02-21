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

    public Text conversationText;

    public GameObject exitMenu;

    // inventario
    private List<GameObject> itemsGameObjects = new List<GameObject>();

    // covnersation
    private Coroutine conversationCoroutine;

    void Start()
    {
        // inizializzazione exit menu
        exitMenuSetup();
    }

    private void Update()
    {
        exitMenuController();
        equipController();
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
        });
        exitMenu.transform.Find("Yes").GetComponent<Button>().onClick.AddListener(() =>
        {
            // sblocca il gioco
            Time.timeScale = 1;

            // chiudi il menu di uscita
            exitMenu.SetActive(false);

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

                // sblocca il gioco
                Time.timeScale = 1;

                // chiudi il menu di uscita
                exitMenu.SetActive(false);
            }
            // apri menu
            else
            {
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

    public void ActionText(string text, string subtext)
    {
        if (text != null)
            actionText.text = text;
        if (subtext != null)
            actionSubtext.text = subtext;
    }

    #region Conversation

    public void ConversationMsg(string message)
    {
        // aggiorna il testo
        conversationText.text = message;

        // ferma l'eventuale coroutine precedente
        if (conversationCoroutine != null)
            StopCoroutine(conversationCoroutine);
    }
    public void ConversationMsg(string message, int durationInSeconds)
    {
        // aggiorna il testo
        conversationText.text = message;

        // ferma l'eventuale coroutine precedente
        if (conversationCoroutine != null)
            StopCoroutine(conversationCoroutine);

        // coroutine per la pulizia del testo
        conversationCoroutine = StartCoroutine(conversationClear(durationInSeconds));
    }
    private IEnumerator conversationClear(int durationInSeconds)
    {
        yield return new WaitForSecondsRealtime(durationInSeconds);
        conversationText.text = "";
    }

    #endregion
}
