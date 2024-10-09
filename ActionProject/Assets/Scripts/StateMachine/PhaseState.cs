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
            GameManager.Instance.CheckConstructBuilding();
            foreach (var buildings in GameManager.Instance.PlayerBuildings)
                buildings.SetActive(false);
            GameManager.Instance.AutoSave();
            base.ExitState();
        }
    }

    public class HuntState : State
    {
        public override void EnterState()
        {
            base.EnterState();
            UIManager.Instance.ExpBarUI.Show();
            Units.PlayerUnitData data = GameManager.Instance.CommanderUnit.UnitData as Units.PlayerUnitData;
            UIManager.Instance.ExpBarUI.ApplyExpValue(data.exp, data.nextExp);
            GameManager.Instance.AddHuntSpawner();
            GameManager.Instance.SetActiveHuntSpawner(true);
            GameManager.Instance.CommanderUnit.SetEnableAutoAttacks(true);
            UIManager.Instance.BaseIndicatorUI.Hide();
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
                unit.SetActive(false);
            UIManager.Instance.BaseIndicatorUI.Show();
            UIManager.Instance.ExpBarUI.Hide();
            base.ExitState();
        }
    }

    public class DefenseState : State
    {
        public override void EnterState()
        {
            base.EnterState();
            CameraManager.Instance.CreateFixedVirtualCamera();
            //UIManager.Instance.CreateTownStagePanel();
            GameManager.Instance.InactiveDefaultBuildings();
            foreach (var buildings in GameManager.Instance.PlayerBuildings)
                buildings.SetActive(true);
            GameManager.Instance.FindSpawnerPoint();
            GameManager.Instance.AddDefenseSpawner();
            GameManager.Instance.SetActiveDefenseSpawner(true);
            GameManager.Instance.SetDefenseSpawner();
            GameManager.Instance.CommanderUnit.NavMeshAgentComp.Warp(GameManager.Instance.StartPos);
            GameManager.Instance.StartWave(GameManager.Instance.EnemyUnits, 1.0f, GameManager.Instance.DefenseSpawner.GetComponent<Spawner>());
            GameManager.Instance.CommanderUnit.SetEnableAutoAttacks(false);
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }

        public override void ExitState()
        {
            GameManager.Instance.EnemySpawners.Clear();
            GameManager.Instance.SetActiveDefenseSpawner(false);
            GameManager.Instance.OnFinishLevel?.Invoke();
            base.ExitState();
        }
    }
}

