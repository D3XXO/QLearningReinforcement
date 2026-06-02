using System.Collections.Generic;
using UnityEngine;

public class QLearning : MonoBehaviour
{
    public Dictionary<string, float> qTable = new Dictionary<string, float>();

    public float learningRate;
    public float discountFactor;
    public float explorationRate;

    public string[] actions = {"Patrol", "Chase", "Attack"};

    public string ChooseAction(string state)
    {
        if (Random.value < explorationRate)
        {
            return actions[Random.Range(0, actions.Length)];
        }

        float maxQ = float.MinValue;
        string bestAction = actions[0];

        foreach (string action in actions)
        {
            string key = state + action;

            if (!qTable.ContainsKey(key))
            {
                qTable[key] = 0;
            }

            if (qTable[key] > maxQ)
            {
                maxQ = qTable[key];
                bestAction = action;
            }
        }

        return bestAction;
    }

    public void UpdateQValue(string state, string action, float reward, string nextState)
    {
        string key = state + action;

        if (!qTable.ContainsKey(key))
        {
            qTable[key] = 0;
        }

        float maxNextQ = 0;

        foreach (string nextAction in actions)
        {
            string nextKey = nextState + nextAction;

            if (!qTable.ContainsKey(nextKey))
            {
                qTable[nextKey] = 0;
            }

            maxNextQ = Mathf.Max(maxNextQ, qTable[nextKey]);
        }

        qTable[key] = qTable[key] + learningRate *(reward + discountFactor * maxNextQ - qTable[key]);
    }
}
