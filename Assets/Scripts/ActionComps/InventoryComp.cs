using Farm.Game;
using Farm.Game.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class InventoryComp : MonoBehaviour
{
    public ItemButtonComp[] Items;

    private Item _equip;
    public Item EquippedItem { get => _equip; set
        {
            bool flipped = false;

            if (_equip != null)
            {
                flipped = _equip.Flip;
                Destroy(_equip.gameObject);
            }
            _equip = Instantiate(value,transform.position+value.InstantiateOffset,Quaternion.identity,transform);
            _equip.transform.localScale = value.InstantiateScale;
            _equip.Flip = flipped;
        }
    }

    public bool TryUseItem()
    {
        if (_equip == null) return false;
        return _equip.AnimateUse();
    }

    public void Flip (bool isPositive)
    {
        if (_equip == null) return;
        _equip.Flip=!isPositive;
    }

}
