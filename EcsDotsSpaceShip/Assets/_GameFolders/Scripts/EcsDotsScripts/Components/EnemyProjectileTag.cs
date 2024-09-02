using Unity.Entities;

namespace SpaceShipEcsDots.Components
{
    public struct EnemyProjectileTag : IComponentData
    {
        public float MoveSpeed;
        public float Direction;
    }

}
