using UnityEngine;
using System.Collections;

public class Hud : MonoBehaviour {

  void OnGUI () {
    GUI.BeginGroup (new Rect (-20, -20, Screen.width + 50, 100));

    GUI.Box (new Rect (-20,-20,Screen.width + 50,100), "");

    GUI.EndGroup ();
  }

}