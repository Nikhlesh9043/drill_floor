using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    Transform _target;
    Animator animator;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameValues.GameOver)
        {
            if (_target != null) agent.SetDestination(_target.position);
        }
    }

    public void AssignTarget(Transform obj)
    {
        _target = obj;
        agent.speed = 5;
    }

    public void Celebrate(Vector3 target)
    {
        //animator.Play("lift");
        //agent.isStopped = true;
        agent.speed = 2;
        agent.SetDestination(target);
    }
}
