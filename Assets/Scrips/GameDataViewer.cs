using TMPro;
using UnityEngine;

namespace Scrips
{
    public class GameDataViewer : MonoBehaviour
    {
        [SerializeField] private CurrentPlayerView _currentPlayerView;
        [SerializeField] private LineView _lineView;
        [SerializeField] private TextMeshProUGUI _winText;

        public void HideLine() =>
            _lineView.HideLine();

        public void ShowHorizontalLine(int cellNumber) =>
            _lineView.ShowHorizontalLine(cellNumber);

        public void ShowVerticalLine(int cellNumber) =>
            _lineView.ShowVerticalLine(cellNumber);

        public void ShowDiagonalLine(int cellNumber) =>
            _lineView.ShowDiagonalLine(cellNumber);

        public void ShowWinMessage(bool isCurrentPlayerWon, int currentPlayer)
        {
            _winText.gameObject.SetActive(true);

            if (isCurrentPlayerWon)
                _winText.text = "Player " + currentPlayer + " WON !!!";
            else
                _winText.text = " --- DRAW ---";
        }

        public void HideWinMessage() => _winText.gameObject.SetActive(false);

        public void ShowCurrentPlayer(int currentPlayer) =>
            _currentPlayerView.RenderCurrentPlayer(currentPlayer);
    }
}