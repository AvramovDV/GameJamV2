using System.Collections.Generic;
using UnityEngine;

namespace GameJam
{
    public class ZombieIndicationUIView : BaseView, IRequireInjection<ZombieController>
    {
        [SerializeField] private List<ZombieImageView> _images;

        private ZombieController _zombieController;

        private void OnDisable()
        {
            _zombieController.ActivateZombieEvent -= OnActivateZombie;
            _zombieController.ChooseZombieEvent -= OnChooseZombie;
            _zombieController.ResetEvent -= OnReset;
        }

        public void InjectDependency(ZombieController dependency)
        {
            _zombieController = dependency;

            _zombieController.ActivateZombieEvent += OnActivateZombie;
            _zombieController.ChooseZombieEvent += OnChooseZombie;
            _zombieController.ResetEvent += OnReset;
        }

        private void OnActivateZombie()
        {
            int index = _zombieController.GetActiveZombieCount() - 1;

            _images[index].gameObject.SetActive(true);
        }

        private void OnChooseZombie()
        {
            int index = _zombieController.GetCurrentZombieIndex();

            ResetSelection();

            _images[index].Select();
        }

        private void ResetSelection()
        {
            int count = _images.Count;

            for (int i = 0; i < count; i++)
            {
                _images[i].Deselect();
            }
        }

        private void OnReset()
        {
            int count = _images.Count;

            for (int i = 0; i < count; i++)
            {
                _images[i].gameObject.SetActive(false);
            }
        }
    }
}

