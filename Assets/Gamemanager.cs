using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public gameoverscreen gameoverscreen;
    public bool GameEnd = false;
    public void Endgame() // afslutter spillet ved at aktivere restart.
    {
        if (GameEnd == false)
        {
            Debug.Log("game over");
            GameEnd = true;
            gameoverscreen.Setup();
        }
    }
    public void Restart()// Bruges ikke i nuv�rende version
    {
        SceneManager.LoadScene("Game Scene"); //�ndres til startsk�rm n�r vi har s�dan en


        //Her kan nemt tilf�jes en main menu button!
    }
}