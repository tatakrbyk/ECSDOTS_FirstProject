using Unity.Entities;
using Unity.Mathematics;

namespace SpaceShipEcsDots.Components
{
    public struct EnemySpawnPositionReferance : IComponentData
    {
        public BlobAssetReference<EnemySpawnPositionBlob> BlobValueReference;
    }

    public struct EnemySpawnPositionBlob
    {
        public BlobArray<float3> Values; // Enemy Prefableri spawnlamak için blob array kuullanmamýz gerekli ECSDOTS
    }

}
