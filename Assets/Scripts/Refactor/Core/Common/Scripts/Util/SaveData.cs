using System.IO;
using System.Runtime.Serialization.Formatters.Binary;;
using UnityEngine;

namespace GDG
{
// Credit: sharkcircuits
[System.Serializable]
public class SaveData
{
    public int highScore;
    public static SaveData RecordHighscoreIntoSave(int highScore)
    {
        SaveData savedData = new SaveData();
        savedData.highScore = highScore;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save.savedData");
        bf.Serialize(file, savedData);
        file.Close();
        Debug.Log("Data saved");
        return savedData;
    }

    public static SaveData LoadSaveData()
    {
        if(File.Exists(Application.persistentDataPath + "/save.savedData"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save.saveData", FileMode.Open);
            SaveData returnValue = (SaveData) bf.Deserialize(file);
            file.Close();
            Debug.Log("Save data found.");
            return returnValue;
        }
        Debug.Log("Failed to load save data.");
        return null;
    }
}
}
