using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Farm.Game.Component
{
    public class MoveComp : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private float _moveSpeed = 1f;

        [HideInInspector] public bool IsBusy { get;private set; }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            IsBusy = false;
        }

        public bool Move(Vector2 point)
        {
            if (IsBusy) return false;
            IsBusy = !IsBusy;
            StartCoroutine(MoveCoroutine(point));
            return true;
        }


        private IEnumerator MoveCoroutine(Vector2 finalPos)
        {
            _animator.SetBool("isMoving", true);
            //Vector2 startPos = transform.position;
            while ((Vector2)transform.position != finalPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, finalPos, _moveSpeed * Time.deltaTime);
                yield return null;
            }
            IsBusy = false;
            _animator.SetBool("isMoving", false);
            yield return null;
        }
    }
}