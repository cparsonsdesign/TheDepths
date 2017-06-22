using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

[RequireComponent(typeof(ThirdPersonCharacter))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AICharacterControl))]
public class PlayerMovement : MonoBehaviour
{
    private ThirdPersonCharacter thirdPersonCharacter = null;   // A reference to the ThirdPersonCharacter on the object
    private AICharacterControl aiControlCharacter = null;
    private CameraRaycaster cameraRaycaster = null;
    private Vector3 currentDestination, clickPoint;
    private GameObject walkTarget;
    NavMeshAgent agent = null;
    [SerializeField]
    private float walkStoppingDistance = 0f;
    [SerializeField]
    private float attackStoppingDistance = 0f;

    private bool isInDirectMode;

    [SerializeField]
    private const int walkableLayerNumber = 8;
    [SerializeField]
    private const int enemyLayerNumber = 9;
    [SerializeField]
    private const int unknownLayerNumber = 10;


    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        aiControlCharacter = GetComponent<AICharacterControl>();
        currentDestination = transform.position;
        agent = GetComponent<NavMeshAgent>();
        walkTarget = new GameObject("walkTarget");

        cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;

    }


    #region (Movement Functions)

    void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
    {
        //print("click!");

        switch (layerHit)
        {
            case walkableLayerNumber:
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if( Physics.Raycast(ray, out hit, 100))
                {
                    aiControlCharacter.SetTarget(null);
                    agent.stoppingDistance = walkStoppingDistance;
                    agent.destination = hit.point;
                }

                break;
            case enemyLayerNumber:
                agent.stoppingDistance = attackStoppingDistance;
                GameObject enemy = raycastHit.collider.gameObject;
                aiControlCharacter.SetTarget(enemy.transform);
                break;
            default:
                Debug.LogWarning("Dont know how to handle this!");
                return;
        }
    }

    private void ProcessDirectMovement()
    {
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate camera relative direction to move:
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

        thirdPersonCharacter.Move(movement, false, false);
    }
    #endregion
}

