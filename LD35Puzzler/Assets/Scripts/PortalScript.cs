using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour
{
	public GameObject[] dimensions;
	public AudioSource[] musicClips;
	public int currentDimension = 0;

	public void OTE(Collider other)
	{
		this.OnTriggerEnter(other);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag=="Respawn")
		{
			dimensions[currentDimension].SetActive(false);
			musicClips[currentDimension].volume=0;
			currentDimension+=1;
			currentDimension%=dimensions.Length;
			dimensions[currentDimension].SetActive(true);
			musicClips[currentDimension].volume=1;
		}
		if(other.tag=="Player")
		{
			if(other.GetComponent<PhysicsObject>().tpCooldown<=0)
			{
				int cd = (currentDimension+1)%dimensions.Length;
				other.transform.parent=dimensions[cd].transform;
				other.GetComponent<PhysicsObject>().tpCooldown=1;
			}
		}
	}
}