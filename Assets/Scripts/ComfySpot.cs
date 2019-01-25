using UnityEngine;

public class ComfySpot : MonoBehaviour
{

    public int comfyPoints;
    public int comfyAddTime;
    public int comfyTime;

    public GameObject billboardText;

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
            billboardText.gameObject.SetActive(true);
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
                Destroy(gameObject);
            }
            timer = 0;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            inTrigger = false;
            billboardText.gameObject.SetActive(false);
            billboardText.GetComponent<TextMesh>().text = "Press E to relax";
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
            billboardText.GetComponent<TextMesh>().text = "Press E to stop relaxing";
            startTimers = true;
            pc.inComfyZone = true;
        }
        else if (Input.GetButtonDown("Interact") && inTrigger && startTimers)
        {
            billboardText.GetComponent<TextMesh>().text = "Press E to relax";
            startTimers = false;
            pc.inComfyZone = false;
        }

        billboardText.gameObject.transform.LookAt(2 * billboardText.gameObject.transform.position - Camera.main.transform.position);
    }
}
