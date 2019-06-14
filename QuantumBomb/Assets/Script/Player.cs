using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameController gameController;
    public BombaQuantica bombaQuantica;

    public float speed;
    public Vector2 dir = Vector2.right;

    public float bolhaTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        //left
        if(Input.GetKeyDown(KeyCode.A))
        {
            if (dir != Vector2.left)
            {
                dir = Vector2.left;
                bombaQuantica.PlayerChangeDir();
            }
        }
        //rigth
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (dir != Vector2.right)
            {
                dir = Vector2.right;
                bombaQuantica.PlayerChangeDir();
            }
        }
        //up
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (dir != Vector2.up)
            {
                dir = Vector2.up;
                bombaQuantica.PlayerChangeDir();
            }
        }
        //down
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (dir != Vector2.down)
            {
                dir = Vector2.down;
                bombaQuantica.PlayerChangeDir();
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator BolhaTimer(GameObject bolha)
    {
        float timer = 0;
        while (timer < bolhaTime)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Destroy(bolha);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Giz":
                break;
            case "GizDourado":
                break;
            case "Bolha":
                collision.gameObject.transform.parent = transform;
                collision.gameObject.transform.localPosition = new Vector3(0, 0, -2);
                StartCoroutine(BolhaTimer(collision.gameObject));
                break;
            case "Freeze":
                bombaQuantica.Freeze();
                Destroy(collision.gameObject);
                break;
            case "Lupa":
                bombaQuantica.Predict();
                Destroy(collision.gameObject);
                break;
            case "Espinho":
                Die();
                break;
        }
    }
}
