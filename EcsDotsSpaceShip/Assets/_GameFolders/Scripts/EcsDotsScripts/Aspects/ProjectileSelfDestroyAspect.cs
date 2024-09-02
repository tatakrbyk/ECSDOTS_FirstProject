using SpaceShipEcsDots.Components;
using Unity.Entities;

namespace SpaceShipEcsDots.Aspects
{
    public readonly partial struct ProjectileSelfDestroyAspect : IAspect
    {
        public readonly Entity entity;
        private readonly RefRW<ProjectileSelfDestroyData> _projecttileSelfDestroyDataRW;

        public bool CanDestroy => _projecttileSelfDestroyDataRW.ValueRW.CanDestroy;

        public void BackCountingProcess(float deltaTime)
        {
            _projecttileSelfDestroyDataRW.ValueRW.CurrentLifeTime += deltaTime;

            if (_projecttileSelfDestroyDataRW.ValueRW.CurrentLifeTime > _projecttileSelfDestroyDataRW.ValueRW.MaxLifeTime)
            {
                _projecttileSelfDestroyDataRW.ValueRW.CanDestroy = true;
            }
        }
    }

}
