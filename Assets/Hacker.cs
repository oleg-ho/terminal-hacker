using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Terminal.WriteLine("Hello %username%!");
        Terminal.WriteLine("Want to hack, huh?");
        Terminal.WriteLine("");
        Terminal.WriteLine("Press 1 to hack store");
        Terminal.WriteLine("Press 2 to hack ATM");
        Terminal.WriteLine("Press 3 to hack GYM");
        Terminal.WriteLine("");
        Terminal.WriteLine("Make your chose:");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
