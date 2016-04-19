using UnityEngine;
using System.Collections;

public class SubPortal : MonoBehaviour
{
	public PortalScript parentPortal;

	void OnTriggerEnter(Collider other)
	{
		this.parentPortal.OTE(other);
	}
}