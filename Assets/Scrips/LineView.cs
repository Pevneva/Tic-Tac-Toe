using UnityEngine;

namespace Scrips
{
    public class LineView : MonoBehaviour
    {
        [SerializeField] private GameObject _line;

        public void HideLine() => 
            _line.SetActive(false);

        public void ShowHorizontalLine(int cellNumber)
        {
            float deltaY = 0;

            if (cellNumber == 0) deltaY = 118; //todo
            if (cellNumber == 6) deltaY = -118; //todo

            var position = new Vector3(0, deltaY, 0);

            _line.transform.localPosition = position;
            _line.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            _line.transform.localScale = new Vector3(1, 1, 1);
            _line.SetActive(true);
        }

        public void ShowVerticalLine(int cellNumber)
        {
            float deltaX = 0;

            if (cellNumber == 0) deltaX = -118; //todo
            if (cellNumber == 2) deltaX = 118; //todo

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
            _line.transform.localScale = new Vector3(1, 1.35f, 1);
            _line.SetActive(true);
        }
    }
}