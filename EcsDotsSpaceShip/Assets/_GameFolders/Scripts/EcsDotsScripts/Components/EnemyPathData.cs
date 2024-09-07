using Unity.Entities;

namespace SpaceShipEcsDots.Components
{
    public struct EnemyPathData : IComponentData
    {
        public BlobAssetReference<EnemySpawnPositionBlob> BlobValueReference;

    }

}
