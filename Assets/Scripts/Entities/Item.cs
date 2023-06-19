using UnityEngine;
using static Enums;

namespace Farm.Game
{
    [RequireComponent(typeof(Animation),typeof(SpriteRenderer))]
    public class Item : MonoBehaviour
    {

        [SerializeField] protected string _itemName;
        public string ItemName { get => _itemName; }
        public ItemType Type;
        public Vector3 InstantiateOffset = Vector3.zero;
        public Vector3 InstantiateScale = Vector3.one;  
        //private Animator _animator;

        private Animation _useAnim;
        private SpriteRenderer _spriteRenderer;
        


        [SerializeField] float _cooldown;
        private float _currentCd;
        public bool IsBusy { get; private set; }

        protected virtual void Start()
        {
            IsBusy = false;
            //_animator = GetComponent<Animator>();
            _useAnim = GetComponent<Animation>();
            _spriteRenderer = GetComponent<SpriteRenderer>();   
        }
        public bool AnimateUse()
        {
            if (IsBusy) return false;
            IsBusy = true;
            _currentCd = _cooldown;
            _useAnim.Play();
            return true;
            //_animator.SetTrigger("Use");
        }
        protected virtual void Update()
        {
            if (_currentCd > 0)
            {
                IsBusy = true;
                _currentCd -= Time.deltaTime;
            }
            else
            {
                IsBusy = false;
            }
        }

        public bool Flip
        {
            get => _spriteRenderer.flipX;
            set
            {
                if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
                if (_spriteRenderer.flipX != value)
                {
                    _spriteRenderer.flipX = value;
                    Vector3 pos = transform.localPosition;
                    transform.localPosition = new Vector3(pos.x * -1, pos.y, pos.z);
                }

            }
        }

    }

}