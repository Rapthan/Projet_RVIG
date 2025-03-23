using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoteMenu : MonoBehaviour
{
    public static VoteMenu Instance;
    private List<NPCMovement> npcMovements;
    private List<Imposter> imposters;
    [SerializeField] private List<VoteButton> buttons;
    
    private Dictionary<Color, VoteButton> colors;
    
    [SerializeField] private Transform player;
    private void Awake()
    {
        Instance = this;
        npcMovements = new List<NPCMovement>();
        imposters = new List<Imposter>();
        
        colors = new Dictionary<Color, VoteButton>();
        foreach (VoteButton button in buttons)
        {
            colors.Add(button.npcColor, button);
        }
    }

    private IEnumerator Start()
    {
        yield return null; // on attent que tout le monde soit instancié
        foreach (NPCMovement npc in npcMovements)
        {
            Color c = npc.renderer.material.color;
            colors[c].gameObject.SetActive(true);
            colors[c].npcMovement = npc;
        }
        foreach (Imposter imposter in imposters)
        {
            Color c = imposter.renderer.material.color;
            colors[c].gameObject.SetActive(true);
            colors[c].imposter = imposter;
        }
    }

    private void Update()
    {
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    public void AddNpc(NPCMovement npc)
    {
        npcMovements.Add(npc);
    }

    public void RemoveNpc(NPCMovement npc)
    {
        npcMovements.Remove(npc);
        Color c = npc.renderer.material.color;
        colors[c].gameObject.SetActive(false);
    }
    
    public void AddImposter(Imposter imposter)
    {
        imposters.Add(imposter);
    }

    public void RemoveImposter(Imposter imposter)
    {
        imposters.Remove(imposter);
        Color c = imposter.renderer.material.color;
        colors[c].gameObject.SetActive(false);
    }
}
