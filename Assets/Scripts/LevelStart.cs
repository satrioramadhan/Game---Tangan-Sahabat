using UnityEngine;

public class LevelStart : MonoBehaviour
{
    void Start()
    {
        GameObject spawn = GameObject.Find("SpawnPoint");
        if (spawn != null)
        {
            PlayerManager.lastCheckPointPos = spawn.transform.position;
        }
    }
}
