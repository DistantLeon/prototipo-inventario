namespace InventoryCycle.Domain;

public static class AccuracyService
{
    public static CountResult Calculate(decimal snapshotQty, decimal countedQty)
    {
        decimal variance = countedQty - snapshotQty;
        decimal accuracyPercentage;

        if (snapshotQty == 0)
        {
            accuracyPercentage = countedQty == 0 ? 100m : 0m;
        }
        else
        {
            accuracyPercentage = (1 - (Math.Abs(variance) / snapshotQty)) * 100;
        }

        return new CountResult
        {
            SnapshotQty = snapshotQty,
            CountedQty = countedQty,
            Variance = variance,
            AccuracyPercentage = accuracyPercentage,
            IsAdjustmentNeeded = variance != 0
        };
    }
}
