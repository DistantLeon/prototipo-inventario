namespace InventoryCycle.Domain;

public static class SchedulerService
{
    public static bool IsDueForCount(InventoryProduct product, DateTime referenceDate)
    {
        if (product.AssignedClass == null)
            return false;

        int frequencyDays = product.AssignedClass switch
        {
            ProductClass.A => 30,
            ProductClass.B => 90,
            ProductClass.C => 90,
            _ => 90
        };

        return product.LastInventoryDate.AddDays(frequencyDays) <= referenceDate;
    }
}
