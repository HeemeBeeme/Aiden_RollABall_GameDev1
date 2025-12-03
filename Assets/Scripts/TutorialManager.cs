using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI TutorialText;

    public int TextPlace = 0;

    public Button Next;
    public Button Back;
    public Button Close;

    public GameObject TutorialBarriers;
    public GameObject Enemy;
    public GameObject TutorialManagerObj;

    public PlayerController playerController;

    public void NextButton()
    {
        TextPlace++;
    }

    public void BackButton()
    {
        TextPlace--;
    }

    public void CloseTutorial()
    {
        TutorialManagerObj.SetActive(false);
    }

    void Update()
    {
        if(TextPlace < 0)
        {
            TextPlace = 0;
        }
        if(TextPlace > 11)
        {
            TextPlace = 11;
        }

        switch(TextPlace)
        {
            case 0:

                TutorialText.text = "Welcome To The Tutorial World!";

                break;

            case 1:

                TutorialText.text = "Here's The Basics Use WASD To Roll Around... Try It Out!";

                break;

            case 2:

                TutorialText.text = "WOW! That Was Awesome... Right? Anyway, Let's Get To The Fun Stuff!";

                break;

            case 3:

                TutorialText.text = "Now, Try To Break One Of Those Rocks Over There...";
                TutorialBarriers.SetActive(false);

                break;

            case 4:

                TutorialText.text = "If You Broke A Golden Rock, Congratulations! These Rocks Are Far More Rare Than Regular Rocks... They Tend To Give More Gems, But They Aren't Always So Generous!";

                break;

            case 5:

                TutorialText.text = "Now, if you look In The Bottom Left Corner You'll Notice A Little Button, Go Ahead Click On It...";

                break;

            case 6:

                TutorialText.text = "This Is The Sidebar, And Where You Will Find How Much Money And How Many Gems You Have!";

                break;

            case 7:

                TutorialText.text = "You Will Also Find Upgrades, And A Way To Level Up!";

                break;

            case 8:

                TutorialText.text = "You Can Also Close The Sidebar With the Button On The Top Right Of The Sidebar";

                break;

            case 9:

                TutorialText.text = "And I Must Warn You... There Is A Dangerous Creature Around Here Named... HIM! Why? Yeah I Don't Know And Don't Care.";

                break;

            case 10:

                TutorialText.text = "Alright Well He's Out To Get You... Better Run";
                Enemy.SetActive(true);

                break;

            case 11:

                TutorialText.text = "Now Go Out And Break Some Rocks, Collect Some Gems, And Make Some MONEY!";

                break;

            case 12:

                TutorialText.text = "Oh And You Can Pause Using The Esc Button!";
                playerController.CanPause = true;
                Close.gameObject.SetActive(true);

                break;
        }
    }
}
