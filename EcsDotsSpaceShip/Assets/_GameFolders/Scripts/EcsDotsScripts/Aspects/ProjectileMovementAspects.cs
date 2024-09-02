using SpaceShipEcsDots.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SpaceShipEcsDots.Aspects
{
    // JobSystem içine yqzmak yerine Aspecte yazýyor ve modüler olarak kullanabiliyoruz.
    public readonly partial struct ProjectileMovementAspects : IAspect
    {
        public readonly Entity entity;
        private readonly RefRW<LocalTransform> _localTransformRW;
        private readonly RefRO<ProjectileMoveData> _projectileMoveRO;

        public void MoveProcess(float deltaTime)
        {
            float3 direction = new float3(0f, _projectileMoveRO.ValueRO.Direction, 0f);
            _localTransformRW.ValueRW.Position += deltaTime * _projectileMoveRO.ValueRO.MoveSpeed * direction;
        }
    }

}
