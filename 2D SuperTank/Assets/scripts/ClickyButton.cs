using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressed;
    [SerializeField] private AudioClip compressClip;
    [SerializeField] private AudioSource _source;
    // Start is called before the first frame update
    
    public void OnPointerDown(PointerEventData evenData)
    {
        _img.sprite = _pressed;
        try
        {
            _source.PlayOneShot(compressClip);

        }
        catch
        {
            Debug.Log("No sound assigned!");
        }
    }
    
    public void OnPointerUp(PointerEventData evenData)
    {
        _img.sprite =_default;
        //_source.PlayOneShot(_unconpressClip);
    }
}
