﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveFileManager {

    private static string saveFileName = "gamesave.dat";

    // salvataggio dei dati di gioco su file
    public static void Save(GameSaveData gsData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + saveFileName);
        bf.Serialize(file, gsData);
        file.Close();
    }

    // caricamento del salvataggio da file
    public static GameSaveData Load()
    {
        GameSaveData gsData = null;

        if (File.Exists(Application.persistentDataPath + "/" + saveFileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + saveFileName, FileMode.Open);
            gsData = (GameSaveData)bf.Deserialize(file);
            file.Close();
        }

        return gsData;
    }
}
