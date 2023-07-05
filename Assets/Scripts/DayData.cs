using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayData
{
    public DateTime date;
    public List<TaskData> tasks;

    public DayData(DateTime date, List<TaskData> tasks)
    {
        this.date = date;
        this.tasks = tasks;
    }
}
