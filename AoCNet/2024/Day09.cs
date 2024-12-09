using AdventOfCodeSupport;

namespace AoC._2024;

public class Day09 : AdventBase
{
    private static ulong CalculateChecksum(int[] disk)
    {
        ulong sum = 0;
        for (var i = 0; i < disk.Length; i++)
        {
            if (disk[i] != -1)
                sum += (ulong)(i * disk[i]);
        }

        return sum;
    }

    protected override object InternalPart1()
    {
        var arraySize = Input.Text.Select(c => int.Parse(c.ToString())).Sum();
        var disk = new int[arraySize];

        var id = 0;
        var cursor = 0;
        foreach (var (i, idx) in Input.Text.Select((i, idx) => (int.Parse(i.ToString()), idx)))
        {
            if (idx % 2 == 0)
            {
                for (var j = 0; j < i; j++)
                    disk[cursor + j] = id;
                id++;
            }
            else
            {
                for (var j = 0; j < i; j++)
                    disk[cursor + j] = -1;
            }

            cursor += i;
        }

        int readCursor = arraySize - 1, writeCursor = 0;

        while (disk[writeCursor] != -1)
            writeCursor++;

        while (readCursor > writeCursor)
        {
            if (disk[readCursor] != -1)
            {
                disk[writeCursor++] = disk[readCursor];

                while (disk[writeCursor] != -1)
                    writeCursor++;

                disk[readCursor] = -1;
            }

            readCursor--;
        }

        return CalculateChecksum(disk);
    }

    private List<(int Size, int Index)> GetEmptyBlocks(int[] disk)
    {
        var result = new List<(int Size, int Index)>();
        for (int i = 0, fileId = disk[0], size = 0, idx = 0; i < disk.Length; i++)
        {
            if (disk[i] == fileId)
                size++;
            else
            {
                if (fileId == -1)
                    result.Add((size, idx));

                fileId = disk[i];
                idx = i;
                size = 1;
            }
        }

        return result;
    }

    protected override object InternalPart2()
    {
        var arraySize = Input.Text.Select(c => int.Parse(c.ToString())).Sum();
        var disk = new int[arraySize];

        var id = 0;
        var cursor = 0;
        foreach (var (i, idx) in Input.Text.Select((i, idx) => (int.Parse(i.ToString()), idx)))
        {
            if (idx % 2 == 0)
            {
                for (var j = 0; j < i; j++)
                    disk[cursor + j] = id;
                id++;
            }
            else
            {
                for (var j = 0; j < i; j++)
                    disk[cursor + j] = -1;
            }

            cursor += i;
        }

        var files = new List<(int Id, int Size, int Index)>();
        var emptyBlocks = new List<(int Size, int Index)>();

        for (int i = 0, fileId = disk[0], size = 0, idx = 0; i < disk.Length; i++)
        {
            if (disk[i] == fileId)
                size++;
            else
            {
                if (fileId == -1)
                    emptyBlocks.Add((size, idx));
                else
                    files.Add((fileId, size, idx));

                fileId = disk[i];
                idx = i;
                size = 1;
            }

            if (i == disk.Length - 1)
                files.Add((fileId, size, idx));
        }

        emptyBlocks = emptyBlocks.OrderBy(b => b.Size).ThenBy(b => b.Index).ToList();

        foreach (var file in files.OrderByDescending(f => f.Id))
        {
            var availableBlock = emptyBlocks.FirstOrDefault(b => b.Size >= file.Size);
            
            if (availableBlock == default)
                continue;

            if (availableBlock.Index > file.Index)
                continue;

            for (var i = 0; i < file.Size; i++)
            {
                disk[availableBlock.Index + i] = file.Id;
                disk[file.Index + i] = -1;
            }

            emptyBlocks = GetEmptyBlocks(disk);
        }

        return CalculateChecksum(disk);
    }
}