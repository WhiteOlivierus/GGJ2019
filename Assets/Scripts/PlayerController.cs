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
    public int maxComfy;
    public int notComfyPoints;
    public int nonComfyRemoveTime;
    [HideInInspector]
    public bool inComfyZone = false;
    [HideInInspector]
    public int stressLevel;
    [Space]
    public int miauwRadius;

    private float nonComfyTimer;
    private Rigidbody rb;
    private bool grounded = true;
    private ComfortUI scoreText;
    private StressUI stressScoreText;
    private CapsuleCollider col;
    [HideInInspector]
    public Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        scoreText = GameObject.Find("ComfyScore").GetComponent<ComfortUI>();
        stressScoreText = GameObject.Find("StressScore").GetComponent<StressUI>();
        col = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        // Crouch();
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
        {
            grounded = true;
            anim.SetBool("Jump", false);
        }
    }

    void Move()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        if (translation > 0.01f && !anim.GetBool("Jump"))
        {
            anim.SetBool("Liggen", false);
            if (Input.GetButton("Sprint"))
            {
                translation *= sprintSpeed;
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);
            }
            else
            {
                anim.SetBool("Walk", true);
                anim.SetBool("Run", false);
            }
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        if (Input.GetButtonUp("Sprint"))
            anim.SetBool("Run", false);

        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
    }

    void Jump()
    {

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity += Vector3.up * jumpVelocity;
            anim.SetBool("Jump", true);
            anim.SetBool("Run", false);
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
            comfyPoints -= notComfyPoints * stressLevel;
        }
        scoreText.ChangeComfortNiveau(comfyPoints, maxComfy);
        stressScoreText.ChangeStressNiveau(stressLevel);
    }

}
