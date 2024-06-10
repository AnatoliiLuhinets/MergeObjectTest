using Environment;

namespace Data
{
    public static class FoldingItemUpgradeData
    {
        public static FoldingItemsType GetTypeForUpgrade(FoldingItemsType type)
        {
            switch (type)
            {
                case FoldingItemsType.BabyCorn:
                    return FoldingItemsType.Corn;
                case FoldingItemsType.DogHouse:
                    return FoldingItemsType.DogHouseBig;
                default:
                    return FoldingItemsType.None;
            }
        }

        public static int GetUpgradeLevel(FoldingItemsType type)
        {
            switch (type)
            {
                case FoldingItemsType.DogHouseBig:
                case FoldingItemsType.Corn:
                    return 1;
                default:
                    return 2;
            }
        }
    }
}
