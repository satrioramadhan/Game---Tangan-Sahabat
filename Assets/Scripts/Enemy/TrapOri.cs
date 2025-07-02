using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapOri : MonoBehaviour
{
    public float upSpeed = 3.0f;   // Kecepatan naik
    public float downSpeed = 0.8f; // Kecepatan turun
    public float range = 3f;
    public float waitTime = 2f;    // Jeda
    float startingY;
    int state = 0; // 0 = naik, 1 = turun, 2 = jeda
    float waitTimer = 0f;

    void Start()
    {
        startingY = transform.position.y;
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case 0: // Naik cepat
                transform.Translate(Vector2.up * upSpeed * Time.fixedDeltaTime);
                if (transform.position.y >= startingY + range)
                {
                    state = 1; // Ganti ke turun
                }
                break;

            case 1: // Turun pelan
                transform.Translate(Vector2.down * downSpeed * Time.fixedDeltaTime);
                if (transform.position.y <= startingY)
                {
                    state = 2; // Ganti ke jeda
                    waitTimer = waitTime; // Reset timer
                }
                break;

            case 2: // Jeda
                waitTimer -= Time.fixedDeltaTime;
                if (waitTimer <= 0f)
                {
                    state = 0; // Kembali ke naik cepat
                }
                break;
        }
    }
}
