using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public GameObject grid;
    public GameObject taskPrefab;

    public TextMeshProUGUI taskNameText;
    public Slider taskGoalSlider;

    public TextMeshProUGUI taskGoalInputText;

    public Sprite playIcon;
    public Sprite pauseIcon;

    private Task taskPlaying;
    public Task TaskPlaying { get => taskPlaying; set => taskPlaying = value; }

    public AudioSource taskAlarm;

    private void Start()
    {
        TaskPlaying = null;
    }

    public void AddTask()
    {
        if (grid.transform.childCount < 4 && taskGoalSlider.value > 0 && taskNameText.text.Length > 0)
        {
            GameObject task = Instantiate(taskPrefab);
            task.transform.SetParent(grid.transform);
            task.transform.localScale = Vector3.one;
            task.GetComponent<Task>().TaskName = taskNameText.text;
            int taskGoal = (int)taskGoalSlider.value * 5;
            task.GetComponent<Task>().TaskGoal = taskGoal;
            AdjustTasksMaxGoal();
        }
    }

    public void AdjustTasksMaxGoal()
    {
        foreach (Transform child in grid.transform)
        {
            child.GetComponent<Task>().AdjustMax(GetMaxGoal());
        }
    }

    public int GetMaxGoal()
    {
        int maxGoal = 0;
        foreach (Transform child in grid.transform)
        {
            int childGoal = child.GetComponent<Task>().TaskGoal;
            if (childGoal > maxGoal)
            {
                maxGoal = childGoal;
            }
        }
        return maxGoal;
    }

    public void DoAfterTaskDelete()
    {
        StartCoroutine(DoAfterTaskDeleteCo());
    }

    private IEnumerator DoAfterTaskDeleteCo()
    {
        yield return new WaitForEndOfFrame();

        AdjustTasksMaxGoal();
    }

    public void UpdateTaskGoalInput()
    {
        taskGoalInputText.SetText(TimeSpan.FromMinutes((int)taskGoalSlider.value * 5).ToString("h':'mm"));
    }
}
