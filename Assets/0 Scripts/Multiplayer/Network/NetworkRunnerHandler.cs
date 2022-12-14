using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using UnityEngine.SceneManagement;

public class NetworkRunnerHandler : MonoBehaviour
{
    public NetworkRunner networkRunnerPrefab;
    
    private NetworkRunner _networkRunner;

    // Start is called before the first frame update
    void Start()
    {
        _networkRunner = Instantiate(networkRunnerPrefab);
        _networkRunner.name = "Network Runner";

        var clientTask = InitializeNetworkRunner(_networkRunner, GameMode.AutoHostOrClient, NetAddress.Any(),
            SceneManager.GetActiveScene().buildIndex, null);
        
        print("Server NetworkRunner started");
    }

    protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode, NetAddress address,
        SceneRef scene, Action<NetworkRunner> initialized)
    {
        var sceneManager = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();

        if (sceneManager == null)
        {
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        runner.ProvideInput = true;
        
        return runner.StartGame(new StartGameArgs
        {
          GameMode  = gameMode,
          Address = address,
          Scene = scene,
          SessionName = "Test Room",
          Initialized = initialized,
          SceneManager = sceneManager
        });
    }
}
