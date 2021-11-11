using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    //public gameoverscreen gameoverscreen;

    public static bool GameEnd = false;

    public GameObject gameOverMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        GameEnd = false;
    }

    public void Endgame() // afslutter spillet ved at aktivere restart.
    {
        if (GameEnd == false)
        {
            Debug.Log("Game over");
            GameEnd = true;
            Time.timeScale = 0f;
            gameOverMenuUI.SetActive(true);
            //gameoverscreen.Setup();
        }
    }
    //public void Restart()// Bruges ikke i nuv�rende version
    //{
        //SceneManager.LoadScene("Game Scene"); //�ndres til startsk�rm n�r vi har s�dan en


        //Her kan nemt tilf�jes en main menu button!
    //}
}
