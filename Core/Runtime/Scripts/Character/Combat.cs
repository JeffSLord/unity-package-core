using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
    [System.Serializable]
public class Combat{
    public List<int> Enemies;
    public List<int> IDsInMelee;
    public List<int> IDsInRange;

    public Combat(){
        this.IDsInMelee = new List<int>();
        this.IDsInRange = new List<int>();
        this.Enemies = new List<int>();
    }
}
}