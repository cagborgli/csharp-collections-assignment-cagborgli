using System.Collections.Generic;

namespace CsharpCollectionsAssignment.hierarchy
{
    public interface IHierarchy<TElement, TParent> 
        where TElement : IHierarchical<TElement, TParent>
        where TParent : TElement
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
         * @param element the element to add to the hierarchy
         * @return true if the element was added successfully, false otherwise
         */
        bool Add(TElement element);
        
        /**
         * @param element the element to search for
         * @return true if the element has been added to the hierarchy, false otherwise
         */
        bool Has(TElement element);

        /**
         * @return all elements in the hierarchy,
         * or an empty set if no elements have been added to the hierarchy
         */
        ISet<TElement> GetElements();

        /**
         * @return all parent elements in the hierarchy,
         * or an empty set if no parents have been added to the hierarchy
         */
        ISet<TParent> GetParents();

        /**
         * @param parent the parent whose children need to be returned
         * @return all elements in the hierarchy that have the given parent as a direct parent,
         * or an empty set if the parent is not present in the hierarchy or if there are no children
         * for the given parent
         */
        ISet<TElement> GetChildren(TParent parent);

        /**
         * @return a map in which the keys represent the parent elements in the hierarchy,
         * and the each value is a set of the direct children of the associate parent, or an
         * empty map if the hierarchy is empty.
         */
        IDictionary<TParent, ISet<TElement>> GetHierarchy();

        /**
         * @param element
         * @return the parent chain of the given element, starting with its direct parent,
         * then its parent's parent, etc, or an empty list if the given element has no parent
         * or if its parent is not in the hierarchy
         */
        IList<TParent> GetParentChain(TElement element);
    }
    
}