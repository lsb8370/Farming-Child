using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EStateEventType
{
    Enter, Update, Exit
}

public class StateEventDispatcher : StateMachineBehaviour
{
    private Dictionary<string, Action> m_enterEventMap;
    private Dictionary<string, Action> m_updateEventMap;
    private Dictionary<string, Action> m_exitEventMap;

    public void AddStateEventCallback (EStateEventType eventType, string stateName, Action callback)
    {
        switch (eventType)
        {
            case EStateEventType.Enter:
                if (m_enterEventMap.ContainsKey(stateName))
                {
                    m_enterEventMap[stateName] += callback;
                }
                else
                {
                    m_enterEventMap[stateName] = callback;
                }
                break;

            case EStateEventType.Update:
                if (m_updateEventMap.ContainsKey(stateName))
                {
                    m_updateEventMap[stateName] += callback;
                }
                else
                {
                    m_updateEventMap[stateName] = callback;
                }
                break;

            case EStateEventType.Exit:
                if (m_exitEventMap.ContainsKey(stateName))
                {
                    m_exitEventMap[stateName] += callback;
                }
                else
                {
                    m_exitEventMap[stateName] = callback;
                }
                break;
        }
    }

    public void RemoveStateEventCallback(EStateEventType eventType, string stateName, Action callback)
    {
        switch (eventType)
        {
            case EStateEventType.Enter:
                if (m_enterEventMap.ContainsKey(stateName))
                {
                    m_enterEventMap[stateName] -= callback;
                }
                break;

            case EStateEventType.Update:
                if (m_updateEventMap.ContainsKey(stateName))
                {
                    m_updateEventMap[stateName] -= callback;
                }
                break;

            case EStateEventType.Exit:
                if (m_exitEventMap.ContainsKey(stateName))
                {
                    m_exitEventMap[stateName] -= callback;
                }
                break;
        }
    }

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var enterEvent in m_enterEventMap)
        {
            if (stateInfo.IsName (enterEvent.Key))
            {
                enterEvent.Value.Invoke();
            }
        }
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var enterEvent in m_updateEventMap)
        {
            if (stateInfo.IsName(enterEvent.Key))
            {
                enterEvent.Value.Invoke();
            }
        }
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var enterEvent in m_exitEventMap)
        {
            if (stateInfo.IsName(enterEvent.Key))
            {
                enterEvent.Value.Invoke();
            }
        }
    }

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}
