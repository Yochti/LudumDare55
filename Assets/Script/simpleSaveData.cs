using UnityEngine;
using System.IO;

public class SimpleSaveTest : MonoBehaviour
{
    void Start()
    {
        string filePath = Application.persistentDataPath + "/TestData.json";
        string jsonData = "{\"totalSouls\": 42}";

        // Essayer d'écrire dans le fichier
        File.WriteAllText(filePath, jsonData);

        // Vérifier si l'écriture a réussi
        Debug.Log("Fichier écrit à : " + filePath);
    }
}