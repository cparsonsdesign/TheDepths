using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour {

    CameraRaycaster camRay;

    [SerializeField]
    private Texture2D walkCursor = null;
    [SerializeField]
    private Texture2D attackCursor = null;
    [SerializeField]
    private Texture2D unknownCursor = null;
    [SerializeField]
    Vector2 cursorHotSpot = new Vector2(0, 0);

    [SerializeField]
    private const int walkableLayerNumber = 8;
    [SerializeField]
    private const int enemyLayerNumber = 9;
    [SerializeField]
    private const int unknownLayerNumber = 10;



    // Use this for initialization
    void Start ()
    {
        camRay = GetComponent<CameraRaycaster>();
        camRay.notifyLayerChangeObservers += OnLayerChange; // TODO consider de-registration when game ends or scene changes.
            
	}

    void OnLayerChange(int newlayer)
    {
        switch (newlayer)
        {
            case walkableLayerNumber:
                Cursor.SetCursor(walkCursor, cursorHotSpot, CursorMode.Auto);
                break;
            case enemyLayerNumber:
                Cursor.SetCursor(attackCursor, cursorHotSpot, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(unknownCursor, cursorHotSpot, CursorMode.Auto);
                return;
        }
    }
}
