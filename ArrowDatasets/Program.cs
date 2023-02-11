using System.Runtime.InteropServices;

public class Program
{

    // Import user32.dll (containing the function we need) and define
    // the method corresponding to the native function.
    [DllImport("arrow_dataset")]
    private static extern void arrow_dataset_initialize();
    private delegate void P_release_schema(ref ArrowSchema schema);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    private struct ArrowSchema
    {
        public IntPtr format;
        public IntPtr name;
        public IntPtr metadata;
        public long flags;
        public long n_children;
        public IntPtr children;
        public IntPtr dictionary;
        public P_release_schema release;
        public IntPtr private_data;
    }

    private delegate void P_release_array(ref ArrowArray array);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    private struct ArrowArray
    {
        // Array data description
        public long length;
        public long null_count;
        public long offset;
        public long n_buffers;
        public long n_children;
        public IntPtr buffers;
        public IntPtr children;
        public IntPtr dictionary;
        public P_release_array release;
        public IntPtr private_data;
    }

    private delegate int P_get_schema(ref ArrowArrayStream stream, out ArrowSchema outSchema);
    private delegate int P_get_next(ref ArrowArrayStream stream, out ArrowArray outNext);
    private delegate string P_get_last_error(ref ArrowArrayStream stream);
    private delegate void P_stream_release(ref ArrowArrayStream stream);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    private struct ArrowArrayStream
    {
        public P_get_schema get_schema;
        public P_get_next get_next;
        public P_get_last_error get_last_error;
        public P_stream_release release;
        public IntPtr private_data;
    }


    [DllImport("arrow_substrait")]
    private static extern ArrowArrayStream arrow_substrait_run(
        byte[] plan, long planLength, IntPtr namedTables, int numNamedTables);


    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Hello world!");
            arrow_dataset_initialize();
            byte[] plan = System.IO.File.ReadAllBytes("/tmp/plan.proto");
            ArrowArrayStream stream = arrow_substrait_run(plan, plan.Length, IntPtr.Zero, 0);
            ArrowSchema schema;
            Console.WriteLine("Loading schema");
            int err = stream.get_schema(ref stream, out schema);
            if (err == 0)
            {
                Console.WriteLine($"format={Marshal.PtrToStringAnsi(schema.format)} name={Marshal.PtrToStringAnsi(schema.name)}");
            }
            else
            {
                Console.WriteLine("Error Code=" + err);
                Console.WriteLine(stream.get_last_error(ref stream));
            }
            Console.WriteLine("Goodbye world!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

}
