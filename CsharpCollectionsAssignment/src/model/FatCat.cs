using System;

namespace CsharpCollectionsAssignment.model
{
    public class FatCat : ICapitalist
    {
        public FatCat(string name, int salary) {
            throw new NotImplementedException();
        }

        public FatCat(string name, int salary, FatCat owner) {
            throw new NotImplementedException();
        }

        /**
         * @return the name of the capitalist
         */
        public String GetName() {
            throw new NotImplementedException();
        }

        /**
         * @return the salary of the capitalist, in dollars
         */
        public int GetSalary() {
            throw new NotImplementedException();
        }

        /**
         * @return true if this element has a parent, or false otherwise
         */
        public bool HasParent() {
            throw new NotImplementedException();
        }

        /**
         * @return the parent of this element, or null if this represents the top of a hierarchy
         */
        public FatCat GetParent() {
            throw new NotImplementedException();
        }
    }
}