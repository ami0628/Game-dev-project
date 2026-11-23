using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

[RequireComponent(typeof(NavMeshAgent))]
public class RandomMovement : MonoBehaviour
{
    [Header("Movement")]
    public float range = 10f; //radius of sphere
    public float minDistance = 3f;
    public float viewAngle = 120f;  // total viewing angle in front of alien

    public float minWait = 2f;
    public float maxWait = 5f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Wander());
    }

    IEnumerator Wander()
    {
        yield return new WaitForSeconds(Random.Range(0f, maxWait)); // random first pause so NPC's don't move all together

        while (true)
        {
            Vector3 start = transform.position;

            PickDestination();
            yield return null;

            float giveUp = Time.time + 15f;

            while ((agent.pathPending || agent.remainingDistance > agent.stoppingDistance) && Time.time < giveUp)
            {
                yield return null;
            }
            agent.ResetPath();

            if (Vector3.Distance(start, transform.position) < minDistance)
            {
                transform.Rotate(0f, 180f, 0f);
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(minWait, maxWait));
            }
        }
    }

    void PickDestination()
    {
        float angle = Random.Range(-viewAngle / 2f, viewAngle / 2f);
        Vector3 dir = Quaternion.Euler(0f, angle, 0f) * transform.forward;
        Vector3 target = transform.position + dir * Random.Range(minDistance, range);

        // snap to navmesh if target has crossed boundary
        if (NavMesh.SamplePosition(target, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}

