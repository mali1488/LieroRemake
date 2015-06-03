using UnityEngine;
using System.Collections;

public class Hud : MonoBehaviour {

  public Texture2D point;
  private float sliderValue = 1.0f;
  private float maxSliderValue = 10.0f;
  private float hudHeight = Screen.height * 0.15f;
  private float labelHeight;
  private float fontSize = 24;
  private float stdHeight = 1200;
  private float fontScale;

  private string playerWeapon1 = "carbine";
  private string playerWeapon2 = "carbine";
  private int playerLives1 = 3;
  private int playerLives2 = 3;
  private int playerKills1 = 0;
  private int playerKills2 = 0;

  private Player player1;
  private Player player2;

  public void Setup(Player player1, Player player2) {
    this.player1 = player1.GetComponent<Player>();
    this.player2 = player2.GetComponent<Player>();
  }
  void Start() {
    labelHeight = hudHeight / 3;
    fontScale = Screen.height / 1200;
    fontSize *= fontScale;
  }

  void OnGUI () {
    /*
      GUI.BeginGroup (new Rect ( Screen.width / 2, 0, 1, Screen.height ));

      GUI.Box (new Rect ( Screen.width / 2 - 100, 0, 10, Screen.height), "");

      GUI.EndGroup ();
    */
    DrawLine(new Vector2(Screen.width / 2, 0), new Vector2(Screen.width / 2, Screen.height), 5);

    GUILayout.BeginArea (new Rect (-20, Screen.height - hudHeight, Screen.width + 50, hudHeight));

    GUI.Box (new Rect (-20, 0,Screen.width + 50, hudHeight), "");

    GUILayout.BeginArea (new Rect (50, 0, Screen.width, hudHeight));

    GUILayout.BeginHorizontal();

    GUILayout.BeginVertical();
    GUI.Label ( new Rect (0, labelHeight * 0.10f, Screen.width / 12, labelHeight), "<size=" + Mathf.Round(fontSize) + ">Weapon: </size>");
    GUI.Label ( new Rect (0, labelHeight * 1.10f, Screen.width / 12, labelHeight), "<size=" + Mathf.Round(fontSize) + ">Lives: </size>");
    GUI.Label ( new Rect (0, labelHeight * 2.10f, Screen.width / 12, labelHeight), "<size=" + Mathf.Round(fontSize) + ">Kills: </size>");
    GUILayout.EndVertical();

    GUILayout.BeginVertical();
    GUI.Label ( new Rect (Screen.width / 12, labelHeight * 0.10f, Screen.width / 12, labelHeight), "<size=" + Mathf.Round(fontSize) + "> " + player1.GetWeaponName() + "</size>");
    GUI.Label ( new Rect (Screen.width / 12, labelHeight * 1.10f, Screen.width / 12, labelHeight), "<size=" + Mathf.Round(fontSize) + "> " + player1.GetLives() + "</size>");
    GUI.Label ( new Rect (Screen.width / 12, labelHeight * 2.10f, Screen.width / 12, labelHeight), "<size=" + Mathf.Round(fontSize) + "> "+ player1.GetKills() + "</size>");
    GUILayout.EndVertical();

    GUILayout.BeginVertical();
    GUI.Label ( new Rect (Screen.width / 2, labelHeight * 0.10f, Screen.width / 12, labelHeight), "<size=" + Mathf.Round(fontSize) + ">Weapon: </size>");
    GUI.Label ( new Rect (Screen.width / 2, labelHeight * 1.10f, Screen.width / 12, labelHeight), "<size=" + Mathf.Round(fontSize) + ">Lives: </size>");
    GUI.Label ( new Rect (Screen.width / 2, labelHeight * 2.10f, Screen.width / 12, labelHeight), "<size=" + Mathf.Round(fontSize) + ">Kills: </size>");
    GUILayout.EndVertical();

    GUILayout.BeginVertical();
    GUI.Label ( new Rect (7*Screen.width / 12, labelHeight * 0.10f, Screen.width / 12, labelHeight), "<size=" + Mathf.Round(fontSize) + "> " + player2.GetWeaponName() + "</size>");
    GUI.Label ( new Rect (7*Screen.width / 12, labelHeight * 1.10f, Screen.width / 12, labelHeight), "<size=" + Mathf.Round(fontSize) + "> " + player2.GetLives() + "</size>");
    GUI.Label ( new Rect (7*Screen.width / 12, labelHeight * 2.10f, Screen.width / 12, labelHeight), "<size=" + Mathf.Round(fontSize) + "> "+ player2.GetKills() + "</size>");
    GUILayout.EndVertical();


    GUILayout.EndHorizontal();

    GUILayout.EndArea ();

    GUILayout.EndArea ();

  }

  public void SetWeapon(int player, string weapon) {
    if(player == 1) {
      playerWeapon1 = weapon;
    }else{
      playerWeapon2 = weapon;
    }
  }

  public void SetLives(int player, int adj) {
    if(player == 1) {
      playerLives1 += adj;
    }else{
      playerLives2 += adj;
    }
  }

  public void SetKills(int player, int adj) {
    if(player == 1) {
      playerKills1 += adj;
    }else{
      playerKills2 += adj;
    }
  }
  private void DrawLine(Vector2 start, Vector2 end, int width)
  {
    Vector2 d = end - start;
    float a = Mathf.Rad2Deg * Mathf.Atan(d.y / d.x);
    if (d.x < 0)
      a += 180;

    int width2 = (int) Mathf.Ceil(width / 2);

    GUIUtility.RotateAroundPivot(a, start);
    GUI.DrawTexture(new Rect(start.x, start.y - width2, d.magnitude, width), point);
    GUIUtility.RotateAroundPivot(-a, start);
  }

}