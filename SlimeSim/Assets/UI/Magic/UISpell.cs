using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpell : MonoBehaviour
{
    public Magic.MagicType magicType;
    public int spellID;
    public Text level, cost, tooltipName, tooltipDescritpion;

    // Start is called before the first frame update
    void Start()
    {
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        switch (magicType)
        {
            case Magic.MagicType.Mage:
                {
                    level.text = string.Format("Lvl.{0}",Magic.mageSpells[spellID].level.ToString());
                    cost.text = Magic.mageSpells[spellID].upgradeCost.ToString();
                    tooltipName.text = Magic.mageSpells[spellID].name.ToString();
                    tooltipDescritpion.text = Magic.mageSpells[spellID].description;
                    break;
                }
            case Magic.MagicType.Priest:
                {
                    level.text = string.Format("Lvl.{0}", Magic.priestSpells[spellID].level.ToString());
                    cost.text = Magic.priestSpells[spellID].upgradeCost.ToString();
                    tooltipName.text = Magic.priestSpells[spellID].name.ToString();
                    tooltipDescritpion.text = Magic.priestSpells[spellID].description;
                    break;
                }
        }
    }

    public void IncreaseSpellLvl() 
    {
        if (Magic.magicTypeChosen == Magic.MagicType.Mage)
        {
            if (GameData.gold >= Magic.mageSpells[spellID].upgradeCost) 
            {
                if (Magic.mageSpells[spellID].IncreaseSpellLevel(1))
                {
                    GameData.AddGold(-Magic.mageSpells[spellID].upgradeCost);
                    UpdateInfo();
                }
            }
        }
        else if (Magic.magicTypeChosen == Magic.MagicType.Priest)
        {
            if (GameData.gold >= Magic.priestSpells[spellID].upgradeCost)
            {
                if (Magic.priestSpells[spellID].IncreaseSpellLevel(1))
                {
                    GameData.AddGold(-Magic.priestSpells[spellID].upgradeCost);
                    UpdateInfo();
                }
            }
        }
    }
}
