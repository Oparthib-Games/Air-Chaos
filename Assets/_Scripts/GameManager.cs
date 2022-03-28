using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject coinText;
    Text cointTextComp;
    public int coin;







    public List<GameObject> set_lists = new List<GameObject>();

    float setSpawnPosY = 10;
    float setGaps = 30;

    GameObject Player;

    void Start()
    {
        cointTextComp = coinText.GetComponent<Text>();


        Player = GameObject.FindObjectOfType<PlayerCtrl>().transform.gameObject;
        SpawnSet();
    }

    void Update()
    {
        if(Player.transform.position.y > setSpawnPosY - setGaps)
        {
            SpawnSet();
        }

        cointTextComp.text = coin.ToString();
    }

    void SpawnSet()
    {
        int RandomIndex = Random.Range(0, set_lists.Count);

        Vector3 setSpawnPos = new Vector3(0, setSpawnPosY, 0);

        GameObject spawned_set = Instantiate(set_lists[RandomIndex], setSpawnPos, Quaternion.identity);

        setSpawnPosY += setGaps;
    }
}
