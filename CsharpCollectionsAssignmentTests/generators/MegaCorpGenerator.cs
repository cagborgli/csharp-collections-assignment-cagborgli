using CsharpCollectionsAssignment;
using CsharpCollectionsAssignment.model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CsharpCollectionsAssignmentTests.generators
{
    public class MegaCorpGenerator
    {
        private static Random random = new Random();

        public static MegaCorp GenerateMegaCorp(int depth = 0, int width = 0)
        {
            int ratio = depth; 
            int size = 1;

            MegaCorp megaCorp = new MegaCorp();
            ISet<FatCat> parents = new HashSet<FatCat>();
            parents.Add(null);

            while (depth > 0)
            {
                if (depth > 1)
                {
                    parents.UnionWith(CapitalistGenerator.GenerateFatCats(size, parents));
                }
                else
                {
                    foreach (ICapitalist capitalist in CapitalistGenerator.GenerateCapitalists(size, parents))
                    {
                        megaCorp.Add(capitalist);
                    }
                }
                size *= ratio;
                depth--;
            }
            return megaCorp;
        }

    }
}

