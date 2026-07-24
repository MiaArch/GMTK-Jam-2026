using System;
using UnityEngine;

namespace NewsTicker
{
    [Serializable]
    public abstract class NewsCondition : ScriptableObject
    {
        public abstract bool Evaluate();
    }
}