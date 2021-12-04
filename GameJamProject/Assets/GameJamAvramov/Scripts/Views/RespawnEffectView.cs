using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam
{
    public class RespawnEffectView : BaseView, IRequireInjection<ZombieController>
    {
        [SerializeField] private ParticleSystem _respawnEffect;

        private ZombieController _zombieController;

        private void OnDisable()
        {
            _zombieController.ChooseZombieEvent -= OnChooseZombie;
        }

        public void InjectDependency(ZombieController dependency)
        {
            _zombieController = dependency;

            _zombieController.ChooseZombieEvent += OnChooseZombie;
        }

        private void OnChooseZombie()
        {
            _respawnEffect.transform.position = _zombieController.CurrentZombie.transform.position;
            _respawnEffect.Play();
        }
    }
}

