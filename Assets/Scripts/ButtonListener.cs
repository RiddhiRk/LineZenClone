using UnityEngine;
using System.Collections;

public class ButtonListener : MonoBehaviour {

#region Public_Variables
    public GameObject allObjects;
    public GameObject startPanel;
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

	void OnDisable(){
	
	}
	#endregion

#region Private_Methods
#endregion

#region Public_Methods

    public void StartGamePlay()
    {
        startPanel.SetActive(false);
        allObjects.SetActive(true);
    }
#endregion

#region Coroutines
#endregion

#region Custom_CallBacks
#endregion
}
