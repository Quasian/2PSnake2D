using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class snake : MonoBehaviour
{
    Vector2 direction;
    Vector2 rotation;
    public GameObject segment;
    List<GameObject> segments = new List<GameObject>();
    bool up, down,left, right;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI player1Score;
    public SnakeID snakeID;
    public int score;


    // Start is called before the first frame update
    void Start()
    {
        right = true; left = false; down = true; up = true;
        reset(); 
    }

    void reset()
    {
        transform.position = new Vector2(0, -1);
        transform.rotation = Quaternion.Euler(0, 0, 90);
        direction = Vector2.right;
        Time.timeScale = 0.05f;
        resetSegments();

    }

    void resetSegments()
    {
        for (int i = 1; i < segments.Count; i++)
        { Destroy(segments[i].gameObject); }

        segments.Clear();
        segments.Add(gameObject);

        for (int i = 0; i < 2; i++)
        { grow(); }
    
    }
   
    void grow()
    {
        score++;
        player1Score.text = " Score:" + score;
        GameObject newSegment = Instantiate(segment);
        newSegment.transform.position = segments[segments.Count - 1].transform.position;
        segments.Add(newSegment);
    
    }

    // Update is called once per frame
    void Update()
    {
        SnakeMovement();
    }

    private void SnakeMovement()
    {
        switch (snakeID)
        {
            case SnakeID.Snake1:
                {
                    snakecontrol();
                    break;
                }
            case SnakeID.Snake2:
                {
                    snakecontrol2();
                    break;
                
                }

        
        
        }
    }

    private void snakecontrol2()
    {
        if (Input.GetKeyDown(KeyCode.W) && down == true)
        {
            direction = Vector2.up;
            rotation = new Vector3(0, 0, 90);
            right = true; left = true; down = false; up = true;

        }
        else if (Input.GetKeyDown(KeyCode.S) && up == true)
        {
            direction = Vector2.down;
            rotation = new Vector3(0, 0, 270);
            right = true; left = true; down = true; up = false;

        }
        else if (Input.GetKeyDown(KeyCode.D) && left == true)
        {
            direction = Vector2.right;
            rotation = new Vector3(0, 0, 0);
            right = true; left = false; down = true; up = true;

        }
        else if (Input.GetKeyDown(KeyCode.A) && right == true)
        {
            direction = Vector2.left;
            rotation = new Vector3(0, 0, 180);
            right = false; left = true; down = true; up = true;

        }
    }

    void snakecontrol()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && down == true)
        {
            direction = Vector2.up;
            rotation = new Vector3(0, 0, 90);
            right = true; left = true; down = false; up = true;
            
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && up == true)
        {
            direction = Vector2.down;
            rotation = new Vector3(0, 0, 270);
            right = true; left = true; down = true; up = false;
          
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && left == true)
        {
            direction = Vector2.right;
            rotation = new Vector3(0, 0, 0);
            right = true; left = false; down = true; up = true;
            
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && right == true)
        {
            direction = Vector2.left;
            rotation = new Vector3(0, 0, 180);
            right = false; left = true; down = true; up = true;
            
        }
    }

    private void FixedUpdate()
    {
        moveSegments();
        movesnake();
    }


    void moveSegments()
    {
        for (int i = segments.Count - 1; i > 0; i--)
        { segments[i].transform.position = segments[i - 1].transform.position; }
    
    }

    void movesnake() 
    { 
        float x = transform.position.x + direction.x;
        float y = transform.position.y + direction.y;
        transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        { grow(); }
        if (collision.gameObject.tag == "obstacle")
        { screenwrap(); }

        if (collision.gameObject.CompareTag("Body"))
        {

            GameOver(gameObject.name);
        }
    }

    private void GameOver(string loosername)
    {
        string winner = null;
        
        if (loosername == "Snake1")
        {
            winner = "Snake2";
        }
        if (loosername == "Snake2")
        {
            winner = "Snake1";
        }
        winnerText.text = winner + " Won";
        Destroy(gameObject, 2f);
    }

    private void screenwrap()
    {
        Vector3 newPos = transform.position;
        if (newPos.x > 1 || newPos.x < 0)
        {
            newPos.x = -newPos.x;
            Debug.Log("right to left");
        }
        if (newPos.y > 1 || newPos.y < 0)
        {
            newPos.y = -newPos.y;
            Debug.Log("left to right");
        }
        transform.position = newPos;
    }
}

public enum SnakeID
{
    Snake1,
    Snake2
}
