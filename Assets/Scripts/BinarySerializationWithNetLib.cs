using NetLib.Extensions;
using UnityEngine;

public class BinarySerializationWithNetLib : MonoBehaviour
{
    private void Start()
    {
        var playerData = new PlayerData
        {
            health = 50,
            shield = 100,
            name = "Bardock",
            position = new Vector3(1, 2, 3)
        };
        Debug.Log($"Original: {playerData}");

        // Making a copy of the data. Struct.
        byte[] bytes = playerData.ToArray();
        var copy = bytes.ToStruct<PlayerData>();
        Debug.Log($"Copy: {copy}");

        // Making a copy of the data and serialize it to JSON and back again.
        byte[] jsonBytes = playerData.ToJsonBinary();
        var jsonCopy = jsonBytes.FromJsonBinary<PlayerData>();
        Debug.Log($"JSON Copy: {jsonCopy}");
    }
}