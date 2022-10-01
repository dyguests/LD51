using Cores.Scenes.Workshops.Entities;
using UnityEngine;

namespace Scenes.Workshops.Models
{
    public class MoldCtlr : MonoBehaviour
    {
        [SerializeField] private MoldInputCtlr inputCtlr;

        private Mold mold;
    }
}