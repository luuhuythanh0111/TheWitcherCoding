using RoslynCSharp;
using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCodeInput : MonoBehaviour
{
    public GameObject gameObjectAttachCode;

    [SerializeField]
    [TextArea(5,12)] string text1,text2;

    private string activeCSharpSource = null;
    private ScriptProxy activeCrawlerScript = null;
    private ScriptDomain domain = null;

    public TMP_InputField inputOfPlayer;

    public GameObject WarningText;
    public Text logError;

    public GameObject CompileCompleted;

    void Start()
    {
        domain = ScriptDomain.CreateDomain("MyTestDomain", true);

        //// Compile and load the source code
        //ScriptType type = domain.CompileAndLoadMainSource(source);

        //// We need to pass a game object because 'Test' 
        // ScriptProxy proxy = type.CreateInstance(gameObject);

        //// Call the 'SayHello' method
        //proxy.Call("SayHello");
    }


    float timer = 2f;
    private void Update()
    {


        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = 2f;
            //var x = this.GetComponent<Example>();
            //Destroy(x);
            //ScriptType type = domain.CompileAndLoadMainSource(source);

            //// We need to pass a game object because 'Test' 
            //ScriptProxy proxy = type.CreateInstance(gameObject);

            //// Call the 'SayHello' method
            //proxy.Call("SayHello");

            //RunCrawler();
        }
    }

    public void RunCrawler()
    {
        // Get the C# code from the input field
        string cSharpSource = text1 + inputOfPlayer.text + text2;

        // Dont recompile the same code
        if (activeCSharpSource != cSharpSource || activeCrawlerScript == null)
        {
            // Remove any other scripts
            StopCrawler();

            //try
            {
                // Compile code
                ScriptType type = domain.CompileAndLoadMainSource(cSharpSource, ScriptSecurityMode.UseSettings);

                // Check for null
                if (type == null)
                {
                    WarningText.SetActive(true);
                    logError.text = File.ReadAllText("LogError.txt");

                    String path = "LogError.txt";

                    using (var stream = new FileStream(path, FileMode.Truncate))
                    {
                        using (var writer = new StreamWriter(stream))
                        {
                            writer.Write("data");
                        }
                    }
                    if (domain.RoslynCompilerService.LastCompileResult.Success == false)
                        throw new Exception("Maze crawler code contained errors. Please fix and try again");
                    else if (domain.SecurityResult.IsSecurityVerified == false)
                        throw new Exception("Maze crawler code failed code security verification");
                    else
                        throw new Exception("Maze crawler code does not define a class. You must include one class definition of any name that inherits from 'RoslynCSharp.Example.MazeCrawler'");
                }

                CompileCompleted.SetActive(true);

                // Check for base class
                //if (type.IsSubTypeOf<MazeCrawler>() == false)
                //    throw new Exception("Maze crawler code must define a single type that inherits from 'RoslynCSharp.Example.MazeCrawler'");




                //// Create an instance
                activeCrawlerScript = type.CreateInstance(gameObjectAttachCode);
                activeCSharpSource = cSharpSource;

                //// Set speed value
                //activeCrawlerScript.Fields["breadcrumbPrefab"] = breadcrumbPrefab;
                //activeCrawlerScript.Fields["moveSpeed"] = mouseSpeed;
            }
            //catch (Exception e)
            //{
            //    // Show the code editor window
            //    codeEditorWindow.SetActive(true);
            //    throw e;
            //}
        }
        else
        {
            // Get the maze crawler instance
            //DebugThis debugThis = activeCrawlerScript.GetInstanceAs<DebugThis>(false);

            // Call the restart method
            //mazeCrawler.Restart();
        }
    }

    /// <summary>
    /// Causes the mouse crawler to stop moving and reset to its initial position.
    /// </summary>
    public void StopCrawler()
    {
        if (activeCrawlerScript != null)
        {
            // Get the maze crawler instance
            PlayerAttackment debugThis = activeCrawlerScript.GetInstanceAs<PlayerAttackment>(false);

            // Call the restart method
            //mazeCrawler.Restart();

            //// Destroy script
            activeCrawlerScript.Dispose();
            activeCrawlerScript = null;
        }
    }
}
