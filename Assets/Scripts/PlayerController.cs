using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

#region Public_Variables
    public float speedOfOverallMovement;
    public float smoothnessOfSwipe;
    
    public static PlayerController instance;
#endregion

#region Private_Variables

//Variables settings for swipe controls
private float _fingerstartTime = 0.0f;
private Vector2 _fingerStartPosition = Vector2.zero;
private bool _isSwipe = false;
private float _minSwipeDistanceHorizontal = 5.0f; 
private float _minSwipeDistanceVertical = 2.5f;
private float _maxSwipeTime = 0.5f;
Vector2 _dest = Vector2.zero;
private Vector3 _direction;

Vector2 firstPressPos;
Vector2 secondPressPos;
Vector2 currentSwipe;
#endregion

#region enumerator
public enum Direction
{
    None,
    Right,
    Left
    
};
Direction _myDirection;
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
	void Start () {
	   
	    _direction = Vector3.up;  //Main direction of player movement

	}
	
	// Update is called once per frame
	void Update ()
	{
        
        
        Vector2 p = Vector2.MoveTowards(transform.position, _dest, smoothnessOfSwipe);
        gameObject.GetComponent<Rigidbody2D>().MovePosition(p);


        Swipe();

        if (_myDirection == Direction.Right)
        {
            Right_Move();
        }

        else if (_myDirection == Direction.Left)
        {
            Left_Move();
        }
        else
        {
            TranslatePos(Vector3.up);            
        }




	    if (Input.GetKey(KeyCode.RightArrow))
	    {
            Debug.Log("Right");
            Right_Move();	        
	    }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
	    {
            TranslatePos(Vector3.up);
	    }

	    if (Input.GetKey(KeyCode.LeftArrow))
	    {
            Left_Move();
	    }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
	    {
            TranslatePos(Vector3.up);	        
	    }

	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
	        Application.Quit();
	    }
	}

	void OnDisable(){
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        //To create endless path
        if (col.gameObject.tag=="PathCreator")
        {
           PathManager.instance.GeneratePath();
            
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Load scene again when player touched red things
        if (col.gameObject.tag=="Sides" || col.gameObject.tag=="Obstacles")
        {
            Application.LoadLevel(Application.loadedLevel);
        }

    }
	#endregion

#region Private_Methods
#endregion

#region Public_Methods

   public Vector2 TranslatePos(Vector2 dir)
    {
        _dest = (Vector2)transform.position + dir;
        return _dest;
    }

    public void Right_Move()
    {
        TranslatePos(new Vector2(1f, 1f));
    }

    public void Left_Move()
    {
        TranslatePos(new Vector2(-1f, 1f));        
    }



    public void Swipe()
{
     if(Input.GetMouseButtonDown(0))
     {
         //save began touch 2d point
        firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
     }


        if (Input.GetMouseButton(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();


            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                _myDirection = Direction.Left;
            }
            //swipe right
            else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                _myDirection = Direction.Right;
            }
            else
            {
                _myDirection = Direction.None;
            }
        
            
        }
        /*if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();


            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                _myDirection = Direction.Left;
            }
            //swipe right
            else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                _myDirection = Direction.Right;
            }
            else
            {
                _myDirection = Direction.None;
            }
        }*/
        
}
 

    //Touch control function
    public void getTouchFunction()
    {

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _isSwipe = true;
                        _fingerstartTime = Time.time;
                        _fingerStartPosition = touch.position;
                        break;

                    case TouchPhase.Canceled:
                        _isSwipe = false;
                        break;

                    case TouchPhase.Ended:
                        float gestureTime = Time.time - _fingerstartTime;
                        float gesturePosition = (touch.position - _fingerStartPosition).magnitude;

                        if (_isSwipe && gestureTime < _maxSwipeTime) //&& gesturePosition > _minSwipeDistanceVertical)
                        {
                            Vector2 _direction = touch.position - _fingerStartPosition;
                            Vector2 _swipeType = Vector2.zero;

                            if (Mathf.Abs(_direction.x) > Mathf.Abs(_direction.y))
                            {
                                _swipeType = Vector2.right*Mathf.Sign(_direction.x);
                            }
                            else
                            {
                                _swipeType = Vector2.up*Mathf.Sign(_direction.y);
                            }

                            if (_swipeType.x != 0.0f && gesturePosition > _minSwipeDistanceHorizontal)
                            {
                                if (_swipeType.x > 0.0f)
                                {
                                   _myDirection=Direction.Right;
                                }
                                else if (_swipeType.x < 0.0f)
                                {
                                    //TranslatePos(Vector2.left);
                                    _myDirection = Direction.Left;
                                }
                                else
                                {
                                    _myDirection=Direction.None;
                                   
                                }
                            }

                           
                        }

                        break;
                }
            }
        }

    }

   
    #endregion

#region Coroutines
#endregion

#region Custom_CallBacks
#endregion
}
