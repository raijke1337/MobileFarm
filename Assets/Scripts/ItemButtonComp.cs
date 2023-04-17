using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Farm.GameManager;

namespace Farm.Game.UI
{
    [RequireComponent(typeof(Image))]
    public class ItemButtonComp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private Image spriteRenderer;
        [SerializeField]
        private Sprite _hoverSprite;
        private Sprite _startSprite;

        public GameCanvasController _canvas;


        [Space]public Item Item;

        public event SimpleEvent<Item> ItemClickEvent;

        public void OnPointerClick(PointerEventData eventData)
        {
            ItemClickEvent?.Invoke(Item);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            spriteRenderer.sprite = _hoverSprite;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            spriteRenderer.sprite = _startSprite;
        }

        private void Start()
        {
            spriteRenderer = GetComponent<Image>();
            _startSprite = spriteRenderer.sprite;
        }



    }
}