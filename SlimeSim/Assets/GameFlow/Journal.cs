using System.Collections;
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
        pages[3] = "Uncertainty, greed, power... Freedom... This yearning grows for each passing.\n" +
            "Is it because of what sought to follow me? Is it because of what I have managed to achieve?\n";
        pages[4] = "What strange figure, floating in place, as if in wait for long.\n" +
            "It paced itself midair, covered by a shadow that pushed me further\n" +
            " the closer I attempted to be.\n";
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
        {
            pages[2] += "But this shade, that demands more and more power...\n" +
                "Numbs this feeling. Instead, from my insides, there's a much more overpowering sensation...\n";

            pages[3] += "Or because of what I feel now growing inside of me...?\n" +
                "After accepting this strangers power... But I need more! Keke...\n" +
                "No!!... I must press forward, until there is no more Shade!\n";

            pages[4] += "It seemed to have mentioned me, Kekeke...\n" +
                "... It seems to have mentioned the Shade that now haunts me.\n" +
                "With most familiarity...\n";
        }
        else if (GameData.priestJoined)
        {
            pages[2] += "And this being of unfamiliar, that asks for what follows me...\n" +
                "They wrap around me in the same way... But what could this mean?\n";

            pages[3] += "But this fellow, that moves by my side, holds these feelings near but far.\n" +
                "I can continue safely as I traverse these lands, now more and more filled with Light.\n" +
                "And count these blessings each time I find myself moving forward.";

            pages[4] += "It seemed to dislike the Hope that joined this journey.\n" +
                "Perhaps this will be the prayer that keeps it at bay...\n" +
                "Perhaps this will only result in disarray...\n";
        }
        else
        {
            pages[2] += "And it becomes more and more familiar, replacing what once I had assumed\n" +
                "had been all. The only thing that, around me, all had consumed...\n";

            pages[3] += "Or perhaps, for the decisions I've made on each encounter.\n" +
                "To show resolution in the path that I take and to keep moving through\n" +
                "these lands that seek to throw challenge after challenge in my wake.\n";
        }

        pages[2] += "Despite what has happened, I must carry on... To somewhere I have not yet gone.";

        pages[4] += "And he spoke of others, have they been watching as well?\n" +
            "Or could it be just a bluff, a trickery to throw me off my path?\n" +
            "But for what reason?... No matter, the time of light is due.\n" +
            "And this fiend... I shan't follow you!";
    }
}