#region License Kerbal Klinic (NRKK)
/*
 * An addon that introduces a method to resurrect dead kerbals for Kerbal Space Program by Squad.
 *
 * Copyright (C) 2018 NepalRAWR
 * Copyright (C) 2019, 2022 zer0Kerbal (zer0Kerbal at hotmail dot com)
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */ 
#endregion

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
// using UnityEngine.Rendering;
// using System.Globalization;
using System.Text.RegularExpressions; // needed for REGEX

namespace KerbalKlinic
{

    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    public class KerbalKlinic : MonoBehaviour
    {   
        
        // GUI
		private GUIStyle buttonStyle;
		private bool hasPositioned;
		private List<string> message;
        private Rect KlinicWindow = new Rect(200, 200, 100, 100);
		private string title;
		private GUIStyle titleStyle;
		private GUIStyle listStyle;
		private bool initDone = false;

		bool isVisible = true;

        Rect MenuWindow;
        private Vector2 scrollPosition;
        // private Rect KlinicWindow = new Rect(200, 200, 100, 100);
        KSP.UI.Screens.ApplicationLauncherButton appLauncherButton;
        bool ButtonPress = false;
        public int ToolbarINT = 0;
        
        ProtoCrewMember SelectedKerb;
        
        public string KlinicPriceString;
        
        public double KlinicPrice;
        
        bool StockPrice;

        // IO
        readonly string RelPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        readonly ConfigNode Konf = ConfigNode.Load(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)+"/files/config.cfg");
        

        public void Awake()
        {
            //Get values from cfg
            MenuWindow = new Rect(Screen.width / 2 + int.Parse(Konf.GetValue("WindowPosX")), Screen.height / 2 + int.Parse(Konf.GetValue("WindowPosY")), 400, 400);
            KlinicPriceString = Konf.GetValue("Cost");
            StockPrice = bool.Parse(Konf.GetValue("StockPrice"));
            //create appbutton
            if (appLauncherButton == null)
            {
                Texture2D texture = new Texture2D(36, 36, TextureFormat.RGBA32, false);
                //object value = texture.LoadRawTextureData(File.ReadAllBytes(RelPath + "/files/button.png"));

                object value = texture.LoadImage(File.ReadAllBytes(RelPath + "/files/button.png"));
                appLauncherButton =  KSP.UI.Screens.ApplicationLauncher.Instance.AddModApplication(
                  () => { ButtonPress = true; },
                  () => { ButtonPress = false;},
                  null, null, null, null,
                  KSP.UI.Screens.ApplicationLauncher.AppScenes.SPACECENTER,
                   texture);
             }
        }
        
        public void OnGUI()
        {
            //create GUI
            if (ButtonPress == true ) 
            {
                GUI.skin = HighLogic.Skin;
                MenuWindow = GUI.Window(0, MenuWindow, MenuGUI, "Kerbal Klinic 1.1");
            }
        }
        void MenuGUI(int windowID)
        {
            //LISTENERSTELLUNG / get dead and alive kerbals
            IEnumerable<ProtoCrewMember> KerbalKIA = HighLogic.CurrentGame.CrewRoster.Kerbals(ProtoCrewMember.KerbalType.Crew, ProtoCrewMember.RosterStatus.Dead);
            IEnumerable<ProtoCrewMember> KerbalAlive = HighLogic.CurrentGame.CrewRoster.Kerbals(ProtoCrewMember.KerbalType.Crew, ProtoCrewMember.RosterStatus.Assigned);
            
            //Toolbar
           string[] toolbarSTRING = new string[] { "KerbalKlinic", "Options" };
           ToolbarINT = GUI.Toolbar(new Rect(20, 30, 360, 30), ToolbarINT, toolbarSTRING);

            //Main Window
            if (ToolbarINT == 0)
            {
                //Label
                GUI.Label(new Rect(100, 70, 200, 20), "Select Kerbal");

                //KerbalSelection
                GUI.BeginGroup(new Rect(50, 100, 300, 230));
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(300), GUILayout.Height(230));
                
                        //generate buttons for dead kerbals
                foreach (ProtoCrewMember p in KerbalKIA)
                {
                    if (p != null)
                    {
                        if (GUILayout.Button(p.ToString()))
                        {
                            Debug.Log(p.ToString());
                            SelectedKerb = p;
                        }
                    }
                }
                
                GUI.EndScrollView();
                GUI.EndGroup();

                //Resurrection Button
                if (SelectedKerb != null && SelectedKerb.rosterStatus != ProtoCrewMember.RosterStatus.Available)
                {
                    if (HighLogic.CurrentGame.Mode == Game.Modes.CAREER)
                    {   if (StockPrice == true)
                         { CalculateHireCost(); }
                        else if (StockPrice == false)
                        { KlinicPrice = double.Parse(KlinicPriceString); }
                        if (GUI.Button(new Rect(20, 340, 360, 60), "Resurrect " + SelectedKerb + " for " + KlinicPrice.ToString() + " funds."))
                        {
                            if (Funding.CanAfford(Convert.ToSingle(KlinicPrice)))
                            {
                                Funding.Instance.AddFunds(-KlinicPrice, TransactionReasons.CrewRecruited);
                                SelectedKerb.rosterStatus = ProtoCrewMember.RosterStatus.Available;
                            }

                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(20, 340, 360, 60), "Resurrect " + SelectedKerb))
                        {SelectedKerb.rosterStatus = ProtoCrewMember.RosterStatus.Available; }
                    }
                }
               
            }

            //Options
            else if(ToolbarINT == 1)
            {
                GUI.Label(new Rect(20, 70, 360, 20), "Change cost");
                KlinicPriceString = GUI.TextField(new Rect(20, 100, 360, 30), KlinicPriceString);
                KlinicPriceString = Regex.Replace(KlinicPriceString, "[^0-9]", "");
                if(KlinicPriceString == null)
                {
                    KlinicPriceString = "0";
                }
                if(GUI.Button(new Rect(20, 150, 360, 50), "Save in config"))
                {
                   
                    Konf.SetValue("Cost", KlinicPriceString);
                    Konf.Save(RelPath+"/files/config.cfg");
                }
                StockPrice = GUI.Toggle(new Rect(20, 220, 360, 40), StockPrice, "Use stock price");
            }
            GUI.DragWindow(new Rect(0, 0, 400, 400));

        }
        void OnDisable()
        {
            KSP.UI.Screens.ApplicationLauncher.Instance.RemoveModApplication(appLauncherButton);

            Konf.SetValue("WindowPosX", string.Format("f", MenuWindow.x - (Screen.width / 2)));
            Konf.SetValue("WindowPosY", string.Format("f", MenuWindow.y - (Screen.height / 2)));

            Konf.SetValue("StockPrice", StockPrice.ToString());

            //Konf.SetValue("WindowPosX", MenuWindow.x - (Screen.width / 2))
            //Konf.SetValue("WindowPosY", MenuWindow.y - Screen.height/2);
            //Konf.SetValue("Stock price", StockPrice);

            Konf.Save(RelPath + "/files/config.cfg");
        }

		private void Start() { }

        private void Update() { }

        private void OnFixedUpdate() { }

        protected void OnDestroy() { }

        void OnHide() { }

        void OnShow() { }

        void CalculateHireCost()
        {
            double HiredKerbals = HighLogic.CurrentGame.CrewRoster.GetActiveCrewCount() + 1;
            KlinicPrice = (150 * Math.Pow(HiredKerbals, 2) + 12350 * HiredKerbals) / 10;
        }
    }
}