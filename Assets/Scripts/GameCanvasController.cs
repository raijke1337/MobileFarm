using Farm.Game;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Farm.Game.UI
{
    public class GameCanvasController : MonoBehaviour
    {
        private InventoryComp _inv;
        [SerializeField] private HorizontalLayoutGroup _contents;
        public void Init()
        { 
            _inv = GameManager.Instance.GetPlayer.GetInventory;
            foreach (var i in _inv.Items)
            {
                var item = Instantiate(i, _contents.transform) ;
                item.ItemClickEvent += I_ItemClickEvent;
            }
        }

        private void I_ItemClickEvent(Item arg)
        {
            _inv.EquippedItem = arg;
        }
    }
}