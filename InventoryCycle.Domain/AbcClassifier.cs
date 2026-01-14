namespace InventoryCycle.Domain;

public static class AbcClassifier
{
    public static List<InventoryProduct> RecalculateClasses(List<InventoryProduct> products)
    {
        if (products == null || products.Count == 0)
            return products ?? new List<InventoryProduct>();

        var sortedProducts = products.OrderByDescending(p => p.AverageOutputRate).ToList();
        int totalCount = sortedProducts.Count;

        int aLimit = (int)Math.Ceiling(totalCount * 0.20);
        int bLimit = (int)Math.Ceiling(totalCount * 0.50); // 20% + 30%

        for (int i = 0; i < totalCount; i++)
        {
            if (i < aLimit)
            {
                sortedProducts[i].AssignedClass = ProductClass.A;
            }
            else if (i < bLimit)
            {
                sortedProducts[i].AssignedClass = ProductClass.B;
            }
            else
            {
                sortedProducts[i].AssignedClass = ProductClass.C;
            }
        }

        return sortedProducts;
    }
}
