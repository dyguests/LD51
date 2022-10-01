using UnityEngine;

namespace Scenes.Workshops.Models
{
    public class IndicatorCtlr : MonoBehaviour
    {
        private static IndicatorCtlr sPrefab;

        [SerializeField] private SpriteRenderer sr;

        [Space] [SerializeField] private float fadeDuration = 0.33f;

        public static IndicatorCtlr Generate(MoldCtlr moldCtlr)
        {
            if (sPrefab == null)
            {
                sPrefab = Resources.Load<IndicatorCtlr>("Prefabs/Models/Indicator");
            }

            var instantiate = Instantiate(sPrefab, moldCtlr.transform, true);
            instantiate.name = "Indicator";
            return instantiate;
        }
    }
}