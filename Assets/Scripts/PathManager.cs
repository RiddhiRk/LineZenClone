using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathManager : MonoBehaviour {

#region Public_Variables
    public static PathManager instance;
    public GameObject pathGameObjectOfLeftStraightSide;
    public GameObject pathGameObjectOfRightStraightSide;
    public int obstacleListCount;
    public int leftShapeListCount;
    public int rightShapeListCount;

    public List<GameObject> LeftShapesList=new List<GameObject>();
    public List<GameObject> RightShapesList = new List<GameObject>();
    public List<GameObject> ObstaclesList=new List<GameObject>(); 

    public GameObject poolManagerForLeftSideStraight;
    public GameObject poolManagerForLeftSideDifferent;
    public GameObject poolManagerForRightSideStraight;
    public GameObject poolManagerForRightSideDifferent;
    

#endregion

#region Private_Variables
    private int _count = 0;
    private ObjectPoolingDemo _scriptLeftStraightPool;
    private ObjectPoolingDemo _scriptLeftDifferentPool;
    private ObjectPoolingDemo _scriptRightStraightPool;
    private ObjectPoolingDemo _scriptRightDifferentPool;
    

#endregion

#region Events
#endregion

	#region Unity_CallBacks
	void Awake(){
	    if (instance==null)
	    {
	        instance = this;
	    }
	}

	void OnEnable(){
	
	}

	// Use this for initialization
	void Start ()
	{

        //Using object pool for some objects to do optimization
	    _scriptLeftStraightPool = poolManagerForLeftSideStraight.GetComponent<ObjectPoolingDemo>();
	    _scriptLeftDifferentPool = poolManagerForLeftSideDifferent.GetComponent<ObjectPoolingDemo>();
        _scriptRightStraightPool = poolManagerForRightSideStraight.GetComponent<ObjectPoolingDemo>();
        _scriptRightDifferentPool = poolManagerForRightSideDifferent.GetComponent<ObjectPoolingDemo>();
	    

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDisable(){
	
	}
	#endregion

#region Private_Methods
#endregion

#region Public_Methods

    public void GeneratePath()
    {

        GameObject pathObjLeftStraightSide = _scriptLeftStraightPool.GetPooledObject();
        pathObjLeftStraightSide.transform.position = pathGameObjectOfLeftStraightSide.transform.Find("Side1").gameObject.transform.Find("TopCenterPivot").gameObject.transform.position;
        pathGameObjectOfLeftStraightSide = pathObjLeftStraightSide;
        pathObjLeftStraightSide.SetActive(true);


        GameObject pathObjRightStraightSide = _scriptRightStraightPool.GetPooledObject();
        pathObjRightStraightSide.transform.position = pathGameObjectOfRightStraightSide.transform.Find("Side2").gameObject.transform.Find("TopCenterPivot").gameObject.transform.position;
        pathGameObjectOfRightStraightSide = pathObjRightStraightSide;
        pathObjRightStraightSide.SetActive(true);


        if (_count%2==0)
        {
            GameObject pathObjLeftDifferentSide = _scriptLeftDifferentPool.GetPooledObject();
            pathObjLeftDifferentSide.transform.position =pathGameObjectOfLeftStraightSide.transform.Find("Side1").gameObject.transform.Find("MidPosPivot").gameObject.transform.position;
            pathObjLeftDifferentSide.SetActive(true);


            GameObject rightShapeObj = Instantiate(RightShapesList[Random.Range(0,rightShapeListCount)]);
            rightShapeObj.transform.position = pathGameObjectOfRightStraightSide.transform.Find("Side2").gameObject.transform.Find("MidPosPivot").gameObject.transform.position;

            if (_count%Random.Range(1,5)==0)
            {
                GameObject obstacleObj = Instantiate(ObstaclesList[Random.Range(0, obstacleListCount)]) as GameObject;
                obstacleObj.transform.position = pathGameObjectOfLeftStraightSide.transform.Find("Side1").gameObject.transform.Find("ObstacleCreator").gameObject.transform.position;
            
            }
            
        }
        
        else
        {
            
            GameObject pathObjRightDifferentSide = _scriptRightDifferentPool.GetPooledObject();
            pathObjRightDifferentSide.transform.position =pathGameObjectOfRightStraightSide.transform.Find("Side2").gameObject.transform.Find("MidPosPivot").gameObject.transform.position;
            pathObjRightDifferentSide.SetActive(true);

            GameObject leftShapeObj = Instantiate(LeftShapesList[Random.Range(0, leftShapeListCount)]);
            leftShapeObj.transform.position = pathGameObjectOfLeftStraightSide.transform.Find("Side1").gameObject.transform.Find("MidPosPivot").gameObject.transform.position;

            if (_count%Random.Range(1,5) == 0)
            {
                GameObject obstacleObj = Instantiate(ObstaclesList[Random.Range(0, obstacleListCount)]) as GameObject;
                obstacleObj.transform.position = pathGameObjectOfRightStraightSide.transform.Find("Side2").gameObject.transform.Find("ObstacleCreator").gameObject.transform.position;
 
            }
            
        }


        _count++;
       


    }
#endregion

#region Coroutines
#endregion

#region Custom_CallBacks
#endregion
}
