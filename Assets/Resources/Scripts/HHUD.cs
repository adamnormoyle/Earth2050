using UnityEngine;
using System.Collections;

public class HHUD : MonoBehaviour {

	Texture2D HUD_Texture;
	Texture2D Reticle_Texture;
	Texture2D HealthBarGood;
	Texture2D HealthBarBad;
	float ReticleSize;
	bool HUD_Enabled;
	string Active_Weapon;

	// Use this for initialization
	void Start () {

		if(ReticleSize == 0){ReticleSize = 50;}

		HealthBarGood  = Resources.Load ("Textures/HealthBar_Good") as Texture2D;
		HealthBarBad  = Resources.Load ("Textures/HealthBar_Bad") as Texture2D;
		Reticle_Texture = Resources.Load ("Textures/Reticle") as Texture2D;
		HUD_Texture = Resources.Load ("Textures/HUD") as Texture2D;

	}
	
	// Update is called once per frame
	void Update () {

		//Update Variables


	}

	void OnGUI(){


		// Draw HUD if it is enabled
		if (HUD_Enabled == true) {

			//HUD Texture
			GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),HUD_Texture);
			//Reticle Texture
			GUI.DrawTexture (new Rect((Screen.width /2) - (ReticleSize /2),(Screen.height /2) - (ReticleSize /2),ReticleSize,ReticleSize),Reticle_Texture);
			// Draw Active Weapon GUI (Need to specify location)
			//GUI.DrawTexture (new Rect(0,0,250,100),Active_Weapon_Texture);


			//Draw HealthBar
			if(PlayerScript.Health <= 20){
			//Low Health
				Graphics.DrawTexture (new Rect((Screen.width /2) - (PlayerScript.Health *4 /2 ) - 75,30,PlayerScript.Health * 4 + 150,24),HealthBarBad,20,20,12,12);
			}else{
			//Good Health
				Graphics.DrawTexture (new Rect((Screen.width /2) - (PlayerScript.Health *4 /2)  -75,30,PlayerScript.Health * 4 + 150,24),HealthBarGood,20,20,12,12);
			}

		}


		

	}
}
