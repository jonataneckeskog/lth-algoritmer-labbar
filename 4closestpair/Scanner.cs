namespace _4closestpair;

public class Scanner
{
    private Stream stream;
    private byte[] buffer = new byte[1024 * 16];
    private int head, tail;

    public Scanner(Stream stream)
    {
        this.stream = stream;
    }

    public IEnumerable<int> AsEnumerable()
    {
        while (true)
        {
            int val = NextInt();
            yield return val;
        }
    }

    public int NextInt()
    {
        int c = Read();

        while (c != -1 && c <= ' ') c = Read();
        if (c == -1) return 0;

        bool isNegative = false;
        if (c == '-')
        {
            isNegative = true;
            c = Read();
        }

        int res = 0;
        while (c >= '0' && c <= '9')
        {
            res = res * 10 + (c - '0');
            c = Read();
        }

        return isNegative ? -res : res;
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
