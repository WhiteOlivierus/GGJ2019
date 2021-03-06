﻿using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{

    public Transform target;
    public float distance = 10.0f;
    public float height = 5.0f;
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;
    public Vector3 offset;


    void LateUpdate()
    {
        if (!target) return;

        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        Vector3 newPos = target.position;
        newPos -= currentRotation * Vector3.forward * distance;
        newPos.y = currentHeight;

        //transform.position = target.position;
        //transform.position -= currentRotation * Vector3.forward * distance;

        //transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        RaycastHit[] hits = Physics.RaycastAll(target.position, newPos-target.position, 10f);
        foreach(RaycastHit hit in hits)
        {
            if(hit.transform.gameObject.tag != "player")
            {
                //newPos = hit.point - (target.position - newPos).normalized;
                break;
            }
        }


        transform.position = newPos;

        transform.LookAt(target.position + offset);
    }
}