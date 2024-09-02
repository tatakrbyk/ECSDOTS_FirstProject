using Unity.Entities;
using Unity.Mathematics;
namespace SpaceShipEcsDots.Components
{
    public struct EnemyMoveTargetData : IComponentData
    {
        public float3 Target;
    }

}
