using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
public class Quest
{
    public static List<Quest> AllQuests{get;set;} = new List<Quest>();
    public delegate void UIUpdate();
    public static event UIUpdate OnUIUpdate;
    public int ID{get;set;}
    public int PartyID{get;set;}
    public QuestType Type{get;set;}
    public Quest(){
        this.ID = Quest.AllQuests.Count;
        Quest.AllQuests.Add(this);
        if(OnUIUpdate!=null){
            OnUIUpdate();
        }
    }
    public static Quest GetQuest(int id){
        return Quest.AllQuests[id];
    }
    
}
}