using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSaveData
{
    public string currentScenePath;
    public int currentCheckpointID;
    public bool copilotSaved;
}
