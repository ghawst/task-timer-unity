using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskData
{
    public string name;
    public int goal;
    public float progress;

    public TaskData(string name, int goal, float progress)
    {
        this.name = name;
        this.goal = goal;
        this.progress = progress;
    }
}
