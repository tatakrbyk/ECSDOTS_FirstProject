using SpaceShipEcsDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Scenes;
using Unity.Transforms;

namespace SpaceShipEcsDots.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(SceneSystemGroup))]
    [BurstCompile]
    public partial struct EnemySpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var elapsedTime = SystemAPI.Time.ElapsedTime;
            var entityCommandBuffer = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

            new EnemySpawnJob()
            {
                DeltaTime = deltaTime,
                ElapsedTime = (float)elapsedTime,
                MyEntityCommandBuffer = entityCommandBuffer.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct EnemySpawnJob : IJobEntity
    {
        public float DeltaTime;
        public float ElapsedTime;
        public EntityCommandBuffer.ParallelWriter MyEntityCommandBuffer;

        [BurstCompile]
        private void Execute(Entity entity, ref EnemySpawnData enemySpawnData, in EnemySpawnPositionReferance enemySpawnPositionRef, [ChunkIndexInQuery] int sortKey)
        {
            enemySpawnData.CurrentTime += DeltaTime;

            if (enemySpawnData.CurrentTime > enemySpawnData.RandomTime)
            {
                enemySpawnData.CurrentTime = 0f;
                uint seedNumber = (uint)math.abs(ElapsedTime + entity.Index);
                enemySpawnData.RandomTime = Random.CreateFromIndex(seedNumber)
                    .NextFloat(enemySpawnData.MinTime, enemySpawnData.MaxTime);

                var newEntity = MyEntityCommandBuffer.Instantiate(sortKey, enemySpawnData.entity);

                MyEntityCommandBuffer.SetComponent(sortKey, newEntity, new EnemyPathData()
                {
                    BlobValueReference = enemySpawnPositionRef.BlobValueReference
                });

                MyEntityCommandBuffer.SetComponent(sortKey, newEntity, new LocalTransform()
                {
                    Position = enemySpawnPositionRef.BlobValueReference.Value.Values[0],
                    Rotation = quaternion.identity,
                    Scale = 1f
                });

                MyEntityCommandBuffer.SetComponent(sortKey, newEntity, new EnemyMoveTargetData()
                {
                    Target = enemySpawnPositionRef.BlobValueReference.Value.Values[1],
                    NextTargetIndex = 1,
                    MaxTargetIndex = enemySpawnPositionRef.BlobValueReference.Value.Values.Length
                });
            }
        }
    }
}