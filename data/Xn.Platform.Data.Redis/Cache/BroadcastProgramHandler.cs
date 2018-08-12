using Xn.Platform.Abstractions.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Data.Redis.Cache
{
    public class PPLiveProgramHandler : RedisString
    {
        public PPLiveProgramHandler() : base(RedisKeyDefinition.PPLiveProgram)
        {

        }
    }

    public class BroadcastProgramHandler : RedisHash
    {
        public BroadcastProgramHandler() : base(RedisKeyDefinition.BroadcastProgram)
        {

        }
    }
}
