using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GameManager : MonoBehaviour {

  public Sprite speaker;
  public Sprite mute;
  public Button button;
  public bool isMute = false;

  public GameObject spawnedObject;
  private GameObject player1;
  private GameObject player2;
  //TODO: make game manager a singleton
  void Start () {
    button.image.overrideSprite = speaker;
    button.onClick.AddListener(() => {
        if(!Input.GetKey("space")){
          Mute();
        }
      });

    StartCoroutine(spawn());
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
    player1 = Instantiate(spawnedObject);
    player1.SendMessage("setGameManager",this);
    player1.GetComponent<Player>().Setup("a", "d", "w", "s", "q", "e", "z", "space", "f", -106, 156, 0, 0, 0.5f, 1.0f);
    player2 = Instantiate(spawnedObject);
    player2.SendMessage("setGameManager",this);
    player2.GetComponent<Player> ().Setup ("left", "right", "up", "down", "k", "l", "m", "n", "b", -120, 156, 0.5f, 0, 0.5f, 1.0f);
    yield return null;
  }

  public void Mute() {
    isMute = !isMute;
    if(isMute) {
      button.image.overrideSprite = mute;
      AudioListener.pause = true;
      AudioListener.volume = 0;
    }else{
      button.image.overrideSprite = speaker;
      AudioListener.pause = false;
      AudioListener.volume = 1;
    }
  }

  public void KillPlayer(Player player)
  {
    StartCoroutine(KillPlayerCo(player));
  }
  private IEnumerator KillPlayerCo(Player player)
  {
    player.Kill();
    yield return new WaitForSeconds(4f);
    player.RespawnAt();
  }
}
