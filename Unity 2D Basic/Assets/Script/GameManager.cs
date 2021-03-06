using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public GameObject[] Stages;

    public PlayerMove player;

    //ui
    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UIRestartBtn;

    private void Update()
    {
        //point
        UIPoint.text = ( totalPoint + stagePoint).ToString();
    }
    // Start is called before the first frame update
    public void NextStage()

    {   // change stage
        if (stageIndex < Stages.Length-1)
        {            
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            //calculate point
            totalPoint += stagePoint;
            stagePoint = 0;


            UIStage.text = "STAGE " + (stageIndex + 1);
        }
        else
        {
            //game clear
            //player control lock
            Time.timeScale = 0;
            //result ui
            Debug.Log("게임 클리어");

            //restart button ui
            Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Clear!";
            UIRestartBtn.SetActive(true);
            
        }
        


    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
            
        }
            
        
        else
        {
            //all health ui off
            UIhealth[0].color = new Color(1, 0, 0, 0.4f);
            //player die effect
            player.OnDie();
            //result UI
            Debug.Log("Die");
            //retry button ui
            UIRestartBtn.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
           

            //plyer reposition
            if (health > 1)
            {
                PlayerReposition();
            }

            //health down
            HealthDown();


        }
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, -1);
        player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }

   

}
