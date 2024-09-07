using SpaceShipEcsDots.Components;
using Unity.Entities;
using UnityEngine;

namespace SpaceShipEcsDots.Authorings
{

    class EnemyAuthoring : MonoBehaviour
    {
        public GameObject ProjectilePrefab;
        public float MoveSpeed = 3f;
        public float MaxFireRandomTime = 5f;
        public float MinFireRandomTime = 1f;
        public int MaxHealth = 1;

    }

    class EnemyAuthoringBaker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<EnemyMoveTargetData>(entity);
            AddComponent<EnemyTag>(entity);
            AddComponent<EnemyPathData>(entity);
            AddComponent<DestroyData>(entity);

         

            AddComponent(entity, new HealthData()
            {
                MaxHealth = authoring.MaxHealth,
                CurrentHealth = authoring.MaxHealth,
            });

            AddComponent(entity, new AttackRandomTimeData()
            {
                MinFireRandomTime =  authoring.MinFireRandomTime,
                MaxFireRandomTime =  authoring.MaxFireRandomTime,
            });

            AddComponent(entity, new AttackData()
            {
                MaxFireTime = Random.Range(authoring.MinFireRandomTime, authoring.MaxFireRandomTime),
                Projectile = GetEntity(authoring.ProjectilePrefab, TransformUsageFlags.Dynamic),
                CurrentFireTime = 0f
            }) ;

            AddComponent(entity, new EnemyMoveData()
            {
                MoveSpeed = authoring.MoveSpeed,
                canPassNextTarget = false
            });
        }
    }
}
