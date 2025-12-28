using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Room_", menuName = "Scriptable Objects/Dungeon/Room")]
public class RoomTemplateSO : ScriptableObject
{
    [HideInInspector] public string guid;

    [Header("房间的游戏对象预制件,这将包含房间和环境游戏对象的所有瓷砖图")]
    public GameObject prefab;

    [HideInInspector] public GameObject previousPrefab; // this is used to regenerate the guid if the so is copied and the prefab is changed

    #region Header ROOM CONFIGURATION

    [Space(10)]
    [Header("ROOM CONFIGURATION")]

    #endregion Header ROOM CONFIGURATION

    #region Tooltip

    [Tooltip("房间节点类型SO。房间节点类型对应于房间节点图中使用的房间节点。走廊是个例外。在房间节点图中，只有一种走廊类型“corridor”。对于房间样板，有2种道路节点类型-CorridorNS和CorridorEW。")]

    #endregion Tooltip

    public RoomNodeTypeSO roomNodeType;

    [Header("局部坐标,房间预制体的左下角")]

    public Vector2Int lowerBounds;

    [Header("局部坐标,房间预制体的右上角")]

    public Vector2Int upperBounds;

    #region Tooltip

    [Tooltip("一个房间最多应该有四个门——每个指南针方向一个。这些瓷砖应具有一致的3块瓷砖开口尺寸，中间的瓷砖位置是门口坐标的位置'")]

    #endregion Tooltip

    [SerializeField] public List<Doorway> doorwayList;

    #region Tooltip

    [Tooltip("房间在tilemap坐标系中的每个可能的生成位置（用于敌人和箱子）都应该添加到此数组中")]

    #endregion Tooltip

    public Vector2Int[] spawnPositionArray;

    /// <summary>
    /// Returns the list of Entrances for the room template
    /// </summary>
    public List<Doorway> GetDoorwayList()
    {
        return doorwayList;
    }

    #region Validation

#if UNITY_EDITOR

    // Validate SO fields
    private void OnValidate()
    {
        // Set unique GUID if empty or the prefab changes
        if (guid == "" || previousPrefab != prefab)
        {
            guid = GUID.Generate().ToString();
            previousPrefab = prefab;
            EditorUtility.SetDirty(this);
        }

        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(doorwayList), doorwayList);

        // Check spawn positions populated
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(spawnPositionArray), spawnPositionArray);
    }

#endif

    #endregion Validation
}