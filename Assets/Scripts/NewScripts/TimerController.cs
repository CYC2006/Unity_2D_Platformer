// 2026/1/1 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private bool isPaused = false;
    private float elapsedTime = 0f;

    void Update()
    {
        if (!isPaused)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimeText();
        }
    }

    public void PauseTimer()
    {
        isPaused = true;
    }

    private void UpdateTimeText()
    {
        int hours = Mathf.FloorToInt(elapsedTime / 3600F);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600F) / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        timeText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}