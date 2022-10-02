using System.Threading.Tasks;
using Cores.Scenes.Workshops.Entities;
using Cores.Scenes.Workshops.Tools;
using Databases.Encoders;
using Tools;
using UnityEngine;

namespace Scenes.Workshops.Models
{
    public class MenuCtlr : MonoBehaviour
    {
        private Mold mold;

        public void OnPlayClick()
        {
            Debug.Log("OnPlayClick");

            App.Instance.map = mold.ToMap();
            App.Instance.mold = mold;
            Debug.Log("Play map cipher:\n" + MapEncoder.Encode(App.Instance.map));
            SceneStacker.LoadScene(SceneNames.Game);
        }

        public void OnCopyClick()
        {
            Debug.Log("OnCopyClick");
        }

        public void OnPasteClick()
        {
            Debug.Log("OnPasteClick");
        }

        public async Task LoadMold(Mold mold)
        {
            this.mold = mold;
        }
    }
}