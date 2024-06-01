namespace ExampleCore.ZookeperSemaforLogic;

public class TimeOutValue : IEquatable<TimeOutValue>, IComparable<TimeOutValue>
{
    private int InMilliseconds { get; }

    private bool IsInfinite => InMilliseconds == Timeout.Infinite;
    
    public TimeSpan TimeSpan => TimeSpan.FromMilliseconds(this.InMilliseconds);
    
    public TimeOutValue(TimeSpan? timeout, string paramName = "timeout")
    {
        if (timeout is { } timeoutValue)
        {
            var totalMilliseconds = (long)timeoutValue.TotalMilliseconds;
            if (totalMilliseconds < -1 || totalMilliseconds > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    paramName: paramName, 
                    actualValue: timeoutValue, 
                    message: $"Must be {nameof(Timeout)}.{nameof(Timeout.InfiniteTimeSpan)} ({Timeout.InfiniteTimeSpan}) or a non-negative value <= {TimeSpan.FromMilliseconds(int.MaxValue)})"
                );
            }

            InMilliseconds = (int)totalMilliseconds;
        }
        else
        {
            InMilliseconds = Timeout.Infinite;
        }
    }
    
    public bool Equals(TimeOutValue other)
    {
        return InMilliseconds == other.InMilliseconds;
    }

    public int CompareTo(TimeOutValue other)
    {
        if (IsInfinite)
            return other.IsInfinite ? 0 : 1;
        
        return other.IsInfinite
                ? -1
                : InMilliseconds.CompareTo(other.InMilliseconds);
    }
}