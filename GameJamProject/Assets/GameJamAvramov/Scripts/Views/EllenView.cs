using Gamekit2D;
using UnityEngine;

namespace GameJam
{
    public class EllenView : BaseView, IRequireInjection<ZombieController>
    {
        [SerializeField] private PlayerCharacter _playerCharacter;
        [SerializeField] private Transform _transform;

        private ZombieController _zombieController;

        private void OnEnable()
        {
            _playerCharacter.DieEvent += OnDie;
        }

        private void OnDisable()
        {
            _playerCharacter.DieEvent -= OnDie;

            _zombieController.ChooseZombieEvent -= OnChooseZombie;
        }

        public void InjectDependency(ZombieController dependency)
        {
            _zombieController = dependency;

            _zombieController.SetPlayerTransform(_transform);

            _zombieController.ChooseZombieEvent += OnChooseZombie;
        }

        private void OnChooseZombie()
        {
            ZombieView zombieView = _zombieController.CurrentZombie;

            _transform.position = zombieView.transform.position;

            SetUpZombiesWeapon(zombieView);

            _playerCharacter.SetChekpoint(zombieView.Checkpoint);
        }

        private void SetUpZombiesWeapon(ZombieView zombieView)
        {
            if (zombieView.IsMeleeEnabled)
            {
                _playerCharacter.EnableMeleeAttack();
                PlayerInput.Instance?.EnableMeleeAttacking();
            }
            else
            {
                _playerCharacter.DisableMeleeAttack();
                PlayerInput.Instance?.DisableMeleeAttacking();
            }

            if (zombieView.IsGunEnabled)
            {
                PlayerInput.Instance?.EnableRangedAttacking();
            }
            else
            {
                PlayerInput.Instance?.DisableRangedAttacking();
            }
        }

        private void OnDie()
        {
            _zombieController.Reset();
        }
    }
}

