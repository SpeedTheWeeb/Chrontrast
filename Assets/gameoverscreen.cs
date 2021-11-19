
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameoverscreen : MonoBehaviour
{

    public void Setup() 
    {
        gameObject.SetActive (true);
    }

    public void RestartButton() 
    {
        SceneManager.LoadScene("Game Scene"); //�ndres til startsk�rm n�r vi har s�dan en
        Time.timeScale = 1f;
    }
    

}
