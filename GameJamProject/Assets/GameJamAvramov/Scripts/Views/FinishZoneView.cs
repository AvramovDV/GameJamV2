using UnityEngine;
using Gamekit2D;

namespace GameJam
{
    public class FinishZoneView : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerCharacter player = collision.GetComponent<PlayerCharacter>();

            if (player != null)
            {
                player.Pause();
            }
        }
    }
}

