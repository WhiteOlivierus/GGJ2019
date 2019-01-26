using UnityEngine;

public class StressMaker : MonoBehaviour
{
    public int cooldown;
    private float timer;
    private bool startCooldown = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && !startCooldown)
        {
            col.GetComponent<PlayerController>().stressLevel += 1;
            startCooldown = true;
        }
    }

    void FixedUpdate()
    {
        if (startCooldown)
            timer += 1f / 60f;

        if (timer > cooldown)
        {
            timer = 0f;
            startCooldown = false;
        }
    }
}
