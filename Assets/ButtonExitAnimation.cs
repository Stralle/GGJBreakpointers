using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExitAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    public void ButtonExitGame(){
        GetComponent<ButtonExitAnimation>().Play("ButtonExitGame")
    }
}
