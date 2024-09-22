using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject heroBtnPre;
    [SerializeField] UnitData[] units;

    private void Start()
    {
        loadHero();
    }

    private void loadHero()
    {
        //foreach(UnitData unit in units) 
        //{
        //    GameObject newSlot = Instantiate(heroBtnPre, content);
        //    HeroSlot slot = newSlot.GetComponent<HeroSlot>();
        //    slot.setHero(unit);
        //}

        for (int i  = 0; i < units.Length; i++) 
        {
            GameObject newSlot = Instantiate(heroBtnPre, content);
            HeroSlot slot = newSlot.GetComponent<HeroSlot>();
            slot.setHero(units[i]);
        }
    }
}
