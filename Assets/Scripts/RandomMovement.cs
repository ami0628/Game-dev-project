using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range; //radius of sphere

    //public Transform centrePoint; //centre of the area the agent wants to move around in
    //transform used instead

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance) //checks if agent is done with path
        {
            Vector3 point;
            if (RandomPoint(transform.position, range, out point)) //pass in our transform point and radius of area
            {
                agent.SetDestination(point);
            }
        }

    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 5.0f, NavMesh.AllAreas))
        {
            //Can modify 3rd argument for range
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }


}