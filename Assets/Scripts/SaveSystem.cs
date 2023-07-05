using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Unity.VisualScripting;
using System.Linq;

public class SaveSystem {

    static string path = Application.persistentDataPath + "/data.txt";

    public static void Save()
    {
        DayData data = new DayData(DateTime.Today, GetCurrentTasksData());
        if(File.Exists(path) && ReadJson().Count > 0 && ReadJson().Last().date == data.date)
        {
            List<string> lines = File.ReadAllLines(path).ToList();
            File.WriteAllLines(path, lines.GetRange(0, lines.Count - 1).ToArray());
        }
        string json = JsonConvert.SerializeObject(data);
        File.AppendAllText(path, json + "," + Environment.NewLine);
    }

    public static void Load()
    {
        if(File.Exists(path))
        {
            List<DayData> dayDatas = ReadJson();
            if(dayDatas.Count > 0 && dayDatas.Last().date == DateTime.Today)
            {
                foreach(TaskData task in dayDatas.Last().tasks)
                {
                    TaskManager.Instance.AddTask(task.name, task.goal, task.progress);
                }
            }
        }
    }

    public static List<DayData> ReadJson()
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
