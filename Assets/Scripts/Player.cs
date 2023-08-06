using System;
using UnityEngine;
//using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    public PipeSystem PlayerPipeSystem;
    public float StartVelocity;
    public float[] Accelerations; 
    public float RotationVelocity;
    public MainMenu MainMenu;
    public HUD PlayerHUD;
    public Vector3 InputYZ;
    public Vector3 movementVelocity;

    private Pipe currentPipe;
    private float acceleration, velocity;
    private float distanceTravelled;
    private float deltaToRotation;
    private float systemRotation;
    private Transform world;
    private Transform rotator;
    private float worldRotation;
    private float avatarRotation;
    private float timer;
    private int[] range = { -1, 1 };
    private int rnd = 0;
    

    public void StartGame(int accelerationMode)
    {
        timer = 0f;
        distanceTravelled = 0f;
        avatarRotation = 0f;
        systemRotation = 0f;
        worldRotation = 0f;
        acceleration = Accelerations[accelerationMode];
        velocity = StartVelocity;
        currentPipe = PlayerPipeSystem.SetupFirstPipe();
        SetupCurrentPipe();
        gameObject.SetActive(true);
        PlayerHUD.SetValues(distanceTravelled);
    }

    public void Die()
    {
        MainMenu.EndGame(distanceTravelled);
        gameObject.SetActive(false);
    }

    private void Start()
    {
        world = PlayerPipeSystem.transform.parent;
        rotator = transform.GetChild(0);
        gameObject.SetActive(false);
    }

    private void SetupCurrentPipe()
    {
        deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
        worldRotation += currentPipe.RelativeRotation;

        if (worldRotation < 0f)
            worldRotation += 360f;
        else if (worldRotation >= 360f)
            worldRotation = 360f;

        world.localRotation = Quaternion.Euler(worldRotation, 0f, 0f);
    }

    private void Update()
    {
        velocity  += acceleration * Time.deltaTime;
        float delta = velocity * Time.deltaTime;
        distanceTravelled += delta;
        systemRotation += delta * deltaToRotation;

        if (systemRotation >= currentPipe.CurveAngle)
        {
            delta = (systemRotation - currentPipe.CurveAngle) / deltaToRotation;
            currentPipe = PlayerPipeSystem.SetupNextPipe();
            SetupCurrentPipe();
            systemRotation = delta * deltaToRotation; 
        }
        PlayerPipeSystem.transform.localRotation = Quaternion.Euler(0f, 0f, systemRotation);
        UpdateAvatarRotation();
        UpdateAvtarPosition();
        PlayerHUD.SetValues(distanceTravelled);
    }

    private void UpdateAvatarRotation()
    {
        if (timer > 5)
        {

            rnd = UnityEngine.Random.Range(0, 2);
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
            avatarRotation += RotationVelocity * Time.deltaTime * range[rnd];
            if (avatarRotation < 0f)
                avatarRotation += 360f;
            else if (avatarRotation >= 360f)
                avatarRotation -= 360f;
            world.localRotation = Quaternion.Euler(avatarRotation, 0f, 0f);
        }
    }

    private void UpdateAvtarPosition()
    {
        InputYZ = new Vector3(0f,Input.GetAxisRaw("Vertical"), -(Input.GetAxisRaw("Horizontal")));
        movementVelocity += 0.01f * InputYZ;

        if (movementVelocity.y < -0.2f)
        {
            movementVelocity.y = -0.2f;
        }
        else if(movementVelocity.y > 1f)
        {
            movementVelocity.y = 1f;
        }

        if (movementVelocity.z < -0.6f)
        {
                movementVelocity.z = -0.6f;
        }
        else if (movementVelocity.z > 0.6f)
        {
            movementVelocity.z = 0.6f;
        }
        gameObject.transform.localPosition = new Vector3(movementVelocity.x,movementVelocity.y, movementVelocity.z);
    }
}
