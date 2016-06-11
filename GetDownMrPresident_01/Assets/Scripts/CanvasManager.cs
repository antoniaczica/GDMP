using UnityEngine;
using System.Collections;

public class CanvasManager : MonoBehaviour
{

    public static CanvasManager main;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (main == null) { main = this; }
        else { Destroy(gameObject); }
    }

    // Use this for initialization
    void Start()
    {
        //print(main);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
