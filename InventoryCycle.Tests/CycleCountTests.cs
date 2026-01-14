using InventoryCycle.Domain;
using Xunit;

namespace InventoryCycle.Tests;

public class CycleCountTests
{
    [Fact]
    public void AbcClassifier_ShouldCorrectlyAssignClasses()
    {
        // Arrange
        var products = new List<InventoryProduct>();
        for (int i = 1; i <= 10; i++)
        {
            products.Add(new InventoryProduct 
            { 
                Id = Guid.NewGuid(), 
                PartNumber = $"P{i}", 
                AverageOutputRate = i * 10 
            });
        }

        // Act
        var result = AbcClassifier.RecalculateClasses(products);

        // Assert
        // Top 20% (2 items) -> A
        Assert.Equal(ProductClass.A, result.First(p => p.PartNumber == "P10").AssignedClass);
        Assert.Equal(ProductClass.A, result.First(p => p.PartNumber == "P9").AssignedClass);

        // Next 30% (3 items) -> B
        Assert.Equal(ProductClass.B, result.First(p => p.PartNumber == "P8").AssignedClass);
        Assert.Equal(ProductClass.B, result.First(p => p.PartNumber == "P7").AssignedClass);
        Assert.Equal(ProductClass.B, result.First(p => p.PartNumber == "P6").AssignedClass);

        // Rest 50% (5 items) -> C
        Assert.Equal(ProductClass.C, result.First(p => p.PartNumber == "P5").AssignedClass);
        Assert.Equal(ProductClass.C, result.First(p => p.PartNumber == "P4").AssignedClass);
        Assert.Equal(ProductClass.C, result.First(p => p.PartNumber == "P3").AssignedClass);
        Assert.Equal(ProductClass.C, result.First(p => p.PartNumber == "P2").AssignedClass);
        Assert.Equal(ProductClass.C, result.First(p => p.PartNumber == "P1").AssignedClass);
    }

    [Fact]
    public void Scheduler_ShouldIdentifyWhenCountIsDue()
    {
        // Arrange
        var referenceDate = new DateTime(2024, 1, 1);
        
        var productA_NotDue = new InventoryProduct 
        { 
            AssignedClass = ProductClass.A, 
            LastInventoryDate = referenceDate.AddDays(-25) 
        };
        var productA_Due = new InventoryProduct 
        { 
            AssignedClass = ProductClass.A, 
            LastInventoryDate = referenceDate.AddDays(-31) 
        };
        
        var productB_NotDue = new InventoryProduct 
        { 
            AssignedClass = ProductClass.B, 
            LastInventoryDate = referenceDate.AddDays(-89) 
        };
        var productB_Due = new InventoryProduct 
        { 
            AssignedClass = ProductClass.B, 
            LastInventoryDate = referenceDate.AddDays(-91) 
        };

        // Act & Assert
        Assert.False(SchedulerService.IsDueForCount(productA_NotDue, referenceDate));
        Assert.True(SchedulerService.IsDueForCount(productA_Due, referenceDate));
        Assert.False(SchedulerService.IsDueForCount(productB_NotDue, referenceDate));
        Assert.True(SchedulerService.IsDueForCount(productB_Due, referenceDate));
    }

    [Fact]
    public void AccuracyService_ShouldCalculateCorrectly()
    {
        // Case 1: Snapshot=100, Counted=95
        var result1 = AccuracyService.Calculate(100m, 95m);
        Assert.Equal(-5m, result1.Variance);
        Assert.Equal(95m, result1.AccuracyPercentage);
        Assert.True(result1.IsAdjustmentNeeded);

        // Case 2: Snapshot=0, Counted=5
        var result2 = AccuracyService.Calculate(0m, 5m);
        Assert.Equal(5m, result2.Variance);
        Assert.Equal(0m, result2.AccuracyPercentage);
        Assert.True(result2.IsAdjustmentNeeded);

        // Case 3: Snapshot=0, Counted=0
        var result3 = AccuracyService.Calculate(0m, 0m);
        Assert.Equal(0m, result3.Variance);
        Assert.Equal(100m, result3.AccuracyPercentage);
        Assert.False(result3.IsAdjustmentNeeded);
    }
}
