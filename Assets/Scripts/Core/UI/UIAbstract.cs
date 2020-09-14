using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void OnPauseMenu(bool isPause);
public abstract class UIAbstract : MonoBehaviour
{
    public static OnPauseMenu OnPauseMenu;
    public UnityEvent OnStartGame;
    public UnityEvent OnPauseGame;
    public UnityEvent OnStopGame;

    public virtual void Start()
    {
        OnStartGame.Invoke();
    }
    protected abstract void OnClickPause();
    
    public virtual void Exit()
    {
        OnStopGame.Invoke();
        Application.Quit();
    }
}
