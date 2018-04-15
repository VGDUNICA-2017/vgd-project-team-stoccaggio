using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    // impostazione della scena
    public PlayerUI playerUI;
    public GameObject player;

    // checkpoints
    public GameObject[] checkPoints;

    // personaggio
    private Equipment playerEquip = new Equipment();

    // shortcuts
    public static SceneController CurrentScene = null;
    public static GameObject CurrentGameObject = null;

    // countdowns
    public Dictionary<string, Countdown> countdowns = new Dictionary<string, Countdown>();
    private string currentUITimer = null;

    private void Awake()
    {
        // aggiornamento shortcut
        CurrentScene = this;
        CurrentGameObject = transform.gameObject;
    }

    private void Start()
    {
        // gestione salvataggio
        int currentCheckpointID = (GameController.CurrentController.gameSaveData == null) ? 0 : GameController.CurrentController.gameSaveData.currentCheckpointID;

        // posizionamento player
        player.transform.position = checkPoints[currentCheckpointID].transform.position;

        // entry transition
        playerUI.CloseTransition();
    }

    private void Update()
    {
        updateCountdowns();
    }

    #region Camera

    public CameraShake GetCameraShake()
    {
        return GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    }

    #endregion

    #region Texts

    public void Notification(string message,int duration)
    {
        playerUI.ConversationMsg(message, duration);
    }

    public void SpeakToSelf(string message)
    {
        //player.GetComponent<AudioSource>().Play();
        playerUI.ConversationMsg("* " + message + " *", 4);
    }

    public void SpeakToSelf(string message, int delay)
    {
        playerUI.ConversationMsg("* " + message + " *", 4, delay);
    }

    public void NpcSpeak(string npcName, string message)
    {
        playerUI.ConversationMsg("[" + npcName + "] " + message, 4);
    }

    public void NpcSpeak(string npcName, string message, int duration)
    {
        playerUI.ConversationMsg("[" + npcName + "] " + message, duration);
    }

    public void NpcSpeak(string npcName, string message, int duration, int delay)
    {
        playerUI.ConversationMsg("[" + npcName + "] " + message, duration, delay);
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

    public bool HasItem(string itemName)
    {
        return playerEquip.Contains(itemName);
    }

    public bool IsEquipped(string itemName)
    {
        return playerEquip.CurrentItem().Name == itemName;
    }

    #endregion

    #region Countdowns

    private void updateCountdowns()
    {
        foreach(KeyValuePair<string, Countdown> cd in countdowns)
            cd.Value.Update(Time.deltaTime);
    }

    public void SetUITimer(string timerName)
    {
        currentUITimer = timerName;
        playerUI.CountdownSet(countdowns[timerName]);
    }

    public void ClearUITimer(string timerName)
    {
        if(currentUITimer == timerName)
            playerUI.CountdownReset();
    }

    #endregion

    public void GameOver()
    {
        playerUI.OpenTransition(() =>
        {
            GameController.CurrentController.LoadGameSave();
        });
    }
}
