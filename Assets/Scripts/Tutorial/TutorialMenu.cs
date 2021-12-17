using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{

    public int movesMade = 0;
    public GameObject MovementKeysGO;
    public GameObject ThrowingShootingKeysGO;
    public GameObject PowerUpsGO;
    public GameObject TargetPracticeGO;


    // Start is called before the first frame update
    void Start () 
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            {
                movesMade++;
                Debug.Log(movesMade);
            }
        else if (Input.GetKeyDown(KeyCode.W))
            {
                movesMade++;
                Debug.Log(movesMade);
            }
        else if(Input.GetKeyDown(KeyCode.D))
            {
                movesMade++;
                Debug.Log(movesMade);
            }
        else if (Input.GetKeyDown(KeyCode.S))
            {
                movesMade++;
                Debug.Log(movesMade);
            }
        
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                movesMade++;
                Debug.Log(movesMade);
            }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                movesMade++;
            }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                movesMade++;
            }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                movesMade++;
            }

        if (movesMade >= 10)
        {
            MovementKeysGO.SetActive(false);
            ThrowingShootingKeysGO.SetActive(true);
        }

        if (ItemBehavior.tradesMade >= 2 && movesMade >=10)
        {
            ThrowingShootingKeysGO.SetActive(false);
            PowerUpsGO.SetActive(true);
        }

        /*if (powerupcondition true)
        {
            PowerUpsGO.SetActive(false);
            TargetPracticeGO-SetActive(true);
        }
        */

    }
}