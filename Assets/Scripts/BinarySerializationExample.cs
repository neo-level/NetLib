using System.Runtime.InteropServices;
using UnityEngine;

public class BinarySerializationExample : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var playerData = new PlayerData
        {
            health = 50,
            shield = 100,
            name = "Red Barbarian",
            position = new Vector3(1, 2, 3)
        };

        Debug.Log($"Original: {playerData}");
        byte[] bytes = ToBytes(playerData);
        var copiedData = ToObject<PlayerData>(bytes);
        Debug.Log($"Copy: {copiedData}");
    }

    /// <summary>
    /// Deserialize an array of bytes and return an
    /// instance of object type T with the serialized data.
    /// </summary>
    /// <param name="data">Array of bytes containing serialized data</param>
    /// <typeparam name="T">Class or Struct type to be created</typeparam>
    /// <returns>An instance of object type T</returns>
    private T ToObject<T>(byte[] data)
    {
        // Creates an area of memory to store the byte array and
        // then copies it to memory.
        var size = Marshal.SizeOf(typeof(T));
        var pointer = Marshal.AllocHGlobal(size);

        int starIndex = 0;
        Marshal.Copy(data, starIndex, pointer, size);

        // Using the PtrToStructure method, copy the bytes out
        // into the Message Structure.
        var copyData = (T) Marshal.PtrToStructure(pointer, typeof(T));
        Marshal.FreeHGlobal(pointer);
        return copyData;
    }

    /// <summary>
    /// Serialize an object to an array of bytes.
    /// </summary>
    /// <param name="data">The object to be serialized</param>
    /// <returns>The serialized object as an array of bytes</returns>
    private byte[] ToBytes(object data)
    {
        // Creates a pointer in memory and allocates the size of the structure
        var size = Marshal.SizeOf(data);
        byte[] buffer = new byte[size];
        var pointer = Marshal.AllocHGlobal(size);

        // Copies the structure to the newly created memory space
        // and then copies it into the byte buffer.
        Marshal.StructureToPtr(data, pointer, true);
        
        int starIndex = 0;
        Marshal.Copy(pointer, buffer, starIndex, size);
        // Free up pointer.
        Marshal.FreeHGlobal(pointer);

        return buffer;
    }
}