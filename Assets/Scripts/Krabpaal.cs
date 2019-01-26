using UnityEngine;

public class Krabpaal : MonoBehaviour
{
    public int coolDown;
    public Color highlightColor;
    private PlayerController pc;
    private bool startCoolDown = false;
    private float timer;

    void Start()
    {
        pc = GameObject.FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter(Collider col)
    {
        gameObject.GetComponent<Renderer>().material.color = highlightColor;
    }

    void OnTriggerStay(Collider col)
    {
        if (Input.GetButtonDown("Interact") && !startCoolDown)
        {
            print("less stress");
            startCoolDown = true;
            if (pc.stressLevel > 0)
                pc.stressLevel -= 1;
        }
    }

    void OnTriggerExit(Collider col)
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    void FixedUpdate()
    {
        if (timer > coolDown)
        {
            startCoolDown = false;
            timer = 0;
        }

        timer += 1f / 60f;
    }
}
