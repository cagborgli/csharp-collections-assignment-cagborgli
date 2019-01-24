using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CsharpCollectionsAssignment.model;
using CsharpCollectionsAssignmentTests.generators;

namespace CsharpCollectionsAssignmentTests.tests
{
    public class WageSlaveTest
    {
        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateWageSlaveNameAndSalary), MemberType = typeof(MemberDataAggregator))]
        public void NoOwnerConstructor(string name, int salary)
        {
            WageSlave wageSlave = new WageSlave(name, salary);
            Assert.Equal(name, wageSlave.GetName());
            Assert.Equal(salary, wageSlave.GetSalary());
            Assert.Null(wageSlave.GetParent());
            Assert.False(wageSlave.HasParent(), "HasParent() did not return false when constructed without an owner");
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateWageSlaveNameAndSalaryAndParent), MemberType = typeof(MemberDataAggregator))]
        public void FullConstructor(string name, int salary, FatCat parent)
        {
            WageSlave wageSlave = new WageSlave(name, salary, parent);
            Assert.Equal(name, wageSlave.GetName());
            Assert.Equal(salary, wageSlave.GetSalary());
            Assert.Equal(parent, wageSlave.GetParent());
            Assert.True(wageSlave.HasParent(), "HasParent() did not return true when constructed with an owner");
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateWageSlaveNameAndSalary), MemberType = typeof(MemberDataAggregator))]
        public void NoOwnerValueEquality(string name, int salary)
        {
            WageSlave firstCopy = new WageSlave(name, salary);
            WageSlave secondCopy = new WageSlave(name, salary);
            Assert.Equal(firstCopy, secondCopy);
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateWageSlaveNameAndSalaryAndParent), MemberType = typeof(MemberDataAggregator))]
        public void FullValueEquality(string name, int salary, FatCat parent)
        {
            WageSlave firstCopy = new WageSlave(name, salary, parent);
            WageSlave secondCopy = new WageSlave(name, salary, parent);
            Assert.Equal(firstCopy, secondCopy);
        }
    }
}
