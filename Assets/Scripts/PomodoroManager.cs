using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PomodoroManager : MonoBehaviour
{
    public static PomodoroManager instance;

    public UnityEngine.UI.Toggle pomodoroToggle;
    public UnityEngine.UI.Button pomodoroButton;
    public TextMeshProUGUI pomodoroTimerText;
    public TextMeshProUGUI breakTimerText;

    public AudioSource workingAlarm;
    public AudioSource breakAlarm;

    float pomodoroTime;
    float breakTime;

    enum State { WORKING, WORKINGALARM, BREAKALARM, BREAK }
    State state;

    private void Awake()
    {
        instance = this;
        pomodoroToggle.isOn = true;
        state = State.WORKING;
        pomodoroTime = 0;
    }

    private void Update()
    {
        if (pomodoroToggle.isOn && TaskManager.Instance.TaskPlaying != null && state == State.WORKING)
        {
            pomodoroTime += Time.deltaTime * 250;
            pomodoroTimerText.SetText(TimeSpan.FromSeconds(pomodoroTime).ToString("h':'mm':'ss"));
            if (pomodoroTime / 60 >= 25)
            {
                PomodoroAlarm();
            }
        }
        if (state == State.BREAK)
        {
            breakTime += Time.deltaTime * 250;
            breakTimerText.SetText(TimeSpan.FromSeconds(breakTime).ToString("h':'mm':'ss"));
            if (breakTime / 60 >= 5)
            {
                PomodoroAlarm();
            }
        }
    }

    void PomodoroAlarm()
    {
        pomodoroButton.interactable = true;
        if (state == State.WORKING)
        {
            pomodoroTimerText.SetText(TimeSpan.FromSeconds(25 * 60).ToString("h':'mm':'ss"));
            state = State.WORKINGALARM;
            workingAlarm.Play();
        }
        else if (state == State.BREAK)
        {
            breakTimerText.SetText(TimeSpan.FromSeconds(5 * 60).ToString("h':'mm':'ss"));
            state = State.BREAKALARM;
            breakAlarm.Play();
        }
    }

    public void PomodoroButton()
    {
        if (state == State.WORKINGALARM)
        {
            state = State.BREAK;
            TaskManager.Instance.TaskPlaying.Pause();
        }
        else if (state == State.BREAKALARM)
        {
            ResetPomodoro();
        }
        StopSoundsAndButton();
    }

    void StopSoundsAndButton()
    {
        workingAlarm.Stop();
        breakAlarm.Stop();
        pomodoroButton.interactable = false;
    }

    public void ResetPomodoro()
    {
        StopSoundsAndButton();
        pomodoroTime = 0;
        pomodoroTimerText.SetText(TimeSpan.FromSeconds(pomodoroTime).ToString("h':'mm':'ss"));
        breakTime = 0;
        breakTimerText.SetText(TimeSpan.FromSeconds(breakTime).ToString("h':'mm':'ss"));
        state = State.WORKING;
    }
}
