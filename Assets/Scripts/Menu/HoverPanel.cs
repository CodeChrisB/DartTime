using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverPanel : MonoBehaviour
{
    Color thisColor;
    Color darkerColor;
    public float darkenOnHover = 0.08f;
    private void Start()
    {
        thisColor = gameObject.GetComponent<Image>().color;
        darkerColor = new Color(thisColor.r - darkenOnHover, thisColor.g - darkenOnHover, thisColor.b - darkenOnHover);
    }
    private void OnMouseEnter()
    {
        gameObject.GetComponent<Image>().color = darkerColor;
    }

    private void OnMouseOver()
    {
        gameObject.GetComponent<Image>().color = darkerColor;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Image>().color = thisColor;
    }
}
