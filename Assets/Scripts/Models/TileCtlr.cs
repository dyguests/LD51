using DG.Tweening;
using UnityEngine;

namespace Models
{
    public abstract class TileCtlr : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer sr;

        [Space] [SerializeField] private float fadeTime = 0.25f;

        protected void Created()
        {
            sr.DOColor(Color.white, fadeTime).From(new Color(1f, 1f, 1f, 0f));
        }
    }
}