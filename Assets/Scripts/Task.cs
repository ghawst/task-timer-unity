using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    private string taskName;
    private int taskGoal;

    public Transform progressObject;
    public GameObject playButton;

    public TextMeshProUGUI taskNameText;
    public TextMeshProUGUI goalText;
    public TextMeshProUGUI currentTimeText;

    public Slider slider;

    private float goalTextInitLocalPosY;
    private float progressObjInitHeight;

    enum State { PAUSED, PLAYING, FINISHED };
    State state;

    private void Awake()
    {
        state = State.PAUSED;
        slider.value = 0;
        goalTextInitLocalPosY = goalText.transform.localPosition.y;
        progressObjInitHeight = progressObject.GetComponent<RectTransform>().rect.height;
    }

    private void Start()
    {
        goalText.SetText(TimeSpan.FromMinutes(taskGoal).ToString("h':'mm"));
        taskNameText.SetText(taskName);
    }

    private void Update()
    {
        if (state == State.PLAYING)
        {
            slider.value += Time.deltaTime / (taskGoal * 60);
            currentTimeText.SetText(TimeSpan.FromMinutes(slider.value * taskGoal).ToString("h':'mm':'ss"));
            if (slider.value == 1)
            {
                Pause();
                state = State.FINISHED;
            }
        }
    }

    public string TaskName { get => taskName; set => taskName = value; }
    public int TaskGoal { get => taskGoal; set => taskGoal = value; }

    public void AdjustMax(int newMax)
    {
        if (newMax >= taskGoal)
        {
            float goalPercentOfMax = (float)taskGoal / newMax;
            progressObject.transform.localScale = new Vector3(1, goalPercentOfMax, 1);
            goalText.transform.localPosition = new Vector3(0, goalTextInitLocalPosY - (progressObjInitHeight - goalPercentOfMax * progressObjInitHeight), 0);
        }
    }

    public void PlayPause()
    {
        if(state != State.FINISHED)
        {
            foreach(Transform child in transform.parent)
            {
                if (child.GetComponent<Task>().state == State.PLAYING && child != transform)
                {
                    child.GetComponent<Task>().Pause();
                }
            }
            if (state == State.PAUSED)
            {
                Play();
            }
            else if (state == State.PLAYING)
            {
                Pause();
            }
        }
    }

    public void Play()
    {
        state = State.PLAYING;
        playButton.transform.Find("Image").GetComponent<Image>().sprite = TaskManager.Instance.pauseIcon;
    }

    public void Pause()
    {
        state = State.PAUSED;
        playButton.transform.Find("Image").GetComponent<Image>().sprite = TaskManager.Instance.playIcon;
    }

    public void Delete()
    {
        TaskManager.Instance.DoAfterTaskDelete();
        Destroy(gameObject);
    }
}
