using System.Collections.Generic;
using Utils;

namespace Decisions
{
    public class DecisionFlags: Singleton<DecisionFlags>
    {
        private static readonly HashSet<string> flags = new();

        public static bool Has(string flag)
        {
            return flags.Contains(flag);
        }

        public static void Set(string flag)
        {
            flags.Add(flag);
        }
    }
}