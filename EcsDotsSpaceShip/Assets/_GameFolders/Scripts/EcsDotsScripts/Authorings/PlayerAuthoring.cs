using SpaceShipEcsDots.Components;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Rendering;

namespace SpaceShipEcsDots.Authorings
{
    public class PlayerAuthoring : MonoBehaviour
    {
        public float MoveSpeed = 5f;
        public float Horizontal = 2f;
        public float Vertical1;
        public float Vertical2;
    }

    // make Entity
    public class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic); // Set Component

            AddComponent(entity, new MoveData()
            {
                MoveSpeed = authoring.MoveSpeed,
            });

            AddComponent(entity, new MoveBorderData()
            {
                Horizontal = authoring.Horizontal,
                Vertical1 = authoring.Vertical1,
                Vertical2 = authoring.Vertical2,
            });

            AddComponent<InputData>(entity);
            AddComponent<PlayerTag>(entity);
        }
    }
}
