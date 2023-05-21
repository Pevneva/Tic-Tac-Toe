using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scrips.Cell
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _crossIcon;
        [SerializeField] private Sprite _zeroIcon;

        public event Action Clicked;

        private void OnEnable() => 
            _button.onClick.AddListener(OnClickButton);

        public void ResetImage() => _image.color = Color.clear;

        public void RenderImage(CellImageType type)
        {
            if (type == CellImageType.NONE)
            {
                ResetImage();
                return;
            }

            switch (type)
            {
                case CellImageType.ZERO: _image.sprite = _zeroIcon;
                    break;
                
                case CellImageType.CROSS: _image.sprite = _crossIcon;
                    break;
            }
            
            _image.color = Color.white;
        }

        private void OnClickButton() => Clicked?.Invoke();
    }
}
