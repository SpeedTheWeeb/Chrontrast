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
    public void Restart()// Bruges ikke i nuværende version
    {
        SceneManager.LoadScene("Game Scene"); //ændres til startskærm når vi har sådan en


        //Her kan nemt tilføjes en main menu button!
    }
}
