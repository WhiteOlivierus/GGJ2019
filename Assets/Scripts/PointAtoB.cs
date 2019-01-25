using UnityEngine.AI;
using UnityEngine;

public class PointAtoB : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public int cycleTime;

    private NavMeshAgent nM;
    private float timer;

    private Vector3 currentTarget;
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
        timer += 1f / 60f;

        if (timer >= cycleTime)
        {
            timer = 0;
            if (currentTarget != targetA)
                currentTarget = targetA;
            else
                currentTarget = targetB;
        }

        nM.SetDestination(currentTarget);
    }
}
