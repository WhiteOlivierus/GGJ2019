using UnityEngine;

public class ComfySpot : MonoBehaviour
{

    public int comfyPoints;
    public int comfyAddTime;
    public int comfyTime;
    public float comfySort;
    [Space]
    public GameObject objectHighlight;
    public Color highlightColor;
    private PlayerController pc;
    private float timer;
    private float comfyTimeTimer;
    private bool startTimers = false;
    private bool inTrigger = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            pc = col.GetComponent<PlayerController>();
            objectHighlight.GetComponent<Renderer>().material.color = highlightColor;
            inTrigger = true;
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
                GameObject.FindObjectOfType<GameManager>().NextRoomZone();
                gameObject.SetActive(false);
            }
            timer = 0;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            inTrigger = false;
            objectHighlight.GetComponent<Renderer>().material.color = Color.white;
            startTimers = false;
            pc.inComfyZone = false;
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

    void Update()
    {
        if (Input.GetButtonDown("Interact") && inTrigger && !startTimers)
        {
            objectHighlight.GetComponent<Renderer>().material.color = Color.white;
            startTimers = true;
            pc.inComfyZone = true;
        }
        else if (Input.GetButtonDown("Interact") && inTrigger && startTimers)
        {
            objectHighlight.GetComponent<Renderer>().material.color = highlightColor;
            startTimers = false;
            pc.inComfyZone = false;
        }
    }
}
