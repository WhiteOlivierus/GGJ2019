using UnityEngine.UI;
using UnityEngine;

public class StressUI : MonoBehaviour
{
    public Sprite[] stressNiveau;
    private int lastN;

    public void ChangeStressNiveau(int n)
    {
        if (n != lastN)
        {
            if (n < stressNiveau.Length)
            {
                GetComponent<Image>().sprite = stressNiveau[n];
                transform.GetChild(0).Rotate(new Vector3(0, 0, -40f));
            }
            lastN = n;
        }
    }
}
