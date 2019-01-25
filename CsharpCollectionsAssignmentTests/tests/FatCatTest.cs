using CsharpCollectionsAssignment.model;
using CsharpCollectionsAssignmentTests.generators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CsharpCollectionsAssignmentTests.tests
{
    public class FatCatTest
    {
        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateFatCatNameAndSalary), MemberType = typeof(MemberDataAggregator))]
        public void NoOwnerConstructor(string name, int salary)
        {
            FatCat fatCat = new FatCat(name, salary);
            Assert.Equal(name, fatCat.GetName());
            Assert.Equal(salary, fatCat.GetSalary());
            Assert.Null(fatCat.GetParent());
            Assert.False(fatCat.HasParent(), "HasParent() did not return false when constructed without an owner");
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateFatCatNameAndSalaryAndParent), MemberType = typeof(MemberDataAggregator))]
        public void FullConstructor(string name, int salary, FatCat parent)
        {
            FatCat fatCat = new FatCat(name, salary, parent);
            Assert.Equal(name, fatCat.GetName());
            Assert.Equal(salary, fatCat.GetSalary());
            Assert.Equal(parent, fatCat.GetParent());
            Assert.True(fatCat.HasParent(), "HasParent() did not return true when constructed with an owner");
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateFatCatNameAndSalary), MemberType = typeof(MemberDataAggregator))]
        public void NoOwnerValueEquality(string name, int salary)
        {
            FatCat firstCopy = new FatCat(name, salary);
            FatCat secondCopy = new FatCat(name, salary);
            Assert.Equal(firstCopy, secondCopy);
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateFatCatNameAndSalaryAndParent), MemberType = typeof(MemberDataAggregator))]
        public void FullValueEquality(string name, int salary, FatCat parent)
        {
            FatCat firstCopy = new FatCat(name, salary, parent);
            FatCat secondCopy = new FatCat(name, salary, parent);
            Assert.Equal(firstCopy, secondCopy);
        }
    }
}
