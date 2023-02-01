using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSelectedButton : MonoBehaviour
{
    void OnEnable(){
        GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(gameObject);
    }
}
