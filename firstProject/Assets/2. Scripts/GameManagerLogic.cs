using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerLogic : MonoBehaviour
{

    public int totalItemCount;
    public int stage;
    public Text stageText;
    public Text playerText;
   

    void Awake()
    {
        stageText.text = "/ " + totalItemCount;
    }
    
    public void GetItem(int count)
    {
        playerText.text = count.ToString();
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Plyaer"){
            SceneManager.LoadScene(stage);
        }

    }
}
