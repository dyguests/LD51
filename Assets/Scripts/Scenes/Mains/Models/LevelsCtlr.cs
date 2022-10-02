using Cysharp.Threading.Tasks;
using Databases.Datas;
using UnityEngine;
using UnlimitedScrollUI;

namespace Scenes.Mains.Models
{
    public class LevelsCtlr : MonoBehaviour
    {
        [SerializeField] private GridUnlimitedScroller unlimitedScroller;

        [Space] [SerializeField] private LevelItemCtlr levelItemCtlrPfb;

        public async UniTask LoadLevels()
        {
            var datas = MapCiphers.datas;
            unlimitedScroller.Generate(levelItemCtlrPfb.gameObject, datas.Length, (index, iCell) =>
            {
                var regularCell = iCell as RegularCell;
                if (regularCell != null) regularCell.onGenerated?.Invoke(index);
            });
        }
    }
}