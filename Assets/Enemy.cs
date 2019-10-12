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
    public Animator animator;

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
                OnIdle();
                break;
            case AIState.moving:
                OnMoving(dist);
                break;
            case AIState.attacking:
                OnAttacking(dist);
                break;
            default:
                break;
        }        
    }

    private void OnAttacking(float dist)
    {
        nav.SetDestination(transform.position);
        if (dist > 2f)
        {
            //we are out of attack range
            aiState = AIState.idle;
            animator.SetBool("attacking", false);
            animator.SetBool("walking", false);
        }
    }

    private void OnIdle()
    {
        //what we do in idle- switch states
        if (CanSeePlayer())
        {
            aiState = AIState.moving;
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }
    }

    void OnMoving(float distance)
    {
        //what we do switch state
        if (CanSeePlayer())
        {
            nav.SetDestination(player.position);
            animator.SetBool("walking", true);
            if (distance < 2f)
            {
                aiState = AIState.attacking;
                animator.SetBool("attacking",true);
                
            }
        }
        else
        {
            nav.SetDestination(transform.position);
            aiState = AIState.idle;
            animator.SetBool("walking", false);
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
