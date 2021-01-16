using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
public class QuestUI : MonoBehaviour
{
    public GameObject questUI;
    // public Quest quest;

    void Start(){
        Quest.OnUIUpdate += RenderQuests;
    }

    public void RenderQuests(){
        foreach (Quest q in Quest.AllQuests)
        {
            GameObject _ui = GameObject.Instantiate(questUI);
            _ui.transform.parent = this.transform;
        }
    }
    public void GenerateQuest(){
        new Quest();
        print(Quest.AllQuests.Count);
    }
}
}