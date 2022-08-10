using StackExchange.Redis;

namespace NRedisStack.Core.RedisStackCommands
{
    public static class ModulPrefixes
    {
        static bool bloomCreated = false;
        static BloomCommands bloomCommands;

        static bool cuckooCreated = false;
        static CuckooCommands cuckooCommands;

        static bool cmsCreated = false;
        static CmsCommands cmsCommands;

        static bool topKCreated = false;
        static TopKCommands topKCommands;

        static bool searchCreated = false;
        static SearchCommands searchCommands;

        static bool jsonCreated = false;
        static JsonCommands jsonCommands;

        static bool timeSeriesCreated = false;
        static TimeSeriesCommands timeSeriesCommands;

        static public BloomCommands BF(this IDatabase db)
        {
            if (!bloomCreated)
            {
                bloomCommands = new BloomCommands(db);
                bloomCreated = true;
            }

            return bloomCommands;
        }

        static public CuckooCommands CF(this IDatabase db)
        {
            if (!cuckooCreated)
            {
                cuckooCommands = new CuckooCommands(db);
                cuckooCreated = true;
            }

            return cuckooCommands;
        }

        static public CmsCommands CMS(this IDatabase db)
        {
            if (!cmsCreated)
            {
                cmsCommands = new CmsCommands(db);
                cmsCreated = true;
            }

            return cmsCommands;
        }

        static public TopKCommands TOPK(this IDatabase db)
        {
            if (!topKCreated)
            {
                topKCommands = new TopKCommands(db);
                topKCreated = true;
            }

            return topKCommands;
        }

        static public SearchCommands FT(this IDatabase db)
        {
            if (!searchCreated)
            {
                searchCommands = new SearchCommands(db);
                searchCreated = true;
            }

            return searchCommands;
        }

        static public JsonCommands JSON(this IDatabase db)
        {
            if (!jsonCreated)
            {
                jsonCommands = new JsonCommands(db);
                jsonCreated = true;
            }

            return jsonCommands;
        }

        static public TimeSeriesCommands TS(this IDatabase db)
        {
            if (!jsonCreated)
            {
                timeSeriesCommands = new TimeSeriesCommands(db);
                timeSeriesCreated = true;
            }

            return timeSeriesCommands;
        }
    }
}