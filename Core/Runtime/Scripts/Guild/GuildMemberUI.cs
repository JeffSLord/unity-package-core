using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
public class GuildMemberUI : MonoBehaviour
{
    public GameObject memberUI;
    public void RenderGuildMembers(){
        foreach (int memberID in GameManagerBehavior.Instance.gameManager.Guild.MemberIDs)
        {
            GameObject _ui = GameObject.Instantiate(memberUI);
            _ui.transform.parent = this.transform;
        }
    }
}
}