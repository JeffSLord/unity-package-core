using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
public class Combat{
    public List<int> IDsInMelee;
    public List<int> IDsInRange;

    public Combat(){
        this.IDsInMelee = new List<int>();
        this.IDsInRange = new List<int>();
    }
}
}