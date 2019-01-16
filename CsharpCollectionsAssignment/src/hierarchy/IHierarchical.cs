using System;

namespace CsharpCollectionsAssignment.hierarchy
{
    public interface IHierarchical<TElement, TParent>
        where TElement : IHierarchical<TElement, TParent>
        where TParent : TElement
    {
        
        /**
         * @return true if this element has a parent, or false otherwise
         */
        bool HasParent();
        
        /**
         * @return the parent of this element, or null if this represents the top of a hierarchy
         */
        TParent GetParent();

    }
}