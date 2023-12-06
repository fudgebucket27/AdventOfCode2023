class Range
{
    public long Start { get; }
    public long End { get; }
    public long DestinationStart { get; }

    public Range(long start, long end, long destinationStart)
    {
        Start = start;
        End = end;
        DestinationStart = destinationStart;
    }

    public bool Contains(long value) => value >= Start && value <= End;
}

class RangeComparer : IComparer<Range>
{
    public int Compare(Range x, Range y)
    {
        if (x.Start > y.End)
            return 1;
        if (y.Start > x.End)
            return -1;
        return 0;
    }
}
