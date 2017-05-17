using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

#region Public_Variables
    public Text scoreText;
    public float score=0;
#endregion

#region Private_Variables
#endregion

#region Events
#endregion

	#region Unity_CallBacks
	void Awake(){
	
	}

	void OnEnable(){
	
	}

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update ()
	{
	    score = score + Time.deltaTime;
	    scoreText.text = "Score: " + Mathf.Floor(score%60);
	}

	void OnDisable(){
	
	}
	#endregion

#region Private_Methods
#endregion

#region Public_Methods
#endregion

#region Coroutines
#endregion

#region Custom_CallBacks
#endregion
}
