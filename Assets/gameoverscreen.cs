
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
        SceneManager.LoadScene("Game Scene"); //ændres til startskærm når vi har sådan en

    }
    

}
