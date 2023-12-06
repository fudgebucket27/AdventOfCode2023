using System.Collections.Concurrent;

class LongRangePartitioner : Partitioner<Tuple<long, long>>
{
    private long _from, _to, _rangeSize;

    public LongRangePartitioner(long from, long to, long rangeSize)
    {
        _from = from;
        _to = to;
        _rangeSize = rangeSize;
    }

    public override bool SupportsDynamicPartitions => true;

    public override IList<IEnumerator<Tuple<long, long>>> GetPartitions(int partitionCount)
    {
        var dynamicPartitions = GetDynamicPartitions();
        var partitions = new List<IEnumerator<Tuple<long, long>>>(partitionCount);

        for (int i = 0; i < partitionCount; i++)
        {
            partitions.Add(dynamicPartitions.GetEnumerator());
        }

        return partitions;
    }

    public override IEnumerable<Tuple<long, long>> GetDynamicPartitions()
    {
        return PartitionEnumerator();
    }

    private IEnumerable<Tuple<long, long>> PartitionEnumerator()
    {
        long nextItem = _from;
        while (nextItem < _to)
        {
            long rangeEnd = Math.Min(nextItem + _rangeSize, _to);
            yield return Tuple.Create(nextItem, rangeEnd);
            nextItem = rangeEnd;
        }
    }
}
