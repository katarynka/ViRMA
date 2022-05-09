using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class ViRMA_Keyboard : MonoBehaviour
{
    private ViRMA_GlobalsAndActions globals;
    private Button[] keys;
    public string typedWordString = "";
    public TextMeshProUGUI typedWordTMP;
    public Image typedWordBg;
    public GameObject loadingIcon;
    private Coroutine activeQueryCoroutine;
    public Hand handInteractingWithKeyboard;
    public ViRMA_SearchTagsScrollableMenu scrollableQueryMenu;
    public GameObject scrollableCanvas;

    public ViRMA_SpawnInFrontOfCamera spawnScrollMenu;

    // flags
    public bool dimExQueryLoading;
    public bool keyboardLoaded;
    public bool keyboardFaded;
    public bool keyboardMoving;

    public OVRCameraRig m_CameraRig;

    private void Awake()
    {
        m_CameraRig = FindObjectOfType<OVRCameraRig>();
        globals = m_CameraRig.GetComponent<ViRMA_GlobalsAndActions>();
        scrollableCanvas = GameObject.Find("ScrollableUnityCanvas");
        scrollableQueryMenu = scrollableCanvas.GetComponentInChildren<ViRMA_SearchTagsScrollableMenu>();
        spawnScrollMenu = scrollableCanvas.GetComponent<ViRMA_SpawnInFrontOfCamera>();


        keys = GetComponentsInChildren<Button>();
    }

    private void Start()
    {
        SetBtnDefaultState();

        typedWordTMP.text = typedWordString;

        StartCoroutine(LateStart());
    }

    private void Update()
    {
        LoadingIndicator();

        KeyboardRepositioning();

        PhysicalKeyboardInput();     // JBAL & KTOB - Physical keyboard interactions

       
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(1);

        // delayed things

        //ToggleDimExKeyboard(true); // for testing
    }

    private void OnTriggerEnter(Collider triggeredCol)
    {
        if (triggeredCol.GetComponent<ViRMA_Drumstick>())
        {
            FadeKeyboard(false);
        }
    }
    private void OnTriggerExit(Collider triggeredCol)
    {
        if (triggeredCol.GetComponent<ViRMA_Drumstick>())
        {
            // Debug.Log("EXIT - Keyboard triggered!");
        }
    }

    public void SetBtnDefaultState()
    {
        foreach (Button key in keys)
        {
            key.onClick.AddListener(() => SubmitKey(key));

            Text keyText = key.GetComponentInChildren<Text>();
            Image keyBackground = key.GetComponent<Image>();
            ViRMA_UiElement virmaBtn = key.GetComponent<ViRMA_UiElement>();

            Color32 bgCol;
            Color32 textCol;

            if (key.name == "CLEAR")
            {
                bgCol = ViRMA_Colors.flatOrange;
                textCol = Color.white;
            }
            else if (key.name == "CLOSE")
            {
                bgCol = new Color32(192, 57, 43, 255);
                textCol = Color.white;
            }
            else if (key.name == "BACKSPACE")
            {
                bgCol = ViRMA_Colors.darkBlue;
                textCol = Color.white;
            }
            else if (key.name == "MOVE")
            {
                bgCol = ViRMA_Colors.grey;
                textCol = Color.white;
            }
            else if (key.name == "SUBMIT")
            {
                bgCol = new Color32(39, 174, 96, 255);
                textCol = Color.white;
            }
            else
            {
                bgCol = ViRMA_Colors.darkBlue;
                textCol = Color.white;
            }

            virmaBtn.GenerateBtnDefaults(bgCol, textCol);
        }
    }
    public void ToggleDimExKeyboard(bool onOff)
    {
        // scaling and appearance
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        FadeKeyboard(false);

        if (onOff)
        {

            // Set keyboard position to be in front of the camera
            transform.position = Camera.main.transform.position + new Vector3(0, -0.1f, 0.4f);
            transform.rotation = Camera.main.transform.rotation;
            keyboardLoaded = true;
            spawnScrollMenu.SpawnScrollMenu();
        }
        else
        {
            transform.position = new Vector3(0, 9999, 0);
            StartCoroutine(globals.dimExplorer.ClearDimExplorer());
            keyboardLoaded = false;
        }
    }
    public void FadeKeyboard(bool toFade)
    {
        if (toFade == true)
        {     
            if (keyboardFaded == false)
            {
                Collider[] keyboardColliders = GetComponentsInChildren<Collider>();
                foreach (var col in keyboardColliders)
                {
                    if (col.gameObject != gameObject)
                    {
                        col.enabled = false;
                    }
                }

                ViRMA_UiElement[] btnElements = GetComponentsInChildren<ViRMA_UiElement>();
                foreach (var btnElement in btnElements)
                {
                    btnElement.buttonFaded = true;
                }

                typedWordTMP.color = new Color(typedWordTMP.color.r, typedWordTMP.color.g, typedWordTMP.color.b, 0.15f);
                typedWordBg.color = new Color(typedWordBg.color.r, typedWordBg.color.g, typedWordBg.color.b, 0.15f);

                keyboardFaded = true;
            }        
        }
        else
        {
            if (keyboardFaded == true)
            {
                Collider[] keyboardColliders = GetComponentsInChildren<Collider>();
                foreach (var col in keyboardColliders)
                {
                    col.enabled = true;
                }

                ViRMA_UiElement[] btnElements = GetComponentsInChildren<ViRMA_UiElement>();
                foreach (var btnElement in btnElements)
                {
                    btnElement.buttonFaded = false;
                }

                typedWordTMP.color = new Color(typedWordTMP.color.r, typedWordTMP.color.g, typedWordTMP.color.b, 1.0f);
                typedWordBg.color = new Color(typedWordBg.color.r, typedWordBg.color.g, typedWordBg.color.b, 1.0f);

                globals.dimExplorer.horizontalRigidbody.velocity = Vector3.zero;

                keyboardFaded = false;
            }    
        }
    }
    private void KeyboardRepositioning()
    {
        if (keyboardMoving)
        {
            if (globals.menuInteraction_Select.GetState(handInteractingWithKeyboard.handType))
            {
                if (handInteractingWithKeyboard)
                {
                    if (transform.parent != handInteractingWithKeyboard)
                    {
                        transform.parent = handInteractingWithKeyboard.transform;
                    }

                    if (keyboardFaded)
                    {
                        FadeKeyboard(false);
                    }

                    if (globals.dimExplorerActions.IsActive())
                    {
                        globals.dimExplorerActions.Deactivate();
                    }
                }      
            }
            else
            {
                if (handInteractingWithKeyboard)
                {
                    if (transform.parent == handInteractingWithKeyboard.transform)
                    {
                        transform.parent = null;
                        keyboardMoving = false;
                    }
                }
            }
        }
    }
    private void SubmitKey(Button key)
    {
        string buttonName = key.gameObject.name;
        
        string submittedChar = "";
        if (key.GetComponentInChildren<Text>())
        {
            submittedChar = key.GetComponentInChildren<Text>().text;
        } 

        if (buttonName == "SUBMIT")
        {
            if (typedWordString.Length > 0)
            {
                if (activeQueryCoroutine != null)
                {
                    StopCoroutine(activeQueryCoroutine);
                }

                dimExQueryLoading = true;
                key.enabled = false;
                StartCoroutine(globals.dimExplorer.ClearDimExplorer());

                Debug.Log("scrollMenu ");
                activeQueryCoroutine = StartCoroutine(ViRMA_APIController.SearchHierachies(typedWordString.ToLower(), (nodes) => {             
                    //StartCoroutine(globals.dimExplorer.LoadDimExplorer(nodes));
                    StartCoroutine(scrollableQueryMenu.PopulateMenu(nodes));
                    activeQueryCoroutine = null;
                    key.enabled = true;
                    // INITIATE BUTTONS HERE???
  
                }));
            }      
        }
        else if (buttonName == "CLOSE")
        {

            if (activeQueryCoroutine != null)
            {
                StopCoroutine(activeQueryCoroutine);
            }
            dimExQueryLoading = false;
            ToggleDimExKeyboard(false);
        }
        else if (buttonName == "BACKSPACE")
        {
            if (typedWordString.Length > 0)
            {
                typedWordString = typedWordString.Substring(0, typedWordString.Length - 1);
            }       
        }
        else if (buttonName == "CLEAR")
        {
            typedWordString = "";
        }
        else if (buttonName == "MOVE")
        {
            handInteractingWithKeyboard = key.GetComponent<ViRMA_UiElement>().handINteractingWithUi;
            keyboardMoving = true;
        }
        else if (buttonName == "SPACE")
        {
            if (typedWordString.Length > 0)
            {
                if (typedWordString.Substring(typedWordString.Length - 1) != " ")
                {
                    typedWordString += " ";
                }
            }             
        }
        else
        {
            typedWordString += submittedChar;
        }

        typedWordTMP.text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(typedWordString.ToLower());
    }
    private void LoadingIndicator()
    {
        if (dimExQueryLoading)
        {
            if (!loadingIcon.transform.parent.gameObject.activeSelf)
            {
                loadingIcon.transform.parent.gameObject.SetActive(true);
            }          
            loadingIcon.transform.Rotate(0, 0, -300f * Time.deltaTime);
        }
        else
        {
            if (loadingIcon.transform.parent.gameObject.activeSelf)
            {
                loadingIcon.transform.parent.gameObject.SetActive(false);
            }        
        }
        
    }

    // JBAL & KTOB - keyboard inputs added to search string
    private void PhysicalKeyboardInput()
    {

            //Check for escapekey
            if (Input.GetKeyDown(KeyCode.Escape) && !keyboardFaded)
            {
                if (activeQueryCoroutine != null)
                {
                    StopCoroutine(activeQueryCoroutine);
                }
                dimExQueryLoading = false;
                ToggleDimExKeyboard(false);
            }

            else if(Input.GetKeyDown(KeyCode.Return) && !keyboardLoaded)
            {
                ToggleDimExKeyboard(true);
                typedWordString = "";
            }

        if (keyboardLoaded)
        {
            foreach (char c in Input.inputString)
            {
                if (c == '\b') // has backspace/delete been pressed?
                {
                    if (typedWordString.Length != 0)
                    {
                        typedWordString = typedWordString.Substring(0, typedWordString.Length - 1);
                    }
                }
                else if ((c == '\n') || (c == '\r') && keyboardLoaded) // enter/return
                {
                    if (typedWordString.Length > 0)
                    {
                        if (activeQueryCoroutine != null)
                        {
                            StopCoroutine(activeQueryCoroutine);
                        }

                        dimExQueryLoading = true;
                        StartCoroutine(globals.dimExplorer.ClearDimExplorer());

                        activeQueryCoroutine = StartCoroutine(ViRMA_APIController.SearchHierachies(typedWordString.ToLower(), (nodes) => {

                            // Populate scrollable menu
                            StartCoroutine(scrollableQueryMenu.PopulateMenu(nodes));
                            activeQueryCoroutine = null;
                            dimExQueryLoading = false;
                        }));

                    }
                }
                else
                {
                    typedWordString += c;

                }
                typedWordTMP.text = typedWordString;
            }
        }
    }

}
