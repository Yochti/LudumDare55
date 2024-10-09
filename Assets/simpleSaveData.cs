using UnityEngine;
using System.IO;

public class SimpleSaveTest : MonoBehaviour
{
    void Start()
    {
        string filePath = Application.persistentDataPath + "/TestData.json";
        string jsonData = "{\"totalSouls\": 42}";

        // Essayer d'�crire dans le fichier
        File.WriteAllText(filePath, jsonData);

        // V�rifier si l'�criture a r�ussi
        Debug.Log("Fichier �crit � : " + filePath);
    }
}