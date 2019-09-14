using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    private static GameObject whoWinsTextShadow, player1MoveText, player2MoveText, player3MoveText, player4MoveText,
        card1, card2, card3, card4, card5, card6, card7, card8, player1life, player2life, player3life, player4life;

    private static GameObject card1img, card2img, card3img, card4img, card5img, card6img, card7img, card8img;

    private static GameObject player1, player2, player3, player4;

    private static int player1LifeVal, player2LifeVal, player3LifeVal, player4LifeVal;

    public static bool firstTurn = true;
    public static Card[] selectedCards;
    public static int diceSideThrown = 0;
    public static int player1StartWaypoint = 0;
    public static int player2StartWaypoint = 0;
    public static int player3StartWaypoint = 0;
    public static int player4StartWaypoint = 0;

    public static bool gameOver = false;

    // Use this for initialization
    void Start () {
        whoWinsTextShadow = GameObject.Find("WhoWinsText");
        player1MoveText = GameObject.Find("Player1MoveText");
        player2MoveText = GameObject.Find("Player2MoveText");
        player3MoveText = GameObject.Find("Player3MoveText");
        player4MoveText = GameObject.Find("Player4MoveText");

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        player3 = GameObject.Find("Player3");
        player4 = GameObject.Find("Player4");

        player1.GetComponent<FollowThePath>().moveAllowed = false;
        player2.GetComponent<FollowThePath>().moveAllowed = false;
        player3.GetComponent<FollowThePath>().moveAllowed = false;
        player4.GetComponent<FollowThePath>().moveAllowed = false;

        whoWinsTextShadow.gameObject.SetActive(false);
        player1MoveText.gameObject.SetActive(true);
        player2MoveText.gameObject.SetActive(false);
        player3MoveText.gameObject.SetActive(false);
        player4MoveText.gameObject.SetActive(false);

        player1life = GameObject.Find("Player1Life");
        player2life = GameObject.Find("Player2Life");
        player3life = GameObject.Find("Player3Life");
        player4life = GameObject.Find("Player4Life");
        player1LifeVal = 4000;
        player2LifeVal = 4000;
        player3LifeVal = 4000;
        player4LifeVal = 4000;

        card1 = GameObject.Find("Card1");
        card2 = GameObject.Find("Card2");
        card3 = GameObject.Find("Card3");
        card4 = GameObject.Find("Card4");
        card5 = GameObject.Find("Card5");
        card6 = GameObject.Find("Card6");
        card7 = GameObject.Find("Card7");
        card8 = GameObject.Find("Card8");

        card1img = GameObject.Find("CardImage1");
        card2img = GameObject.Find("CardImage2");
        card3img = GameObject.Find("CardImage3");
        card4img = GameObject.Find("CardImage4");
        card5img = GameObject.Find("CardImage5");
        card6img = GameObject.Find("CardImage6");
        card7img = GameObject.Find("CardImage7");
        card8img = GameObject.Find("CardImage8");

        selectedCards = new Card[6];
    }

    // Update is called once per frame
    void Update()
    {
        player1life.GetComponent<Text>().text = player1LifeVal.ToString();
        player2life.GetComponent<Text>().text = player2LifeVal.ToString();
        player3life.GetComponent<Text>().text = player3LifeVal.ToString();
        player4life.GetComponent<Text>().text = player4LifeVal.ToString();
        if (isMaxCards())
        {
            makeUnselectable();
        }
        else
        {
            makeSelectable();
        }
        if (firstTurn)
        {
            setCards(player1);
            firstTurn = false;
        }
        if (diceSideThrown != 0)
        {
            diceSideThrown = selectedCards[diceSideThrown - 1].level;
            //Debug.Log(diceSideThrown);
        }
        if (card1img.GetComponent<Toggle>().isOn) { }
        Debug.Log("player 1 moveAllowed " + player1.GetComponent<FollowThePath>().moveAllowed);
        Debug.Log("player 2 moveAllowed " + player2.GetComponent<FollowThePath>().moveAllowed);
        Debug.Log("player 3 moveAllowed " + player3.GetComponent<FollowThePath>().moveAllowed);
        Debug.Log("player 4 moveAllowed " + player4.GetComponent<FollowThePath>().moveAllowed);
        if (player1.GetComponent<FollowThePath>().waypointIndex > 
            player1StartWaypoint + diceSideThrown)
        {
            setCards(player1);
            player1.GetComponent<FollowThePath>().moveAllowed = false;
            setActiveText(player2MoveText);
            player1StartWaypoint = player1.GetComponent<FollowThePath>().waypointIndex - 1;

        }

        if (player2.GetComponent<FollowThePath>().waypointIndex >
            player2StartWaypoint + diceSideThrown)
        {
            setCards(player2);
            player2.GetComponent<FollowThePath>().moveAllowed = false;
            setActiveText(player3MoveText);
            player2StartWaypoint = player2.GetComponent<FollowThePath>().waypointIndex - 1;
        }

        if (player3.GetComponent<FollowThePath>().waypointIndex >
            player3StartWaypoint + diceSideThrown)
        {
            setCards(player3);
            player3.GetComponent<FollowThePath>().moveAllowed = false;
            setActiveText(player4MoveText);
            player3StartWaypoint = player3.GetComponent<FollowThePath>().waypointIndex - 1;
            //resetCardsOnDie();
        }

        if (player4.GetComponent<FollowThePath>().waypointIndex >
            player4StartWaypoint + diceSideThrown)
        {
            setCards(player4);
            player4.GetComponent<FollowThePath>().moveAllowed = false;
            setActiveText(player1MoveText);
            player4StartWaypoint = player4.GetComponent<FollowThePath>().waypointIndex - 1;
            //resetCardsOnDie();
        }

        if (player1.GetComponent<FollowThePath>().waypointIndex == 
            player1.GetComponent<FollowThePath>().waypoints.Length)
        {
            whoWinsTextShadow.gameObject.SetActive(true);
            whoWinsTextShadow.GetComponent<Text>().text = "Player 1 Wins";
            gameOver = true;
        }

        if (player2.GetComponent<FollowThePath>().waypointIndex ==
            player2.GetComponent<FollowThePath>().waypoints.Length)
        {
            whoWinsTextShadow.gameObject.SetActive(true);
            player1MoveText.gameObject.SetActive(false);
            player2MoveText.gameObject.SetActive(false);
            whoWinsTextShadow.GetComponent<Text>().text = "Player 2 Wins";
            gameOver = true;
        }
    }

    public static void MovePlayer(int playerToMove)
    {
        switch (playerToMove) { 
            case 1:
                player1.GetComponent<FollowThePath>().moveAllowed = true;
                break;

            case 2:
                player2.GetComponent<FollowThePath>().moveAllowed = true;
                break;
            case 3:
                player3.GetComponent<FollowThePath>().moveAllowed = true;
                break;
            case 4:
                player4.GetComponent<FollowThePath>().moveAllowed = true;
                break;
        }
    }
    //take in which player to set active
    public void setActiveText(GameObject player)
    {
        player1MoveText.gameObject.SetActive(false);
        player2MoveText.gameObject.SetActive(false);
        player3MoveText.gameObject.SetActive(false);
        player4MoveText.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
    }

    public void setCards(GameObject currPlayer)
    {
        card1.GetComponent<CardDisplay>().card = currPlayer.GetComponent<Hand>().cards[0];
        card2.GetComponent<CardDisplay>().card = currPlayer.GetComponent<Hand>().cards[1];
        card3.GetComponent<CardDisplay>().card = currPlayer.GetComponent<Hand>().cards[2];
        card4.GetComponent<CardDisplay>().card = currPlayer.GetComponent<Hand>().cards[3];
        card5.GetComponent<CardDisplay>().card = currPlayer.GetComponent<Hand>().cards[4];
        card6.GetComponent<CardDisplay>().card = currPlayer.GetComponent<Hand>().cards[5];
        card7.GetComponent<CardDisplay>().card = currPlayer.GetComponent<Hand>().cards[6];
        card8.GetComponent<CardDisplay>().card = currPlayer.GetComponent<Hand>().cards[7];
    }

    public bool isMaxCards()
    {
        int count = 0;
        if (card1img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            count++;
        }
        if (card2img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            count++;
        }
        if (card3img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            count++;
        }
        if (card4img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            count++;
        }
        if (card5img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            count++;
        }
        if (card6img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            count++;
        }
        if (card7img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            count++;
        }
        if (card8img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            count++;
        }
        if(count >= 6)
        {
            return true;
        }
        return false;
    }

    public void makeUnselectable()
    {
        if (!card1img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            card1img.GetComponent<Dieslot>().cardToSelect.interactable = false;
        }
        if (!card2img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            card2img.GetComponent<Dieslot>().cardToSelect.interactable = false;
        }
        if (!card3img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            card3img.GetComponent<Dieslot>().cardToSelect.interactable = false;
        }
        if (!card4img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            card4img.GetComponent<Dieslot>().cardToSelect.interactable = false;
        }
        if (!card5img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            card5img.GetComponent<Dieslot>().cardToSelect.interactable = false;
        }
        if (!card6img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            card6img.GetComponent<Dieslot>().cardToSelect.interactable = false;
        }
        if (!card7img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            card7img.GetComponent<Dieslot>().cardToSelect.interactable = false;
        }
        if (!card8img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            card8img.GetComponent<Dieslot>().cardToSelect.interactable = false;
        }
    }

    public void makeSelectable()
    {
        card1img.GetComponent<Dieslot>().cardToSelect.interactable = true;
        card2img.GetComponent<Dieslot>().cardToSelect.interactable = true;
        card3img.GetComponent<Dieslot>().cardToSelect.interactable = true;
        card4img.GetComponent<Dieslot>().cardToSelect.interactable = true;
        card5img.GetComponent<Dieslot>().cardToSelect.interactable = true;
        card6img.GetComponent<Dieslot>().cardToSelect.interactable = true;
        card7img.GetComponent<Dieslot>().cardToSelect.interactable = true;
        card8img.GetComponent<Dieslot>().cardToSelect.interactable = true;
    }
    public static Sprite[] putCardsOnDie()
    {
        int currSprite = 0;
        Sprite[] images = new Sprite[6];
        if (card1img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            images[currSprite] = card1img.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().sprite;
            selectedCards[currSprite] = card1.GetComponent<CardDisplay>().card;
            currSprite++;
        }
        if (card2img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            images[currSprite] = card2img.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().sprite;
            selectedCards[currSprite] = card2.GetComponent<CardDisplay>().card;
            currSprite++;
        }
        if (card3img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            images[currSprite] = card3img.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().sprite;
            selectedCards[currSprite] = card3.GetComponent<CardDisplay>().card;
            currSprite++;
        }
        if (card4img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            images[currSprite] = card4img.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().sprite;
            selectedCards[currSprite] = card4.GetComponent<CardDisplay>().card;
            currSprite++;
        }
        if (card5img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            images[currSprite] = card5img.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().sprite;
            selectedCards[currSprite] = card5.GetComponent<CardDisplay>().card;
            currSprite++;
        }
        if (card6img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            images[currSprite] = card6img.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().sprite;
            selectedCards[currSprite] = card6.GetComponent<CardDisplay>().card;
            currSprite++;
        }
        if (card7img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            images[currSprite] = card7img.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().sprite;
            selectedCards[currSprite] = card7.GetComponent<CardDisplay>().card;
            currSprite++;
        }
        if (card8img.GetComponent<Dieslot>().cardToSelect.isOn)
        {
            images[currSprite] = card8img.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().sprite;
            selectedCards[currSprite] = card8.GetComponent<CardDisplay>().card;
            currSprite++;
        }
        return images;
    }

    public static void resetCardsOnDie()
    {
        Debug.Log("reset");
        for (int i = 0; i < selectedCards.Length-1; i++)
        {
            selectedCards[i] = Resources.LoadAll<Card>("EmptyCard/")[0];
        }
    }
}
