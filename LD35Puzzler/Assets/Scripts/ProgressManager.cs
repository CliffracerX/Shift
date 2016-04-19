using UnityEngine;
using System.Collections;

public class ProgressManager : MonoBehaviour
{
	public bool[] completedLevels;
	public bool[] unlockedLevels;
	public string[] levelNames;
	public GameObject[] levels;
	public enum MenuStates {Logo=0, MainMenu=1, LevelSelect=2, Instructions=3, InGame=4}
	public static MenuStates currentState = MenuStates.Logo;
	public GUIStyle bigButtonStyle;
	public static ProgressManager instance;
	public Texture2D logo;

	void Start()
	{
		for(int i = 0; i<levelNames.Length; i++)
		{
			if(PlayerPrefs.HasKey(levelNames[i]))
			{
				completedLevels[i] = bool.Parse(PlayerPrefs.GetString(levelNames[i]));
				unlockedLevels[i] = bool.Parse(PlayerPrefs.GetString(levelNames[i]));
				if(i<levels.Length-1)
				{
					unlockedLevels[i+1] = bool.Parse(PlayerPrefs.GetString(levelNames[i]));
				}
			}
		}
		ProgressManager.instance=this;
	}

	void Update()
	{
		ProgressManager.instance=this;
	}

	public static void UpdateCompletion(int level, bool status)
	{
		PlayerPrefs.SetString(ProgressManager.instance.levelNames[level], status.ToString());
		ProgressManager.instance.completedLevels[level]=status;
		ProgressManager.instance.unlockedLevels[level]=status;
		if(level<ProgressManager.instance.levels.Length-1)
			ProgressManager.instance.unlockedLevels[level+1]=status;
	}

	void OnGUI()
	{
		if(currentState==MenuStates.Logo)
		{
			currentState=MenuStates.MainMenu;
		}
		if(currentState==MenuStates.MainMenu)
		{
			if(GUI.Button(new Rect(Screen.width/2-128, Screen.height/2-128, 256, 48), "LEVEL\nSELECT", bigButtonStyle))
			{
				currentState=MenuStates.LevelSelect;
			}
			if(GUI.Button(new Rect(Screen.width/2-128, Screen.height/2+128, 256, 48), "CONTROLS &\nINSTRUCTIONS", bigButtonStyle))
			{
				currentState=MenuStates.Instructions;
			}
			GUI.DrawTexture(new Rect(Screen.width/2-128, 16, 256, 96), logo);
		}
		if(currentState==MenuStates.LevelSelect)
		{
			if(GUI.Button(new Rect(Screen.width/2-128, Screen.height-64, 256, 48), "BACK", bigButtonStyle))
			{
				currentState=MenuStates.MainMenu;
			}
			for(int i = 0; i<levels.Length+1; i++)
			{
				if(i<levels.Length)
				{
					if(unlockedLevels[i])
					{
						int numPerPage = (int)(Screen.width/256);
						int pageNum = ((int)(i/numPerPage));
						if(GUI.Button(new Rect(16 + ((i%numPerPage)*256), 16+(pageNum*64), 256, 48),  levelNames[i].Replace("\\n", "\n")))
						{
							currentState=MenuStates.InGame;
							levels[i].SetActive(true);
						}
					}
				}
			}
		}
		if(currentState==MenuStates.Instructions)
		{
			if(GUI.Button(new Rect(Screen.width/2-128, Screen.height-64, 256, 48), "BACK", bigButtonStyle))
			{
				currentState=MenuStates.MainMenu;
			}
			GUI.Label(new Rect(Screen.width/2-128, 16, 256, 768), "--CONTROLS--" +
				"\nWASD for movement, by default" +
				"\nMouse cursor looks around" +
				"\nLMB for dragging objects (by default)" +
				"\nEscape to free cursor (by default)" +
				"\n\n--GAMEPLAY--" +
				"\nYour goal is simple enough." +
				"\nReach the exit in each test." +
				"\nHow you do so many change, based" +
				"\non the test, your own skill, etc." +
				"\n\nThere's always a number of 'ninja'" +
				"\nsolutions to each test, where you may" +
				"\nbypass the normal, intended solutions." +
				"\nThe most basic test element is boxes" +
				"\nand buttons, like from Portal and" +
				"\nother games like it." +
				"\nThere's multiple 'dimensions' in" +
				"\neach test, with different challenges," +
				"\ntest elements, and ways to the exit." +
				"\nProbable Open Rifts To All Space, or" +
				"\nPORTALs, are used to traverse those" +
				"\ndimensions.  They're always in the same" +
				"\nplace in each test, sending boxes or people" +
				"\nalike through them on collision." +
				"\nTest elements in one dimension will" +
				"\nfrequently change the other dimensions." +
				"\n\nThis concludes the simple info screen." +
			     "\nYou're on your own now.  Good luck!");
		}
	}
}