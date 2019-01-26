using UnityEngine;
using UnityEngine.UI;
public class CatGoals : MonoBehaviour
{
    public Sprite[] goals;
    public GameObject goalObject;
    private GameObject currentGoals;

    void Start()
    {
        NewGoal();
    }

    void Update()
    {

    }

    void NewGoal()
    {
        currentGoals = Instantiate(goalObject);
        currentGoals.transform.SetParent(transform);
        currentGoals.GetComponent<Image>().sprite = goals[Random.Range(0, goals.Length)];
    }
}
