using System;
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

        public async Task LoadMold(Mold mold)
        {
            this.mold = mold;
        }

        public void OnPlayClick()
        {
            Debug.Log("OnPlayClick");

            App.Instance.map = mold.ToMap();
            App.Instance.mold = App.Instance.map.ToMold();
            Debug.Log("Play map cipher:\n" + MapEncoder.Encode(App.Instance.map));
            SceneStacker.LoadScene(SceneNames.Game);
        }

        public void OnCopyClick()
        {
            Debug.Log("OnCopyClick");
            var map = mold.ToMap();
            var cipher = MapEncoder.Encode(map);
            Debug.Log("Copy map cipher:\n" + cipher);
            ClipboardUtils.CopyToClipboard(cipher);
        }

        public void OnPasteClick()
        {
            Debug.Log("OnPasteClick");
            var cipher = ClipboardUtils.PasteFromClipboard();
            Debug.Log("Paste map cipher:\n" + cipher);

            try
            {
                var map = MapEncoder.Decode(cipher);
                var mold = map.ToMold();
                App.Instance.mold = mold;
                SceneStacker.ReloadScene();
            }
            catch (Exception e)
            {
                Debug.Log("Paste map cipher failed!\n" + cipher);
                // todo toast failed.
            }
        }
    }
}