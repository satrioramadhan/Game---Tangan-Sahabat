using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerManager.lastCheckPointPos = transform.position;
            GetComponent<SpriteRenderer>().color = Color.white;
            AudioManager.instance.Play("CheckPoint");
        }
    }
}
