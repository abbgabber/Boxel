using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject leaveButton;

    [SerializeField]
    private Button startServerButton;

    [SerializeField]
    private Button startHostButton;

    [SerializeField]
    private Button startClientButton;

    [SerializeField]
    private TextMeshProUGUI playersInGameText;


    private void Start()
    {
        startHostButton.onClick.AddListener(() =>
        {
            if(NetworkManager.Singleton.StartHost())
            {
                Debug.Log("Host started");
                leaveButton.SetActive(true);
            }
            else
            {
                Debug.Log("Host not started");
            }
        });

        startServerButton.onClick.AddListener(() =>
        {
            if(NetworkManager.Singleton.StartServer())
            {
                Debug.Log("Server started");
                leaveButton.SetActive(true);
            }
            else
            {
                Debug.Log("Server not started");
            }

        });

        startClientButton.onClick.AddListener(() =>
        {
            if(NetworkManager.Singleton.StartClient())
            {
                Debug.Log("Client started");
                leaveButton.SetActive(true);
            }
            else
            {
                Debug.Log("Client not started");
            }

        });
    }

    private void Update()
    {
        playersInGameText.text = $"Players in game: {PlayersManager.Instance.PlayersInGame}";
    }

    public void Leave()
        {
            if (NetworkManager.Singleton.IsHost)
            {
                NetworkManager.Singleton.Shutdown();
                if(PlayersManager.Instance.playersInGame.Value != 0)
                {
                    PlayersManager.Instance.playersInGame.Value--;
                }
                Debug.Log("Host shut down");
            }
            else if (NetworkManager.Singleton.IsClient)
            {
                NetworkManager.Singleton.Shutdown();
                if(PlayersManager.Instance.playersInGame.Value != 0)
                {
                    PlayersManager.Instance.playersInGame.Value--;
                }
                Debug.Log("Client shut down");
            }
            else if (NetworkManager.Singleton.IsServer)
            {
                NetworkManager.Singleton.Shutdown();
                Debug.Log("Server shut down");
            }

            leaveButton.SetActive(false);
        }
}