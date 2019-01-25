using CsharpCollectionsAssignment;
using CsharpCollectionsAssignment.model;
using CsharpCollectionsAssignmentTests.generators;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace CsharpCollectionsAssignmentTests.tests
{
    public class MegaCorpTest
    {

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorp), MemberType = typeof(MemberDataAggregator))]
        public void AddNull(MegaCorp megaCorp)
        {
            Assert.False(megaCorp.Add(null), "#Add() returned true when called with null");
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndWageSlave), MemberType = typeof(MemberDataAggregator))]
        public void AddHasParentlessWageSlave(MegaCorp megaCorp, WageSlave wageSlave)
        {
            Assert.False(megaCorp.Add(wageSlave), "#Add() returned true when called with a parent-less WageSlave");
            Assert.False(megaCorp.Has(wageSlave), "#Has() returned true when called with a parent-less WageSlave that failed to be added");
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndFatCat), MemberType = typeof(MemberDataAggregator))]
        public void AddHasParentlessFatCat(MegaCorp megaCorp, FatCat fatCat)
        {
            Assert.True(megaCorp.Add(fatCat), "#Add() returned false when called with a parent-less FatCat");
            Assert.True(megaCorp.Has(fatCat), "#Has() returned false when called with a parent-less FatCat that failed to be added");
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndCapitalist), 1, MemberType = typeof(MemberDataAggregator))]
        public void AddHasCapitalistWithParent(MegaCorp megaCorp, ICapitalist capitalist)
        {
            Assert.True(megaCorp.Add(capitalist), "#Add() returned false when called with a Capitalist");
            Assert.True(megaCorp.Has(capitalist), "#Has() returned false when called with the Capitalist that was just added");
            Assert.True(megaCorp.Has(capitalist.GetParent()), "#Has() returned false when called with the parent of the Capitalist that was just added");
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndCapitalists), -1, MemberType = typeof(MemberDataAggregator))]
        public void AddHasMultipleArbitraryCapitalists(MegaCorp megaCorp, ISet<ICapitalist> capitalists)
        {
            foreach (ICapitalist capitalist in capitalists)
            {
                Assert.True(megaCorp.Add(capitalist), "#Add() returned false when called with a Capitalist");
                Assert.True(megaCorp.Has(capitalist), "#Has() returned false when called with the Capitalist that was just added");

                ICapitalist parent = capitalist;
                while (parent.HasParent())
                {
                    parent = parent.GetParent();
                    Assert.True(megaCorp.Has(capitalist.GetParent()), "#Has() returned false when called with a parent of the Capitalist that was just added");
                }
            }
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndCapitalists), -1, MemberType = typeof(MemberDataAggregator))]
        public void AddHasMultipleArbitraryCapitalistNoDuplicates(MegaCorp megaCorp, ISet<ICapitalist> capitalists)
        {
            foreach (ICapitalist capitalist in capitalists)
            {
                Assert.True(megaCorp.Add(capitalist), "#Add() returned false when called with an arbitrary Capitalist");
                Assert.True(megaCorp.Has(capitalist), "#Has() returned false when called with the arbitrary Capitalist that was just added");
                Assert.False(megaCorp.Add(capitalist), "#Add() returned true when called with the arbitrary Capitalist that was just added");
            }

            foreach (ICapitalist capitalist in capitalists)
            {
                Assert.True(megaCorp.Has(capitalist), "#Has() returned false when called with a previously-added Capitalist");
                Assert.False(megaCorp.Add(capitalist), "#Add() returned true when called with a previously-added Capitalist after adding multiple Capitalists");
            }
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorp), MemberType = typeof(MemberDataAggregator))]
        public void GetElementsEmpty(MegaCorp megaCorp)
        {
            ISet<ICapitalist> elements = megaCorp.GetElements();
            Assert.NotNull(elements);
            Assert.Empty(elements);
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndCapitalist), -1, MemberType = typeof(MemberDataAggregator))]
        public void GetElementsDefensiveCopy(MegaCorp megaCorp, ICapitalist capitalist)
        {
            ISet<ICapitalist> elements = megaCorp.GetElements();
            elements.Add(capitalist);
            Assert.False(megaCorp.Has(capitalist), "#GetElements() returned a live set of elements that allowed external changes to the MegaCorp");
            elements = megaCorp.GetElements();
            Assert.False(megaCorp.Has(capitalist), "#GetElements() returned a live set of elements that allowed external changes to the MegaCorp");
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndCapitalists), -1, MemberType = typeof(MemberDataAggregator))]
        public void GetElementsMultipleArbitraryCapitalists(MegaCorp megaCorp, ISet<ICapitalist> capitalists)
        {
            ISet<ICapitalist> expected = new HashSet<ICapitalist>(capitalists);
            foreach (ICapitalist capitalist in capitalists)
            {
                megaCorp.Add(capitalist);

                ICapitalist parent = capitalist;
                while (parent != null)
                {
                    expected.Add(parent);
                    parent = parent.GetParent();
                }
            }
            ISet<ICapitalist> elements = megaCorp.GetElements();

            Assert.True(expected.SetEquals(elements), "#GetElements() returned a set that did not equal the set of previously-added Capitalists and their parents");
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorp), MemberType = typeof(MemberDataAggregator))]
        public void GetParentsEmpty(MegaCorp megaCorp)
        {
            ISet<FatCat> parents = megaCorp.GetParents();
            Assert.NotNull(parents);
            Assert.Empty(parents);
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndFatCat), MemberType = typeof(MemberDataAggregator))]
        public void GetParentsDefensiveCopy(MegaCorp megaCorp, FatCat fatCat)
        {
            ISet<FatCat> parents = megaCorp.GetParents();
            parents.Add(fatCat);
            Assert.False(megaCorp.Has(fatCat), "#GetParents() returned a live set of parents that allowed external changes to the MegaCorp");
            parents = megaCorp.GetParents();
            Assert.False(parents.Contains(fatCat), "#GetParents() returned a live set of parents that allowed external changes to the MegaCorp");
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndFatCat), MemberType = typeof(MemberDataAggregator))]
        public void GetParentsFatCat(MegaCorp megaCorp, FatCat fatCat)
        {
            megaCorp.Add(fatCat);
            ISet<FatCat> parents = megaCorp.GetParents();
            Assert.NotEmpty(parents);
            Assert.Equal(1, parents.Count);
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndWageSlave), 1, MemberType = typeof(MemberDataAggregator))]
        public void GetParentsWageSlaveWithParent(MegaCorp megaCorp, WageSlave wageSlave)
        {
            megaCorp.Add(wageSlave);
            FatCat parent = wageSlave.GetParent();
            ISet<FatCat> parents = megaCorp.GetParents();
            Assert.NotEmpty(parents);
            Assert.Equal(1, parents.Count);
            Assert.True(parents.Contains(parent), "#getParents() returned a set that did not contain the parent of the WageSlave added to the MegaCorp");
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndCapitalists), MemberType = typeof(MemberDataAggregator))]
        public void GetParentsMultipleArbitraryCapitalists(MegaCorp megaCorp, ISet<ICapitalist> capitalists)
        {
            ISet<FatCat> expected = new HashSet<FatCat>();
            foreach (ICapitalist capitalist in capitalists)
            {
                megaCorp.Add(capitalist);
                FatCat parent = capitalist.GetType() == typeof(FatCat) ? (FatCat)capitalist : capitalist.GetParent();
                while (parent != null)
                {
                    expected.Add(parent);
                    parent = parent.GetParent();
                }
            }
            ISet<FatCat> parents = megaCorp.GetParents();
            Assert.True(expected.SetEquals(parents), "#GetParents() returned a set that did not equal the set of all parents of the added Capitalists");
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndFatCat), MemberType = typeof(MemberDataAggregator))]
        public void GetChildrenEmpty(MegaCorp megaCorp, FatCat fatCat)
        {
            ISet<ICapitalist> children = megaCorp.GetChildren(fatCat);
            Assert.NotNull(children);
            Assert.Empty(children);
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndFatCatAndCapitalist), MemberType = typeof(MemberDataAggregator))]
        public void GetChildrenDefensiveCopy(MegaCorp megaCorp, FatCat fatCat, ICapitalist capitalist)
        {
            megaCorp.Add(fatCat);
            ISet<ICapitalist> children = megaCorp.GetChildren(fatCat);
            Assert.Empty(children);
            children.Add(capitalist);
            Assert.False(megaCorp.Has(capitalist), "#GetChildren() returned a live set that allowed external changes to the MegaCorp");
            children = megaCorp.GetChildren(fatCat);
            Assert.False(children.Contains(capitalist), "#GetChildren() returned a live set that allowed external changes to the MegaCorp");
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndFatCat), 1, MemberType = typeof(MemberDataAggregator))]
        public void GetChildrenFatCatWithParent(MegaCorp megaCorp, FatCat fatCat)
        {
            megaCorp.Add(fatCat);
            ISet<ICapitalist> children = megaCorp.GetChildren(fatCat);
            Assert.Empty(children);
            children = megaCorp.GetChildren(fatCat.GetParent());
            Assert.True(children.Contains(fatCat), "#getChildren() returned a set that does not contain the previously-added FatCat when called with its parent");
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndFatCatAndCapitalists), MemberType = typeof(MemberDataAggregator))]
        public void GetChildrenMultipleCapitalistsWithSharedParent(MegaCorp megaCorp, FatCat parent, ISet<ICapitalist> children)
        {
            megaCorp.Add(parent);
            ISet<ICapitalist> expected = new HashSet<ICapitalist>();
            foreach (ICapitalist parentless in children)
            {
                ICapitalist withParent = parentless.GetType() == typeof(FatCat)
                            ? (ICapitalist)new FatCat(parentless.GetName(), parentless.GetSalary(), parent)
                                : (ICapitalist)new WageSlave(parentless.GetName(), parentless.GetSalary(), parent);
                megaCorp.Add(withParent);
                expected.Add(withParent);
            }

            Assert.True(expected.SetEquals(megaCorp.GetChildren(parent)), "#GetChildren() returned a set that did not equal the set of children of the previously-added FatCat");
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndFatCatAndTwoSetsOfCapitalists), MemberType = typeof(MemberDataAggregator))]
        public void GetChildrenMultipleCapitalistsSomeWithSharedParent(MegaCorp megaCorp, FatCat parent, ISet<ICapitalist> children, ISet<ICapitalist> loose)
        {
            megaCorp.Add(parent);
            ISet<ICapitalist> expected = new HashSet<ICapitalist>();
            foreach (ICapitalist parentless in children)
            {
                ICapitalist withParent = parentless.GetType() == typeof(FatCat)
                            ? (ICapitalist)new FatCat(parentless.GetName(), parentless.GetSalary(), parent)
                                : (ICapitalist)new WageSlave(parentless.GetName(), parentless.GetSalary(), parent);
                megaCorp.Add(withParent);
                expected.Add(withParent);
            }

            foreach (ICapitalist looseCapitalist in loose)
            {
                megaCorp.Add(looseCapitalist);
            }
            Assert.True(expected.SetEquals(megaCorp.GetChildren(parent)), "#getChildren() returned a set that did not equal the set of children of a previously-added FatCat after adding loose capitalists");
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorp), MemberType = typeof(MemberDataAggregator))]
        public void GetHierarchyEmpty(MegaCorp megaCorp)
        {
            IDictionary<FatCat, ISet<ICapitalist>> hierarchy = megaCorp.GetHierarchy();
            Assert.NotNull(hierarchy);
            Assert.Empty(hierarchy);
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndFatCat), MemberType = typeof(MemberDataAggregator))]
        public void GetHierarchyInitializesChildSets(MegaCorp megaCorp, FatCat fatCat)
        {
            megaCorp.Add(fatCat);
            ISet<ICapitalist> children = megaCorp.GetHierarchy()[fatCat];
            Assert.NotNull(children);
            Assert.Empty(children);
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndWageSlave), 1, MemberType = typeof(MemberDataAggregator))]
        public void GetHierarchyDefensiveCopy(MegaCorp megaCorp, WageSlave wageSlave)
        {
            ISet<ICapitalist> children = new HashSet<ICapitalist>();
            children.Add(wageSlave);
            IDictionary<FatCat, ISet<ICapitalist>> hierarchy = megaCorp.GetHierarchy();
            hierarchy.Add(wageSlave.GetParent(), children);
            Assert.False(megaCorp.Has(wageSlave.GetParent()), "#getHierarchy() returned a live map that allowed external changes to the MegaCorp");
            Assert.False(megaCorp.Has(wageSlave), "#getHierarchy() returned a live map that allowed external changes to the MegaCorp");

            hierarchy = megaCorp.GetHierarchy();

            Assert.False(children.SetEquals(hierarchy.ContainsKey(wageSlave.GetParent()) ? hierarchy[wageSlave.GetParent()] : new HashSet<ICapitalist>()),
                "#getHierarchy() returned a live map that allowed external changes to the MegaCorp");
            Assert.False(hierarchy.ContainsKey(wageSlave.GetParent()), "#getHierarchy() returned a live map that allowed external changes to the MegaCorp");

            megaCorp.Add(wageSlave.GetParent());
            hierarchy = megaCorp.GetHierarchy();
            children = hierarchy[wageSlave.GetParent()];
            children.Add(wageSlave);
            Assert.False(megaCorp.Has(wageSlave), "#getHierarchy() returned a live map that allowed external changes to the MegaCorp");

            hierarchy = megaCorp.GetHierarchy();
            Assert.False(hierarchy[wageSlave.GetParent()].Contains(wageSlave), "#getHierarchy() returned a live map that allowed external changes to the MegaCorp");
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorp), 5, MemberType = typeof(MemberDataAggregator))]
        public void GetHierarchyConsistencyWithOneLevel(MegaCorp megaCorp)
        {
            IDictionary<FatCat, ISet<ICapitalist>> hierarchy = megaCorp.GetHierarchy();
            ISet<FatCat> expectedParents = megaCorp.GetParents();

            Assert.True(expectedParents.SetEquals(hierarchy.Keys),
                "#GetHierarchy() returned a map with a key set that did not match the MegaCorp's parents");

            ISet<ICapitalist> actualElements = new HashSet<ICapitalist>();
            foreach (FatCat parent in expectedParents)
            {
                actualElements.Add(parent);
                ISet<ICapitalist> expectedChildren = megaCorp.GetChildren(parent);
                foreach (ICapitalist capitalist in expectedChildren)
                {
                    actualElements.Add(capitalist);
                }
                Assert.True(expectedChildren.SetEquals(hierarchy[parent]),
                    "#GetHierarchy() returned a map in which a key's associated set of values did not match the MegaCorp's children for that key");
            }
            Assert.True(megaCorp.GetElements().SetEquals(actualElements),
                    "#GetHierarchy() returned a map in which a key's associated set of values did not match the MegaCorp's children for that key");
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndCapitalist), -1, MemberType = typeof(MemberDataAggregator))]
        public void GetParentChainEmpty(MegaCorp megaCorp, ICapitalist capitalist)
        {
            IList<FatCat> actual = megaCorp.GetParentChain(null);
            Assert.NotNull(actual);
            Assert.Empty(actual);

            actual = megaCorp.GetParentChain(capitalist);
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        [Theory]
        [MemberData(nameof(MemberDataAggregator.GenerateMegaCorpAndCapitalist), -1, 5, MemberType = typeof(MemberDataAggregator))]
        public void GetParentChainMatchesInternalStructure(MegaCorp megaCorp, ICapitalist capitalist)
        {
            megaCorp.Add(capitalist);
            FatCat parent = capitalist.GetParent();
            ISet<FatCat> expected = new HashSet<FatCat>();
            while (parent != null)
            {
                expected.Add(parent);
                parent = parent.GetParent();
            }
            Assert.True(expected.SetEquals(megaCorp.GetParentChain(capitalist)),
                "#GetParentChain() returned a list that did not match the calculated structure of the arbitrary Capitalist that was just added to the MegaCorp");
        }
    }
}