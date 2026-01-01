// 2026/1/1 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class LaserMoveRight : MonoBehaviour
{
    [Header("laser movespeed")]
    public float moveSpeed = 5f;

    [Header("laser right bound")]
    public float boundaryX = 10f;

    public Vector3 initialPosition; // 初始位置

    private void Start()
    {
        
        // 將雷射移動到初始位置
        transform.position = initialPosition;
    }

    private void OnEnable()
    {
        // 每次喚醒時重置位置
        transform.position = initialPosition;
    }

    private void Update()
    {
        // 向右移動
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        // 檢查是否超出右邊界
        if (transform.position.x > boundaryX)
        {
            // 雷射超出邊界後隱藏並重置位置
            gameObject.SetActive(false);
            transform.position = initialPosition;
        }
    }
}