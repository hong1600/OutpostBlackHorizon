using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInventory : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject heroBtn1Pre;
    [SerializeField] GameObject heroBtn2Pre;
    [SerializeField] GameObject heroBtn3Pre;
    [SerializeField] GameObject heroBtn4Pre;
    [SerializeField] GameObject heroBtn5Pre;
    [SerializeField] UnitData[] units;

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
                case eUnitGrade.C:
                    GameObject newSlot1 = Instantiate(heroBtn1Pre, content);
                    HeroSlot slot1 = newSlot1.GetComponent<HeroSlot>();
                    slot1.setHero(units[i]);
                    break;
                case eUnitGrade.B:
                    GameObject newSlot2 = Instantiate(heroBtn2Pre, content);
                    HeroSlot slot2 = newSlot2.GetComponent<HeroSlot>();
                    slot2.setHero(units[i]);
                    break;
                case eUnitGrade.A:
                    GameObject newSlot3 = Instantiate(heroBtn3Pre, content);
                    HeroSlot slot3 = newSlot3.GetComponent<HeroSlot>();
                    slot3.setHero(units[i]);
                    break;
                case eUnitGrade.S:
                    GameObject newSlot4 = Instantiate(heroBtn4Pre, content);
                    HeroSlot slot4 = newSlot4.GetComponent<HeroSlot>();
                    slot4.setHero(units[i]);
                    break;
                case eUnitGrade.SS:
                    GameObject newSlot5 = Instantiate(heroBtn5Pre, content);
                    HeroSlot slot5 = newSlot5.GetComponent<HeroSlot>();
                    slot5.setHero(units[i]);
                    break;
            }
        }
    }
}
