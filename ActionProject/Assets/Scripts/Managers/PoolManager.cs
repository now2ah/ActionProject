using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;
using Action.Game;

namespace Action.Manager
{
    public enum ePoolType
    {
        NORMALPROJECTILE,
        RANGEENEMY_PROJECTILE,
    }

    public class PoolManager : Singleton<PoolManager>
    {
        GameObject _normalProjectileObj;
        GameObject _rangeEnemyProjectileObj;

        ObjectPooler<Projectile> _normalProjectilePool;
        ObjectPooler<Projectile> _rangeEnemyProjectilePool;

        public ObjectPooler<Projectile> NormalProjectilePool => _normalProjectilePool;
        public ObjectPooler<Projectile> RangeEnemyProjectilePool => _rangeEnemyProjectilePool;

        public override void Initialize()
        {
            base.Initialize();
            _normalProjectilePool = new ObjectPooler<Projectile>();
            _rangeEnemyProjectilePool = new ObjectPooler<Projectile>();
            _GetObjectPrefabs();
            _SetUpAllObjectPools();
        }

        void _GetObjectPrefabs()
        {
            _normalProjectileObj = Resources.Load("Prefabs/Misc/Projectiles/NormalProjectile") as GameObject;
            _rangeEnemyProjectileObj = Resources.Load("Prefabs/Misc/Projectiles/RangeEnemyProjectile") as GameObject;
        }

        void _SetUpAllObjectPools()
        {
            if (_normalProjectileObj.TryGetComponent<NormalProjectile>(out NormalProjectile normalProjectile))
                _normalProjectilePool.Initialize(normalProjectile, 50, this.gameObject);

            if (_rangeEnemyProjectileObj.TryGetComponent<RangeEnemyProjectile>(out RangeEnemyProjectile rangeEnemyProjectile))
                _rangeEnemyProjectilePool.Initialize(rangeEnemyProjectile, 50, this.gameObject);
        }
    }
}

