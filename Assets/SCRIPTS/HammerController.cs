using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    float hammer_interval = 0.05f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            Hammer(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Container"))
        {
            HammerContainer(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            Hammer(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Container"))
        {
            HammerContainer(other.gameObject);
        }
    }

    void Hammer(GameObject obj)
    {
        if (!GameValues.GameOver)
        {
            hammer_interval -= Time.deltaTime;

            if (hammer_interval < 0)
            {
                obj.GetComponent<BlockController>().DamageBlock();
                LevelCreation.GetInstance().ScorePop();
                hammer_interval = LevelCreation.GetInstance().levelInfoList[GameValues.CurrentLevel-1].hammer_interval;
            }
        }
    }


    void HammerContainer(GameObject obj)
    {
        if (!GameValues.GameOver)
        {
            hammer_interval -= Time.deltaTime;

            if (hammer_interval < 0)
            {
                obj.GetComponent<ContainerController>().DamageContainer();
                LevelCreation.GetInstance().ScorePop();
                hammer_interval = LevelCreation.GetInstance().levelInfoList[GameValues.CurrentLevel - 1].hammer_interval;
            }
        }
    }
}
