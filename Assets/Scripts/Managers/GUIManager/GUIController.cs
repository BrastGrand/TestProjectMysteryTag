using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TestProjectForMysteryTag
{
    public enum ScreenLayer
    {
        None = 0,
        Bottom = 1,
        Main = 2,
        Top = 3,
        Popup = 4
    }

    public class GUIController : Singleton<GUIController>
    {
        [SerializeField]
        private GameObject screensRoot = null;

        [SerializeField]
        private GUIScreen[] currentScreens = new GUIScreen[0];

        [SerializeField]
        private GameObject[] screenPrefabs = new GameObject[0];

        private List<GUIScreen> screens;
        private Stack<GUIScreen> screenStack = new Stack<GUIScreen>();

        void Awake()
        {
            Debug.Log("GUIController awake");
            Instance = this;
            screens = currentScreens.ToList();
        }

        void Start()
        {
            Debug.Log("GuiController start");
            foreach (var curPrefab in screenPrefabs)
            {
               // Debug.Log("Screen " + curPrefab.name + " instantiated");
                GameObject curScreen = Instantiate(curPrefab);
                if (screensRoot != null)
                    curScreen.transform.SetParent(screensRoot.transform, false);
                else
                    curScreen.transform.SetParent(this.gameObject.transform, false);

                GUIScreen guiScreen = curScreen.GetComponent<GUIScreen>();
                screens.Add(guiScreen);
            }

            foreach (var curScreen in screens)
            {
                    curScreen.gameObject.SetActive(false);
            }

            foreach (var curScreen in currentScreens)
            {
                curScreen.Show();
            }

        }

        public T FoundScreen<T>() where T: GUIScreen
        {
            foreach (var curScreen in screens)
            {
                if (curScreen.GetType() == typeof(T))
                    return curScreen as T;
            }
            return null;
        }

        public GUIScreen FoundScreen(Type typeScreen)
        {
            foreach (var curScreen in screens)
            {
                if (curScreen.GetType() == typeScreen)
                    return curScreen;
            }
            return null;
        }

        public void ShowScreen<T>(bool hideAll = false) where T : GUIScreen
        {
            GUIScreen foundScreen = FoundScreen<T>();
            if (foundScreen != null)
            {
                ShowScreen(foundScreen, hideAll);
            }
            else Debug.LogWarning("Screen " + typeof(T) + " not found!");
        }

        public void ShowScreen(GUIScreen screen, bool hideAll = false)
        {
            if (hideAll)
            {
                foreach (var curScreen in screens)
                {
                    if (curScreen.GetType() != screen.GetType())
                        curScreen.Hide();
                }
                screenStack.Clear();
            }

            screen.Show();
            SortByLayer();
        }

        public void PushScreen<T>(bool isPopup) where T: GUIScreen
        {
            GUIScreen foundScreen = FoundScreen<T>();
            if (foundScreen != null)
            { 
                PushScreen(foundScreen, isPopup);
            }
            else Debug.LogWarning("Screen " + typeof(T) + " not found!");
        }

        public void PushScreen(GUIScreen screen, bool isPopup)
        {
            if (!isPopup)
            {
                foreach (var curScreen in screenStack)
                {
                    Debug.Log(curScreen.gameObject);
                    curScreen.Hide();
                }
            }

            screen.Show();
            screenStack.Push(screen);

            int i = 0;
            foreach (var curScreen in screenStack)
            {
                curScreen.OffsetZ = i++;
            }

            SortByLayer();
        }

        public void PopScreen()
        {
            if (screenStack.Count == 0)
                return;

            GUIScreen curScreen = screenStack.Pop();
            curScreen.Hide();

            if (screenStack.Count > 0)
            {
                GUIScreen newScreen = screenStack.Peek();
                newScreen.Show();
            }

            SortByLayer();
        }

        public GUIScreen GetTopPopupScreen()
        {
            if (screenStack.Count > 0)
                return screenStack.Peek();

            return null;
        }

        public void HideAll()
        {
            Debug.Log("HideAll");

            foreach (var curScreen in screens)
            {
                curScreen.Hide();
            }
            screenStack.Clear();
            SortByLayer();
        }

        public void HideScreen<T>() where T : GUIScreen
        {
            GUIScreen foundScreen = FoundScreen<T>();
            if (foundScreen != null)
            {
                foundScreen.Hide();
                SortByLayer();
            }
            else Debug.LogWarning("Screen " + typeof(T) + " not found!");
        }


        private void SortByLayer()
        {
            screens.Sort((screen, guiScreen) =>
            {
                float leftScreen = (float)screen.ScreenLayer + (float)screen.OffsetZ / 100.0f;
                float rightScreen = (float)guiScreen.ScreenLayer + (float)guiScreen.OffsetZ / 100.0f;
                return leftScreen.CompareTo(rightScreen);
            });

            for (int i = 0; i < screens.Count; i++)
            {
                screens[i].transform.SetSiblingIndex(i);
            }
        }
    }


}
