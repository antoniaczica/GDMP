using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class RoundManager : MonoBehaviour
{

    public static RoundManager main;
    public enum RoundState { Idle, Playing, AssassinRevealed, PresidentDown, PresidentSaved, TimeOut, SpawnSelect };

    public Animator mainTitle;
    public Animator presidentDownTitle;
    public ParticleSystem presidentDownParticles;
    public ColorCorrectionCurves presidentDownColor;
    public AudioSource presidentDownSound;
    public Animator presidentSavedTitle;
    public Animator outOfTimeTitle;
    public Blur blur;
    public AudioSource musicSource;

    public float roundTime = 90;
    public Text roundTimeText;

    public PlayerMovement disableAssassin;
    public PlayerTakedown disableAssassinTakedown;
    public PlayerMovement disableBodyguard;

    RoundState state = RoundState.Idle;

    public int assassinPlayerNum;

    GameScore gameScore;


    public GameObject player;

    void Awake()
    {
        main = this;
        //roundTimeText.text = "" + Mathf.Round(roundTime);
    }

    void Start()
    {
//        disableAssassin.enabled = false;
//        disableAssassinTakedown.enabled = false;
//        disableBodyguard.enabled = false;
        StartCoroutine(StartHideMainTitle());

        gameScore = GameObject.FindGameObjectWithTag("Environment").GetComponent<GameScore>();

        assassinPlayerNum = gameScore.getAssassinPlayerNum();
    }

    void Update()
    {
        if (state == RoundState.SpawnSelect)
        {
			disableAssassin.setSpawning (true);
			disableBodyguard.setSpawning (true);
            if (Input.GetAxis("RightStickX" + assassinPlayerNum) == 1) { SelectSpawnDirection("right"); }
            else if (Input.GetAxis("RightStickX" + assassinPlayerNum) == -1) { SelectSpawnDirection("left"); }
            else if (Input.GetAxis("RightStickY" + assassinPlayerNum) == -1) { SelectSpawnDirection("down"); }
        }
    }

    public RoundState GetRoundState()
    {
        return state;
    }


    IEnumerator StartHideMainTitle()
    {
        while (mainTitle.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        mainTitle.gameObject.SetActive(false);
//        disableAssassin.enabled = true;
//        disableAssassinTakedown.enabled = true;
//        disableBodyguard.enabled = true;
        musicSource.Play();

        // UI asking assassin to select spawn point should show at this point

        state = RoundState.SpawnSelect;
    }

    public void SelectSpawnDirection(string direction)
    {
		disableAssassin.setSpawning (false);
		disableBodyguard.setSpawning (false);
        if (direction == "down")
        {
            player.transform.position = new Vector3(8, 0, -8);
        }
        else if (direction == "left")
        {
            player.transform.position = new Vector3(-8, 0, -8);
        }
        else if (direction == "right")
        {
            player.transform.position = new Vector3(8, 0, 8);
        }

        Object[] spawners = Object.FindObjectsOfType<CrowdSpawner>();
        for (int i = 0; i < spawners.Length; i++)
        {
            CrowdSpawner spawner = (CrowdSpawner)spawners[i];
            spawner.SpawnCrowd();
        }
        print(spawners[0]);

        state = RoundState.Playing;
        gameScore.newRoundStarted();
        blur.enabled = false;
        // UI asking assassin to select spawn point should be hidden at this point
    }

    public void AssassinRevealed()
    {

    }

    public void PresidentDown()
    {
        if (state != RoundState.Playing)
            return;
        state = RoundState.PresidentDown;
        presidentDownParticles.Play();
        presidentDownTitle.gameObject.SetActive(true);
        presidentDownTitle.SetTrigger("Go");
        presidentDownColor.enabled = true;
        presidentDownSound.Play();
        musicSource.volume = 0.5f;
        StartCoroutine(EPresidentDown());
    }

    IEnumerator EPresidentDown()
    {
        gameScore.assassinWins();

        yield return new WaitForSeconds(4);
        NewRound();
    }

    public void PresidentSaved()
    {
        if (state != RoundState.Playing)
            return;
        state = RoundState.PresidentSaved;
        blur.enabled = true;
        presidentSavedTitle.SetTrigger("Go");
        StartCoroutine(EPresidentSaved());
    }

    IEnumerator EPresidentSaved()
    {
        gameScore.bodyguardWins();

        yield return new WaitForSeconds(4);
        NewRound();
    }

    public void OutOfTime()
    {
        if (state != RoundState.Playing)
            return;
        state = RoundState.TimeOut;
        blur.enabled = true;
        outOfTimeTitle.SetTrigger("Go");
        StartCoroutine(EOutOfTime());
    }

    IEnumerator EOutOfTime()
    {
        gameScore.bodyguardWins();

        yield return new WaitForSeconds(3);
        NewRound();
    }

    public void NewRound()
    {
        SceneManager.LoadScene(0);

        gameScore.newRoundStart();
    }

}