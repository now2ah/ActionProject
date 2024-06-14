using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;
using Action.Game;
using Action.Units;

namespace Action.Manager
{
    public enum ePoolType
    {
        NORMAL_ENEMY,
        RANGE_ENEMY,
        NORMALPROJECTILE,
        RANGEENEMY_PROJECTILE,
    }

    public class PoolManager : Singleton<PoolManager>
    {
        GameObject _normalEnemyObj;
        GameObject _rangeEnemyObj;

        GameObject _normalProjectileObj;
        GameObject _rangeEnemyProjectileObj;

        ObjectPooler<EnemyUnit> _normalEnemyPool;
        ObjectPooler<EnemyUnit> _rangeEnemyPool;

        ObjectPooler<Projectile> _normalProjectilePool;
        ObjectPooler<Projectile> _rangeEnemyProjectilePool;

        public ObjectPooler<EnemyUnit> NormalEnemyPool => _normalEnemyPool;
        public ObjectPooler<EnemyUnit> RangeEnemyPool => _rangeEnemyPool;

        public ObjectPooler<Projectile> NormalProjectilePool => _normalProjectilePool;
        public ObjectPooler<Projectile> RangeEnemyProjectilePool => _rangeEnemyProjectilePool;

        public override void Initialize()
        {
            base.Initialize();
            _normalEnemyPool = new ObjectPooler<EnemyUnit>();
            _rangeEnemyPool = new ObjectPooler<EnemyUnit>();
            _normalProjectilePool = new ObjectPooler<Projectile>();
            _rangeEnemyProjectilePool = new ObjectPooler<Projectile>();
            _GetObjectPrefabs();
            _SetUpAllObjectPools();
        }

        public ObjectPooler<EnemyUnit> GetEnemyPool(Enums.eEnemyType type)
        {
            switch (type)
            {
                case Enums.eEnemyType.NORMAL:
                    return _normalEnemyPool;

                case Enums.eEnemyType.RANGE:
                    return _rangeEnemyPool;

                default:
                    return null;
            }
        }

        void _GetObjectPrefabs()
        {
            _normalEnemyObj = Resources.Load("Prefabs/Units/Enemy/NormalEnemy") as GameObject;
            _rangeEnemyObj = Resources.Load("Prefabs/Units/Enemy/RangeEnemy") as GameObject;
            _normalProjectileObj = Resources.Load("Prefabs/Misc/Projectiles/NormalProjectile") as GameObject;
            _rangeEnemyProjectileObj = Resources.Load("Prefabs/Misc/Projectiles/RangeEnemyProjectile") as GameObject;
        }

        void _SetUpAllObjectPools()
        {
            if (_normalEnemyObj.TryGetComponent<NormalEnemy>(out NormalEnemy normalEnemy))
                _normalEnemyPool.Initialize(normalEnemy, GameManager.Instance.Constants.NORMALENEMY_MAX_AMOUNT, this.gameObject);

            if (_rangeEnemyObj.TryGetComponent<RangeEnemy>(out RangeEnemy rangeEnemy))
                _rangeEnemyPool.Initialize(rangeEnemy, GameManager.Instance.Constants.RANGEENEMY_MAX_AMOUNT, this.gameObject);

            if (_normalProjectileObj.TryGetComponent<NormalProjectile>(out NormalProjectile normalProjectile))
                _normalProjectilePool.Initialize(normalProjectile, GameManager.Instance.Constants.NORMALPROJECTILE_MAX_AMOUNT, this.gameObject);

            if (_rangeEnemyProjectileObj.TryGetComponent<RangeEnemyProjectile>(out RangeEnemyProjectile rangeEnemyProjectile))
                _rangeEnemyProjectilePool.Initialize(rangeEnemyProjectile, GameManager.Instance.Constants.RANGEENEMYPROJECTILE_MAX_AMOUNT, this.gameObject);
        }
    }
}

