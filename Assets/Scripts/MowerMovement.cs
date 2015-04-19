using UnityEngine;
using System.Collections;

public class MowerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float speedLevelMod;
    public float turningRate;
    [HideInInspector]
    public Vector3 destination;

    Vector3 direction;
    float speed;
    Gardener gardener;
    AudioSource audioSource;

    void Start()
    {
        gardener = GameObject.Find("Garden").GetComponent<Gardener>();
        audioSource = GetComponent<AudioSource>();
        destination = transform.position;
        destination.y = 0;
    }

    void FixedUpdate()
    {
        if (destination.x == transform.position.x && destination.z == transform.position.z) {
            float x = Mathf.Round(transform.position.x / (gardener.tileSize * gardener.voxelSize)) * gardener.tileSize * gardener.voxelSize;
            x = x + gardener.tileSize * gardener.voxelSize * Input.GetAxisRaw("Horizontal");
            float z = Mathf.Round(transform.position.z / (gardener.tileSize * gardener.voxelSize)) * gardener.tileSize * gardener.voxelSize;
            z = z + gardener.tileSize * gardener.voxelSize * Input.GetAxisRaw("Vertical");
            if (Input.GetAxisRaw("Horizontal") != 0 && x >= 0 && x < gardener.voxelSize * gardener.tileSize * (gardener.gardenSize.x - 1)) {
                destination.x = x;
                UpdateMovement();
            } else if (Input.GetAxisRaw("Vertical") != 0 && z >= 0 && z < gardener.voxelSize * gardener.tileSize * (gardener.gardenSize.y - 1)) {
                destination.z = z;
                UpdateMovement();
            } else {
                audioSource.clip = Resources.Load("SFX_MowerStand") as AudioClip;
            }
        } else {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.fixedDeltaTime * speed);
        }
        float y = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, y, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turningRate * Time.fixedDeltaTime);
        if (audioSource.isPlaying == false) {
            audioSource.Play();
        }
    }

    void UpdateMovement()
    {
        audioSource.clip = Resources.Load("SFX_MowerMovement") as AudioClip;
        direction = destination - transform.position;
        speed = moveSpeed;
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Grass");
        foreach (GameObject grass in tiles) {
            if (destination.x >= grass.transform.position.x && destination.x < grass.transform.position.x + gardener.voxelSize) {
                if (destination.z >= grass.transform.position.z && destination.z < grass.transform.position.z + gardener.voxelSize) {
                    speed = moveSpeed - speedLevelMod * grass.GetComponent<Grass>().Level;
                }
            }
        }
        Mow(tiles);
    }

    void Mow(GameObject[] tiles)
    {
        float x = destination.x - Mathf.Floor(gardener.tileSize / 2f) * gardener.voxelSize;
        float z = destination.z - Mathf.Floor(gardener.tileSize / 2f) * gardener.voxelSize;
        float l = gardener.tileSize * gardener.voxelSize;
        bool mowed = false;
        GameObject[] moles = GameObject.FindGameObjectsWithTag("Mole");
        foreach (GameObject mole in moles) {
            if (mole.transform.position.x >= x && mole.transform.position.x < x + l) {
                if (mole.transform.position.z >= z && mole.transform.position.z < z + l) {
                    if (mole.transform.position.y <= 0.5f) {
                        if (mole.GetComponent<Mole>() != null) {
                            Vector3 pos = mole.transform.position;
                            pos.y = 0.3f;
                            gardener.killCount += 1;
                            Instantiate(Resources.Load("Blood"), pos, Quaternion.identity);
                        } else {
                            gardener.sackCount += 1;
                            gardener.molehillCount -= 1;
                        }
                        Destroy(mole);
                        mowed = true;
                    }
                }
            }
        }
        if (mowed == false) {
            foreach (GameObject grass in tiles) {
                if (grass.transform.position.x >= x && grass.transform.position.x < x + l) {
                    if (grass.transform.position.z >= z && grass.transform.position.z < z + l) {
                        if (grass.GetComponent<Grass>().Level == 4) {
                            gardener.bushCount += 1;
                        } else if (grass.GetComponent<Grass>().Level == 3) {
                            gardener.bushCount -= 1;
                            gardener.golfCount += 1;
                        } else if (grass.GetComponent<Grass>().Level == 2) {
                            gardener.golfCount -= 1;
                        } else if (grass.GetComponent<Grass>().Level == 1) {
                            gardener.clayCount += 1;
                        }
                        grass.GetComponent<Grass>().Level = grass.GetComponent<Grass>().Level - 1;
                    }
                }
            }
        }
    }
}
