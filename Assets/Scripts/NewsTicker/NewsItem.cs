using System;
using System.Collections.Generic;
using UnityEngine;

namespace NewsTicker
{
    [Serializable]
    public class NewsItem
    {
        [TextArea]
        public string headline;

        [Range(0,100)]
        public int weight = 10;
        
        [SerializeReference]
        public List<NewsCondition> conditions = new();
    }
}