using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
public class GameManager
{
    public Guild Guild{get;set;}
    public GameManager(){
        Guild = new Guild();
    }
}
}