using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupMenu : MonoBehaviour
{
    private int _numberOfNpc = 4;
    [SerializeField] private Dropdown npcDropdown;
    private int _numberOfImposter = 1;
    [SerializeField] private Dropdown imposterDropdown;

    [SerializeField] private NPCManager spawner;
    [SerializeField] private List<GameObject> objectsToSpawn = new List<GameObject>();

    public void ChangeNumberOfNpc()
    {
        _numberOfNpc = npcDropdown.value + 4;
    }
    
    public void ChangeNumberOfImposter()
    {
        _numberOfImposter = imposterDropdown.value + 1;
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
        spawner.SetNumberOfNPC(_numberOfNpc);
        spawner.SetNumberOfImposter(_numberOfImposter);
        foreach (GameObject objectToSpawn in objectsToSpawn)
        {
            objectToSpawn.SetActive(true);
        }
    }
}
