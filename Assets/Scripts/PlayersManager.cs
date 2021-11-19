using Boxel.Core.Singletons;
using Unity.Netcode;
using UnityEngine;

public class PlayersManager : Singleton<PlayersManager>
{
    public NetworkVariable<int> playersInGame = new NetworkVariable<int>();

    public int PlayersInGame
    {
        get
        {
            return playersInGame.Value;
        }
    }

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            if(IsServer)
            {
                Debug.Log("a player just connected...");
                playersInGame.Value++;
            }
        };

        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            if(IsServer)
            {
                Debug.Log("a player just disconnected...");
                if(playersInGame.Value != 0)
                {
                    playersInGame.Value--;
                }
            }
        };
    }
}