using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
public class Guild
{
    public List<int> MemberIDs{get;set;}
    public List<int> TempMemberIDs{get;set;}
    public int Gold{get;set;}
    


    public Guild(){
        this.MemberIDs = new List<int>();
        this.TempMemberIDs = new List<int>();
    }
}
}