using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface CollectableBehavior
{
    void OnCollected(GameObject player);
}
