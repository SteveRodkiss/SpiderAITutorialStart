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
                if (CanSeePlayer())
                {
                    aiState = AIState.moving;
                    //set the targer destination
                    //nav.SetDestination(player.position);
                }
                break;
            case AIState.moving:
                OnMoving(dist);
                break;
            case AIState.attacking:
                nav.SetDestination(transform.position);
                if (dist > 2f)
                {
                    //we are out of attack range
                    aiState = AIState.idle;
                }
                break;
            default:
                break;
        }        
    }

    void OnMoving(float distance)
    {
        //what we do switch state
        if (CanSeePlayer())
        {
            nav.SetDestination(player.position);
            if (distance < 2f)
            {
                aiState = AIState.attacking;
            }
        }
        else
        {
            nav.SetDestination(transform.position);
            aiState = AIState.idle;
        }
    }




    bool CanSeePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, direction);
        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray,out RaycastHit hit,10f))
        {
            if (hit.collider.tag == "Player" && angle < 45f)
            {
                //hit the player
                return true;
            }
        }
        return false;
    }



}
