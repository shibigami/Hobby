using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Journal
{
    public static string[] pages { get; private set; }
    public static int numberOfPages { get; private set; }


    public static void SetInitialStory()
    {
        numberOfPages = 25;
        pages = new string[numberOfPages];

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
        pages[4] = "What strange figure, floating in place, as if in wait for long. " +
            "It paced itself midair, covered by a shadow that pushed me further the closer I attempted to be.\n";
        pages[5] = "Before this Light showed up all I did was stumble in the Dark.\n" +
            "Before this Light came along all I did was pace myself\n" +
            "slowly as I tried finding my way around these Lands.\n" +
            "Before this Light came along... I had nothing...\n" +
            "And now, I can pursue all I can be. And new places to see.";
        pages[6] = "The magnitude of my surroundings is overbearing.\n" +
            "The uncertainty that there might be something more beyond the Light.\n" +
            "Or there might be nothing but a shadowy Abyss.\n" +
            "But the excitment doesn't fully allow me to thread carefully.\n" +
            "Long have I threaded full of care. Now...\n" +
            "My will pushes me to leave this nightmare!";
        pages[7] = "I heard a pleasant sound once. It muffled all other sounds around. " +
            "As I sat, listening in on this harmony, feelings of comfort settled. " +
            "And I sat on, letting it carry my comfort until it stopped. ";
        pages[8] = "Just as he said, there are others around, watching. " +
            "Hiding away, almost unseen. All, the same power pursuing.\n" +
            "What is it that draws them to something so common? That set themselves " +
            "scattered through these lands. To the point where one falls cursed, binded, " +
            "after offering their own kindness?\n" +
            "But my resolve stands!\nAnd in the light of this discovery, the discovery of " +
            "where the light takes me shall take from me what I take from the tracks of where it leads.";
        pages[9] = "Since that day, that brought a resounding echo inside myself, " +
            "shifting away my surroundings for the moment it lasted. For long I've tried. To mimic those " +
            "sounds that resonate in me.\n";
        pages[10] = "The impressive measure of this dimension. Lands filled with power and deception.\n" +
            "And as I make my way forward, failing time and time again, I come back to my previous self. " +
            "With a full recollection of the moments previous to my end.\n";
        pages[11] = "What a discovery! Hidden inside capsules, these beings emanate a similarity that follows me " +
            "around. And as I observed them, spending my time humming away the melody, they increased in size faster " +
            "than had I waited just staring. The power of this melody seems to affect more than just myself. And these " +
            "capsules are sure to show me more than I could without them. Another step less towards " +
            "what I seek, through these lands that less seem dark and bleak.";
        pages[12] = "Again, he showed up in front of me with such impressive power. This time speaking as if he " +
            "knew me and as if I had been here before. I recall the slight sensation of these lands, but no more.\n";
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
        //mage branch
        if (GameData.mageJoined)
        {
            pages[2] += "But this shade, that demands more and more power...\n" +
                "Numbs this feeling. Instead, from my insides, there's a much more overpowering sensation...\n";

            pages[3] += "Or because of what I feel now growing inside of me...?\n" +
                "After accepting this strangers power... But I need more! Keke...\n" +
                "No!!... I must press forward, until there is no more Shade!";

            pages[4] += "It seemed to have mentioned me, Kekeke... " +
                "... It seems to have mentioned the Shade that now haunts me. With most familiarity... ";

            pages[7] += "... Kekeke. It must be her! I knew she was around!!! " +
                "KekakakaKekeka! " +
                "Stop!! I am me! Get away from my consciousness, fiend!!";

            pages[9] += "Kekeke. You'll never replace her! Kekekeakakekeke!!!\n" +
                "... And you'll never replace me, fiend! Stay on your own ground!";

            pages[10] += "...Yes, I wonder why? Kakake. Have you ever questioned it before? But perhaps " +
                "you were just unaware. But don't worry. It won't be long... KekakekekeKekekeke!!\n" +
                "What do you mean?! Never! Know your place! I shall reject it every time you take!";

            pages[12] += "Kekeke. How could you know what you have not seen? But tell me, do these lands not " +
                "feel like home? Kekekekakeke!\n" +
                "Like home? I find it hard believing in what you say. But have I really been here before? And " +
                "for how long? And the things that fall and cover all around... It is not warm or cold. Only a " +
                "softness that dissapears moments after it is touched.";
        }
        //priest branch
        else if (GameData.priestJoined)
        {
            pages[2] += "And this being of unfamiliar, that asks for what follows me...\n" +
                "They wrap around me in the same way... But what could this mean?\n";

            pages[3] += "But this fellow, that moves by my side, holds these feelings near but far.\n" +
                "I can continue safely as I traverse these lands, now more and more filled with Light.\n" +
                "And count these blessings each time I find myself moving forward.";

            pages[4] += "It seemed to dislike the Hope that joined this journey. " +
                "Perhaps this will be the prayer that keeps it at bay... " +
                "Perhaps this will only result in disarray... ";

            pages[7] += "... Yes, my Lady... I knew I felt her Light as well at some point. " +
                "Was it this you felt?... " +
                "Hums echoed atop my head. But the comfort was not the same.. " +
                "The intensity was short to the one that this comfort inflamed.";

            pages[9] += "... I can still sense her, as if she had left this trail on purpose. It might " +
                "not be too late yet...\n Hums echo once again as I try to follow them this time.";

            pages[10] += "... That which you must not have questioned does not need to be " +
                "questioned... Unless... for the loss, you are ready... Forgive me, but some things are better " +
                "kept in the unknown that surrounds us...\n" +
                "At that moment I felt slight sadness emanating from this light being, that now felt farther.";

            pages[12] += "... These lands. It is not advisable to stay for long. Nothing good will come from questioning " +
                "their reason to be...\n" +
                "Again the light faded slightly from the one accompanying me... What is it about these lands and what covers " +
                "them? This soft, not cold, not warm, that makes me feel close to where I stand.";
        }
        //before getting the mage or priest
        else
        {
            pages[2] += "And it becomes more and more familiar, replacing what once I had assumed\n" +
                "had been all. The only thing that, around me, all had consumed...\n";

            pages[3] += "Or perhaps, for the decisions I've made on each encounter.\n" +
                "To show resolution in the path that I take and to keep moving through\n" +
                "these lands that seek to throw challenge after challenge in my wake.";
        }

        //last part of each branch page
        pages[2] += "Despite what has happened, I must carry on... To somewhere I have not yet gone.";

        pages[4] += "And he spoke of others, have they been watching as well? " +
            "Or could it be just a bluff, a trickery to throw me off my path? " +
            "But for what reason?... No matter, the time of light is due. " +
            "And this fiend... I shan't follow you!";

        pages[9] += "This melody aids me in moving forward, along with this light.\n And forward " +
            "I shall move, humming this melody that soothes this blight.";
    }
}