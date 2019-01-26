using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComfortUI : MonoBehaviour
{

    public void ChangeComfortNiveau(int c, int m)
    {
        if (c < 0)
            c = 0;

        transform.GetChild(0).localScale = new Vector3((1f / m) * c,
                                                        transform.GetChild(0).localScale.y,
                                                        transform.GetChild(0).localScale.z);
    }
}
