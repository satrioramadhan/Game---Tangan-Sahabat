using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semak : MonoBehaviour
{
    public float speed = 2;
    int dir = 1;

    public Transform semakVisual;  // drag child Semak (sprite) ke sini di inspector
    public float rotationSpeed = 100f;

    void FixedUpdate()
    {
        // Gerak semak ke kiri / kanan
        transform.Translate(Vector2.right * speed * dir * Time.fixedDeltaTime);

        // Rotasi visual semak
        float rotDirection = dir == 1 ? -1 : 1;  // CW saat ke kanan, CCW saat ke kiri
        semakVisual.Rotate(Vector3.forward * rotDirection * rotationSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WallRight"))
        {
            dir = -1;
        }
        else if (other.CompareTag("WallLeft"))
        {
            dir = 1;
        }
    }
}
