using NRedisStack.RedisStackCommands;
using NRedisStack.Core.Literals;
using NRedisStack.Core;
using NRedisStack.Core.DataTypes;
using StackExchange.Redis;

namespace NRedisStack
{

    public static class CoreCommandBuilder
    {
        public static SerializedCommand ClientSetInfo(SetInfoAttr attr, string value)
        {
            string attrValue = attr switch
            {
                SetInfoAttr.LibraryName => CoreArgs.lib_name,
                SetInfoAttr.LibraryVersion => CoreArgs.lib_ver,
                _ => throw new System.NotImplementedException(),
            };

            return new SerializedCommand(RedisCoreCommands.CLIENT, RedisCoreCommands.SETINFO, attrValue, value);
        }

        public static SerializedCommand BZMPop(double timeout, RedisKey[] keys, MinMaxModifier minMaxModifier, long? count)
        {
            if (keys.Length == 0)
            {
                throw new ArgumentException("At least one key must be provided.");
            }

            List<object> args = new List<object>();

            args.Add(timeout);
            args.Add(keys.Length);
            args.AddRange(keys.Cast<object>());
            args.Add(minMaxModifier == MinMaxModifier.Min ? CoreArgs.MIN : CoreArgs.MAX);

            if (count != null)
            {
                args.Add(CoreArgs.COUNT);
                args.Add(count);
            }

            return new SerializedCommand(RedisCoreCommands.BZMPOP, args);
        }

        public static SerializedCommand BZPopMin(RedisKey[] keys, double timeout)
        {
            return BlockingCommandWithKeysAndTimeout(RedisCoreCommands.BZPOPMIN, keys, timeout);
        }

        public static SerializedCommand BZPopMax(RedisKey[] keys, double timeout)
        {
            return BlockingCommandWithKeysAndTimeout(RedisCoreCommands.BZPOPMAX, keys, timeout);
        }

        public static SerializedCommand BLMPop(double timeout, RedisKey[] keys, ListSide listSide, long? count)
        {
            if (keys.Length == 0)
            {
                throw new ArgumentException("At least one key must be provided.");
            }

            List<object> args = new List<object>();

            args.Add(timeout);
            args.Add(keys.Length);
            args.AddRange(keys.Cast<object>());
            args.Add(listSide == ListSide.Left ? CoreArgs.LEFT : CoreArgs.RIGHT);

            if (count != null)
            {
                args.Add(CoreArgs.COUNT);
                args.Add(count);
            }

            return new SerializedCommand(RedisCoreCommands.BLMPOP, args);
        }

        public static SerializedCommand BLPop(RedisKey[] keys, double timeout)
        {
            return BlockingCommandWithKeysAndTimeout(RedisCoreCommands.BLPOP, keys, timeout);
        }

        public static SerializedCommand BRPop(RedisKey[] keys, double timeout)
        {
            return BlockingCommandWithKeysAndTimeout(RedisCoreCommands.BRPOP, keys, timeout);
        }

        private static SerializedCommand BlockingCommandWithKeysAndTimeout(String command, RedisKey[] keys, double timeout)
        {
            if (keys.Length == 0)
            {
                throw new ArgumentException("At least one key must be provided.");
            }

            List<object> args = new List<object>();
            args.AddRange(keys.Cast<object>());
            args.Add(timeout);

            return new SerializedCommand(command, args);
        }
    }
}
