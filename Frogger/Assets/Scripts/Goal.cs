using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] 
    private GameObject frog;
    
    private bool _isFilled;

    private Action OnGoalFilled;

    private void Start()
    {
        OnGoalFilled += ShowFrog;
    }

    private void ShowFrog()
    {
        frog.SetActive(true);
    }

    public bool IsFilled
    {
        get => _isFilled;
        set
        {
            OnGoalFilled?.Invoke();
            _isFilled = value;
        }
    }
}
