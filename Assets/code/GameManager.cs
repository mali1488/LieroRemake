/// <summary>
/// Spawns players and handle their deaths and respawn. Also spawn HUD and toggles volume button.
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GameManager : MonoBehaviour {

  public Sprite mute;
  [SerializeField]
  private Button button = null;
  public bool isMute = false;

  public GameObject spawnedObject;
  private GameObject player1Prefab;
  private GameObject player2Prefab;
  private Player player1;
  private Player player2;
  public GameObject hudPrefab;
  private Hud hud;

  void Start () {
    button.onClick.AddListener(() => {
        if(!Input.GetKey("space")){
          Mute();
        }
      });

    StartCoroutine(spawn());
    hud = hudPrefab.GetComponent<Hud>();
    hud.Setup(player1, player2);
  }

  void Update() {

    if(player1.GetHealth() <= 0 && !player1.IsDead) {
      player1.Die();
      player2.SetKills();
      player1.RespawnAt();
    }

    if(player2.GetHealth() <= 0 && !player2.IsDead) {
      player2.Die();
      player1.SetKills();
      player2.RespawnAt();
    }
  }

  IEnumerator spawn() {
    // The real keybindings choosen from the main menu
    /*
      moveRightPlayer1 = PlayerPrefs.GetString ("right");
      moveLeftPlayer1 = PlayerPrefs.GetString ("left");
      aimUpPlayer1 = PlayerPrefs.GetString ("up");
      aimDownPlayer1 = PlayerPrefs.GetString ("down");
      prevWeaponPlayer1 = PlayerPrefs.GetString ("next");
      nextWeaponPlayer1 = PlayerPrefs.GetString ("prev");
      shootPlayer1 = PlayerPrefs.GetString ("shoot");
      jump1 = PlayerPrefs.GetString ("jump");

      moveRightPlayer2 = PlayerPrefs.GetString ("right2");
      moveLeftPlayer2 = PlayerPrefs.GetString ("left2");
      aimUpPlayer2 = PlayerPrefs.GetString ("up2");
      aimDownPlayer2 = PlayerPrefs.GetString ("down2");
      shootPlayer2 = PlayerPrefs.GetString ("shoot2");
    */
    player1Prefab = Instantiate(spawnedObject);
    player1 = player1Prefab.GetComponent<Player>();
    player1.Setup("a", "d", "w", "s", "q", "e", "z", "space", "f", -106, 156, 0, 0, 0.5f, 1.0f);

    player2Prefab = Instantiate(spawnedObject);
    player2 = player2Prefab.GetComponent<Player>();
    player2.Setup ("left", "right", "up", "down", "k", "l", "m", "n", "b", 20, 156, 0.5f, 0, 0.5f, 1.0f);


    yield return null;
  }

  public void Mute() {
    isMute = !isMute;
    if(isMute) {
      button.image.overrideSprite = mute;
      AudioListener.pause = true;
      AudioListener.volume = 0;
    }else{
      button.image.overrideSprite = null;
      AudioListener.pause = false;
      AudioListener.volume = 1;
    }
  }

}
