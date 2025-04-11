using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class KuNai_Skill : Skill
{
    [SerializeField] private GameObject kunaiPrefab;
    
    [SerializeField] private float kunaiGravity;
    [SerializeField] private Vector2 lanunchForce;
    
    [SerializeField] private int dotsNums;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;

    private GameObject[] dots;
    private Vector2 finnalDir;
    
    protected override void Start()
    {
        base.Start();
        
        GenerateDots();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            finnalDir = new Vector2(AimDirection().normalized.x * lanunchForce.x, AimDirection().normalized.y * lanunchForce.y);
        }
        
        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dotsNums; i++)
            {
                dots[i].transform.position = DotPosition(i * spaceBetweenDots);
            }
            SetDotsActive(true);
        }
    }

    public void CreateKuNai()
    {
        GameObject newKuNai = Instantiate(kunaiPrefab, dotsParent.transform.position, transform.rotation);
        KuNai_Skill_Controller newKuNaiScripts = newKuNai.GetComponent<KuNai_Skill_Controller>();
        
        newKuNaiScripts.SetKuNai(kunaiGravity, finnalDir);
        SetDotsActive(false);
    }

    public void SetDotsActive(bool isActive)
    {
        for (int i = 0; i < dotsNums; i++)
        {
            dots[i].SetActive(isActive);
        }
    }
    
    public void GenerateDots()
    {
        dots = new GameObject[dotsNums];
        for (int i = 0; i < dotsNums; i++)
        {
            dots[i] = Instantiate(dotPrefab, dotsParent.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 AimDirection()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 playerPos = player.transform.position;
        Vector2 dir = mousePos - (Vector2)dotsParent.transform.position;

        return dir;
    }

    private Vector2 DotPosition(float t)
    {
        Vector2 pos = (Vector2)dotsParent.transform.position + new Vector2(
                          AimDirection().normalized.x * lanunchForce.x, AimDirection().normalized.y * lanunchForce.y) * t +
                      0.5f * (Physics2D.gravity * kunaiGravity) * (t * t);
        return pos;
    }
}
