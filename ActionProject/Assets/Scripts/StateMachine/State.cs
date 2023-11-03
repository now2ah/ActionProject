using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.State
{
    public class State
    {
        public virtual void EnterState() { }
        public virtual void ExitState() { }
        public virtual void UpdateState() { }
    }
}
