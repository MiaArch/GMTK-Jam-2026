using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Decisions
{
    public class DecisionFlags: Singleton<DecisionFlags>
    {
        private readonly HashSet<string> flags = new();

        public bool Has(string flag)
        {
            // Debug.Log(flags.Count == 0
            //     ? "Flags: (none)"
            //     : $"Flags: {string.Join(", ", flags)}");
            bool result = flags.Contains(flag);
            // Debug.Log($"Contains: {result}");

            return result;
            
        }

        public void Set(string flag)
        {
            flags.Add(flag);
        }
    }
}