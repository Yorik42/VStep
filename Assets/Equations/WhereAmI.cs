using UnityEngine;
using System.Collections;

public class WhereAmI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(this.transform.position.ToString());
	}
}
