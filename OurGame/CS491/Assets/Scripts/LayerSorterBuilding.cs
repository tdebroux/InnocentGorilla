using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorterBuilding : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = (Mathf.RoundToInt((transform.position.y) * 100f) - 100) * -1;
    }
}
