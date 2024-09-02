using Unity.Entities;


namespace SpaceShipEcsDots.Components
{
    public struct EnemyMoveData : IComponentData
    {
        public float MoveSpeed;
        public bool canPassNextTarget;
    }
}

