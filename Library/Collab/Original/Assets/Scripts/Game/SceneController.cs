using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    // impostazione della scena
    public PlayerUI playerUI;

    // personaggio
    private Equipment playerEquip = new Equipment();

    // supporto
    private GameController gameController;

    // shortcut
    public static SceneController CurrentScene = null;

    private void Awake()
    {
        // aggiornamento shortcut
        CurrentScene = this;
    }

    private void Start()
    {
        // gameController
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    #region Texts

    public void SpeakToSelf(string message)
    {

    }

    public void NpcSpeak(string npcName, string message)
    {
        playerUI.ConversationMsg("[" + npcName + "] " + message, 4);
    }

    #endregion

    #region Equip

    public List<Item> GetItems()
    {
        return playerEquip.GetItems();
    }

    public void AddItem(Item item)
    {
        playerEquip.AddItem(item);
        playerUI.RefreshInventory(playerEquip);
    }

    public void RemoveItem(string itemName)
    {
        playerEquip.RemoveItem(itemName);
        playerUI.RefreshInventory(playerEquip);
    }

    public void EquipNextItem()
    {
        playerEquip.EquipNext();
        playerUI.RefreshInventory(playerEquip);
    }

    public void EquipPrevItem()
    {
        playerEquip.EquipPrevious();
        playerUI.RefreshInventory(playerEquip);
    }

    #endregion

    public void GameOver()
    {
        gameController.LoadScene("Scenes/Terra");
    }
}
