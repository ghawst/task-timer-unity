using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HistoryItem : MonoBehaviour
{
    public GameObject taskContent;

    public GameObject taskItemPrefab;

    public TextMeshProUGUI dateText;

    private void Awake()
    {
        foreach(Transform child in taskContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void Initialize(DayData dayData)
    {
        dateText.SetText(dayData.date.ToString("yyyy MMMM dd ddd"));
        foreach(TaskData taskData in dayData.tasks)
        {
            GameObject taskItem = Instantiate(taskItemPrefab);
            taskItem.transform.SetParent(taskContent.transform);
            taskItem.transform.localScale = Vector3.one;
            taskItem.GetComponent<TaskItem>().Initialize(taskData);

            HistoryManager.Instance.AddTaskToTotal(taskData);
        }
    }
}
