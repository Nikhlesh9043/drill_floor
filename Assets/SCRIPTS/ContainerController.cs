using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerController : MonoBehaviour
{
    [HideInInspector] public int health_value = 50;
    public GameObject car;
    public GameObject fractured_roof;
    public GameObject fractured_container_no_roof;
    public GameObject fractured_container;


    public void DamageContainer()
    {
        //block_animator.Play("pop");
        health_value -= 1;

        if (health_value < 0)
        {
            DestroyContainer();
        }
    }
    public void DamageContainer(int damage_value)
    {
        //block_animator.Play("pop");
        health_value -= damage_value;

        if (health_value < 0)
        {
            DestroyContainer();
        }
    }

    void DestroyContainer()
    {
        Gameplay.GetInstance().GameOver(true);

        //fractured_container_no_roof.SetActive(true);
        fractured_roof.SetActive(true);
        fractured_container.SetActive(true);

        car.SetActive(true);
        Invoke("DisablePlayer", 1.5f);


        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    void DisablePlayer()
    {
        car.GetComponent<Animator>().Play("runaway");
        //fractured_container_no_roof.SetActive(false);
        fractured_container.SetActive(true);
    }
}
