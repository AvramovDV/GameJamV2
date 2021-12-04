using UnityEngine;
using Gamekit2D;

namespace GameJam
{
    public class ZombieView : BaseView, IRequireInjection<ZombieController>
    {
        [SerializeField] private CapsuleCollider2D _collider;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidBody;

        private Vector3 _startPosition;
        private ZombieController _zombieController;

        [field: SerializeField] public int ZombieId { get; private set; }

        [field: SerializeField] public bool IsMeleeEnabled { get; private set; }

        [field: SerializeField] public bool IsGunEnabled { get; private set; }

        public Checkpoint Checkpoint { get; private set; }

        private void Start()
        {
            _startPosition = transform.position;
            SetUpCheckpoint();
        }

        private void OnDisable()
        {
            _zombieController.ChooseZombieEvent -= OnZombieChoosen;
            _zombieController.ResetEvent -= OnReset;
        }

        public void InjectDependency(ZombieController dependency)
        {
            _zombieController = dependency;

            _zombieController.ChooseZombieEvent += OnZombieChoosen;
            _zombieController.ResetEvent += OnReset;

            InitZombieModel();
        }

        private void InitZombieModel()
        {
            _zombieController.AddZombie(this);
        }

        private void OnZombieChoosen()
        {
            bool isChoosen = _zombieController.CurrentZombie.ZombieId == ZombieId;

            _spriteRenderer.enabled = !isChoosen;
            _collider.enabled = !isChoosen;
            _animator.enabled = !isChoosen;
            _rigidBody.isKinematic = isChoosen;
        }

        private void OnReset()
        {
            transform.position = _startPosition;
        }

        private void SetUpCheckpoint()
        {
            Checkpoint = new GameObject("Checkpoint").AddComponent<Checkpoint>();
            Checkpoint.transform.position = _startPosition;
            Checkpoint.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}

