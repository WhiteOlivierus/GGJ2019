﻿using UnityEngine;

public class StressMaker : MonoBehaviour
{
    public int cooldown;
    public int fieldOfView;
    private float timer;
    private bool startCooldown = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && !startCooldown)
        {
            Vector3 direction = col.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfView / 2)
            {
                print(angle);
                col.GetComponent<PlayerController>().stressLevel += 1;
                startCooldown = true;
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
