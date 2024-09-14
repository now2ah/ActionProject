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
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }

        public override void ExitState()
        {
            foreach (var buildings in GameManager.Instance.PlayerBuildings)
                buildings.SetActive(false);
            base.ExitState();
        }
    }

    public class HuntState : State
    {
        public override void EnterState()
        {
            base.EnterState();
            GameManager.Instance.AddHuntSpawner();
            GameManager.Instance.SetActiveHuntSpawner(true);
            
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }

        public override void ExitState()
        {
            GameManager.Instance.SetActiveHuntSpawner(false);
            GameManager.Instance.EnemySpawners.Clear();
            foreach (GameObject unit in GameManager.Instance.EnemyUnits)
            {
                unit.SetActive(false);
                //if(unit.TryGetComponent<Units.EnemyUnit>(out Units.EnemyUnit comp))
            }
            base.ExitState();
        }
    }

    public class DefenseState : State
    {
        public override void EnterState()
        {
            base.EnterState();
            foreach (var buildings in GameManager.Instance.PlayerBuildings)
                buildings.SetActive(true);
            GameManager.Instance.FindSpawnerPoint();
            GameManager.Instance.AddDefenseSpawner();
            GameManager.Instance.SetActiveDefenseSpawner(true);
            GameManager.Instance.SetDefenseSpawner();
            GameManager.Instance.CommanderUnit.NavMeshAgentComp.Warp(GameManager.Instance.StartPos);
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }

        public override void ExitState()
        {
            GameManager.Instance.EnemySpawners.Clear();
            GameManager.Instance.SetActiveDefenseSpawner(false);
            base.ExitState();
        }
    }
}

