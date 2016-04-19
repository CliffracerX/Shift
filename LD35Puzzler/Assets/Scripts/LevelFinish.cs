using UnityEngine;
using System.Collections;

public class LevelFinish : MonoBehaviour
{
	public int levelID;

	void OnTriggerEnter(Collider other)
	{
		if(other.tag=="Respawn")
		{
			ProgressManager.UpdateCompletion(levelID, true);
			ProgressManager.currentState=ProgressManager.MenuStates.LevelSelect;
			Application.LoadLevel(0);
		}
	}
}