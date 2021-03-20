using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public Animator talkPanel;
    // public Text talkText;
    public TypeEffect talk;
    public GameObject scanObject;
    public Image portraitImg;
    public Animator portraitAnim;
    public Sprite prevSprite;
    public bool isAction;
    public int talkIndex;


    // Start is called before the first frame update

    private void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }

    public void Action(GameObject scanObj)
    {

        //get current object
        scanObject = scanObj;
        ObjectData objectData = scanObj.GetComponent<ObjectData>();
        Talk(objectData.id, objectData.isNpc);
 
        //visible talk for action
        talkPanel.SetBool("isShow", isAction);
       
    }
    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = 0;
        string talkData = "";

        //set talk data
        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;

        }
            
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        }

        //end talk
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }
            
        //continuew talk
        if (isNpc)
        {
            talk.SetMsg(talkData.Split(':')[0]);
            //show portrait
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);

            //portrit animation
            if(prevSprite != portraitImg.sprite)
            {
                portraitAnim.SetTrigger("doEffect");
                prevSprite = portraitImg.sprite;
            }
                
        }
        else
        {
            talk.SetMsg(talkData);

            //hideportrait
            portraitImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;
        talkIndex++;    

    }

}
