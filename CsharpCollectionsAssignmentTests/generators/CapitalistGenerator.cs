using CsharpCollectionsAssignment.model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CsharpCollectionsAssignmentTests.generators
{
    class CapitalistGenerator
    {
        private static Random random = new Random();
        
        public static string GenerateFatCatName()
        {
            return "FatCat-" + random.Next();
        }

        public static string GenerateWageSlaveName()
        {
            return "WageSlave-" + random.Next();
        }

        public static int GenerateSalary()
        {
            return random.Next(100, 10000);
        }

        /*
         * Methods responsible for instantiating Capitalists
         */
        public static ICapitalist GenerateCapitalist(FatCat parent = null)
        {
            if (random.NextDouble() >= 0.5)
            {
                return GenerateFatCat(parent);
            }
            else
            {
                return GenerateWageSlave(parent);
            }
        }

        public static FatCat GenerateFatCat(FatCat parent = null)
        {
            string name = GenerateFatCatName();
            int salary = GenerateSalary();

            return parent != null ? new FatCat(name, salary, parent) : new FatCat(name, salary);
        }

        public static WageSlave GenerateWageSlave(FatCat parent = null)
        {
            string name = GenerateWageSlaveName();
            int salary = GenerateSalary();

            return parent != null ? new WageSlave(name, salary, parent) : new WageSlave(name, salary);
        }

        /*
         * Methods responsible for generating at a specific depth
         */
        public static ICapitalist GenerateCapitalistAtDepth(int depth, FatCat parent = null)
        {
            return GenerateCapitalist(GenerateFatCatAtDepth(depth, parent));
        }

        public static FatCat GenerateFatCatAtDepth(int depth, FatCat parent = null)
        {
            return GenerateFatCat(depth > 0 ? GenerateFatCatAtDepth(depth - 1, parent) : parent);
        }

        public static WageSlave GenerateWageSlaveAtDepth(int depth, FatCat parent = null)
        {
            return GenerateWageSlave(depth > 0 ? GenerateFatCatAtDepth(depth - 1, parent) : parent);
        }

        /*
         * Methods responsible for generating multiple Capitalists
         */
        public static ISet<ICapitalist> GenerateCapitalists(int count, FatCat parent = null)
        {
            ISet<FatCat> parents = new HashSet<FatCat>();
            parents.Add(parent);
            return GenerateCapitalists(count, parents);
        }

        public static ISet<ICapitalist> GenerateCapitalists(int count, ISet<FatCat> parents)
        {
            ISet<ICapitalist> result = new HashSet<ICapitalist>();
            for (int i = 0; i < count; i++)
            {
                result.Add(GenerateCapitalist(parents.ElementAt(random.Next(parents.Count))));
            }
            return result;
        }

        public static ISet<FatCat> GenerateFatCats(int count, FatCat parent = null)
        {
            ISet<FatCat> parents = new HashSet<FatCat>();
            parents.Add(parent);
            return GenerateFatCats(count, parents);
        }

        public static ISet<FatCat> GenerateFatCats(int count, ISet<FatCat> parents)
        {
            ISet<FatCat> result = new HashSet<FatCat>();
            for (int i = 0; i < count; i++)
            {
                result.Add(GenerateFatCat(parents.ElementAt(random.Next(parents.Count))));
            }
            return result;
        }

        public static ISet<WageSlave> GenerateWageSlaves(int count, FatCat parent = null)
        {
            ISet<FatCat> parents = new HashSet<FatCat>();
            parents.Add(parent);
            return GenerateWageSlaves(count, parents);
        }

        public static ISet<WageSlave> GenerateWageSlaves(int count, ISet<FatCat> parents)
        {
            ISet<WageSlave> result = new HashSet<WageSlave>();
            for (int i = 0; i < count; i++)
            {
                result.Add(GenerateWageSlave(parents.ElementAt(random.Next(parents.Count))));
            }
            return result;
        }

        /*
         * Methods responsible for generating multiple Capitalists at a specific depth
         */
        public static ISet<ICapitalist> GenerateCapitalistsAtDepth(int count, int depth, FatCat parent = null)
        {
            ISet<FatCat> parents = new HashSet<FatCat>();
            parents.Add(parent);
            return GenerateCapitalistsAtDepth(count, depth, parents);
        }

        public static ISet<ICapitalist> GenerateCapitalistsAtDepth(int count, int depth, ISet<FatCat> parents)
        {
            ISet<FatCat> result = new HashSet<FatCat>();
            foreach (FatCat parent in parents)
            {
                result.Add(GenerateFatCatAtDepth(depth, parent));
            }
            return GenerateCapitalists(count, result);
        }

        public static ISet<FatCat> GenerateFatCatsAtDepth(int count, int depth, FatCat parent = null)
        {
            ISet<FatCat> parents = new HashSet<FatCat>();
            parents.Add(parent);
            return GenerateFatCatsAtDepth(count, depth, parents);
        }

        public static ISet<FatCat> GenerateFatCatsAtDepth(int count, int depth, ISet<FatCat> parents)
        {
            ISet<FatCat> result = new HashSet<FatCat>();
            foreach (FatCat parent in parents)
            {
                result.Add(GenerateFatCatAtDepth(depth, parent));
            }
            return GenerateFatCats(count, result);
        }

        public static ISet<WageSlave> GenerateWageSlavesAtDepth(int count, int depth, FatCat parent = null)
        {
            ISet<FatCat> parents = new HashSet<FatCat>();
            parents.Add(parent);
            return GenerateWageSlavesAtDepth(count, depth, parents);
        }

        public static ISet<WageSlave> GenerateWageSlavesAtDepth(int count, int depth, ISet<FatCat> parents)
        {
            ISet<FatCat> result = new HashSet<FatCat>();
            foreach (FatCat parent in parents)
            {
                result.Add(GenerateFatCatAtDepth(depth, parent));
            }
            return GenerateWageSlaves(count, result);
        }
    }
}
