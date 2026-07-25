using Resource;
using UnityEngine;
using UnityEngine.Serialization;

namespace NewsTicker.NewsConditions
{
    [CreateAssetMenu(menuName = "News Conditions/Resource Condition")]
    public class ResourceCondition : NewsCondition
    {
        public ResourceType resource;
        public int minimumAmount;
        public bool moreThanEqualsTo = true;

        public override bool Evaluate()
        {
            if (moreThanEqualsTo) return ResourceManager.Instance.GetAmount(resource) >= minimumAmount;
            
            return ResourceManager.Instance.GetAmount(resource) < minimumAmount;

        }
    }
}