using TMPro;
using UnityEngine;

namespace Scrips
{
    public class CurrentPlayerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentPlayerText;

        public void RenderCurrentPlayer(int currentPlayer) => 
            _currentPlayerText.text = "Player " + currentPlayer;
    }
}
