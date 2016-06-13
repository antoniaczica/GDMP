using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{

	public static GameScore main;

	public float defaultRoundTime = 60;
	float roundTime;
	public Text roundTimeText;

    float bodyguardTimer;
    public Text bodyguardTimerText;

	public int firstPlayerScore = 0;
	public int secondPlayerScore = 0;
	public Text scoreOne;
	public Text scoreTwo;

    bool bodyguardOnStage = false;
	bool firstPlayerIsAssassin = true;

	RoundManager roundManager;

//	public Image blueAssassin;
//	public Image blueSecurity;
//	public Image redAssassin;
//	public Image redSecurity;


	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		if (main == null) { main = this; }
		else { Destroy(gameObject); }
	}

	// Use this for initialization
	void Start()
	{
//		redSecurity.enabled = false;
//		blueSecurity.enabled = true;
//		redAssassin.enabled = true;
//		blueAssassin.enabled = false;
		roundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RoundManager>();
		scoreOne.text = firstPlayerScore.ToString();
		scoreTwo.text = secondPlayerScore.ToString();
		newRoundStarted();
	}

	// Update is called once per frame
	void Update()
	{
        if (roundManager == null)
		{
			roundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RoundManager>();
		}

		RoundManager.RoundState roundState = roundManager.GetRoundState();

		if (roundState == RoundManager.RoundState.Playing)
		{
			roundTime -= Time.deltaTime;
			roundTimeText.text = "" + Mathf.Round(roundTime);
			if (roundTime < 0)
			{
				roundManager.OutOfTime();
			}

            if (bodyguardIsOnStage())
            {
                bodyguardTimer -= Time.deltaTime;
            }
            else if (bodyguardTimer < 10)
            {
                bodyguardTimer += Time.deltaTime;
            }
            bodyguardTimerText.text = "" + Mathf.Round(bodyguardTimer);

            if (bodyguardTimer < 0)
            {
                roundManager.OutOfTime();
            }
        }
	}

    public bool bodyguardIsOnStage()
    {
        GameObject bodyguard = GameObject.Find("Dev_Player_01 (Bodyguard)");
        float x = bodyguard.transform.position.x;
        float z = bodyguard.transform.position.z;

        if (x < 2.5 && x > -4)
        {
            if (z < 3.8 && z > -2.7)
            {
                return true;
            }
        }
        return false;
    }

	public void newRoundStarted()
	{
        bodyguardTimer = 10;
        bodyguardTimerText.text = "" + Mathf.Round(bodyguardTimer);
        roundTime = defaultRoundTime;
		roundTimeText.text = "" + Mathf.Round(roundTime);
	}

	public void bodyguardWins()
	{
		if (firstPlayerIsAssassin){
			secondPlayerScore++;
			scoreTwo.text = secondPlayerScore.ToString();
		}
		else{
			firstPlayerScore++;
			scoreOne.text = firstPlayerScore.ToString();
		}

		if (firstPlayerScore > 5 || secondPlayerScore > 5)
		{
			GameOver();
		}
	}

	public void assassinWins()
	{
		if (firstPlayerIsAssassin)
		{
			firstPlayerScore++;
			scoreOne.text = firstPlayerScore.ToString();
		}
		else
		{
			secondPlayerScore++;
			scoreTwo.text = secondPlayerScore.ToString();
		}        

		if (firstPlayerScore > 5 || secondPlayerScore > 5)
		{
			GameOver();
		}
	}

	public void GameOver()
	{
		// Game over logic to go here
		if (firstPlayerScore > 5)
		{
			// First player wins
		}
		else if (secondPlayerScore > 5)
		{
			// Second player wins
		}
	}

	public bool playerOneIsAssassin()
	{
		return firstPlayerIsAssassin;
	}

	public int getPlayerNum(string objName)
	{
		//print (objName);
		if (objName == "Dev_Player_01 (Assassin)") {
			return firstPlayerIsAssassin ? 1 : 2;
		} else if (objName == "Dev_Player_01 (Bodyguard)" || objName == "InspectionCam (2)") {
			return firstPlayerIsAssassin ? 2 : 1;
		} else {
			return -1;
		}
	}

	public int getAssassinPlayerNum()
	{
		return firstPlayerIsAssassin ? 1 : 2;
	}


	public void newRoundStart()
	{
        firstPlayerIsAssassin = !firstPlayerIsAssassin;
		flipPlayerImages ();
	}

	public void flipPlayerImages()
	{
		if (firstPlayerIsAssassin)
		{
//			redSecurity.enabled = false;
//			blueSecurity.enabled = true;
//			redAssassin.enabled = true;
//			blueAssassin.enabled = false;
		}
		else {
//			redSecurity.enabled = true;
//			blueSecurity.enabled = false;
//			redAssassin.enabled = false;
//			blueAssassin.enabled = true;
		}
	}


}
