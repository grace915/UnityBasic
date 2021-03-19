using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public Image portraitImg;
    public bool isAction;
    public int talkIndex;
    // Start is called before the first frame update

    private void Awake()
    {
        talkPanel.SetActive(false);
    }
    public void Action(GameObject scanObj)
    {

        scanObject = scanObj;
        ObjectData objectData = scanObj.GetComponent<ObjectData>();
        Talk(objectData.id, objectData.isNpc);
 

        talkPanel.SetActive(isAction);
       
    }
    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }
            

        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0];
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;
        talkIndex++;

    }

}
