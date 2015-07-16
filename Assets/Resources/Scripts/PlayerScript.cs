using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	static public float Health;
	static public float Armour;
	static public float Speed;
	static public float Power;
	static public float DataTimer;

	// Use this for initialization
	void Start () {
	
		Health = 100;
		DataTimer = 5;

	}
	
	// Update is called once per frame
	void Update () {
	

		UpgradeAttribute (Power, 0.1f);


	}

	void UpgradeAttribute(float attribute,float i){

		attribute += i;
		Debug.Log (attribute);

	}


}
