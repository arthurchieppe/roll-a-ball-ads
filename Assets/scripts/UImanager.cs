using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public GameObject player;
    public GameObject endPanel;
    public GameObject youWonText;
    public GameObject countText;
    public TextMeshProUGUI timerText;
    public GameObject youLostText;
    public GameObject AdDisplay;


    public float timerGameOver;
    private float timePenalty;


    [SerializeField] private GameObject pickupParent;

    // Start is called before the first frame update
    void Start()
    {
        endPanel.SetActive(false);
        youWonText.SetActive(false);
        youLostText.SetActive(false);
        updateTimer();

    }

    // Update is called once per frame
    void Update()
    {
        timerGameOver -= Time.deltaTime;
        if (timePenalty > 0)
        {
            timePenalty -= Time.deltaTime;
            if (timePenalty <= 0)
            {
                timerText.color = Color.black;
                timePenalty = 0;
            }
        }
        updateTimer();
        CheckEndgame();
    }

    int countActiveChildren()
    {
        int count = 0;
        foreach (Transform child in pickupParent.transform)
        {
            if (child.gameObject.activeSelf)
            {
                count++;
            }
        }

        return count;
    }

    void CheckEndgame()
    {
        if (!player.activeSelf) return;
        if (countActiveChildren() != 0 && player.activeSelf && timerGameOver > 0) return;
        endPanel.SetActive(true);
        if (timerGameOver > 0) {
            youWonText.SetActive(true);
            AdDisplay.GetComponent<AdDisplay>().adStarted = true;
        } else {
            youLostText.SetActive(true);
            endPanel.GetComponent<Image>().color = new Color(255,0,0, (float) 0.6);
            AdDisplay.GetComponent<AdDisplay>().adStarted = true;
        };
        countText.SetActive(false);
        player.SetActive(false);
        timerText.GetComponent<TextMeshProUGUI>().enabled = false;

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void updateTimer()
    {
        // Cast timer to int to remove decimal places
        if (timePenalty > 0)
        {
            timerText.text = "Time Left: " + ((int) timerGameOver).ToString() + "(-3)";
        }
        else 
        {
            timerText.text = "Time Left: " + ((int) timerGameOver).ToString();
        }
    }

    public void deductTimer()
    {
        timerGameOver -= 3;
        updateTimer();
        timerText.color = Color.red;
        timePenalty = 2;
    }
    
}


