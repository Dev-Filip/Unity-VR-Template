using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeadJumper : MonoBehaviour
{
    public int cacheSize = 10;
    private List<float> headPositionCache;
   
    private float playerHeight = 1f;
    private float jumpHeight = 1.4f;
    [SerializeField] private GameObject headReference;

    //public TextMeshProUGUI text;
    private void Start()
    {
        headPositionCache = new List<float>(cacheSize);
    }
    private void FixedUpdate()
    {
        CacheHeadPosition();
    }
    private void CacheHeadPosition()
    {
        
        if(headPositionCache.Count>cacheSize-1)
        {
            headPositionCache.RemoveAt(0);
            headPositionCache.Add(headReference.transform.localPosition.y);
            //text.text = headReference.transform.localPosition.y.ToString();
            return;
        }
        headPositionCache.Add(headReference.transform.localPosition.y);

    }
    public bool CheckForJump()
    {
        if (headPositionCache.Count < cacheSize)
            return false;
        if(headPositionCache[cacheSize-1]-headPositionCache[0]>0.15f && headPositionCache[cacheSize-1]>jumpHeight)
        {
            return true;
        }
        return false;
    }
}
