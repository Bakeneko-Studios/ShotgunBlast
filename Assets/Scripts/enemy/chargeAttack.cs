using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chargeAttack : MonoBehaviour
{
    public NavMeshAgent agent;
    public float chargeSpeed = 20;
    public float chargeDuration = 1;
    float chargeTime = 0;
    void charge()
    {
        agent.Move(transform.forward * Time.deltaTime * chargeSpeed);
        //agent.SetDestination(player.position);
    }

    public void attack()
    {
        chargeTime = chargeDuration;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (chargeTime > 0)
        {
            charge();
            chargeTime -= Time.deltaTime;
        }
    }
}
