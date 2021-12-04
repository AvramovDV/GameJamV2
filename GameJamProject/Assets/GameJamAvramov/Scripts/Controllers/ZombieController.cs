using System;
using System.Collections.Generic;
using Gamekit2D;
using UnityEngine;

namespace GameJam
{
    public class ZombieController
    {
        private Dictionary<int, ZombieView> _zombiesDict = new Dictionary<int, ZombieView>();
        private List<ZombieView> _activeZombies = new List<ZombieView>();
        private Transform _palyerTransform;

        public event Action ChooseZombieEvent = delegate { };
        public event Action ActivateZombieEvent = delegate { };
        public event Action ResetEvent = delegate { };

        public ZombieView CurrentZombie { get; private set; }

        public void AddZombie(ZombieView zombie)
        {
            if (!_zombiesDict.ContainsKey(zombie.ZombieId))
            {
                _zombiesDict.Add(zombie.ZombieId, zombie);
            }
            else
            {
                throw new ArgumentException("Zombies has the same Ids!");
            }
        }

        public void SetPlayerTransform(Transform transform)
        {
            _palyerTransform = transform;
        }

        public void Init()
        {
            ActivateZombie(_zombiesDict[0].ZombieId);

            ChooseZombie(0);
        }

        public void ChooseZombie(int zombieIndex)
        {
            if (_activeZombies.Count > zombieIndex)
            {
                ZombieView zombie = _activeZombies[zombieIndex];

                if (CurrentZombie != zombie)
                {
                    if (CurrentZombie != null)
                    {
                        CurrentZombie.transform.position = _palyerTransform.position;
                    }

                    CurrentZombie = zombie;

                    ChooseZombieEvent.Invoke();
                }
            }
        }

        public void ActivateZombie(int zombieId)
        {
            ZombieView zombie = _zombiesDict[zombieId];

            if (!_activeZombies.Contains(zombie))
            {
                _activeZombies.Add(zombie);

                ActivateZombieEvent.Invoke();
            }
        }

        public int GetActiveZombieCount() => _activeZombies.Count;

        public int GetCurrentZombieIndex() => _activeZombies.IndexOf(CurrentZombie);

        public void Reset()
        {
            ResetEvent.Invoke();

            _activeZombies.Clear();

            CurrentZombie = null;

            Init();
        }
    }
}

