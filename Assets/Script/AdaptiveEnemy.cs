using UnityEngine;
using UnityEngine.AI;

public class AdaptiveEnemy : MonoBehaviour
{
    public Transform player;

    NavMeshAgent agent;
    QLearning qLearning;

    string currentState;
    string currentAction;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        qLearning = GetComponent<QLearning>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > 10)
        {
            currentState = "PlayerFar";
        }
        else if (distance > 3)
        {
            currentState = "PlayerNear";
        }
        else
        {
            currentState = "PlayerVeryNear";
        }

        currentAction = qLearning.ChooseAction(currentState);
        ExecuteAction(currentAction);
    }

    void ExecuteAction(string action)
    {
        switch (action)
        {
            case "Patrol":
                Patrol();
                break;

            case "Chase":
                Chase();
                break;

            case "Attack":
                Attack();
                break;
        }
    }

    void Patrol()
    {
        agent.isStopped = false;
    }

    void Chase()
    {
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        agent.isStopped = true;

        float reward = 10;

        qLearning.UpdateQValue(currentState, currentAction, reward, currentState);
        Debug.Log("Adaptive Attack");
    }
}
