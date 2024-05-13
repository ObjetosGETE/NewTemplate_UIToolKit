using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CustomBuildWindow : EditorWindow
    {
        private readonly string[] _toolbarStrings = {"SESC","SENAC"};
        private int _selectedSplashScreen = 0;

        private const string LogoSescPath = "SplashArt/LOGOSESC";
        private const string LogoSenacPath = "SplashArt/LOGOSENAC";


        [InitializeOnLoadMethod]
        private static void InitOnLoad()
        {
            // hijack the Build button in Unity's Build Settings window
            BuildPlayerWindow.RegisterGetBuildPlayerOptionsHandler(OnGetBuildPlayerOptions);
            BuildPlayerWindow.RegisterBuildPlayerHandler(OnBuildPlayer);
        
        }
 
        private static BuildPlayerOptions OnGetBuildPlayerOptions(BuildPlayerOptions buildPlayerOptions)
        {
            OpenCustomBuildWindow(buildPlayerOptions);
            return buildPlayerOptions;
        }
 
        private static void OnBuildPlayer(BuildPlayerOptions buildPlayerOptions)
        {
            // Do nothing here as the build is triggered from the Build button of the custom window
        }
 
        public static CustomBuildWindow OpenCustomBuildWindow(BuildPlayerOptions buildPlayerOptions)
        {
            var w = EditorWindow.GetWindow<CustomBuildWindow>();
            // here you can store initial options from Unity's Build window, such as if "Build" or "Build And Run" was pressed
            w._buildOptions = buildPlayerOptions.options;
            w.maxSize = new Vector2(200,100);
            w.minSize = new Vector2(200,100);

            w.Show();
            return w;
        }
 
        private BuildOptions _buildOptions;
 
        private void OnEnable()
        {
            titleContent = new GUIContent("Custom Build Settings");
        }
 
        private void OnGUI()
        {
            // add your GUI controls to modify the build here
            GUILayout.Label("Qual Splash Screen deseja?");

            _selectedSplashScreen = GUILayout.Toolbar(_selectedSplashScreen, _toolbarStrings);
 
            // Build button
            var buildButtonLabel = (_buildOptions & BuildOptions.AutoRunPlayer) == 0 ? "Build" : "Build And Run";
            if (!GUILayout.Button(buildButtonLabel)) return;
            // Remove this if you don't want to close the window when starting a build
            Close();
 
            DoBuild();
        }
 
        private void DoBuild()
        {
            var buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.options = _buildOptions;
        
            // Here you can modify the buildPlayerOptions or the project with values set in the window

            PlayerSettings.SplashScreen.show = true;
            PlayerSettings.SplashScreen.backgroundColor = Color.white;
            PlayerSettings.SplashScreen.showUnityLogo = false;
            PlayerSettings.SplashScreen.unityLogoStyle = PlayerSettings.SplashScreen.UnityLogoStyle.DarkOnLight;
            PlayerSettings.SplashScreen.animationMode = PlayerSettings.SplashScreen.AnimationMode.Dolly;

            var newLogo = PlayerSettings.SplashScreenLogo.Create();
            newLogo.duration = 3f;
            Sprite newSprite;

            switch (_selectedSplashScreen)
            {
                case 0:
                    newSprite = Resources.Load<Sprite>(LogoSescPath);
                    newLogo.logo = newSprite;
                    break;
                case 1:
                    newSprite = Resources.Load<Sprite>(LogoSenacPath);
                    newLogo.logo = newSprite;
                    break;
                default:
                    break;
            }

            var l = new List<PlayerSettings.SplashScreenLogo>
            {
                newLogo
            };
            PlayerSettings.SplashScreen.logos = l.ToArray();

            try
            {
                // This gets the default scene list and options from Unity's Build Settings window
                // This prompts for the build output location and stores it in buildPlayerOptions.locationPathName
                buildPlayerOptions = BuildPlayerWindow.DefaultBuildMethods.GetBuildPlayerOptions(buildPlayerOptions);
            }
            catch (BuildPlayerWindow.BuildMethodException)
            {
                // Hide an exception from log if user cancels the build location prompt
                return;
            }

            // Execute the build (using the default build method in this example)
            BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(buildPlayerOptions);
        }
    }
}