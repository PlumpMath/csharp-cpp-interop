using System;
using System.Xml;
using System.IO;
using System.Runtime.InteropServices;

class main
{
    [StructLayoutAttribute(LayoutKind.Explicit)]
    class managed
    {
        [FieldOffset(0)] public uint a;
        [FieldOffset(4)] public uint b;
        [FieldOffset(8)] public uint c;
    }

    unsafe public static void Main()
    {
        // foo_1
        {
            Console.WriteLine("call native: foo_1");
            foo_1();
            Console.WriteLine("================================");
        }

        // foo_2
        {
            Console.WriteLine("call native: foo_2");
            foo_2(123, "Hello World!");
            Console.WriteLine("================================");
        }

        // foo_3
        {
            Console.WriteLine("call native: foo_3");
            foo_3(new managed(){a=1,b=2,c=3});
            Console.WriteLine("================================");
        }

        // foo_4
        {
            Console.WriteLine("call native: foo_4");
            byte[] face = new byte[]
            {
                0x30,0x2e,0x30,
                0x20,0x2d,0x20,
            };

            IntPtr dataPtr = Marshal.AllocHGlobal(32);
            Marshal.Copy(face, 0, dataPtr, face.Length);

            foo_4(dataPtr, face.Length);

            Marshal.FreeHGlobal(dataPtr);
            Console.WriteLine("================================");
        }

        // foo_5
        {
            Console.WriteLine("call native: foo_5");
            IntPtr dataPtr;
            int length = 0;

            foo_5(out dataPtr, out length);

            byte[] face = new byte[length];
            Marshal.Copy(dataPtr, face, 0, length);

            for (int i=0; i<2; ++i)
            {
                for (int j=0; j<3; ++j)
                {
                    Console.Write((char)face[i*3+j]);
                    Console.Write(" ");
                }
                Console.Write("\n");
            }
            Console.WriteLine("================================");
        }

        // foo_6
        {
            Console.WriteLine("call native: foo_6");
            IntPtr dataPtr;
            int length = 0;

            foo_6(out dataPtr, out length);

            using (var stream = new UnmanagedMemoryStream((Byte*)dataPtr, length))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(stream);
                    Console.WriteLine(doc.InnerXml);
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine("================================");
        }

        int count = 1000000000;

        // empty
        {
            Console.WriteLine("call empty");
            DateTime t0 = DateTime.Now;
            for (int i=0; i<count; ++i)
            {

            }
            DateTime t1 = DateTime.Now;
            TimeSpan t = t1-t0;
            Console.WriteLine("count: " + count + ", time: " + t.TotalMilliseconds + " ms.");
            Console.WriteLine("================================");
        }

        // foo_7
        {
            Console.WriteLine("call native: foo_7");
            DateTime t0 = DateTime.Now;
            for (int i=0; i<count; ++i)
            {
                foo_7(i);
            }
            DateTime t1 = DateTime.Now;
            TimeSpan t = t1-t0;
            Console.WriteLine("count: " + count + ", time: " + t.TotalMilliseconds + " ms.");
            Console.WriteLine("================================");
        }

        // foo_8
        {
            Console.WriteLine("call native: foo_8");
            DateTime t0 = DateTime.Now;
            for (int i=0; i<count; ++i)
            {
                foo_8("Hello World!");
            }
            DateTime t1 = DateTime.Now;
            TimeSpan t = t1-t0;
            Console.WriteLine("count: " + count + ", time: " + t.TotalMilliseconds + " ms.");
            Console.WriteLine("================================");
        }

        // foo_9
        {
            Console.WriteLine("call native: foo_9");
            DateTime t0 = DateTime.Now;
            IntPtr dataPtr = Marshal.AllocHGlobal(32);
            int length = 32;
            for (int i=0; i<count; ++i)
            {
                foo_9(dataPtr, length);
            }
            DateTime t1 = DateTime.Now;
            TimeSpan t = t1-t0;
            Console.WriteLine("count: " + count + ", time: " + t.TotalMilliseconds + " ms.");
            Console.WriteLine("================================");
        }

        // foo_10
        {
            Console.WriteLine("call native: foo_10");
            DateTime t0 = DateTime.Now;
            IntPtr dataPtr;
            int length = 0;
            for (int i=0; i<count; ++i)
            {
                foo_10(out dataPtr, out length);

            }
            DateTime t1 = DateTime.Now;
            TimeSpan t = t1-t0;
            Console.WriteLine("count: " + count + ", time: " + t.TotalMilliseconds + " ms.");
            Console.WriteLine("================================");
        }
    }

    [DllImport("./native.so")]
    static extern int foo_1();

    [DllImport("./native.so")]
    static extern int foo_2(int a, string b);

    [DllImport("./native.so")]
    static extern int foo_3(managed st);

    [DllImport("./native.so")]
    static extern int foo_4(IntPtr dataPtr, int length);

    [DllImport("./native.so")]
    static extern int foo_5(out IntPtr dataPtr, out int length);

    [DllImport("./native.so")]
    static extern int foo_6(out IntPtr dataPtr, out int length);

    [DllImport("./native.so")]
    static extern int foo_7(int a);

    [DllImport("./native.so")]
    static extern int foo_8(string b);

    [DllImport("./native.so")]
    static extern int foo_9(IntPtr dataPtr, int length);

    [DllImport("./native.so")]
    static extern int foo_10(out IntPtr dataPtr, out int length);
}
