using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour, IItem
{
    public static event Action<int> OnOrbCollect;
    public int worth = 1;
    public void Collect()
    {
        SoundManager.instance.PlayCoinCollectSound();
        OnOrbCollect.Invoke(worth);
        Destroy(gameObject);
    }
}
