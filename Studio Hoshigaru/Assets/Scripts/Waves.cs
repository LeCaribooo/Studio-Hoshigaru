using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Waves : MonoBehaviour
{
    private int CountWaves = 0;
    private bool W_inprogress = false;
    private bool RoomCleared = false;
    public Canvas DecompteCanvas;
    public Text DecompteTxt;
    public Canvas EndRoom;

    [SerializeField]
    private string backscene;

    [SerializeField]
    private int plusWaves_MAX;

    [SerializeField]
    private int plusWaves_MIN;

    [SerializeField]
    private int nbWaves;

    [SerializeField]
    private GameObject[] spawnpoint = new GameObject[8];

    [SerializeField]
    private GameObject[] enemies = new GameObject[2];

    private List<GameObject> mobwaves = new List<GameObject>();

    //Timer
    float time = 6f;
    float endtime = 10f;

    //== Borne Random ==\\

    // /!\ DOIT AVOIR LA MEME LENGHT QUE LE NOMBRE D'ENNEMIS DIFFERENTS
    [SerializeField]
    private int[] nbenemies_EACH_MAX = new int[2];

    // /!\ DOIT AVOIR LA MEME LENGHT QUE LE NOMBRE D'ENNEMIS DIFFERENTS
    [SerializeField]
    private int[] nbenemies_EACH_MIN = new int[2];

    //==================\\


    // Update is called once per frame
    void Update()
    {
        if (CountWaves <= nbWaves && W_inprogress && !RoomCleared)
        {
            DecompteCanvas.gameObject.SetActive(true);
            time -= Time.deltaTime;
            int sec = (int)time;
            DecompteTxt.text = "00 : 0" + sec.ToString();
            if (time <= 0f)
            {
                Debug.Log("Start Waves");
                W_inprogress = false;
                WavesFct();
                time = 6f;
            }

        }
        if (IsClear() && !W_inprogress)
        {
            CountWaves++;
            Debug.Log("Waves clear : " + CountWaves);
            W_inprogress = true;
            RoomCleared = CountWaves == nbWaves;


        }

        if (RoomCleared)
        {
            W_inprogress = true;
            Debug.Log("Room Cleared !");
            DecompteCanvas.gameObject.SetActive(true);
            EndRoom.gameObject.SetActive(true);
            endtime -= Time.deltaTime;
            int sec = (int)endtime;
            DecompteTxt.text = "00 : 0" + sec.ToString();
            if (endtime <= 0f)
            {
                RoomCleared = false;
                LoadRoom();
            }
        } 
    }

    //Lance les différentes vagues.
    private void WavesFct()
    {
        Fill_mobwaves();
        foreach (GameObject mob in mobwaves)
        {
            Debug.Log("Spawn & Instantiate");
            int spawnPoint = Random.Range(0, spawnpoint.Length);
            string name = mob.name;
            Debug.Log("Mob name :" + name);
            PhotonNetwork.Instantiate
                (Path.Combine("Prefab", "Enemy", name), spawnpoint[spawnPoint].transform.position, Quaternion.identity);

        }
        

    }

    //Vérifie si la vague est clear.
    private bool IsClear()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        return enemy.Length == 0;
    }

    //Rempli la liste mobwaves de tous les monstres.
    private void Fill_mobwaves()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            int maxRandom = nbenemies_EACH_MAX[i];
            int minRandom = nbenemies_EACH_MIN[i];
            int nb_mob = Random.Range(minRandom, maxRandom);
            Debug.Log("Nombre de " + enemies[i].name + " : " + nb_mob);
            for (int j = 0; j < nb_mob; j++)
            {
                mobwaves.Add(enemies[i]);
            }
        }
    }

    public void LoadRoom()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        foreach (var joueur in player)
        {
            DontDestroyOnLoad(joueur);
        } 
        PhotonNetwork.LoadLevel(backscene) ;
        Debug.Log("Room Loaded : " + backscene);
    }
}
