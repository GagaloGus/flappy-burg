using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using UnityEngine;

public static class JSONFileManager
{
    static readonly string filepath = $"{Application.persistentDataPath}";
    struct Info
    {
        public List<int> highscores;
    }

    public static List<int> Load(string filename)
    {
        //Guarda la lista de puntaje
        string file = Path.Combine(filepath, filename + ".json");
        if (File.Exists(file))
        {
            StreamReader strRead = new StreamReader(file);
            string jsonFile = strRead.ReadToEnd();

            Info info = JsonUtility.FromJson<Info>(jsonFile);

            List<int> result = info.highscores;

            Debug.Log("File Loaded");
            return result;

        }
        else { throw new System.Exception("No json savefile"); }
    }

    public static void Save(string filename, List<int> content)
    {
        //Carga la lista de puntaje
        string file = Path.Combine(filepath, filename + ".json");
        StreamWriter strWriter = new StreamWriter(file);

        Info info = new Info
        {
            highscores = content
        };

        string jsonInfo = JsonUtility.ToJson(info);
        strWriter.WriteLine(jsonInfo);

        strWriter.Close();

        Debug.Log("File Saved");
    }
}
