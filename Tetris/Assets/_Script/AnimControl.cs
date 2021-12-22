using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void _Button_listup()
    {
        animator.SetBool("showlist", true);
        //GetComponent<Animator>().Play("Up");
    }
    public void _Button_listdown()
    {
        animator.SetBool("showlist", false);
        //GetComponent<Animator>().Play("Down");
    }
    public void _Button_panelup()
    {
        animator.SetBool("showpanel", true);
        //GetComponent<Animator>().Play("Panelup");
    }
    public void _Button_paneldown()
    {
        animator.SetBool("showpanel", false);
        //GetComponent<Animator>().Play("Paneldown");
    }
}
