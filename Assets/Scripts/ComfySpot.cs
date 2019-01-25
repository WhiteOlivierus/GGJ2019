using UnityEngine;

public class ComfySpot : MonoBehaviour
{

    public int comfyPoints;
    public int comfyAddTime;
    public int comfyTime;

    private PlayerController pc;
    private float timer;
    private float comfyTimeTimer;
    private bool startTimers = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            pc.inComfyZone = true;
            print(startTimers);
            startTimers = true;
            pc = col.GetComponent<PlayerController>();
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (timer >= comfyAddTime)
        {
            if (comfyTimeTimer < comfyTime)
                pc.comfyPoints += comfyPoints;
            else
            {
                pc.inComfyZone = false;
                Destroy(gameObject);
            }
            timer = 0;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            startTimers = false;
            print(startTimers);
            pc = col.GetComponent<PlayerController>();
        }
    }

    void FixedUpdate()
    {

        if (startTimers)
        {
            timer += 1f / 60f;
            comfyTimeTimer += 1f / 60f;
        }
    }

}
