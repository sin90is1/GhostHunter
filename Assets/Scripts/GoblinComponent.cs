using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class GoblinComponent : MonoBehaviour
{
    public float EatDistance = 0.3f;
    public Animator animator;
    public NavMeshAgent agent;
    public float Speed = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject GetClosestFairy()
    { 
        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        List<GameObject> FairiesList = FairySpawnners.instance.SpawnedFairies;

        foreach (GameObject item in FairiesList)
        {
            Vector3 GoblinPosition = transform.position;
            GoblinPosition.y = 0f;
            Vector3 FairyPosition = item.transform.position;
            FairyPosition.y = 0f;

            float d = Vector3.Distance(GoblinPosition, FairyPosition);

            if (d < minDistance)

            {
                minDistance = d;
                closest = item;
            }
        }

        if(minDistance <=EatDistance)
        {
            FairySpawnners.instance.DestroyFairy(closest);
        }

        return closest;
    }
    // Update is called once per frame
    void Update()
    {
        if(!agent.enabled)
            return;

        GameObject closest = GetClosestFairy();

        if(closest)
        {
            Vector3 targetPosition = closest.transform.position;
            agent.SetDestination(targetPosition);
            agent.speed = Speed;
        }

    }

    public void Die()
    {
        agent.enabled = false;
        animator.SetTrigger("Death");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
