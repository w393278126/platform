using System;
using System.Diagnostics;
using System.Threading;
using Xn.Platform.Core.Extensions;
using StackExchange.Redis;

namespace Xn.Platform.Abstractions.Redis.Configuration
{
    public class RedisSettings
    {
        readonly ConfigurationOptions _configuration;
        readonly System.IO.TextWriter _connectionMultiplexerLog;

        ConnectionMultiplexer _connection;
        readonly object _connectionLock = new object();

        public int Db { get; set; }
        public bool Master { get; set; }

        public string Server { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        public RedisSettings(string connectionString, int db = 0, bool master = false , System.IO.TextWriter connectionMultiplexerLog = null)
            : this(ConfigurationOptions.Parse(connectionString), db, master, connectionMultiplexerLog)
        {
        }

        public RedisSettings(ConfigurationOptions configuration, int db = 0, bool master = false, System.IO.TextWriter connectionMultiplexerLog = null)
        {
            Db = db;
            Master = master;
            _configuration = configuration;
            Server = _configuration.ToString();
            var split = Server.Split(':');
            Host = split[0];
            Port = split[1].AsInt();
            _connectionMultiplexerLog = connectionMultiplexerLog;
        }

        public ConnectionMultiplexer GetConnection()
        {
            if (_connection == null || !_connection.IsConnected)
            {
                lock (_connectionLock)
                {
                    if (_connection != null && _connection.IsConnected) return _connection;
                    if (_connection != null)
                    {
                        _connection.Close(false);
                        _connection.Dispose();
                        _connection = null;
                    }

                    var tryCount = 0;
                    bool allowRetry;
                    do
                    {
                        allowRetry = false;
                        try
                        {
                            var sw = Stopwatch.StartNew();
                            var innerSw = Stopwatch.StartNew();
                            try
                            {
                                // Sometimes ConnectionMultiplexer.Connect is failed and issue does not solved https://github.com/StackExchange/StackExchange.Redis/issues/42
                                // I've created manualy Connect and control timeout.
                                // I recommend set connectTimeout from 1000 to 5000. (configure your network latency)
                                var tcs = new System.Threading.Tasks.TaskCompletionSource<ConnectionMultiplexer>();
                                var connectThread = new Thread(_ =>
                                {
                                    try
                                    {
                                        var connTask = ConnectionMultiplexer.ConnectAsync(_configuration, _connectionMultiplexerLog)
                                            .ContinueWith(x =>
                                            {
                                                innerSw.Stop();
                                                if (x.IsCompleted)
                                                {
                                                    if (!tcs.TrySetResult(x.Result))
                                                    {
                                                        // already faulted
                                                        x.Result.Close(false);
                                                        x.Result.Dispose();
                                                    }
                                                }
                                            });
                                        if (!connTask.Wait(this._configuration.ConnectTimeout))
                                        {
                                            tcs.TrySetException(new TimeoutException("Redis Connect Timeout. Elapsed:" + sw.Elapsed.TotalMilliseconds + "ms"));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        tcs.TrySetException(ex);
                                    }
                                });
                                connectThread.Start();

                                _connection = tcs.Task.GetAwaiter().GetResult();
                                _connection.IncludeDetailInExceptions = true;
                                sw.Stop();
                            }
                            catch (Exception)
                            {
                                sw.Stop();
                                _connection = null;
                            }
                        }
                        catch (TimeoutException)
                        {
                            tryCount++;
                            allowRetry = true;
                        }
                    } while (_connection == null && allowRetry);
                }
            }

            return _connection;
        }
    }
}