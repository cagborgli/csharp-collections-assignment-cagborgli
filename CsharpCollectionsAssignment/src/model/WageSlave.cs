using System;

namespace CsharpCollectionsAssignment.model
{
    public class WageSlave : ICapitalist
    {
        public string Name;
        public int Salary;
        public FatCat Owner;

        public WageSlave(string name, int salary)
        {
            if (name == null || salary == 0)
            {
                throw new InvalidOperationException();
            }

            Name = name;
            Salary = salary;
        }

        public WageSlave(String name, int salary, FatCat owner)
        {
            if (name == null || salary == 0 || owner == null)
            {
                throw new InvalidOperationException();
            }

            Name = name;
            Salary = salary;
            Owner = owner;
        }

        /**
         * @return the name of the capitalist
         */
        public String GetName()
        {
           return Name;
        }

        /**
         * @return the salary of the capitalist, in dollars
         */
        public int GetSalary()
        {
           return Salary;
        }

        /**
         * @return true if this element has a parent, or false otherwise
         */
        public bool HasParent()
        {
            if (Owner == null)
            {
                return false;
            }

            return true;
        }

        /**
         * @return the parent of this element, or null if this represents the top of a hierarchy
         */
        public FatCat GetParent()
        {
            return Owner;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }

            if (obj == null)
            {
                return false;
            }

            WageSlave slave;

            if (obj is FatCat)
            {
                return false;
            }

            slave = (WageSlave)obj;
            return (slave.Name == Name && slave.Owner == Owner && slave.Salary == Salary);
        }
    }
}