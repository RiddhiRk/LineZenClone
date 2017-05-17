using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolingDemo : MonoBehaviour
{
    #region Public_Variables
    public GameObject obj;
    public static ObjectPoolingDemo instance; //Singleton
    public int pooledAmount;
    public bool isPoolGrow;
    public int maxGrowAmount;
    public List<GameObject> pooledObjectList;
    #endregion

    #region Private_Variables
    private GameObject _tempGameObject;
    private int _count = 0;
    #endregion

    #region Events
    #endregion

    #region Unity_CallBacks
    void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        
    }

    // Use this for initialization
    void Start()
    {
        pooledObjectList = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            _tempGameObject = (GameObject)Instantiate(obj);      
            _tempGameObject.SetActive(false);
            pooledObjectList.Add(_tempGameObject);
        }
        //PathManager.instance.InitialCenterPoints();
    }

    #endregion

    #region Private_Methods
    #endregion

    #region Public_Methods
  
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjectList.Count; i++)
        {
            if (!pooledObjectList[i].activeInHierarchy)
            {
                return pooledObjectList[i];
            }
        }

        if (isPoolGrow)// && pooledObjectList.Count < maxGrowAmount)
        {
            _tempGameObject = (GameObject)Instantiate(obj);
            pooledObjectList.Add(_tempGameObject);
            return _tempGameObject;
        }
        return null;
    }

    public void DestroyPooledObject(GameObject go)
    {
        go.SetActive(false);
    }

    public void GameOverDisableObjects()
    {
        //pooledObjectList.Clear();
    }

    public void ResetPositions()
    {
        _count = 0;
    }

    #endregion

    #region Coroutines
    #endregion

    #region Custom_CallBacks
    #endregion
}
