using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float sprintSpeed;
    public float rotationSpeed;
    public float jumpVelocity;
    public float fallMultiplier;

    private Rigidbody rb;
    private bool grounded = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        if (Input.GetButton("Sprint"))
        {
            translation *= sprintSpeed;
        }

        transform.Translate(0, 0, translation);

        transform.Rotate(0, rotation, 0);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = Vector3.up * jumpVelocity;
            grounded = false;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            print(col.gameObject.layer);
            grounded = true;
        }
    }
}
