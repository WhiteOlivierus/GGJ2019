using UnityEngine;

public class StressMaker : MonoBehaviour
{
    public int cooldown;
    public int fieldOfView;
    public AudioClip nani;
    private float timer;
    private bool startCooldown = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && !startCooldown)
        {
            print("detected");
            Vector3 direction = col.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfView)
            {
                print(angle);
                col.GetComponent<PlayerController>().stressLevel += 1;
                startCooldown = true;
                GetComponent<AudioSource>().clip = nani;
                GetComponent<AudioSource>().Play();
            }
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
