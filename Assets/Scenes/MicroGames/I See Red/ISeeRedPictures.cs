using System.Collections.Generic;
using UnityEngine;

public class ISeeRedPictures : MonoBehaviour
{

    [SerializeField] public List<Texture> redPictures = new List<Texture>();
    [SerializeField] public List<Texture> notRedPictures = new List<Texture>();
    [SerializeField] public Material pictureMaterial;
    [SerializeField] public int numberOfPicturesToShow;
    [SerializeField] public float howLongToShowPicture;
    [SerializeField] public bool isPlaying = false;
    [SerializeField] public GameObject iSeeRedGameObject;

    public bool isRed = false;
    public bool hasShownRed = false;

    public int numberOfRedPictures;
    public int numberOfNotRedPictures;

    public float showPictureTimer;
    public float chanceOfRedPicture;

    public ISeeRed iSeeRed;

    // Start is called before the first frame update
    

    public void Initialise()
    {
        Debug.Log("Starting ISeeRedPictures");
        numberOfRedPictures = redPictures.Count;
        numberOfNotRedPictures = notRedPictures.Count;

        if (iSeeRedGameObject.TryGetComponent<ISeeRed>(out iSeeRed))
        {
            
            showPictureTimer = howLongToShowPicture;
        }
        else
            Debug.Log(this.name + ":  No ISeeRed");
    }

    // Update is called once per frame
    void Update()
    {

        if (isPlaying)
        {
            if (showPictureTimer >= howLongToShowPicture)
            {

                ShowPicture();
                showPictureTimer = 0;

            }
            showPictureTimer += Time.deltaTime;

        }
        
    }

    private void ShowPicture()
    {
        Debug.Log("ShowPicture function");
        if (Random.value >= chanceOfRedPicture)
        {
            pictureMaterial.SetTexture("_MainTex", redPictures[Random.Range(0,numberOfRedPictures)]);
            iSeeRed.isRed = true;

        }
        else
        {
            pictureMaterial.SetTexture("_MainTex", notRedPictures[Random.Range(0, numberOfNotRedPictures)]);
            iSeeRed.isRed = false;

        }
        

    }
    private void OnDestroy()
    {
        pictureMaterial.SetTexture("_MainTex", null);
    }
}
