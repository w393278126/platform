using System;
using System.Linq;
using Kafka.Client.Producers.Partitioning;
using Xn.Platform.Core.Extensions;

namespace Xn.Platform.Infrastructure.Web.Kafka
{
    public class CustomStringPartitioner<TKey> : IPartitioner<TKey>
    {
        private static readonly Random Randomizer = new Random();

        public int Partition(TKey key, int numPartitions)
        {
            var stringKey = key as string;
            if (stringKey.IsNullOrEmpty())
            {
                return Randomizer.Next(numPartitions);
            }

            return stringKey.Sum(k => k) % numPartitions;
        }
    }
}