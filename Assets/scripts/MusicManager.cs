using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] Image musicSprite;
    [SerializeField] Sprite On, Off;


    private void Start()
    {
        musicSource = GetComponent<AudioSource>();  
    }
    public void OnClick()
    {
        if (musicSource.mute)
        {
            musicSprite.sprite = On;
            musicSource.mute = false;
        }
        else
        {
            musicSprite.sprite = Off;
            musicSource.mute = true;
        }      
    }
}
