using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : StateMachineBehaviour
{
    float timer;
    List<Transform> wayPoints1= new List<Transform>();
    NavMeshAgent agent;
    Transform player;
    float chaseRange=3;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
    player = GameObject.FindGameObjectWithTag("Player").transform;
    agent= animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
    timer=0;
    GameObject go = GameObject.FindGameObjectWithTag("WayPoints1");
    foreach (Transform t in go.transform)
    {
        wayPoints1.Add(t);
    }
    agent.SetDestination(wayPoints1[Random.Range(0,wayPoints1.Count)].position);
    
       
   }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        float distance= Vector3.Distance(player.position, animator.transform.position);

        if(agent.remainingDistance<=agent.stoppingDistance)
            agent.SetDestination(wayPoints1[Random.Range(0,wayPoints1.Count)].position);
            agent.speed=1f;
        timer+= Time.deltaTime;
        if (timer>10 && distance>chaseRange)
            animator.SetBool("IsPatrolling",false);
        
        if(distance<chaseRange)
             {agent.SetDestination(player.position);
                agent.speed=2.5f;
             }

        if(distance<2 &&timer>0.75f)
             {agent.SetDestination(player.position);
                animator.SetBool("Attack",true);
             }
           
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
        timer=0;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }
}
