using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

public class PathDistructor : MonoBehaviour {

#region Public_Variables
   
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
	void Update () {
	
	}

	void OnDisable(){
	
	}


    //To destroy path which player has already passed through it
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="Sides")
        {
            //col.gameObject.transform.position = new Vector3(0,0,0);
            col.gameObject.transform.parent.gameObject.SetActive(false);
        }

        if (col.gameObject.tag=="Obstacles" || col.gameObject.tag=="Good")
        {
            Destroy(col.gameObject);
        }
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
