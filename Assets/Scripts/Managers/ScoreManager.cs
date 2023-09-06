using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour
{

    public static int score;
    public static int highschore;
    public static float health;
    public static string quickFireTimer;
    public static int bombsInventory;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highschoreText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI quickFireTimerText;
    [SerializeField] private GameObject bomb1;
    [SerializeField] private GameObject bomb2;
    [SerializeField] private GameObject bomb3;
    [SerializeField] private GameObject bomb4;

    //private static ScoreManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        //SetSingleton();

        
        //set score
    }

    private void CheckForBombs()
    {
        bomb1.SetActive(false);
        bomb2.SetActive(false);
        bomb3.SetActive(false);
        bomb4.SetActive(false);
        switch (bombsInventory)
        {
            case 0:
                break;
            case 1:
                bomb1.SetActive(true); 
                break;
            case 2:
                bomb2.SetActive(true);
                break;
            case 3:
                bomb3.SetActive(true);
                break;
            case 4:
                bomb4.SetActive(true);
                break;

        }
    }
    //void SetSingleton()
    //{
    //    if (instance != null && instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        instance = this;
    //    }
    //    DontDestroyOnLoad(this);
    //}

    public void SetScoreCanvas(int _score, int _highscore, float _health)
    {
        score = _score;
        highschore = _highscore;
        health = _health;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score " + score.ToString();
        healthText.text = "Health: " + Mathf.RoundToInt(health).ToString();
        //highscore
        //quickfire timer
        quickFireTimerText.text =  quickFireTimer;
        CheckForBombs();
    }
}
