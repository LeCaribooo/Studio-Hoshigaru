using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.IO;

public class LaunchGame : MonoBehaviourPunCallbacks, IInRoomCallbacks
{

    public int currentScene;
    public int multiplayScene;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if(currentScene == multiplayScene)
        {
            CreatePlayer();
        }
    }

    private void CreatePlayer()
    {
        Debug.Log("On créer un perso");
        //creates player network controller but not player character
       PhotonNetwork.Instantiate(Path.Combine("Prefab", "Photon", "PhotonNetworkPlayer"), transform.position, Quaternion.identity, 0);
       
    }
}
