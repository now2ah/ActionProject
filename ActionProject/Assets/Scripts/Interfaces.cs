using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Action
{
    public interface IMovable
    {
        NavMeshAgent NavMeshAgentComp { get; set; }
        float Speed { get; set; }   //없어도 될지도?
        void SetSpeed(float speed);

    }
}
