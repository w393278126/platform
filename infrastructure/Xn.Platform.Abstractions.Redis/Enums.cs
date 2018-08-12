using System;

namespace Xn.Platform.Abstractions.Redis
{
    //
    // Summary:
    //     Behaviour markers associated with a given command
    [Flags]
    public enum CommandFlags
    {
        //
        // Summary:
        //     Default behaviour.
        None = 0,
        //
        // Summary:
        //     This operation should be performed on the master if it is available, but read
        //     operations may be performed on a slave if no master is available. This is the
        //     default option.
        PreferMaster = 0,
        //
        // Summary:
        //     This command may jump regular-priority commands that have not yet been written
        //     to the redis stream.
        HighPriority = 1,
        //
        // Summary:
        //     The caller is not interested in the result; the caller will immediately receive
        //     a default-value of the expected return type (this value is not indicative of
        //     anything at the server).
        FireAndForget = 2,
        //
        // Summary:
        //     This operation should only be performed on the master.
        DemandMaster = 4,
        //
        // Summary:
        //     This operation should be performed on the slave if it is available, but will
        //     be performed on a master if no slaves are available. Suitable for read operations
        //     only.
        PreferSlave = 8,
        //
        // Summary:
        //     This operation should only be performed on a slave. Suitable for read operations
        //     only.
        DemandSlave = 12,
        //
        // Summary:
        //     Indicates that this operation should not be forwarded to other servers as a result
        //     of an ASK or MOVED response
        NoRedirect = 64
    }


    //
    // Summary:
    //     Indicates when this operation should be performed (only some variations are legal
    //     in a given context)
    public enum When
    {
        //
        // Summary:
        //     The operation should occur whether or not there is an existing value
        Always = 0,
        //
        // Summary:
        //     The operation should only occur when there is an existing value
        Exists = 1,
        //
        // Summary:
        //     The operation should only occur when there is not an existing value
        NotExists = 2
    }
    //
    // Summary:
    //     When performing a range query, by default the start / stop limits are inclusive;
    //     however, both can also be specified separately as exclusive
    [Flags]
    public enum Exclude
    {
        //
        // Summary:
        //     Both start and stop are inclusive
        None = 0,
        //
        // Summary:
        //     Start is exclusive, stop is inclusive
        Start = 1,
        //
        // Summary:
        //     Start is inclusive, stop is exclusive
        Stop = 2,
        //
        // Summary:
        //     Both start and stop are exclusive
        Both = 3
    }
    //
    // Summary:
    //     Specifies how elements should be aggregated when combining sorted sets
    public enum Aggregate
    {
        //
        // Summary:
        //     The values of the combined elements are added
        Sum = 0,
        //
        // Summary:
        //     The least value of the combined elements is used
        Min = 1,
        //
        // Summary:
        //     The greatest value of the combined elements is used
        Max = 2
    }

    //
    // Summary:
    //     The direction in which to sequence elements
    public enum Order
    {
        //
        // Summary:
        //     Ordered from low values to high values
        Ascending = 0,
        //
        // Summary:
        //     Ordered from high values to low values
        Descending = 1
    }
    //
    // Summary:
    //     Describes an algebraic set operation that can be performed to combine multiple
    //     sets
    public enum SetOperation
    {
        //
        // Summary:
        //     Returns the members of the set resulting from the union of all the given sets.
        Union = 0,
        //
        // Summary:
        //     Returns the members of the set resulting from the intersection of all the given
        //     sets.
        Intersect = 1,
        //
        // Summary:
        //     Returns the members of the set resulting from the difference between the first
        //     set and all the successive sets.
        Difference = 2
    }
}
