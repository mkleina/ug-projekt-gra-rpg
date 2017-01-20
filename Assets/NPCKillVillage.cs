using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCKillVillage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // NOTE: Przykład zmiany dialogu NPC
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var allKilled = true;
        foreach (var enemy in enemies)
            if (enemy.GetComponent<NPCGroup>().groupID == 1 && !enemy.GetComponent<Animator>().GetBool("isDead"))
                allKilled = false;

        if (allKilled)
            GetComponent<NPCTalk>().setDialog(1);
    }
}
