using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskItem : MonoBehaviour
{
    public TextMeshProUGUI taskNameText;
    public TextMeshProUGUI taskProgressText;
    public TextMeshProUGUI taskGoalText;

    public void Initialize(TaskData taskData)
    {
        taskNameText.SetText(taskData.name);
        taskProgressText.SetText(TimeSpan.FromSeconds(taskData.progress).ToString("h':'mm"));
        float percentageOfCompletion = Mathf.Clamp(taskData.progress / taskData.goal / 60, 0, 1);
        taskProgressText.color = new Color(1 - percentageOfCompletion, percentageOfCompletion, 0);
        taskGoalText.SetText(TimeSpan.FromMinutes(taskData.goal).ToString("h':'mm"));
    }
}
