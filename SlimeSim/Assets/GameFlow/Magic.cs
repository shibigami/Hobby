using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Magic
{
    public enum MagicType { None, Mage, Priest };
    public static MagicType magicTypeChosen { get; private set; }
    public static Spell[] mageSpells { get; private set; }
    public static Spell[] priestSpells { get; private set; }

    public static void init()
    {
        mageSpells = new Spell[8];
        priestSpells = new Spell[8];
        magicTypeChosen = MagicType.None;

        //increase jump height
        mageSpells[0] = new Spell("Dark Heights", 0, 15, 100, 7);
        mageSpells[0].SetDescription("Allows Gmooh to jump higher.");
        //increase max velocity
        mageSpells[1] = new Spell("Shadow Run", 0, 20, 300, 0.05f);
        mageSpells[1].SetDescription("Lets Gmooh reach a higher running speed.");
        //adds dark meditate mechanic when sitting,allowing the player to see the exit for some time
        //power is the duration
        mageSpells[2] = new Spell("Inner Gloom", 0, 10, 500, 1, 45);
        mageSpells[2].SetDescription("Shows the level exit for some time. 45 sec. cooldown.");
        //allows the player to teleport to the beginning point of the stage
        //power is the amount of times it can be used per stage
        mageSpells[3] = new Spell("Trick Death", 0, 5, 1000, 1, 15);
        mageSpells[3].SetDescription("Takes Gmooh back to the start of the stage.");
        //unlocks place a block mechanic, allowing players to place a platform in front of them
        //power is the height of the platform
        mageSpells[4] = new Spell("Tangible Shade", 0, 20, 1000, 0.1f, 60);
        mageSpells[4].SetDescription("Allows Gmooh to conjure a platform");
        //allows the player to create a point that allows him to teleport to
        //power level is the max allowed teleports per lvl
        mageSpells[5] = new Spell("Forced Control", 0, 10, 500, 1, 45);
        mageSpells[5].SetDescription("Temporarily push the guiding light.");
        //this skill only affects story mechanics but may unlock secrets and achievements
        mageSpells[6] = new Spell("Feed the Darkling", 0, 999, 1000, 1);
        mageSpells[6].SetDescription("No one really knows what this does. UPGRADE AT OWN RISK!");
        //unlocks back to the past mechanic, letting the player travel to a stage previously visited
        //power is amount of uses per stage but stands to reason that the max level is 1
        mageSpells[7] = new Spell("Black Hole", 0, 1, 999999, 1);
        mageSpells[7].SetDescription("Takes Gmooh back to previous levels.");


        //increase jump height
        priestSpells[0] = new Spell("Light Wings", 0, 20, 100, 6);
        priestSpells[0].SetDescription("Allows Gmooh to jump higher.");
        //increase max velocity
        priestSpells[1] = new Spell("Holy Boost", 0, 20, 300, 0.05f);
        priestSpells[1].SetDescription("Lets Gmooh reach a higher running speed.");
        //adds light meditate mechanic when sitting,allowing the player to see the exit for some time
        //power is the duration
        priestSpells[2] = new Spell("Inner Bloom", 0, 10, 500, 1, 20);
        priestSpells[2].SetDescription("Shows the level exit for some time. 20 sec. cooldown.");
        //allows the player to teleport to the beginning point of the stage
        //power is the amount of times it can be used per stage
        priestSpells[3] = new Spell("Second Life", 0, 5, 1000, 1, 15);
        priestSpells[3].SetDescription("Takes Gmooh back to the start of the stage.");
        //unlocks place a block mechanic, allowing players to place a platform in front of them
        //power is the height of the platform
        priestSpells[4] = new Spell("Heaven's Volume", 0, 20, 1000, 0.1f, 60);
        priestSpells[4].SetDescription("Allows Gmooh to conjure a platform");
        //allows the player to shoot a light projectile forward
        //power level is the radius
        priestSpells[5] = new Spell("Gentle Control", 0, 10, 500, 0.25f, 45);
        priestSpells[5].SetDescription("Temporarily create a guiding light.");
        //this skill only affects story mechanics but may unlock secrets and achievements
        priestSpells[6] = new Spell("Prayer of belief", 0, 999, 1000, 1);
        priestSpells[6].SetDescription("No one really knows what this does. UPGRADE AT OWN RISK!");
        //unlocks back to the past mechanic, letting the player travel to a stage previously visited
        //power is amount of uses per stage but stands to reason that the max level is 1
        priestSpells[7] = new Spell("Heavenly Travel", 0, 1, 999999, 1);
        priestSpells[7].SetDescription("Takes Gmooh back to previous levels.");

    }

    public static void SetMagicTypeChosen(MagicType magicType)
    {
        magicTypeChosen = magicType;
    }
}