using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Journal
{
    public static string[] pages { get; private set; }
    public static int numberOfPages { get; private set; }

    public static void init()
    {
        numberOfPages = 25;
        pages = new string[numberOfPages];

        //hardcoded individual page info
        pages[0] = "I was born in darkness, lived in darkness, and in darkness... I shall die....\n" +
            "My memories are but dreams I see while awake.\n" +
            "And the place I find myself in... Surrounded by darkness, so familiar...\n" +
            "Touched by the most unfamiliar that seeks to surround me now.\n" +
            "Maybe... Just maybe... Not in darkness I shall leave.\n" +
            "In the Light, after leaving, I shall live.";
        pages[1] = "";
        pages[2] = "";
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
}