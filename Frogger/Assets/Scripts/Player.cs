using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action OnPlayerGoal;
    public Action OnPlayerDeath;
    
    [SerializeField]
    private float hopDistance = 1;
    
    [SerializeField]
    private float smoothness = 25;

    [SerializeField]
    private WallBounds wallBounds;

    [SerializeField] 
    private GameObject particlePrefab;

    private bool isMoving;
    private Transform currentParent;

    private void Update()
    {
        HandlePlayerInput();
        HandleOutOfBounds();
        DetectGround();
    }

    private void HandlePlayerInput()
    {
        var up = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        var down = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
        var left = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        var right = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        
        if (up)
        {
            StartCoroutine(MovePlayer(hopDistance, false));
        } 
        else if (down)
        {
            StartCoroutine(MovePlayer(-hopDistance, false));
        }
        else if (left)
        {
            StartCoroutine(MovePlayer(-hopDistance));
        } 
        else if (right)
        {
            StartCoroutine(MovePlayer(hopDistance));
        }
    }

    void HandleOutOfBounds()
    {
        var playerTf = transform;
        var playerPos = playerTf.position;
        var offset = 0.75f;
        if (playerPos.x < wallBounds.left - offset || playerPos.x > wallBounds.right + offset)
        {
            OnPlayerDeath?.Invoke();
        }
    }

    IEnumerator MovePlayer(float step, bool isXAxis = true)
    {
        var playerTf = transform;
        var playerPos = (Vector2) playerTf.position;
        var x = isXAxis ? playerPos.x + step : playerPos.x;
        var y = !isXAxis ? playerPos.y + step : playerPos.y;
        var direction = GetDirection(step, isXAxis);
        var targetPosition = GetTargetPosition(x, y, direction);
        
        yield return StartCoroutine(MoveToTile(targetPosition));
    }

    void DetectGround()
    {
        var playerTf = transform;
        Ray ray = new Ray(playerTf.position, playerTf.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.DrawLine(playerTf.position, hitInfo.point, Color.green);
            var groundTag = hitInfo.transform.tag;
            switch (groundTag)
            {
                case "Log":
                case "Lily Pad":
                    SetPlayerParent(hitInfo.transform);
                    break;
                case "Water":
                case "Grass":
                    // Kill player
                    OnPlayerDeath?.Invoke();
                    Destroy(gameObject);
                    break;
                default:
                    SetPlayerParent(null);
                    break;
            }
        }
    }

    Vector2 GetTargetPosition(float x, float y, string direction)
    {
        switch (direction)
        {
            case "up":
                if (y > wallBounds.top)
                {
                    return new Vector2(x, wallBounds.top);
                }
                break; 
            case "down":
                if (y < wallBounds.bottom)
                {
                    return new Vector2(x, wallBounds.bottom);
                }
                break;
            case "left":
                if (x < wallBounds.left)
                {
                    return new Vector2(wallBounds.left, y);
                }
                break;
            case "right":
                if (x > wallBounds.right)
                {
                    return new Vector2(wallBounds.right, y);
                }
                break;
        }
        return new Vector2(x, y);
    }

    string GetDirection(float step, bool isXAxis)
    {
        var isUp = step > 0 && !isXAxis;
        var isDown = step < 0 && !isXAxis;
        var isLeft = step < 0 && isXAxis;

        if (isUp)
        {
            return "up";
        }
        if (isDown)
        {
            return "down";
        }
        if (isLeft)
        {
            return "left";
        }
        return "right";
    }

    IEnumerator MoveToTile(Vector2 tilePos)
    {
        if (isMoving)
        {
            yield break;
        }

        isMoving = true;
        
        var playerTf = transform;
        while (Vector2.Distance(playerTf.position, tilePos) > 0.05f)
        {
            playerTf.position = Vector2.MoveTowards(playerTf.position, tilePos, smoothness * Time.deltaTime);
            yield return null;
        }
        playerTf.position = tilePos;
        isMoving = false;
    }

    private void HandleGoal(Transform goalTf)
    {
        var goal = goalTf.GetComponent<Goal>();
       
        if (goal.IsFilled)
        {
            OnPlayerDeath?.Invoke();
        }
        else
        {
            goal.IsFilled = true;
            OnPlayerGoal?.Invoke();
        }
        Destroy(gameObject);
    }

    private void SetPlayerParent(Transform newParent)
    {
        if (currentParent == newParent)
        {
            return;
        }
        currentParent = newParent;
        transform.parent = newParent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            // Kill Player
            OnPlayerDeath?.Invoke();
            Destroy(gameObject);
        }

        if (other.CompareTag("Goal"))
        {
            HandleGoal(other.transform);
        }
    }

    private void OnDestroy()
    {
        if(!gameObject.scene.isLoaded) return;
        var playerPos = transform.position;
        var bloodPosition = playerPos + (Vector3.forward * -1);
        Instantiate(particlePrefab, bloodPosition, Quaternion.identity);
    }
}
