using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    private GameControls gameControls;

    //private float inputMovement;

    private float lastFall = 0f;

    private void OnEnable()
    {
        gameControls = new GameControls();

        gameControls.Default.Horizontal.performed += e => PressHorizontal(e.ReadValue<float>());

        gameControls.Default.Down.performed += e => PressDown();

        gameControls.Default.Rotate.performed += e => PressRotate();

        gameControls.Enable();

    }

    private void OnDisable()
    {
        gameControls.Default.Horizontal.performed -= e => PressHorizontal(e.ReadValue<float>());

        gameControls.Default.Down.performed -= e => PressDown();

        gameControls.Default.Rotate.performed -= e => PressRotate();

        gameControls.Disable();
    }

    private void Start()
    {
        // Default position not valid? Then it's game over
        if (!isValidGridPos())
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //MoveHorizontal();

        FallOverTime();

    }

    private void PressHorizontal(float inputMovement)
    {
        MoveHorizontal(inputMovement);
    }
    private void MoveHorizontal(float inputMovement)
    {
        // Pressing left or right
        if (inputMovement != 0)
        {
            int _inputMovement = System.Math.Sign(inputMovement);
            
            Transform beforeTransform = transform;

            transform.position += new Vector3(_inputMovement, 0, 0);

            if (isValidGridPos())
            {
                updateGrid();
            }
            else
            {
                transform.position -= new Vector3(_inputMovement, 0, 0);
            }
        }
    }

    private void FallOverTime()
    {
        if (Time.time - lastFall >= 1)
        {
            lastFall = Time.time;
            MoveDown();
        }
    }

    private void PressRotate()
    {
        transform.Rotate(0, 0, -90);

        // See if valid
        if (isValidGridPos())
        {
            // It's valid. Update grid.
            updateGrid();
        }
        else
        {
            // It's not valid. revert.
            transform.Rotate(0, 0, 90);
        }
    }

    private void PressDown()
    {
        MoveDown();
    }

    private void MoveDown()
    {
        // Modify position
        transform.position += new Vector3(0, -1, 0);

        // See if valid
        if (isValidGridPos())
        {
            // It's valid. Update grid.
            updateGrid();
        }
        else
        {
            // It's not valid. revert.
            transform.position += new Vector3(0, 1, 0);

            Land();
        }
    }

    private void Land()
    {
        // Clear filled horizontal lines
        Playfield.deleteFullRows();

        // Spawn next Group
        FindObjectOfType<Spawner>().SpawnNext();

        // Disable script
        this.enabled = false;
    }

    private bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);

            // Not inside Border?
            if (!Playfield.insideBorder(v))
            {
                return false;
            }
            // Block in grid cell (and not part of same group)?
            if (Playfield.grid[(int)v.x, (int)v.y] != null && Playfield.grid[(int)v.x, (int)v.y].parent != transform)
            {
                return false;
            }
        }
        return true;
    }

    private void updateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < Playfield.h; ++y)
        {
            for (int x = 0; x < Playfield.w; ++x)
            {
                if (Playfield.grid[x, y] != null)
                {
                    if (Playfield.grid[x, y].parent == transform)
                    {
                        Playfield.grid[x, y] = null;
                    }
                }
            }
        }

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = child;
        }
    }

}
