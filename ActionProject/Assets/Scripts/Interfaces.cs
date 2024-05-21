using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Action
{
    public interface IMovable
    {
        NavMeshAgent NavMeshAgentComp { get; set; }
        float Speed { get; set; }   //��� ������?
        void SetSpeed(float speed);

    }
}
