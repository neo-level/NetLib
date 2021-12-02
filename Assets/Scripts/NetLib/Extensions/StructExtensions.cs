using System.Runtime.InteropServices;

namespace NetLib.Extensions
{
    public static class StructExtensions
    {
        public static T ToStruct<T>(this byte[] data) where T : struct
        {
            var size = Marshal.SizeOf(typeof(T));
            var pointer = Marshal.AllocHGlobal(size);
            int startIndex = 0;
            Marshal.Copy(data, startIndex, pointer, size);

            var copyData = (T) Marshal.PtrToStructure(pointer, typeof(T));
            Marshal.FreeHGlobal(pointer);

            return copyData;
        }


        public static byte[] ToArray(this object data)
        {
            var size = Marshal.SizeOf(data);
            byte[] buffer = new byte[size];
            var pointer = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(data, pointer, true);

            int startIndex = 0;
            Marshal.Copy(pointer, buffer, startIndex, size);
            Marshal.FreeHGlobal(pointer);

            return buffer;
        }
    }
}