public struct PoolInfo
{
    public PoolInfo(int amountAllTimeObj, int poolCountAll, int poolCountActive)
    {
        AmountAllTimeObj = amountAllTimeObj;
        PoolCountAll = poolCountAll;
        PoolCountActive = poolCountActive;
    }

    public int AmountAllTimeObj { get; private set; }
    public int PoolCountAll { get; private set; }
    public int PoolCountActive { get; private set; }
}
