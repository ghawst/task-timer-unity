using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryManager : MonoBehaviour
{
    public GameObject historyScreen;

    public GameObject historyContent;
    public GameObject progressContent;

    public GameObject historyItemPrefab;

    private void Awake()
    {
        foreach(Transform child in historyContent.transform)
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
        List<DayData> dayDataList = SaveSystem.ReadJson();
        foreach(DayData dayData in dayDataList)
        {
            GameObject historyItem = Instantiate(historyItemPrefab);
            historyItem.transform.SetParent(historyContent.transform);
            historyItem.transform.localScale = Vector3.one;
            historyItem.GetComponent<HistoryItem>().Initialize(dayData);
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
}
