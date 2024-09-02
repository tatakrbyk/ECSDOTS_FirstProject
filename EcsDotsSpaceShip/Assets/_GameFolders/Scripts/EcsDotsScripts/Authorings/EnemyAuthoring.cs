using SpaceShipEcsDots.Components;
using Unity.Entities;
using UnityEngine;

namespace SpaceShipEcsDots.Authorings
{

    class EnemyAuthoring : MonoBehaviour
    {
        public float MoveSpeed = 3f;

    }

    class EnemyAuthoringBaker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<EnemyMoveTargetData>(entity);
            AddComponent<EnemyTag>(entity);
            AddComponent(entity, new EnemyMoveData()
            {
                MoveSpeed = authoring.MoveSpeed,
                canPassNextTarget = false
            });
        }
    }
}
