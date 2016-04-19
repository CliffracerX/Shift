using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour
{
	public GameObject objToEnable;
	public string pressedType;
	public bool objEnableType;
	public AudioClip goodClip,failClip;

	void OnTriggerStay(Collider other)
	{
		if(other.tag=="Player")
		{
			PhysicsObject po = other.GetComponent<PhysicsObject>();
			if(po.objType==pressedType)
			{
				if(objToEnable.activeSelf!=objEnableType)
				{
					this.GetComponent<AudioSource>().clip=goodClip;
					this.GetComponent<AudioSource>().Play();
				}
				objToEnable.SetActive(objEnableType);
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag=="Player")
		{
			PhysicsObject po = other.GetComponent<PhysicsObject>();
			if(po.objType==pressedType)
			{
				objToEnable.SetActive(!objEnableType);
				this.GetComponent<AudioSource>().clip=failClip;
				this.GetComponent<AudioSource>().Play();
			}
		}
	}
}