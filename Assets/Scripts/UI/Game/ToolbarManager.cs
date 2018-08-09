using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToolbarManager : MonoBehaviour
{
    public Text txtLevelNumber;
    public Text txtTimer;

    /// <summary>
    ///  execute służy jako przełącznik informujący kod kiedy uruchomic timer
    /// </summary>
    private bool execute = false;

    public static ToolbarManager ins;

    public CustomTimer timer = new CustomTimer();

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        try
        {
            txtLevelNumber.text = SceneManager.GetSceneAt(1).name.Split('_')[1];
        }catch(Exception ex)
        {
            Debug.LogWarning("Problemy z ładowaniem ToolbarManager \n" + ex.Message);
        }
    }

    private void Update()
    {
        if (execute)
        {
            timer.UpdateTimer(Time.deltaTime);
        }

        DisplayClock();
    }

    public int lineCounter
    {
        get { return lineCounter; }
        set
        {
            if (value < 0) // upewnienie się że licznik nie przyjmie wartości ujemnych
                lineCounter = 0;
            else
                lineCounter = value;
        }
    }

    public void StartGame()
    {
        execute = true;
    }

    public void StopGame()
    {
        execute = false;
        timer.Reset();
    }

    public void PauseGame()
    {
        execute = false;
    }

    private void DisplayClock()
    {
        txtTimer.text = timer.ToMinutes();
    }
}
