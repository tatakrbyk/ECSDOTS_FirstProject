using Unity.Entities;

namespace SpaceShipEcsDots.Components
{
    public struct ProjectileMoveData : IComponentData
    {
        public float MoveSpeed;
        public float Direction;
    }

}
