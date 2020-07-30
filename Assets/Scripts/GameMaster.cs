using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public GameObject mass;
    MassControl masscontrol;
    [SerializeField] GameObject starttext;
    [SerializeField] GameObject endtext;
    [SerializeField] GameObject wintext;
    bool started = false;
    bool ended = false;

    void Start()
    {
        masscontrol = mass.GetComponent<MassControl>();
    }

    void Update()
    {
        if(mass.transform.childCount == 0)
        {
            FailGame();
        }

        if (Input.GetKeyDown(KeyCode.Space) && started == false)
        {
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.Space) && ended == true)
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (masscontrol.wingame)
        {
            WinGame();
        }
    }

    void FailGame()
    {
        masscontrol.startgame = false;
        started = false;
        endtext.SetActive(true);
        ended = true;

    }

    void StartGame()
    {
        masscontrol.startgame = true;
        started = true;
        starttext.SetActive(false);
    }

    void WinGame()
    {
        masscontrol.startgame = false;
        started = false;
        wintext.SetActive(true);
        ended = true;
    }
}
