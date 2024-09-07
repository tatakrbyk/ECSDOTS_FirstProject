using Unity.Entities;

namespace SpaceShipEcsDots.Components
{
    public struct EnemySpawnData : IComponentData
    {
        public Entity entity;
        public float MinTime;
        public float MaxTime;
        public float CurrentTime;
        public float RandomTime;
    }
}
