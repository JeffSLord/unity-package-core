using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
public class GameManagerBehavior : MonoBehaviour
{
    public static GameManagerBehavior Instance{get;set;}
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManagerBehavior.Instance != null && GameManagerBehavior.Instance!=this){
            Destroy(this.gameObject);
        }else{
            GameManagerBehavior.Instance = this;
            GameManagerBehavior.Instance.gameManager = new GameManager();
        }
        // manager = new GameManager();

        // if(GameManagerBehavior.manager == null){
        //     GameManagerBehavior.manager = new GameManagerBehavior();
        // }
    }
}
}