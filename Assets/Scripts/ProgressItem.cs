using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressItem : MonoBehaviour
{
    public TextMeshProUGUI taskNameText;
    public TextMeshProUGUI taskProgressText;

    public void Initialize(string taskName, float taskProgress)
    {
        taskNameText.SetText(taskName);
        taskProgressText.SetText(TimeSpan.FromSeconds(taskProgress).ToString("h':'mm"));
    }
}
