using UnityEngine;
using UnityEngine.UI;

namespace Mirror.Examples.Basic
{
    public class PlayerUI : MonoBehaviour
    {


        [Header("Player Components")]
        public Image image;
        public Player _player;
        public Button _readyUpButton;

        [Header("Child Text Objects")]
        public Text playerNameText;

        // Sets a highlight color for the local player
        public void SetLocalPlayer()
        {
            // add a visual background for the local player in the UI
            image.color = new Color(1f, 1f, 1f, 0.1f);
        }

        // This value can change as clients leave and join
        public void OnPlayerNumberChanged(byte newPlayerNumber)
        {
            playerNameText.text = string.Format("Player {0:00}", newPlayerNumber);
        }

        // Random color set by Player::OnStartServer
        public void OnPlayerColorChanged(Color32 newPlayerColor)
        {
            playerNameText.color = newPlayerColor;
        }

        public void TogglePlayerReadyUp()
        {
            _player.GetComponent<NetworkRoomPlayer>().CmdChangeReadyState(true);
            _player.enabled= false;
            _readyUpButton.gameObject.SetActive(false);
        }

    }
}
