using SpaceShipEcsDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace SpaceShipEcsDots.Systems
{

    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(AttackSystem))]
    partial struct AttackRandomTimeSetSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var elapsedTime = SystemAPI.Time.ElapsedTime;
            new AttackRandomTimeSetJob()
            {
                ElapsedTime = (float)elapsedTime,
            }.ScheduleParallel();
        }    
    }

    [BurstCompile]
    public partial struct AttackRandomTimeSetJob : IJobEntity
    {
        public float ElapsedTime;

        [BurstCompile]
        private void Execute(Entity entity, ref AttackData attackData, in AttackRandomTimeData attackRandomTimeData)
        {
            if(attackData.CanChange)
            {
                attackData.CanChange = false;
                uint seedNumber = (uint)math.abs(ElapsedTime + entity.Index);
                attackData.MaxFireTime = Random.CreateFromIndex(seedNumber).NextFloat(attackRandomTimeData.MaxFireRandomTime, attackRandomTimeData.MaxFireRandomTime);
            }
        }
    }

}
