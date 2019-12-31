using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    private SceneChangeManager sceneChangeManager;
    private List<Sprite> backgroundList = new List<Sprite>();

    //public Sprite background0;
    public Sprite background1;
    public Sprite background2;
    public Sprite background3;
    public Sprite background4;

    public List<Sprite> BackgroundList { get { return backgroundList; } set { value = backgroundList; } }

    /*
    void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float cameraHeight = Camera.main.orthographicSize * 2;
        Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        Vector2 scale = transform.localScale;
        if (cameraSize.x >= cameraSize.y)
        { // Landscape (or equal)
            scale *= cameraSize.x / spriteSize.x;
        }
        else
        { // Portrait
            scale *= cameraSize.y / spriteSize.y;
        }

        transform.position = Vector2.zero; // Optional
        transform.localScale = scale;
    }
    */
    // Start is called before the first frame update
    void Start()
    {
        sceneChangeManager = SceneChangeManager.instance;

        //backgroundList.Add(background0);
        backgroundList.Add(background1);
        backgroundList.Add(background2);
        backgroundList.Add(background3);
        backgroundList.Add(background4);

        gameObject.GetComponent<Image>().sprite = backgroundList[sceneChangeManager.CurrentLevel -1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
