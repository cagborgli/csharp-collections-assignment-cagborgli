using System;
using System.Collections.Generic;
using CsharpCollectionsAssignment.hierarchy;
using CsharpCollectionsAssignment.model;

namespace CsharpCollectionsAssignment
{
    public class MegaCorp : IHierarchy<ICapitalist, FatCat>
    {
        /**
         * Adds a given element to the hierarchy.
         * <p>
         * If the given element is already present in the hierarchy,
         * do not add it and return false
         * <p>
         * If the given element has a parent and the parent is not part of the hierarchy,
         * add the parent and then add the given element
         * <p>
         * If the given element has no parent but is a Parent itself,
         * add it to the hierarchy
         * <p>
         * If the given element has no parent and is not a Parent itself,
         * do not add it and return false
         *
         * @param capitalist the element to add to the hierarchy
         * @return true if the element was added successfully, false otherwise
         */
        public bool Add(ICapitalist element)
        {
            throw new NotImplementedException();
        }

        /**
         * @param capitalist the element to search for
         * @return true if the element has been added to the hierarchy, false otherwise
         */
        public bool Has(ICapitalist element)
        {
            throw new NotImplementedException();
        }

        /**
         * @return all elements in the hierarchy,
         * or an empty set if no elements have been added to the hierarchy
         */
        public ISet<ICapitalist> GetElements()
        {
            throw new NotImplementedException();
        }

        /**
         * @return all parent elements in the hierarchy,
         * or an empty set if no parents have been added to the hierarchy
         */
        public ISet<FatCat> GetParents()
        {
            throw new NotImplementedException();
        }

        /**
         * @param fatCat the parent whose children need to be returned
         * @return all elements in the hierarchy that have the given parent as a direct parent,
         * or an empty set if the parent is not present in the hierarchy or if there are no children
         * for the given parent
         */
        public ISet<ICapitalist> GetChildren(FatCat parent)
        {
            throw new NotImplementedException();
        }

        /**
         * @return a map in which the keys represent the parent elements in the hierarchy,
         * and the each value is a set of the direct children of the associate parent, or an
         * empty map if the hierarchy is empty.
         */
        public IDictionary<FatCat, ISet<ICapitalist>> GetHierarchy()
        {
            throw new NotImplementedException();
        }

        /**
         * @param capitalist
         * @return the parent chain of the given element, starting with its direct parent,
         * then its parent's parent, etc, or an empty list if the given element has no parent
         * or if its parent is not in the hierarchy
         */
        public IList<FatCat> GetParentChain(ICapitalist element)
        {
            throw new NotImplementedException();
        }
    }
}