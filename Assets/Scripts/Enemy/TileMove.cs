using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMove : MonoBehaviour
{
    [Header("Arah Gerak (X: 0=Kanan, 1=Kiri | Y: 0=Atas, 1=Bawah)")]
    public int directionX = -1; // 0=Kanan, 1=Kiri, -1=Ga gerak X
    public int directionY = -1; // 0=Atas, 1=Bawah, -1=Ga gerak Y

    public float speed = 1f;
    public float range = 3f;

    private Vector3 startPosition;
    private bool movingForward = true;
    private Vector2 moveDirection;

    void Start()
    {
        startPosition = transform.position;

        float x = 0, y = 0;
        if (directionX == 0) x = 1; // Kanan
        else if (directionX == 1) x = -1; // Kiri

        if (directionY == 0) y = 1; // Atas
        else if (directionY == 1) y = -1; // Bawah

        moveDirection = new Vector2(x, y).normalized;

        if (moveDirection == Vector2.zero)
        {
            Debug.LogWarning(gameObject.name + " moveDirection 0! Setting default ke kanan.");
            moveDirection = Vector2.right;
        }
    }

    void Update()
    {
        Vector3 offset = transform.position - startPosition;

        if (movingForward)
        {
            transform.Translate(moveDirection * speed * Time.deltaTime);
            if (Vector3.Dot(offset, moveDirection) >= range)
                movingForward = false;
        }
        else
        {
            transform.Translate(-moveDirection * speed * Time.deltaTime);
            if (Vector3.Dot(offset, moveDirection) <= 0)
                movingForward = true;
        }
    }
}
