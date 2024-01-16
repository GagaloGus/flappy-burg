using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using UnityEngine;

public class JSONFileManager : MonoBehaviour
{
    public static JSONFileManager instance = new();
    string filepath = $"{Application.persistentDataPath}";
    struct Info
    {
        public List<int> highscores;
    }

    public List<int> Load(string filename)
    {
        string file = Path.Combine(filepath, filename + ".json");
        if (File.Exists(file))
        {
            StreamReader strRead = new StreamReader(file);
            string jsonFile = strRead.ReadToEnd();

            Info info = JsonUtility.FromJson<Info>(file);

            List<int> result = info.highscores;

            print("File Loaded");
            return result;

        }
        else { throw new System.Exception("No json savefile"); }
    }

    public void Save(string filename, List<int> content)
    {
        string file = Path.Combine(filepath, filename + ".json");
        StreamWriter strWriter = new StreamWriter(filepath);

        Info info = new Info();

        info.highscores = content;

        string jsonInfo = JsonUtility.ToJson(info);
        strWriter.WriteLine(jsonInfo);

        strWriter.Close();

        print("File Saved");
    }
}
