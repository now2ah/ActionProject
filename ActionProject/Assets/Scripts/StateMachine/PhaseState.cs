using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;

namespace Action.State
{
    public class TownBuildState : State
    {

        public override void EnterState()
        {
            base.EnterState();
            GameManager.Instance.StartPhase(eGamePhase.TownBuild);
            GameManager.Instance.AddAllEnemySpawners();
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }

    public class HuntState : State
    {
        public override void EnterState()
        {
            base.EnterState();
            GameManager.Instance.StartPhase(eGamePhase.Hunt);
            GameManager.Instance.SetActiveHuntSpawner(true);
            GameManager.Instance.AddAllEnemySpawners();
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }

        public override void ExitState()
        {
            GameManager.Instance.SetActiveHuntSpawner(false);   
            base.ExitState();
        }
    }

    public class DefenseState : State
    {
        public override void EnterState()
        {
            base.EnterState();
            GameManager.Instance.StartPhase(eGamePhase.Defense);
            for (int i = 0; i < GameManager.Instance.EnemySpawners.Count; i++)
                GameManager.Instance.StartWave(GameManager.Instance.EnemyUnits, 1.0f, i);
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}

