using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienAnimation : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
    [SerializeField] float dampTime = 0.1f;

    static readonly int SpeedHash = Animator.StringToHash("Speed");

    void Awake()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (anim == null) anim = GetComponent<Animator>();
    }

    void Update()
    {
        float speed = (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
        ? agent.velocity.magnitude
        : 0f;

        anim.SetFloat(SpeedHash, speed, dampTime, Time.deltaTime);
    }
}
