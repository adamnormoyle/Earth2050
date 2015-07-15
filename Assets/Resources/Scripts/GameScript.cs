using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;



public class GameScript : MonoBehaviour {

	bool GameActive;


	float DataScanner_PowerRequirement;


	bool ConsoleActive;
	string ConsoleText;

	int ConsoleOutputHeight;
	public List<string> ConsoleOutput;
	string OutputText = "";

	Transform Root;



	GameObject[] GameOBJ;



	// Use this for initialization
	void Start () {

		Root= this.transform;

		while(Root.parent != null){
			Root = Root.parent;
			Debug.Log(Root.name);
		}
	
		GameActive = true;
		ConsoleActive = false;
		ConsoleText = "";
		ConsoleOutput = new List<string>{"Test"};







	}
	
	// Update is called once per frame
	void Update () {

		GameInput ();

		if (GameActive == false) {

			// Pause Game
			Time.timeScale = 0;

			Screen.showCursor = true;
			Screen.lockCursor = false;

		} else {

			// Resume Game
			Time.timeScale = 1;

			Screen.showCursor = false;
			Screen.lockCursor = true;

		}



	}

	void GameInput(){

		//Pause
		if(Input.GetKey (KeyCode.Escape)){GameActive = !GameActive;}
		//ConsoleKey
		if(Input.GetKey ("`")){ConsoleActive = true;}
		//Data Scanner
		if(Input.GetKey (KeyCode.C)) {

			if(PlayerScript.Power >= DataScanner_PowerRequirement){

				GameOBJ = (GameObject[])GameObject.FindGameObjectsWithTag("Object");

				foreach(GameObject OBJ in GameOBJ){
				
				if(OBJ && OBJ.transform.parent == null){
				//OBJ.gameObject.BroadcastMessage("DataScanner",PlayerScript.DataTimer);
			
				}

				}
			}

		}

	}

	void OnGUI(){


		if (ConsoleActive == true) {

			//Close Console
			if(Event.current.isKey  && Event.current.keyCode == KeyCode.Escape){ConsoleActive = false;}

			//Send Command
			if(Event.current.isKey  && Event.current.keyCode == KeyCode.Return){

				string[] temp_command = ConsoleText.Split(" "[0]);

				string Command = temp_command[0];
				string Variable = temp_command[1];
				float value;
				System.Single.TryParse (temp_command [2], out value);

				ConsoleCommand(Command,Variable,value);
				ConsoleText = "";
			}

			if(ConsoleOutput.Count >= 4){

				for(int i = ConsoleOutput.Count ; i >= (4);)
					ConsoleOutput.RemoveAt(0);

				
			}

			
			

			//Start Console


			GUI.TextArea (new Rect(0,0,Screen.width,50 * (ConsoleOutput.Count +1)),OutputText);

			GUI.SetNextControlName ("Console");
			ConsoleText = GUI.TextField (new Rect(0,50 * (ConsoleOutput.Count +1),Screen.width,50),ConsoleText);

			//Focus Console
			if(GUI.GetNameOfFocusedControl ()==""){GUI.FocusControl ("Console");}


		}

	}



	//Console Commands
	

	void Set(string parameter , float value){

		if (parameter == "health") {
						PlayerScript.Health = value;
				} else
		if (parameter == "armour") {
						PlayerScript.Armour = value;
				} else
		if (parameter == "speed") {
						PlayerScript.Speed = value;
				} else
		if (parameter == "game_speed") {
						Time.timeScale = value;
				} else
		if (parameter == "Damage") {
						GetComponentInChildren<WeaponScript> ().Damage = value;
				} else
		if (parameter == "ammo") {
						GetComponentInChildren<WeaponScript> ().Ammo = value;
				} else {
						
				}

	}

	void Spawn(string parameter){





	}


	void ConsoleCommand(string cmd , string var , float val){


		ConsoleOutput.Add(">> "+cmd +" "+var+" "+val);
		string OutputText = ConsoleOutput[ConsoleOutput.Count] + "\n";

		if (cmd == "set") {Set (var, val);}







}
}

