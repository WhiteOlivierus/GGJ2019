using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] roomZones;
    public Sprite[] rooms;
    public Sprite[] levels;
    public Image room;
    public Image level;
    private int roomIndex;
    void Start() { NextRoomZone(); }

    public void NextRoomZone()
    {
        roomIndex = Random.Range(0, roomZones.Length);
        roomZones[roomIndex].GetComponent<RoomZone>().ActiveComfySpot(Random.Range(1, 3));
    }

    public void ShowRoom()
    {
        room.sprite = rooms[roomIndex];
    }

    public void ShowLevel(int l)
    {
        level.sprite = levels[l - 1];
    }
}
