﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Journal
{
    public static string[] pages { get; private set; }
    public static int numberOfPages { get; private set; }

    public static string[] sideKickChoices { get; private set; }

    public static void init()
    {
        numberOfPages = 25;
        pages = new string[numberOfPages];
        sideKickChoices = new string[5];

        //hardcoded individual page info
        pages[0] = "I was born in darkness, lived in darkness, and in darkness... I shall die....\n" +
            "My memories are but dreams I see while awake.\n" +
            "And the place I find myself in... Surrounded by darkness, so familiar...\n" +
            "But touched by the most unfamiliar, that seeks to surround me now.\n" +
            "Maybe... Just maybe... Not in darkness I shall leave.\n" +
            "And, in the Light, after leaving, I shall live.";
        pages[1] = "The darkness dissipates and tries to surround me once more.\n" +
            "But, afraid of this unfamiliar, it cowers around it.\n";
        pages[2] = "This unfamiliar grows as time carries me around what was once unseen.\n" +
            "Slowly I feel a sense growing, as if wrapped around my own being.\n";
        pages[3] = "";
        pages[4] = "";
        pages[5] = "";
        pages[6] = "";
        pages[7] = "";
        pages[8] = "";
        pages[9] = "";
        pages[10] = "";
        pages[11] = "";
        pages[12] = "";
        pages[13] = "";
        pages[14] = "";
        pages[15] = "";
        pages[16] = "";
        pages[17] = "";
        pages[18] = "";
        pages[19] = "";
        pages[20] = "";
        pages[21] = "";
        pages[22] = "";
        pages[23] = "";
        pages[24] = "";
    }
    public static string GetPage(int index)
    {
        return pages[index];
    }

    public static void UpdateSideKickBranch()
    {
        if (GameData.sideKickJoined)
            pages[1] += "Perhaps this shiny follower could be of aid...\n In what follows, that shines this path ahead.\n";
        else
            pages[1] += "Perhaps if this shine shined a shine shinier then this darkest darkness...\n And showed the path ahead.\n";
        pages[1] += "But this dakness, so familiar, still won't let me go...\n" +
        "And so... I must go where it won't let me.";
    }

    public static void UpdateMagePriestBranch()
    {
        if (GameData.mageJoined)
            pages[2] += "But this shade, that demands more and more power...\n" +
                "Numbs this feeling. Instead, from my insides, there's a much more overpowering sensation...\n";
        else if (GameData.priestJoined)
            pages[2] += "And this being of unfamiliar, that asks for what follows me...\n" +
                "They wrap around me in the same way... But what could this mean?\n";
        else
            pages[2] += "And it becomes more and more familiar, replacing what once I had assumed\n" +
                "had been all. The only thing that, around me, all had consumed...\n";
        pages[2] += "Despite what has happened, I must carry on... To somewhere I have not yet gone.";
    }
}