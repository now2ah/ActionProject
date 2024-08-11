using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    bool _isActivated;
    int _level;

    public bool IsActivated { get { return _isActivated; } set { _isActivated = value; } }
    public int Level { get { return _level; } set { _level = value; } }

    public void LevelUp()
    {
        _level++;
    }

    protected virtual void Awake()
    {
        _isActivated = false;
        _level = 1;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
