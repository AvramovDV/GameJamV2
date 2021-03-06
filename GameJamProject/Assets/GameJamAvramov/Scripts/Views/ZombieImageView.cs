using UnityEngine;
using UnityEngine.UI;

namespace GameJam
{
    public class ZombieImageView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Image _weaponImage;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _deselectedColor;

        public void Select()
        {
            _image.color = _selectedColor;
            _weaponImage.color = _selectedColor;
        }

        public void Deselect()
        {
            _image.color = _deselectedColor;
            _weaponImage.color = _deselectedColor;
        }
    }
}

