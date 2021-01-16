using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
public class Party
{
    public List<int> CharacterIDs{get;set;}
    public Party(){
        this.CharacterIDs = new List<int>();
    }
    public Party(List<int> ids){
        this.CharacterIDs = ids;
    }
}
}