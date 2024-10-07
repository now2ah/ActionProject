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
        GUIDEDPROJECTILE,
        RANGEENEMY_PROJECTILE,
        ARROW_PROJECTILE,
        EXP_ORB
    }

    public class PoolManager : Singleton<PoolManager>
    {
        GameObject _normalEnemyObj;
        GameObject _rangeEnemyObj;

        GameObject _normalProjectileObj;
        GameObject _guidedProjectileObj;
        GameObject _rangeEnemyProjectileObj;
        GameObject _arrowProjectileObj;

        GameObject _expOrbObj;

        ObjectPooler<EnemyUnit> _normalEnemyPool;
        ObjectPooler<EnemyUnit> _rangeEnemyPool;

        ObjectPooler<Projectile> _normalProjectilePool;
        ObjectPooler<Projectile> _guidedProjectilePool;
        ObjectPooler<Projectile> _rangeEnemyProjectilePool;
        ObjectPooler<Projectile> _arrowProjectilePool;

        ObjectPooler<ExpOrb> _expOrbPool;

        public ObjectPooler<EnemyUnit> NormalEnemyPool => _normalEnemyPool;
        public ObjectPooler<EnemyUnit> RangeEnemyPool => _rangeEnemyPool;

        public ObjectPooler<Projectile> NormalProjectilePool => _normalProjectilePool;
        public ObjectPooler<Projectile> GuidedProjectilePool => _guidedProjectilePool;
        public ObjectPooler<Projectile> RangeEnemyProjectilePool => _rangeEnemyProjectilePool;
        public ObjectPooler<Projectile> ArrowProjectilePool => _arrowProjectilePool;

        public ObjectPooler<ExpOrb> ExpOrbPool => _expOrbPool;

        public override void Initialize()
        {
            base.Initialize();
            
            _normalEnemyPool = new ObjectPooler<EnemyUnit>();
            _rangeEnemyPool = new ObjectPooler<EnemyUnit>();
            _normalProjectilePool = new ObjectPooler<Projectile>();
            _guidedProjectilePool = new ObjectPooler<Projectile>();
            _rangeEnemyProjectilePool = new ObjectPooler<Projectile>();
            _arrowProjectilePool = new ObjectPooler<Projectile>();
            _expOrbPool = new ObjectPooler<ExpOrb>();
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
            _normalEnemyObj = Resources.Load("Prefabs/Units/Enemy/NormalSlime") as GameObject;
            _rangeEnemyObj = Resources.Load("Prefabs/Units/Enemy/RangeSlime") as GameObject;
            _normalProjectileObj = Resources.Load("Prefabs/Misc/Projectiles/NormalProjectile") as GameObject;
            _guidedProjectileObj = Resources.Load("Prefabs/Misc/Projectiles/GuidedProjectile") as GameObject;
            _rangeEnemyProjectileObj = Resources.Load("Prefabs/Misc/Projectiles/RangeEnemyProjectile") as GameObject;
            _arrowProjectileObj = Resources.Load("Prefabs/Misc/Projectiles/ArrowSample") as GameObject;
            _expOrbObj = Resources.Load("Prefabs/Misc/ExpOrb") as GameObject;
        }

        void _SetUpAllObjectPools()
        {
            if (_normalEnemyObj.TryGetComponent<NormalEnemy>(out NormalEnemy normalEnemy))
                _normalEnemyPool.Initialize(normalEnemy, GameManager.Instance.Constants.NORMALENEMY_MAX_AMOUNT, this.gameObject);

            if (_rangeEnemyObj.TryGetComponent<RangeEnemy>(out RangeEnemy rangeEnemy))
                _rangeEnemyPool.Initialize(rangeEnemy, GameManager.Instance.Constants.RANGEENEMY_MAX_AMOUNT, this.gameObject);

            if (_normalProjectileObj.TryGetComponent<NormalProjectile>(out NormalProjectile normalProjectile))
                _normalProjectilePool.Initialize(normalProjectile, GameManager.Instance.Constants.NORMALPROJECTILE_MAX_AMOUNT, this.gameObject);

            if (_guidedProjectileObj.TryGetComponent<GuidedProjectile>(out GuidedProjectile guidedProjectile))
                _guidedProjectilePool.Initialize(guidedProjectile, GameManager.Instance.Constants.GUIDEDPROJECTILE_MAX_AMOUNT, this.gameObject);

            if (_rangeEnemyProjectileObj.TryGetComponent<RangeEnemyProjectile>(out RangeEnemyProjectile rangeEnemyProjectile))
                _rangeEnemyProjectilePool.Initialize(rangeEnemyProjectile, GameManager.Instance.Constants.RANGEENEMYPROJECTILE_MAX_AMOUNT, this.gameObject);

            if (_arrowProjectileObj.TryGetComponent<Arrow>(out Arrow arrowProjectile))
                _arrowProjectilePool.Initialize(arrowProjectile, GameManager.Instance.Constants.ARROWPROJECTILE_MAX_AMOUNT, this.gameObject);

            if (_expOrbObj.TryGetComponent<ExpOrb>(out ExpOrb expOrb))
                _expOrbPool.Initialize(expOrb, GameManager.Instance.Constants.EXPORB_MAX_AMOUNT, this.gameObject);
        }
    }
}

