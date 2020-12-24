using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsList : MonoBehaviour
{
    public UnityEvent OnOptionsPickUp;
    public static Action OnPlayerDeath;
    public static Action<int> OnHealthPickup;
    public static Action<int> OnScoreAction;
}
