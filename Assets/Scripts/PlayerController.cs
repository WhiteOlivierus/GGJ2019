using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float sprintSpeed;
    public float rotationSpeed;
    public float jumpVelocity;
    public float fallMultiplier;
    [Space]
    public int comfyPoints = 100;
    public int notComfyPoints;
    public int nonComfyRemoveTime;
    public bool inComfyZone = false;
    [Space]
    public int stressLevel = 0;
    [Space]
    public int miauwRadius;

    private float nonComfyTimer;
    private Rigidbody rb;
    private bool grounded = true;
    private Text scoreText;
    private Text stressScoreText;
    private CapsuleCollider col;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        scoreText = GameObject.Find("ComfyScore").GetComponent<Text>();
        stressScoreText = GameObject.Find("StressScore").GetComponent<Text>();
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        Move();
        Jump();
        Crouch();
        Miauw();
        ChangePoints();
    }

    void FixedUpdate()
    {
        nonComfyTimer += 1f / 60f;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Floor"))
            grounded = true;
    }

    void Move()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        if (Input.GetButton("Sprint") && grounded)
        {
            translation *= sprintSpeed;
        }

        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
    }

    void Jump()
    {

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity += Vector3.up * jumpVelocity;
            grounded = false;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    void Crouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            col.radius /= 2f;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            col.radius *= 2f;
        }
    }

    void Miauw()
    {
        if (Input.GetButtonDown("Miauw"))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, miauwRadius, 1 << 9);

            if (hitColliders.Length > 0)
            {
                foreach (Collider col in hitColliders)
                {
                    print(col.gameObject.name);
                    col.gameObject.GetComponent<PointAtoB>().Investigate(transform.position, true);
                }
            }
        }
    }

    void ChangePoints()
    {
        if (!inComfyZone && nonComfyTimer > nonComfyRemoveTime)
        {
            nonComfyTimer = 0;
            comfyPoints -= notComfyPoints;
        }
        scoreText.text = comfyPoints.ToString();
        stressScoreText.text = stressLevel.ToString();
    }

}
