using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;
using Platformer.Model; // 這裡要加入，因為金幣資料通常存在 Model 裡

namespace Platformer.Mechanics
{
    /// <summary>
    /// Marks a trigger as a VictoryZone, usually used to end the current game level.
    /// </summary>
    public class VictoryZone : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collider)
        {
            var p = collider.gameObject.GetComponent<PlayerController>();
            if (p != null)
            {
                // Pause the timer
                TimerController timerController = FindAnyObjectByType<TimerController>();
                if (timerController != null)
                {
                    timerController.PauseTimer();
                }

                var ev = Schedule<PlayerEnteredVictoryZone>();
                ev.victoryZone = this;
            }
        }
    }
}