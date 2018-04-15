using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour {

    // impostazione della scena
    public PlayerUI playerUI;

    // shortcuts
    public static CutsceneController CurrentScene = null;
    public static GameObject CurrentGameObject = null;

    private void Awake()
    {
        // aggiornamento shortcut
        CurrentScene = this;
        CurrentGameObject = transform.gameObject;
    }

    private void Start()
    {
        // entry transition
        playerUI.CloseTransition();
    }

    #region Texts

    public void Notification(string message, int duration)
    {
        playerUI.ConversationMsg(message, duration);
    }

    public void SpeakToSelf(string message)
    {
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

}
