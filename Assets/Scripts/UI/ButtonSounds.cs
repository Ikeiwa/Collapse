using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

[RequireComponent(typeof(Button),typeof(AudioSource))]
public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    private AudioSource source;
    public AudioClip hover;
    public AudioClip press;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        source.PlayOneShot(hover);
    }

    public void OnClick()
    {
        source.PlayOneShot(press);
    }

    public void OnSelect(BaseEventData eventData)
    {
        source.PlayOneShot(hover);
    }
}
