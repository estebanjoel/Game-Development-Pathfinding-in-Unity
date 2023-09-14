using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    List<AgentAStar> agents;
    
    void Awake()
    {
        agents = new List<AgentAStar>();
        agents.AddRange(GetComponentsInChildren<AgentAStar>());
        Invoke("VeryLateStart", 3);
    }

    void VeryLateStart()
    {
        foreach(AgentAStar agent in agents)
        {
            agent.FollowPath();
        }
    }
}
