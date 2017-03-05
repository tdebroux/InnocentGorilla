using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorterStationary : MonoBehaviour {
    public int displacement;
    // Use this for initialization
    void Start () {
        GetComponent<SpriteRenderer>().sortingOrder = (Mathf.RoundToInt((transform.position.y) * 100f) - displacement) * -1;
    }
}
