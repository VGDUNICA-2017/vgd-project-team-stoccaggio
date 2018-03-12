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

    public GameObject exitMenu;

    public GameObject conversationText;

    // inventario
    private List<GameObject> itemsGameObjects;
    private Equipment playerEquipment;

    void Start()
    {
        // inventario
        itemsGameObjects = new List<GameObject>();
        playerEquipment = new Equipment();

        playerEquipment.AddItem(new Item("empty", ""));
        playerEquipment.AddItem(new Item("bottle", "Peracotto Micidiale 1"));
        playerEquipment.AddItem(new Item("altro", "Peracotto Micidiale 2"));
        playerEquipment.AddItem(new Item("armor", "Peracotto Micidiale 3"));


        refreshInventory(playerEquipment);


        Instantiate(missionPrefab, missionsPanel.transform);
        Instantiate(missionPrefab, missionsPanel.transform);

        // inizializzazione exit menu
        exitMenuSetup();
    }

    private void Update()
    {
        exitMenuController();
        equipController();
    }

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
            GameObject.FindWithTag("GameController").GetComponent<LoadingController>().LoadScene("Scenes/MainMenu");
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

    private void loadItemDetails(GameObject itemGameObject, Item item)
    {
        itemGameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/" + item.Name);
    }
    private void loadItemDetails(Image itemImage, Text itemText, Item item)
    {
        itemImage.sprite = Resources.Load<Sprite>("Items/" + item.Name);
        itemText.text = item.DisplayName;
    }

    private void refreshInventory(Equipment updatedEquip)
    {
        // elimina i vecchi gameObject
        foreach(GameObject itemGameObject in itemsGameObjects)
            Destroy(itemGameObject);
        itemsGameObjects.Clear();

        // posiziona i nuovi
        foreach (Item item in updatedEquip.GetOrderedList())
        {
            if(updatedEquip.CurrentItem() != item)
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

    private void equipController()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerEquipment.EquipPrevious();
            refreshInventory(playerEquipment);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            playerEquipment.EquipNext();
            refreshInventory(playerEquipment);
        }
    }
}
