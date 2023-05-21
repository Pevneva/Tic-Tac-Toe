using UnityEngine;
using UnityEngine.UI;

namespace Scrips
{
    public class LineView : MonoBehaviour
    {
        [SerializeField] private GameObject _line;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;

        private float _delta;
        private float _increaseFactor = 1.35f;

        private void Start() =>
            _delta = _gridLayoutGroup.cellSize.x + _gridLayoutGroup.spacing.x;

        public void HideLine() =>
            _line.SetActive(false);

        public void ShowHorizontalLine(int cellNumber)
        {
            float deltaY = 0;

            if (cellNumber == 0) deltaY = _delta;
            if (cellNumber == 6) deltaY = -_delta;

            var position = new Vector3(0, deltaY, 0);

            _line.transform.localPosition = position;
            _line.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            _line.transform.localScale = new Vector3(1, 1, 1);
            _line.SetActive(true);
        }

        public void ShowVerticalLine(int cellNumber)
        {
            float deltaX = 0;

            if (cellNumber == 0) deltaX = -_delta;
            if (cellNumber == 2) deltaX = _delta;

            var position = new Vector3(deltaX, 0, 0);

            _line.transform.localPosition = position;
            _line.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            _line.transform.localScale = new Vector3(1, 1, 1);
            _line.SetActive(true);
        }

        public void ShowDiagonalLine(int index)
        {
            float angle = index == 1 ? 45 : -45;

            _line.transform.localPosition = Vector3.zero;
            _line.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            _line.transform.localScale = new Vector3(1, _increaseFactor, 1);
            _line.SetActive(true);
        }
    }
}