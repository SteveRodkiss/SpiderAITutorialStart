using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    public enum AIState { idle,moving,attacking};

    public AIState aiState = AIState.idle;

    NavMeshAgent nav;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        switch (aiState)
        {
            case AIState.idle:
                //what we do in idle- switch states
                if (dist < 10f)
                {
                    aiState = AIState.moving;
                    //set the targer destination
                    //nav.SetDestination(player.position);
                }
                break;
            case AIState.moving:
                //what we do switch state
                nav.SetDestination(player.position);
                if (dist > 10f)
                {
                    nav.SetDestination(transform.position);
                    aiState = AIState.idle;
                }
                break;
            case AIState.attacking:
                break;
            default:
                break;
        }


        
    }
}
