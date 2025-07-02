using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    public string memoID;

    private void Start()
    {
        // Kalau sudah dikumpulkan di runtime, langsung hancurkan
        if (MemoTracker.collectedMemos.Contains(memoID))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (!MemoTracker.collectedMemos.Contains(memoID))
            {
                MemoTracker.collectedMemos.Add(memoID); // Catat sudah dikumpulkan
                PlayerManager.numberOfMemo++;  
                AudioManager.instance.Play("Memo");
                Destroy(gameObject);                    // Hilangkan dari scene
            }
        }
    }
}
