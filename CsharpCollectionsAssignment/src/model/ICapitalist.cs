using CsharpCollectionsAssignment.hierarchy;

namespace CsharpCollectionsAssignment.model
{
    public interface ICapitalist : IHierarchical<ICapitalist, FatCat>
    {
        /**
         * @return the name of the capitalist
         */
        string GetName();

        /**
         * @return the salary of the capitalist, in dollars
         */
        int GetSalary();
    }
}