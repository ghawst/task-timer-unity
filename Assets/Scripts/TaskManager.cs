using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    float saveTimer;
    public float saveTime;

    private void Start()
    {
        TaskPlaying = null;
        SaveSystem.Load();
    }

    private void Update()
    {
        //if (Input.GetKeyUp(KeyCode.S))
        //{
        //    SaveSystem.Save();
        //}
        //if (Input.GetKeyUp(KeyCode.L))
        //{
        //    SaveSystem.Load();
        //}
        if(saveTimer < saveTime * 60)
        {
            saveTimer += Time.deltaTime;
        }
        else
        {
            saveTimer = 0;
            SaveSystem.Save();
        }
    }

    public void AddTaskFromForm()
    {
        if (grid.transform.childCount < 4 && taskGoalSlider.value > 0 && taskNameText.text.Length > 0)
        {
            AddTask(taskNameText.text, (int)taskGoalSlider.value * 5, 0);
        }
    }

    public void AddTask(string name, int goal, float progress)
    {
        GameObject task = Instantiate(taskPrefab);
        task.transform.SetParent(grid.transform);
        task.transform.localScale = Vector3.one;
        task.GetComponent<Task>().TaskName = name;
        task.GetComponent<Task>().TaskGoal = goal;
        task.GetComponent<Task>().TaskTime = progress;
        task.GetComponent<Task>().UpdateTaskTimeDisplay();
        AdjustTasksMaxGoal();
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

    private void OnApplicationQuit()
    {
        SaveSystem.Save();
    }
}
