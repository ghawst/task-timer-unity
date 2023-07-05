using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class SaveSystem {

    static string path = Application.persistentDataPath + "/data.txt";

    public static void Save()
    {
        DayData data = new DayData(DateTime.Today, GetCurrentTasksData());
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.AppendAllText(path, json + ",");
    }

    public static void Load()
    {
        foreach(DayData data in ReadJson(path))
        {
            Debug.Log(data);
        }
    }

    public static List<DayData> ReadJson(string path)
    {
        string json = "[" + File.ReadAllText(path) + "]";
        return JsonConvert.DeserializeObject<List<DayData>>(json);
    }

    public static List<TaskData> GetCurrentTasksData()
    {
        List<TaskData> tasks = new List<TaskData>();
        foreach(Transform child in TaskManager.Instance.grid.transform)
        {
            Task task = child.GetComponent<Task>();
            string name = task.TaskName;
            int goal = task.TaskGoal;
            float progress = task.TaskTime;
            tasks.Add(new TaskData(name, goal, progress));
        }
        return tasks;
    }
}
