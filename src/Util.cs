using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace SleuthKit
{
    /// <summary>
    /// Marshals unmanaged UTF8 string to and from System.String.
    /// </summary>
    internal class UTF8Marshaler : ICustomMarshaler
    {
        static UTF8Marshaler marshaler = new UTF8Marshaler();

        private Hashtable allocated = new Hashtable();

        public static ICustomMarshaler GetInstance(string cookie)
        {
            return marshaler;
        }

        public void CleanUpManagedData(object ManagedObj)
        {
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
            /* This is a hack not to crash on mono!!! */
            if (allocated.Contains(pNativeData))
            {
                Marshal.FreeHGlobal(pNativeData);
                allocated.Remove(pNativeData);
            }
            else
            {
                Console.WriteLine("WARNING: Trying to free an unallocated pointer!");
                Console.WriteLine("         This is most likely a bug in mono");
            }
        }

        public int GetNativeDataSize()
        {
            return -1;
        }

        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            if (ManagedObj == null)
                return IntPtr.Zero;
            if (ManagedObj.GetType() != typeof(string))
                throw new ArgumentException("ManagedObj", "Can only marshal type of System.string");

            //byte[] array = Encoding.UTF8.GetBytes((string)ManagedObj);
            byte[] array = new UTF8Encoding(true).GetBytes((string)ManagedObj);
            
            int size = Marshal.SizeOf(typeof(byte)) * (array.Length + 1);

            IntPtr ptr = Marshal.AllocHGlobal(size);

            /* This is a hack not to crash on mono!!! */
            allocated.Add(ptr, null);

            Marshal.Copy(array, 0, ptr, array.Length);
            Marshal.WriteByte(ptr, array.Length, 0);

            return ptr;
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            if (pNativeData == IntPtr.Zero)
                return null;

            int size = 0;
            while (Marshal.ReadByte(pNativeData, size) > 0)
                size++;

            byte[] array = new byte[size];
            Marshal.Copy(pNativeData, array, 0, size);

            return Encoding.UTF8.GetString(array);
        }
    }

    /// <summary>
    /// Useful for printing out objects.
    /// </summary>
    internal class ObjectDumper
    {
        public static void Write(object element)
        {
            Write(element, 0);
        }

        public static void Write(object element, int depth)
        {
            Write(element, depth, Console.Out);
        }

        public static void Write(object element, int depth, TextWriter log)
        {
            ObjectDumper dumper = new ObjectDumper(depth);
            dumper.writer = log;
            dumper.WriteObject(null, element);
        }

        TextWriter writer;
        int pos;
        int level;
        int depth;

        private ObjectDumper(int depth)
        {
            this.depth = depth;
        }

        private void Write(string s)
        {
            if (s != null)
            {
                writer.Write(s);
                pos += s.Length;
            }
        }

        private void WriteIndent()
        {
            for (int i = 0; i < level; i++) writer.Write("\t");
        }

        private void WriteLine()
        {
            writer.WriteLine();
            pos = 0;
        }

        private void WriteTab()
        {
            Write("  ");
            while (pos % 8 != 0) Write(" ");
        }

        private void WriteObject(string prefix, object element)
        {
            if (element == null || element is ValueType || element is string)
            {
                WriteIndent();
                Write(prefix);
                WriteValue(element);
                WriteLine();
            }
            else
            {
                IEnumerable enumerableElement = element as IEnumerable;
                if (enumerableElement != null)
                {
                    foreach (object item in enumerableElement)
                    {
                        if (item is IEnumerable && !(item is string))
                        {
                            WriteIndent();
                            Write(prefix);
                            Write("...");
                            WriteLine();
                            if (level < depth)
                            {
                                level++;
                                WriteObject(prefix, item);
                                level--;
                            }
                        }
                        else
                        {
                            WriteObject(prefix, item);
                        }
                    }
                }
                else
                {
                    MemberInfo[] members = element.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);
                    WriteIndent();
                    Write(prefix);
                    bool propWritten = false;
                    foreach (MemberInfo m in members)
                    {
                        FieldInfo f = m as FieldInfo;
                        PropertyInfo p = m as PropertyInfo;
                        if (f != null || p != null)
                        {
                            if (propWritten)
                            {
                                WriteTab();
                            }
                            else
                            {
                                propWritten = true;
                            }
                            Write(m.Name);
                            Write("=");
                            Type t = f != null ? f.FieldType : p.PropertyType;
                            if (t.IsValueType || t == typeof(string))
                            {
                                WriteValue(f != null ? f.GetValue(element) : p.GetValue(element, null));
                            }
                            else
                            {
                                if (typeof(IEnumerable).IsAssignableFrom(t))
                                {
                                    Write("...");
                                }
                                else
                                {
                                    Write("{ }");
                                }
                            }
                        }
                    }
                    if (propWritten) WriteLine();
                    if (level < depth)
                    {
                        foreach (MemberInfo m in members)
                        {
                            FieldInfo f = m as FieldInfo;
                            PropertyInfo p = m as PropertyInfo;
                            if (f != null || p != null)
                            {
                                Type t = f != null ? f.FieldType : p.PropertyType;
                                if (!(t.IsValueType || t == typeof(string)))
                                {
                                    object value = f != null ? f.GetValue(element) : p.GetValue(element, null);
                                    if (value != null)
                                    {
                                        level++;
                                        WriteObject(m.Name + ": ", value);
                                        level--;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void WriteValue(object o)
        {
            if (o == null)
            {
                Write("null");
            }
            else if (o is DateTime)
            {
                Write(((DateTime)o).ToShortDateString());
            }
            else if (o is ValueType || o is string)
            {
                Write(o.ToString());
            }
            else if (o is IEnumerable)
            {
                Write("...");
            }
            else
            {
                Write("{ }");
            }
        }
    }

    /// <summary>
    /// Helpful methods for IntPtr
    /// </summary>
    static class IntPtrExtensions
    {
        /// <summary>
        /// Advances position of the pointer
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="cbSize"></param>
        /// <returns></returns>
        public static IntPtr Increment(this IntPtr ptr, int cbSize)
        {
            return new IntPtr(ptr.ToInt64() + cbSize);
        }

        /// <summary>
        /// Increments by the size of the structure or object.  Calls <c>Marshal.SizeOf(typeof(T))</c> on what you pass in.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ptr"></param>
        /// <returns></returns>
        public static IntPtr Increment<T>(this IntPtr ptr)
        {
            return ptr.Increment(Marshal.SizeOf(typeof(T)));
        }

        /// <summary>
        /// Treats the pointer like the start of an array, and allows you to fetch item at the specified index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ptr"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T ElementAt<T>(this IntPtr ptr, int index)
        {
            var offset = Marshal.SizeOf(typeof(T)) * index;
            var offsetPtr = ptr.Increment(offset);
            return (T)Marshal.PtrToStructure(offsetPtr, typeof(T));
        }
    }
}
