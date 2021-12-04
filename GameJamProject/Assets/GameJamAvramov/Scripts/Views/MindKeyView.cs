using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam
{
    public class MindKeyView : BaseView, IRequireInjection<ZombieController>
    {
        [SerializeField] private ZombieView _zombie;
        [SerializeField] private GameObject _pickUpEffect;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private GameObject _blueMotes;
        [SerializeField] private GameObject _bgGlow;
        [SerializeField] private GameObject _shadow;


        private ZombieController _zombieController;

        private float _deactivationDelay = 0.5f;

        private void OnDisable()
        {
            _zombieController.ResetEvent -= OnReset;
        }

        public void InjectDependency(ZombieController dependency)
        {
            _zombieController = dependency;

            _zombieController.ResetEvent += OnReset;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Gamekit2D.PlayerCharacter>())
            {
                _zombieController.ActivateZombie(_zombie.ZombieId);

                SetOff();
            }
        }

        private IEnumerator DeactivateKeyCoroutine()
        {
            yield return new WaitForSeconds(_deactivationDelay);
            SetOff();
        }

        private void OnReset()
        {
            SetOn();
        }

        private void SetOn()
        {
            _spriteRenderer.enabled = true;
            _animator.enabled = true;
            _collider.enabled = true;
            _blueMotes.SetActive(true);
            _bgGlow.SetActive(true);
            _shadow.SetActive(true);
            _pickUpEffect.SetActive(false);
        }

        private void SetOff()
        {
            _spriteRenderer.enabled = false;
            _animator.enabled = false;
            _collider.enabled = false;
            _blueMotes.SetActive(false);
            _bgGlow.SetActive(false);
            _shadow.SetActive(false);
            _pickUpEffect.SetActive(true);
        }
    }
}

