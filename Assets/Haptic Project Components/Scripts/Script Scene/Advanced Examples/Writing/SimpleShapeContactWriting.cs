using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class SimpleShapeContactWriting : HapticClassScript {

	//Generic Haptic Functions
	private GenericFunctionsClass myGenericFunctionsClassScript;

	//private Writing myWritingScript;
	//private GameObject myResetButton;
	//private Color[] buttonResetColors = {new Color(1.0f, 1.0f, 1.0f), new Color(0.25f, 0.97f, 0.37f)};

    private Rigidbody rb;

    //Workspace Update Value
    float[] workspaceUpdateValue = new float[1];

    private bool isKinematic;

    /*****************************************************************************/

    void Awake()
	{
		myGenericFunctionsClassScript = transform.GetComponent<GenericFunctionsClass>();

        rb = hapticCursor.gameObject.GetComponent<Rigidbody>();

        isKinematic = true;
		//myResetButton = GameObject.Find("reset");
		//myResetButton.GetComponent<Renderer>().material.color = buttonResetColors[0];
		//myWritingScript = GameObject.Find("Black_Board").GetComponent<Writing>();
	}

	void Start()
	{

		if(PluginImport.InitHapticDevice())
		{
			Debug.Log("OpenGL Context Launched");
			Debug.Log("Haptic Device Launched");
			
			myGenericFunctionsClassScript.SetHapticWorkSpace();
			myGenericFunctionsClassScript.GetHapticWorkSpace();

            //Update Workspace as function of camera
            //PluginImport.UpdateWorkspace(myHapticCamera.transform.rotation.eulerAngles.y);//To be deprecated

            //Update the Workspace as function of camera
            //for (int i = 0; i < workspaceUpdateValue.Length; i++)
                //workspaceUpdateValue[i] = myHapticCamera.transform.rotation.eulerAngles.y;

            //PluginImport.UpdateHapticWorkspace(ConverterClass.ConvertFloatArrayToIntPtr(workspaceUpdateValue));

            //Set Mode of Interaction
            /*
			 * Mode = 0 Contact
			 * Mode = 1 Manipulation - So objects will have a mass when handling them
			 * Mode = 2 Custom Effect - So the haptic device simulate vibration and tangential forces as power tools
			 * Mode = 3 Puncture - So the haptic device is a needle that puncture inside a geometry
			 */
            PluginImport.SetMode(ModeIndex);
            //Show a text descrition of the mode
            myGenericFunctionsClassScript.IndicateMode();

            //Set the touchable face(s)
            PluginImport.SetTouchableFace(ConverterClass.ConvertStringToByteToIntPtr(TouchableFace));

        }
		else
			Debug.Log("Haptic Device cannot be launched");

        /***************************************************************/
        //Set Environmental Haptic Effect
        /***************************************************************/
        // Constant Force Example - We use this environmental force effect to simulate the weight of the cursor
        //myGenericFunctionsClassScript.SetEnvironmentConstantForce();

        // Viscous Force Example
        myGenericFunctionsClassScript.SetEnvironmentViscosity();


        /***************************************************************/
        //Setup the Haptic Geometry in the OpenGL context 
        //And read haptic characteristics
        /***************************************************************/
        myGenericFunctionsClassScript.SetHapticGeometry();
		
		//Get the Number of Haptic Object
		//Debug.Log ("Total Number of Haptic Objects: " + PluginImport.GetHapticObjectCount());
		
		/***************************************************************/
		//Launch the Haptic Event for all different haptic objects
		/***************************************************************/
		PluginImport.LaunchHapticEvent();
	}

    public bool GetIsKinematic()
    {
        return isKinematic;
    }

    public void SetIsKinematic(bool value)
    {
        isKinematic = value;
    }

    private void FixedUpdate()
    {
        //Associate the cursor object with the haptic proxy value  
        //myGenericFunctionsClassScript.GetProxyValues();
    }


    void Update()
	{
        /*if (PluginImport.GetButtonState(1, 1) && isKinematic)
        {
            Debug.Log("Inside button press");
            isKinematic = !isKinematic;

            GameObject[] capsules = GameObject.FindGameObjectsWithTag("ElectrodeCapsule");

            foreach (GameObject go in capsules)
            {
                Rigidbody goRB = go.GetComponent<Rigidbody>();
                goRB.isKinematic = false;
            }
        }*/
        
        if (PluginImport.GetButtonState(1, 2))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /*if (PluginImport.GetButtonState(1, 1))
        {
            isKinematic = !isKinematic;

            GameObject[] capsules = GameObject.FindGameObjectsWithTag("ElectrodeCapsule");

            foreach (GameObject go in capsules)
            {
                Rigidbody goRB = go.GetComponent<Rigidbody>();
                goRB.isKinematic = false;
            }
        }*/
        

        /***************************************************************/
        //Update Workspace as function of camera
        /***************************************************************/
        //PluginImport.UpdateWorkspace(myHapticCamera.transform.rotation.eulerAngles.y);//To be deprecated

        //Update the Workspace as function of camera
        //for (int i = 0; i < workspaceUpdateValue.Length; i++)
            //workspaceUpdateValue[i] = myHapticCamera.transform.rotation.eulerAngles.y;

        //PluginImport.UpdateHapticWorkspace(ConverterClass.ConvertFloatArrayToIntPtr(workspaceUpdateValue));

        /***************************************************************/
        //Update cube workspace
        /***************************************************************/
        myGenericFunctionsClassScript.UpdateGraphicalWorkspace();
		
		/***************************************************************/
		//Haptic Rendering Loop
		/***************************************************************/
		PluginImport.RenderHaptic ();

        //Associate the cursor object with the haptic proxy value  
        myGenericFunctionsClassScript.GetProxyValues(isKinematic);
        //myGenericFunctionsClassScript.GetProxyValues();

        /*if (isKinematic)
        {
            GameObject[] capsules = GameObject.FindGameObjectsWithTag("ElectrodeCapsule");

            foreach (GameObject go in capsules)
            {
                Rigidbody goRB = go.GetComponent<Rigidbody>();
                goRB.constraints = RigidbodyConstraints.None;
            }
            isKinematic = false;
        }*/


        //Vector3 pos = ConverterClass.ConvertDouble3ToVector3(ConverterClass.ConvertIntPtrToDouble3(PluginImport.GetProxyPosition()));
        //Vector3 movement = (rb.position - pos).normalized;
        //rb.MovePosition(rb.position + movement * Time.deltaTime);
        //hapticCursor.transform.position = pos;
        //myGenericFunctionsClassScript.GetTouchedObject();

        //Reset the writing on the board
        //if(ConverterClass.ConvertIntPtrToByteToString( PluginImport.GetTouchedObjectName()) == "reset") // GetTouchedObjectName - To be deprecated
        //if (ConverterClass.ConvertIntPtrToByteToString(PluginImport.GetTouchedObjName(1)) == "reset")
        //{
        //myWritingScript.cleanBoard();

        //Change the Color of the button material
        //myResetButton.GetComponent<Renderer>().material.color = buttonResetColors[1];
        //}
        //else
        //myResetButton.GetComponent<Renderer>().material.color = buttonResetColors[0];

    }

	void OnDisable()
	{
		if (PluginImport.HapticCleanUp())
		{
			Debug.Log("Haptic Context CleanUp");
			Debug.Log("Desactivate Device");
			Debug.Log("OpenGL Context CleanUp");
		}
	}
}
