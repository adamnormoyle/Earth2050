using UnityEngine;
using System.Collections;

public class ObjectScript : MonoBehaviour {


	public string Name;
	public string Description;
	public float Distance;

	Texture2D Tag_Texture;
	Texture2D Thumb_Texture;
	public Material Tag_Mat;

	TextMesh Name_Mesh;
	TextMesh Description_Mesh;

	Font Text_Font;
	Material Text_Mat;

	GameObject DescriptionOBJ;
	GameObject NameOBJ;
	GameObject Tag;

	bool ScanActive;
	bool ScanHit;

	RaycastHit Hit;
	Collider Col;

	//Calculate Heading,Distance and Direction
	Vector3 P;
	Vector3 H;
	Vector3 Dir;


	// Use this for initialization
	void Start () {

		//Global setup
		Text_Font = Resources.Load ("Fonts/Default") as Font;
		Text_Mat = Text_Font.material;
		ScanActive = false;



		//Setting Up Name Label
		NameOBJ = GameObject.CreatePrimitive (PrimitiveType.Cube);
		NameOBJ.transform.TransformPoint (NameOBJ.transform.position);
		NameOBJ.transform.localScale = new Vector3 (-1f,1f,1f);
		Destroy (NameOBJ.GetComponent<Collider> ());
		NameOBJ.transform.SetParent (this.transform);
		NameOBJ.transform.localPosition = new Vector3 (0,1, 0);
		NameOBJ.transform.rotation = new Quaternion (0, 0, 0, 0);
		NameOBJ.GetComponent<MeshRenderer>().material = Text_Mat;

		Name_Mesh = NameOBJ.AddComponent<TextMesh> ();
		Name_Mesh.text = Name;
		Name_Mesh.fontSize = 0;
		Name_Mesh.characterSize = 0.1f;
		Name_Mesh.font = Text_Font;
		Name_Mesh.alignment = TextAlignment.Center;
		Name_Mesh.anchor = TextAnchor.MiddleCenter;
		Name_Mesh.color = Color.blue;

		//Setting Up Description Label
		DescriptionOBJ = GameObject.CreatePrimitive (PrimitiveType.Cube);
		DescriptionOBJ.transform.localScale = new Vector3 (-0.4f,0.4f,0.4f);
		Destroy (DescriptionOBJ.GetComponent<Collider> ());
		DescriptionOBJ.transform.SetParent (NameOBJ.transform);
		DescriptionOBJ.transform.localPosition = new Vector3 (0, -0.3f, 0);
		DescriptionOBJ.transform.localRotation = new Quaternion (0, 0, 0, 0);
		Description_Mesh = DescriptionOBJ.AddComponent<TextMesh> ();
		DescriptionOBJ.GetComponent<MeshRenderer>().material = Text_Mat;

		Description_Mesh.text = Description;
		Description_Mesh.fontSize = 0;
		Description_Mesh.characterSize = 0.1f;
		Description_Mesh.font = Text_Font;
		Description_Mesh.alignment = TextAlignment.Center;
		Description_Mesh.anchor = TextAnchor.MiddleCenter;
		Description_Mesh.color = Color.blue;

		NameOBJ.SetActive (false);



		//Calculate Heading,Distance and Direction
		P = GameObject.Find ("Player").transform.position;
		H = NameOBJ.transform.position - P;
		Distance = H.magnitude;
		Dir = H / Distance;

	}
	
	// Update is called once per frame
	void Update () {
	
		//Calculate Heading,Distance and Direction
		P = GameObject.Find ("Player").transform.position;
		H = NameOBJ.transform.position - P;
		Distance = H.magnitude;
		Dir = H / Distance;

		if(Input.GetKey ("c") && ScanActive == false && GameObject.Find ("ScanEffect").GetComponent<ParticleSystem> ().isPlaying == false){

		GameObject.Find ("ScanEffect").GetComponent<ParticleSystem> ().Play ();

		}
	}

	 IEnumerator DataScanner(float T){

		ScanActive = true;

		//Loop Show tags on objects close to player for X amount of seconds
		//
		//
		float e = Distance / T;

		for (float i = 0 ; i < T+1; i += 1f * Time.fixedDeltaTime){

				//Check if the player is in direct LOS of object
				if (Physics.Raycast (NameOBJ.transform.position, -Dir, out Hit)) {

						//Look at Player using Heading
				NameOBJ.transform.LookAt (P);

				//Debug.Log (Hit.transform.name);
						
				Debug.DrawRay(NameOBJ.transform.position , -Dir);
						
						if (Hit.transform.name == "Player" && Hit.distance < T*2) {
								
								NameOBJ.SetActive(true);
								
								if (Hit.distance < 3) {
										DescriptionOBJ.GetComponent<MeshRenderer> ().enabled = true;
								} else {
										DescriptionOBJ.GetComponent<MeshRenderer> ().enabled = false;
								}
								
						} else {

								NameOBJ.SetActive (false);
								DescriptionOBJ.GetComponent<MeshRenderer> ().enabled = false;
								ScanHit = false;

						}
					
				}

			yield return null;

		}



		NameOBJ.SetActive (false);
		ScanActive = false;
		ScanHit = false;
		GameObject.Find ("ScanEffect").GetComponent<ParticleSystem> ().Stop ();

	}

	void OnParticleCollision(GameObject e){

		if (e.name == "ScanEffect" && ScanHit == false) {
		
			ScanHit = true;
			StartCoroutine(DataScanner(PlayerScript.DataTimer));


		}

	}

}
