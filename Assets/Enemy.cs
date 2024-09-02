using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private Transform playerTarget;
    bool caughtPlayer;
    private Animator animator;
    float speed = 0.25f;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    GameObject LoseUI;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = playerTarget.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.win)
            Move();
    }

    private void Move()
    {
        if (!caughtPlayer)
        {
            transform.LookAt(playerTarget);
            
            transform.Translate(Vector3.forward * speed);
            transform.position = new Vector3(transform.position.x, -2.5f, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            caughtPlayer = true;
            animator.SetTrigger("JumpScare");
            cam.depth = 2;
            playerTarget.transform.GetChild(playerTarget.transform.childCount - 1).transform.SetParent(null, false);
            Destroy(playerTarget.gameObject);
            Destroy(GetComponent<AudioSource>());
            FindObjectOfType<AudioManager>().Found();
            LoseUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

}
