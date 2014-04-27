﻿using System;
using System.Runtime.Caching;
using R.Scheduler.Contracts.Interfaces;

namespace R.Scheduler.Persistance
{
    /// <summary>
    /// InMemory implementation of IPluginStore
    /// </summary>
    public class InMemoryPluginStore : IPluginStore
    {
        private static readonly ObjectCache Cache = MemoryCache.Default;
        private CacheItemPolicy _policy; 

        /// <summary>
        /// Get registered plugin
        /// </summary>
        /// <param name="pluginName"></param>
        /// <returns></returns>
        public Plugin GetRegisteredPlugin(string pluginName)
        {
            Plugin retval = null;

            if (Cache.Contains(pluginName))
            {
                retval = Cache[pluginName] as Plugin; 
            }

            return retval;
        }

        /// <summary>
        /// Register new plugin, or update existing one.
        /// </summary>
        /// <param name="plugin"></param>
        public void RegisterPlugin(Plugin plugin)
        {
            _policy = new CacheItemPolicy {AbsoluteExpiration = DateTimeOffset.Now.AddHours(10.00)};

            Cache.Set(plugin.Name, plugin, _policy); 
        }
    }
}