using UnityEngine.AI;
using UnityEngine;

public class PointAtoB : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public int cycleTime;
    public Vector3 currentTarget;
    [Space]
    public bool investigating = false;
    public int investigationTime;

    private NavMeshAgent nM;
    private float timer;

    private Vector3 targetA;
    private Vector3 targetB;

    void Start()
    {
        nM = GetComponent<NavMeshAgent>();
        targetA = transform.InverseTransformPoint(pointA.position);
        targetB = transform.InverseTransformPoint(pointB.position);
        currentTarget = targetA;
    }

    void FixedUpdate()
    {
        if (nM.remainingDistance <= .2f)
        {
            timer += 1f / 60f;
        }
        if (!investigating)
        {
            if (timer >= cycleTime)
            {
                timer = 0;
                if (currentTarget != targetA)
                    currentTarget = targetA;
                else
                    currentTarget = targetB;
            }
        }
        else
        {
            if (timer >= investigationTime)
            {
                timer = 0;
                currentTarget = targetA;
            }
        }
        nM.SetDestination(currentTarget);
    }

    public void Investigate(Vector3 cat, bool goLook)
    {
        currentTarget = cat;
        investigating = goLook;
        timer = 0;
    }
}
