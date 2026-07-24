using Resource;
using UnityEngine;

namespace NewsTicker.NewsConditions
{
    [CreateAssetMenu(menuName = "News Conditions/Resource Condition")]
    public class ResourceCondition : NewsCondition
    {
        public ResourceType resource;
        public int minimumAmount;

        public override bool Evaluate()
        {
            return ResourceManager.Instance.GetAmount(resource) >= minimumAmount;
        }
    }
}