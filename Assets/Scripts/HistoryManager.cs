using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryManager : MonoBehaviour
{
    public static HistoryManager Instance { get; private set; }

    public GameObject historyScreen;

    public GameObject historyContent;
    public GameObject progressContent;

    public GameObject historyItemPrefab;
    public GameObject progressItemPrefab;

    Dictionary<string, float> totalTasks;

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
        foreach (Transform child in historyContent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in progressContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void Start()
    {
        historyScreen.SetActive(false);
        totalTasks = new Dictionary<string, float>();
        List<DayData> dayDataList = SaveSystem.ReadJson();
        foreach(DayData dayData in dayDataList)
        {
            GameObject historyItem = Instantiate(historyItemPrefab);
            historyItem.transform.SetParent(historyContent.transform);
            historyItem.transform.localScale = Vector3.one;
            historyItem.GetComponent<HistoryItem>().Initialize(dayData);
        }
        foreach(KeyValuePair<string, float> task in totalTasks)
        {
            GameObject progressItem = Instantiate(progressItemPrefab);
            progressItem.transform.SetParent(progressContent.transform);
            progressItem.transform.localScale = Vector3.one;
            progressItem.GetComponent<ProgressItem>().Initialize(task.Key, task.Value);
        }
    }

    public void OpenHistory()
    {
        historyScreen.SetActive(true);
    }

    public void CloseHistory()
    {
        historyScreen.SetActive(false);
    }

    public void AddTaskToTotal(TaskData taskData)
    {
        if (totalTasks.ContainsKey(taskData.name))
        {
            totalTasks[taskData.name] += taskData.progress;
        }
        else
        {
            totalTasks.Add(taskData.name, taskData.progress);
        }
    }
}
