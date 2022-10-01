using Cores.Entities;
using DG.Tweening;
using Tools;
using UnityEngine;
using UnityEngine.Pool;

namespace Scenes.Workshops.Models
{
    public class IndicatorCtlr : MonoBehaviour, IPoolElement
    {
        private static IndicatorCtlr sPrefab;

        [SerializeField] private SpriteRenderer sr;

        [Space] [SerializeField] private float fadeDuration = 0.33f;

        private MoldCtlr moldCtlr;

        private Vector2Int pos;
        private IObjectPool<IndicatorCtlr> pool;

        public Vector2Int Pos => pos;

        public static IndicatorCtlr Generate(MoldCtlr moldCtlr, IObjectPool<IndicatorCtlr> pool)
        {
            if (sPrefab == null)
            {
                sPrefab = Resources.Load<IndicatorCtlr>("Prefabs/Models/Indicator");
            }

            var instantiate = Instantiate(sPrefab, moldCtlr.transform, true);
            instantiate.name = "Indicator";
            instantiate.moldCtlr = moldCtlr;
            instantiate.pool = pool;
            return instantiate;
        }

        public void Acquired()
        {
            gameObject.SetActive(true);
        }

        public void Released()
        {
            gameObject.SetActive(false);
        }

        public void Destroyed()
        {
            Destroy(gameObject);
        }

        public void Appear(Vector2Int pos)
        {
            this.pos = pos;
            transform.localPosition = moldCtlr.Pos2Position(pos);
            sr.DOColor(Color.white, fadeDuration);
        }

        public void Disappear()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(sr.DOColor(ColorEx.Transparent, fadeDuration));
            sequence.AppendCallback(() => pool.Release(this));
        }
    }
}