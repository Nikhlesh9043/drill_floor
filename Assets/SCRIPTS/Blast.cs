using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            other.GetComponent<BlockController>().DamageBlock(GameValues.Base1);
        }
        else if (other.gameObject.CompareTag("Container"))
        {
            other.GetComponent<ContainerController>().DamageContainer((int)LevelCreation.GetInstance().levelInfoList[GameValues.CurrentLevel - 1].container_health_value/4);
        }
    }
}
