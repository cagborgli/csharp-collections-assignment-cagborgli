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

        public static MegaCorp GenerateMegaCorp(int depth = 0, int width = 0, int ratio = 1)
        {
            int size = width + (int)Math.Ceiling(Math.Pow(depth != 0 ? depth : ratio, depth));

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

