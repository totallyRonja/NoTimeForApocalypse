using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonAudio : MonoBehaviour, ISelectHandler, IPointerEnterHandler{
    public AudioSource source;
    public AudioClip clip;

    void Awake(){
        GetComponent<Button>().onClick.AddListener(OnClick);
        print(gameObject);
    }

    public void OnClick(){
        source.PlayOneShot(clip);
    }
    public void OnSelect(BaseEventData eventData){
        source.PlayOneShot(clip);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        source.PlayOneShot(clip);
    }
}