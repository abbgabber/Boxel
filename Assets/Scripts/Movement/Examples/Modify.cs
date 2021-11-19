using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modify : MonoBehaviour
{
    Vector2 rot;
    public GameObject pauseMenu;
    public GameObject crosshair;
    static bool paused = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!paused)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position,transform.forward,out hit, 100))
                {
                    // Check if it hit a tree
                    if (hit.collider.CompareTag("Tree"))
                    {
                        Chunk chunk = hit.collider.transform.parent.GetComponent<Chunk>();
                        chunk.destroyTree(hit);
                    }

                    EditTerrain.SetBlock(hit, new BlockAir());
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
                {
                    // Check so it didnt hit a tree
                    if (!hit.collider.CompareTag("Tree"))
                    {
                        EditTerrain.SetBlock(hit, new Block(), true);
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(pauseMenu, crosshair);
        }
    }

    private void FixedUpdate()
    {
        if (!paused)
        {
            rot = new Vector2(
                rot.x + Input.GetAxis("Mouse X") * 3,
                rot.y + Input.GetAxis("Mouse Y") * 3
                );
            transform.localRotation = Quaternion.AngleAxis(rot.x, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(rot.y, Vector3.left);

            transform.position += transform.forward * 3 * Input.GetAxis("Vertical");
            transform.position += transform.right * 3 * Input.GetAxis("Horizontal");
        }
    }

    public static void PauseGame(GameObject pauseMenu, GameObject crosshair)
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (pauseMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            paused = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            paused = false;
        }
        crosshair.SetActive(!paused);
    }
}
