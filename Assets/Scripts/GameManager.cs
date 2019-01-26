using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] roomZones;

    void Start() { NextRoomZone(); }

    public void NextRoomZone()
    {
        roomZones[Random.Range(0, roomZones.Length)].GetComponent<RoomZone>().ActiveComfySpot(Random.Range(1, 3));
    }
}
