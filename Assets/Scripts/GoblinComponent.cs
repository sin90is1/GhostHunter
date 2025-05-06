using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class GoblinComponent : MonoBehaviour
{
    public NavMeshAgent agent;
    public float Speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = Camera.main.transform.position;

        agent.SetDestination(targetPosition);
        agent.speed = Speed;
    }
}
