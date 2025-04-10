using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill : Skill
{
    [SerializeField] private int dotsNums;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;

    private GameObject[] dots;
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    
    public void GenerateDots()
    {

        dots = new GameObject[dotsNums];
        for (int i = 0; i < dotsNums; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }

        for (int i = 0; i < dotsNums; i++)
        {
            dots[i].transform.position = DotPosition(i * spaceBetweenDots);
        }
    }

    private Vector2 AimDirection()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = player.transform.position;
        Vector2 dir = mousePos - playerPos;

        return dir;
    }

    private Vector2 DotPosition(float t)
    {
        Vector2 pos = (Vector2)player.transform.position + new Vector2(AimDirection().normalized.x, AimDirection().normalized.y) * t +
                      0.5f * Physics2D.gravity * (t * t);
        return pos;
    }
}
