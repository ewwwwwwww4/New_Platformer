using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerControl : MonoBehaviour
{


    float horizontalMove;
    public float speed = 2f;

    Rigidbody2D myBody;

    bool grounded = false;

    public float castDist = 1f;
    public float jumpPower = 2f;
    public float gravityScale = 5f;
    public float gravityFall = 40f;

    //public float hurtforce = 3f;

    public AudioClip soundEffect;


    //public Transform bulletSpawnPoint;
    //public GameObject bullerPrefab;
    //  public float bullerSpeed = 10;

    //private bool enterAllowed;
    //private string sceneToLoad;
    public string targetSceneName = "the end";

    public string targetSceneName2 = "Lv.2";

    public string targetSceneName3 = "lv3";

    public string targetSceneName4 = "win";


    private int life = 5;

    private Vector3 respawnPoint;

    bool jump = false;
    bool doubleJump;

    //  bool fall = false;
    Animator myAnim;
    SpriteRenderer myRend;

    public GameObject life01, life02, life03, life04,life05;
    public GameObject cameraObject;

    private bool isTeleporting = false; // 是否正在传送
    private Vector3 targetTeleportPosition; // 目标传送位置

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myRend = GetComponent<SpriteRenderer>();
        respawnPoint = transform.position;
    }


    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");

        //  if (Input.GetKeyDown(KeyCode.Return))  {
        //    var bullet = Instantiate(bullerPrefab,bulletSpawnPoint.position,bulletSpawnPoint.rotation);
        //  bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bullerSpeed;  }

        float verticalVelocity = myBody.velocity.y; // 获取玩家的垂直速度


        if (grounded && !Input.GetButton("Jump"))
        {
            doubleJump = false;
            //  myAnim.SetBool("jumping", false);
            //  myAnim.SetBool("falling", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded || doubleJump)
            {
                //   myAnim.SetBool("jumping", true);
                jump = true;
                doubleJump = !doubleJump;
            }
        }

        if (verticalVelocity > 0.1f)
        {
            // 玩家正在上升，设置为跳跃动画
            myAnim.SetBool("jumping", true);
            myAnim.SetBool("falling", false);
            myAnim.SetBool("running", false);
        }
        else if (verticalVelocity < -0.1f)
        {
            // 玩家正在下降，设置为下落动画
            myAnim.SetBool("jumping", false);
            myAnim.SetBool("falling", true);
            myAnim.SetBool("running", false);
        }
        else
        {
            if (horizontalMove > 0.2f)
            {
                myAnim.SetBool("running", true);
                myRend.flipX = false;
            }
            else if (horizontalMove < -0.2f)
            {
                myAnim.SetBool("running", true);
                myRend.flipX = true;
            }
            else
            {
                myAnim.SetBool("running", false);
            }
            myAnim.SetBool("jumping", false);
            myAnim.SetBool("falling", false);
        }
    }

    void FixedUpdate()
    {
        float moveSpeed = horizontalMove * speed;


        if (jump)
        {
            myBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }


        if (myBody.velocity.y >= 0)
        {
            myBody.gravityScale = gravityScale;
        }
        else if (myBody.velocity.y < 0)
        {
            myBody.gravityScale = gravityFall;
        }


        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);

        Debug.DrawRay(transform.position, Vector2.down * castDist, Color.red);


        if (hit.collider != null && hit.transform.name == "Ground")
        {
            myAnim.SetBool("jumping", false);
            myAnim.SetBool("falling", false);
            grounded = true;
        }
        else
        {
            myAnim.SetBool("falling", true);
            grounded = false;
        }

        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

       
            if (collision.gameObject.tag == "Trap")
        {
            AudioSource.PlayClipAtPoint(soundEffect, transform.position);
             CameraShake cameraShakeComponent = cameraObject.GetComponent<CameraShake>();
             cameraShakeComponent.TriggerShake();
            life--;
            Life();
            // 获取 Trap 和玩家的位置
            Vector3 trapPosition = collision.gameObject.transform.position;
            Vector3 playerPosition = transform.position;

            // 指定水平移动距离，你可以根据需要调整
            float moveDistance = -3.0f; // 例如，移动1个单位

            // 计算玩家应该移动的位置
            float targetX = (trapPosition.x > playerPosition.x) ? playerPosition.x + moveDistance : playerPosition.x - moveDistance;

            // 设置新的玩家位置，保持垂直位置不变
            transform.position = new Vector3(targetX, playerPosition.y, playerPosition.z);

            // if (transform.position.y > collision.gameObject.transform.position.y) {}
        }


        if (collision.gameObject.tag == "Door1")
        {
            SceneManager.LoadScene(targetSceneName2);
        }
        if (collision.gameObject.tag == "Door2")
        {
            SceneManager.LoadScene(targetSceneName3);
        }
        if (collision.gameObject.tag == "Door3")
        {
            SceneManager.LoadScene(targetSceneName4);
        }


        void Life()
        {
            if (life == 5)
            {
                life05.SetActive(true);
                life04.SetActive(true);
                life03.SetActive(true);
                life02.SetActive(true);
                life01.SetActive(true);
            }
            if (life == 4)
            {
                life05.SetActive(false);
                life04.SetActive(true);
                life03.SetActive(true);
                life02.SetActive(true);
                life01.SetActive(true);
            }
            if (life == 3)
            {
                life05.SetActive(false);
                life04.SetActive(false);
                life03.SetActive(true);
                life02.SetActive(true);
                life01.SetActive(true);
            }
            if (life == 2)
            {
                life05.SetActive(false);
                life04.SetActive(false);
                life03.SetActive(false);
                life02.SetActive(true);
                life01.SetActive(true);
            }
            if (life == 1)
            {
                life05.SetActive(false);
                life04.SetActive(false);
                life03.SetActive(false);
                life02.SetActive(false);
                life01.SetActive(true);
            }
            if (life < 1)
            {
                life05.SetActive(false);
                life04.SetActive(false);
                life03.SetActive(false);
                life02.SetActive(false);
                life01.SetActive(false);
                //sceneToLoad = "the end";
                // enterAllowed = true;
                SceneManager.LoadScene(targetSceneName);
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DDL")
        {

            if (!isTeleporting)
            {
                StartCoroutine(TeleportWithDelay(respawnPoint));
                // 在其他脚本中使用 CameraShake 组件触发摄像机震动
               // CameraShake cameraShakeComponent = cameraObject.GetComponent<CameraShake>();
               // cameraShakeComponent.TriggerShake();

            }
        }
        else if (collision.tag == "checkpoint")
        {
            respawnPoint = transform.position; // 设置复活点
        }
    }

    // 带有延迟的传送协程
    private IEnumerator TeleportWithDelay(Vector3 teleportPosition)
    {
        isTeleporting = true;
        targetTeleportPosition = teleportPosition;

        // 在1秒延迟后传送
        yield return new WaitForSeconds(0.3f);

        // 传送玩家到目标位置
        transform.position = targetTeleportPosition;

        // 传送完成后将isTeleporting重置为false
        isTeleporting = false;

        life--;
        Life(); // 碰到死亡区域，生命值减少
    }
    void Life()
    {
        {
            if (life == 5)
            {
                life05.SetActive(true);
                life04.SetActive(true);
                life03.SetActive(true);
                life02.SetActive(true);
                life01.SetActive(true);
            }
            if (life == 4)
            {
                life05.SetActive(false);
                life04.SetActive(true);
                life03.SetActive(true);
                life02.SetActive(true);
                life01.SetActive(true);
            }
            if (life == 3)
            {
                life05.SetActive(false);
                life04.SetActive(false);
                life03.SetActive(true);
                life02.SetActive(true);
                life01.SetActive(true);
            }
            if (life == 2)
            {
                life05.SetActive(false);
                life04.SetActive(false);
                life03.SetActive(false);
                life02.SetActive(true);
                life01.SetActive(true);
            }
            if (life == 1)
            {
                life05.SetActive(false);
                life04.SetActive(false);
                life03.SetActive(false);
                life02.SetActive(false);
                life01.SetActive(true);
            }
            if (life < 1)
            {
                life05.SetActive(false);
                life04.SetActive(false);
                life03.SetActive(false);
                life02.SetActive(false);
                life01.SetActive(false);
                //sceneToLoad = "the end";
                // enterAllowed = true;
                SceneManager.LoadScene(targetSceneName);
            }
        }
    }
}

