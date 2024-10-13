using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject heroBtn1Pre;
    [SerializeField] GameObject heroBtn2Pre;
    [SerializeField] GameObject heroBtn3Pre;
    [SerializeField] GameObject heroBtn4Pre;
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
                case 0:
                    GameObject newSlot1 = Instantiate(heroBtn1Pre, content);
                    HeroSlot slot1 = newSlot1.GetComponent<HeroSlot>();
                    slot1.setHero(units[i]);
                    break;
                case 1:
                    GameObject newSlot2 = Instantiate(heroBtn2Pre, content);
                    HeroSlot slot2 = newSlot2.GetComponent<HeroSlot>();
                    slot2.setHero(units[i]);
                    break;
                case 2:
                    GameObject newSlot3 = Instantiate(heroBtn3Pre, content);
                    HeroSlot slot3 = newSlot3.GetComponent<HeroSlot>();
                    slot3.setHero(units[i]);
                    break;
                case 3:
                    GameObject newSlot4 = Instantiate(heroBtn4Pre, content);
                    HeroSlot slot4 = newSlot4.GetComponent<HeroSlot>();
                    slot4.setHero(units[i]);
                    break;
            }
        }
    }
}
