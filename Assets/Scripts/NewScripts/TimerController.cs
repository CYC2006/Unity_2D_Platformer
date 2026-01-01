// 2026/1/1 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI timeText; // Reference to the Text component in the Canvas
    private float elapsedTime = 0f;

    void Update()
    {
        // Update elapsed time
        elapsedTime += Time.deltaTime;

        // Convert elapsed time to hours, minutes, and seconds
        int hours = Mathf.FloorToInt(elapsedTime / 3600);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // Update the text to display the formatted time
        timeText.text = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
    }
}