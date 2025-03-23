using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteButton : MonoBehaviour
{
    public Color npcColor; //à définir en sérialisant
    
    public NPCMovement npcMovement;
    public Imposter imposter;
    
    public void Click()
    {
        if (npcMovement != null) npcMovement.Die();
        else imposter.Die();
    }
}
