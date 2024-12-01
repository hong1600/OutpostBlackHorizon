using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInventory : MonoBehaviour
{
    public Transform content;
    public GameObject heroBtn1Pre;
    public GameObject heroBtn2Pre;
    public GameObject heroBtn3Pre;
    public GameObject heroBtn4Pre;
    public GameObject heroBtn5Pre;
    public UnitData[] units;

    private void Start()
    {
        loadHero();
    }

    private void loadHero()
    {
        for (int i  = 0; i < units.Length; i++)
        {
            switch (units[i].unitGrade)
            {
                case EUnitGrade.C:
                    GameObject newSlot1 = Instantiate(heroBtn1Pre, content);
                    HeroSlot slot1 = newSlot1.GetComponent<HeroSlot>();
                    slot1.setHero(units[i]);
                    break;
                case EUnitGrade.B:
                    GameObject newSlot2 = Instantiate(heroBtn2Pre, content);
                    HeroSlot slot2 = newSlot2.GetComponent<HeroSlot>();
                    slot2.setHero(units[i]);
                    break;
                case EUnitGrade.A:
                    GameObject newSlot3 = Instantiate(heroBtn3Pre, content);
                    HeroSlot slot3 = newSlot3.GetComponent<HeroSlot>();
                    slot3.setHero(units[i]);
                    break;
                case EUnitGrade.S:
                    GameObject newSlot4 = Instantiate(heroBtn4Pre, content);
                    HeroSlot slot4 = newSlot4.GetComponent<HeroSlot>();
                    slot4.setHero(units[i]);
                    break;
                case EUnitGrade.SS:
                    GameObject newSlot5 = Instantiate(heroBtn5Pre, content);
                    HeroSlot slot5 = newSlot5.GetComponent<HeroSlot>();
                    slot5.setHero(units[i]);
                    break;
            }
        }
    }
}
