using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private Image dashImage;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && dashImage.fillAmount <= 0)
            dashImage.fillAmount = 1;
        
        FreshImageCooldown(dashImage, PlayerManager.instance.player.dashCooldown);
    }

    private void FreshImageCooldown(Image _image, float _cooldown)
    {
        if (_image.fillAmount > 0)
        {
            _image.fillAmount -= Time.deltaTime / _cooldown;
        }
    }
}
