namespace _3makingfriends;

public class Scanner
{
    private Stream stream;
    private byte[] buffer = new byte[1024 * 16];
    private int head, tail;

    public Scanner(Stream stream)
    {
        this.stream = stream;
    }

    public int NextInt()
    {
        int c = Read();
        while (c <= ' ') c = Read();

        int res = 0;
        while (c > ' ')
        {
            res = res * 10 + c - '0';
            c = Read();
        }
        return res;
    }

    private int Read()
    {
        if (head >= tail)
        {
            head = 0;
            tail = stream.Read(buffer, 0, buffer.Length);
            if (tail == 0) return -1;
        }
        return buffer[head++];
    }
}
