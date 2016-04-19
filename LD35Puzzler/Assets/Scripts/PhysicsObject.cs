using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhysicsObject : MonoBehaviour
{
	public string objType;
	public Dictionary<string, GameObject> morphs;
	public Dictionary<string, bool> morphRotates;
	public Dictionary<string, bool> morphPosJumps;
	public string[] morphStrings;
	public GameObject[] morphObjs;
	public bool[] shouldRotateBools;
	public bool[] shouldJumpBools;
	public float tpCooldown;
	public bool hasPainted = false;

	void Start()
	{
		morphs = new Dictionary<string, GameObject>();
		morphRotates = new Dictionary<string, bool>();
		morphPosJumps = new Dictionary<string, bool>();
		for(int i = 0; i<morphStrings.Length; i++)
		{
			morphs.Add(morphStrings[i], morphObjs[i]);
			morphRotates.Add(morphStrings[i], shouldRotateBools[i]);
			morphPosJumps.Add(morphStrings[i], shouldJumpBools[i]);
		}
	}

	void Update()
	{
		tpCooldown-=Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag=="Changer" && !hasPainted)
		{
			ChangerScript cs = other.GetComponent<ChangerScript>();
			if(this.morphs.ContainsKey(cs.type))
			{
				GameObject go = (GameObject)Instantiate(morphs[cs.type], transform.position, transform.rotation);
				if(!morphRotates[cs.type])
				{
					go.transform.rotation=morphs[cs.type].transform.rotation;
				}
				if(morphPosJumps[cs.type])
				{
					go.transform.position=other.transform.position;
				}
				/*if(go.GetComponent<Rigidbody>() && this.gameObject.GetComponent<Rigidbody>())
				{
					go.GetComponent<Rigidbody>().velocity=this.gameObject.GetComponent<Rigidbody>().velocity;
					go.GetComponent<Rigidbody>().angularVelocity=this.gameObject.GetComponent<Rigidbody>().angularVelocity;
				}*/
				hasPainted=true;
				go.transform.parent=this.transform.parent;
				Destroy(this.gameObject);
			}
		}
	}
}