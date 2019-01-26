using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomZone : MonoBehaviour
{
    public GameObject[] level1;
    public GameObject[] level2;
    public GameObject[] level3;

    public void ActiveComfySpot(int level)
    {
        GameObject currentComfyZone;

        switch (level)
        {
            case 1:
                currentComfyZone = level1[Random.Range(0, level1.Length)];
                currentComfyZone.SetActive(true);
                break;
            case 2:
                currentComfyZone = level2[Random.Range(0, level2.Length)];
                currentComfyZone.SetActive(true);
                break;
            case 3:
                currentComfyZone = level3[Random.Range(0, level3.Length)];
                currentComfyZone.SetActive(true);
                break;
            default:
                break;
        }
        GameObject.FindObjectOfType<GameManager>().ShowRoom();
        GameObject.FindObjectOfType<GameManager>().ShowLevel(level);
    }
}
