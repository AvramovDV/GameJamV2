using Gamekit2D;
using UnityEngine;

namespace GameJam
{
    public class GameJamMainView : MonoBehaviour
    {
        private ZombieController _zombieController;

        private void Awake()
        {
            CreateDependencies();
            SceneInjection();
            StartInit();
        }

        private void Update()
        {
            CheckInput();
        }

        private void CreateDependencies()
        {
            _zombieController = new ZombieController();
        }

        private void SceneInjection()
        {
            BaseView[] views = FindObjectsOfType<BaseView>();
            int count = views.Length;

            for (int i = 0; i < count; i++)
            {
                InjectDependencies(views[i]);
            }
        }

        private void InjectDependencies(object target)
        {
            if (target is IRequireInjection<ZombieController> requireZombieController)
            {
                requireZombieController.InjectDependency(_zombieController);
            }
        }

        private void StartInit()
        {
            _zombieController.Init();
        }

        private void CheckInput()
        {
            PlayerInput playerInput = PlayerInput.Instance;

            if (playerInput.HaveControl && Input.GetKeyDown(KeyCode.Alpha1))
            {
                _zombieController.ChooseZombie(0);
            }

            if (playerInput.HaveControl && Input.GetKeyDown(KeyCode.Alpha2))
            {
                _zombieController.ChooseZombie(1);
            }

            if (playerInput.HaveControl && Input.GetKeyDown(KeyCode.Alpha3))
            {
                _zombieController.ChooseZombie(2);
            }
        }
    }
}

