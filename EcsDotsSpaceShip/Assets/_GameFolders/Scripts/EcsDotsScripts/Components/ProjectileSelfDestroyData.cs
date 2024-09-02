using Unity.Entities;

namespace SpaceShipEcsDots.Components
{
    public struct ProjectileSelfDestroyData : IComponentData
    {
        public float CurrentLifeTime;
        public float MaxLifeTime;
        public bool CanDestroy;
    }
}
