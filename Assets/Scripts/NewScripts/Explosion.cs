using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 0.5f); //¼½©ñ0.5¬í
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
