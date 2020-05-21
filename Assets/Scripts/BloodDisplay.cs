using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDisplay : MonoBehaviour
{
    public List<GameObject> bleed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.blood <= 5 && GameManager.Instance.blood >= 0)
        {
            for (int i = 0; i < 5 - GameManager.Instance.blood; ++i)
            {
                bleed[i].SetActive(false);
            }
            for (int i = 5 - GameManager.Instance.blood; i < 5; ++i)
            {
                bleed[i].SetActive(true);
            }
        }
    }
}
