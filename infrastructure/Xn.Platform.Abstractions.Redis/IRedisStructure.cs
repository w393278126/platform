using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using StackExchange.Redis;

namespace Xn.Platform.Abstractions.Redis
{
    /// <summary>
    /// Key������
    /// 
    /// todo
    /// http://stackoverflow.com/questions/4006324/how-to-atomically-delete-keys-matching-a-pattern-using-redis 
    /// https://github.com/StackExchange/StackExchange.Redis/blob/master/Docs/KeysScan.md
    /// OBJECT subcommand [arguments [arguments]]
    /// SORT key [BY pattern] [LIMIT offset count] [GET pattern [GET pattern ...]] [ASC | DESC] [ALPHA] [STORE destination]
    /// </summary>
    public interface IRedisStructure
    {
        /// <summary>
        /// �������з��ϸ���ģʽ pattern �� key ��
        /// KEYS* ƥ�����ݿ������� key ��
        /// KEYS h?llo ƥ�� hello �� hallo �� hxllo �ȡ�
        /// KEYS h*llo ƥ�� hllo �� heeeeello �ȡ�
        /// KEYS h[ae]llo ƥ�� hello �� hallo ������ƥ�� hillo ��
        /// ��������� \ ���
        /// KEYS ���ٶȷǳ��죬����һ��������ݿ���ʹ������Ȼ��������������⣬�������Ҫ��һ�����ݼ��в����ض��� key ������û����� Redis �ļ��Ͻṹ(set)�����档
        /// ʱ�临�Ӷȣ�O(N)�� N Ϊ���ݿ��� key ��������
        /// </summary>
        /// <remarks>
        /// KEYS pattern
        /// </remarks>
        /// <returns>
        /// ���ϸ���ģʽ�� key �б��
        /// </returns>
        /// <returns></returns>
        string[] Keys(int database = 0, string pattern = null, int pageSize = 10, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// ɾ��������һ������ key ��
        /// �����ڵ� key �ᱻ���ԡ�
        /// ʱ�临�Ӷȣ�O(N)�� N Ϊ��ɾ���� key ��������
        /// ɾ�������ַ������͵� key ��ʱ�临�Ӷ�ΪO(1)��
        /// ɾ�������б�����ϡ����򼯺ϻ��ϣ�����͵� key ��ʱ�临�Ӷ�ΪO(M)�� M Ϊ�������ݽṹ�ڵ�Ԫ�������� 
        /// <remarks>
        /// DEL key [key ...] http://redis.io/commands/del
        /// </remarks>
        /// <returns>
        /// ��ɾ�� key ��������
        /// </returns>
        /// </summary>
        bool Delete(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ���л����� key �������ر����л���ֵ��ʹ�� RESTORE ������Խ����ֵ�����л�Ϊ Redis ����
        /// ���л����ɵ�ֵ�����¼����ص㣺
        /// ������ 64 λ��У��ͣ����ڼ����� RESTORE �ڽ��з����л�֮ǰ���ȼ��У��͡�
        /// ֵ�ı����ʽ�� RDB �ļ�����һ�¡�
        /// RDB �汾�ᱻ���������л�ֵ���У������Ϊ Redis �İ汾��ͬ��� RDB ��ʽ�����ݣ���ô Redis ��ܾ������ֵ���з����л�������
        /// ���л���ֵ�������κ�����ʱ����Ϣ��
        /// ʱ�临�Ӷȣ����Ҹ������ĸ��Ӷ�Ϊ O(1) ���Լ��������л��ĸ��Ӷ�Ϊ O(N* M) ������ N �ǹ��� key �� Redis ������������� M ������Щ�����ƽ����С��
        /// ������л��Ķ����ǱȽ�С���ַ�������ô���Ӷ�Ϊ O(1) ��
        /// <remarks>
        /// DUMP key http://redis.io/commands/dump
        /// </remarks>
        /// <returns>
        /// ��� key �����ڣ���ô���� nil �����򣬷������л�֮���ֵ��
        /// </returns>
        /// </summary>
        byte[] Dump(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ������ key �Ƿ����
        /// ʱ�临�Ӷȣ�O(1)��
        /// <remarks>
        /// EXISTS key http://redis.io/commands/exists
        /// </remarks>
        /// <returns>
        /// �� key ���ڣ����� 1 �����򷵻� 0 ��
        /// </returns>
        /// </summary>
        bool Exists(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// Ϊ���� key ��������ʱ�䣬�� key ����ʱ(����ʱ��Ϊ 0 )�����ᱻ�Զ�ɾ����
        /// �� Redis �У���������ʱ��� key ����Ϊ����ʧ�ġ�(volatile)��
        /// ����ʱ�����ͨ��ʹ�� DEL ������ɾ������ key ���Ƴ������߱� SET �� GETSET ���д(overwrite)������ζ�ţ����һ������ֻ���޸�(alter)һ��������ʱ��� key ��ֵ��������һ���µ� key ֵ������(replace)���Ļ�����ô����ʱ�䲻�ᱻ�ı䡣
        /// ����˵����һ�� key ִ�� INCR �����һ���б���� LPUSH ������߶�һ����ϣ��ִ�� HSET �����������������޸� key ���������ʱ�䡣
        /// ��һ���棬���ʹ�� RENAME ��һ�� key ���и�������ô������� key ������ʱ��͸���ǰһ����
        /// RENAME �������һ�ֿ����ǣ����Խ�һ��������ʱ��� key ��������һ��������ʱ��� another_key ����ʱ�ɵ� another_key(�Լ���������ʱ��)�ᱻɾ����Ȼ��ɵ� key �����Ϊ another_key ����ˣ��µ� another_key ������ʱ��Ҳ��ԭ���� key һ����
        /// ʹ�� PERSIST ��������ڲ�ɾ�� key ������£��Ƴ� key ������ʱ�䣬�� key ���³�Ϊһ�����־õġ�(persistent) key ��
        /// ��������ʱ��
        /// ���Զ�һ���Ѿ���������ʱ��� key ִ�� EXPIRE �����ָ��������ʱ���ȡ���ɵ�����ʱ�䡣
        /// ����ʱ��ľ�ȷ��
        /// �� Redis 2.4 �汾�У�����ʱ����ӳ��� 1 ����֮�� ���� Ҳ���ǣ����� key �Ѿ����ڣ��������ǿ����ڹ���֮��һ����֮�ڱ����ʵ��������µ� Redis 2.6 �汾�У��ӳٱ����͵� 1 ����֮�ڡ�
        /// Redis 2.1.3 ֮ǰ�Ĳ�֮ͬ��
        /// �� Redis 2.1.3 ֮ǰ�İ汾�У��޸�һ����������ʱ��� key �ᵼ������ key ��ɾ������һ��Ϊ���ܵ�ʱ����(replication)������ƶ������ģ�������һ�����Ѿ����޸���
        /// ʱ�临�Ӷȣ�O(1)
        /// 
        /// EXPIREAT �����ú� EXPIRE ���ƣ�������Ϊ key ��������ʱ�䡣
        /// ��ͬ���� EXPIREAT ������ܵ�ʱ������� UNIX ʱ���(unix timestamp)��
        /// ʱ�临�Ӷȣ�O(1)
        /// 
        /// �������� EXPIRE ������������ƣ��������Ժ���Ϊ��λ���� key ������ʱ�䣬������ EXPIRE ��������������Ϊ��λ��
        /// ʱ�临�Ӷȣ�O(1)
        /// 
        /// �������� EXPIREAT �������ƣ������Ժ���Ϊ��λ���� key �Ĺ��� unix ʱ������������� EXPIREAT ����������Ϊ��λ��
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// EXPIRE key seconds http://redis.io/commands/expire
        /// EXPIREAT key timestamp http://redis.io/commands/expireat
        /// PEXPIRE key milliseconds http://redis.io/commands/pexpire
        /// PEXPIREAT key milliseconds-timestamp http://redis.io/commands/pexpireat
        /// </remarks>
        /// <returns>
        /// ���óɹ����� 1 �� �� key �����ڻ��߲���Ϊ key ��������ʱ��ʱ(�����ڵ��� 2.1.3 �汾�� Redis ���㳢�Ը��� key ������ʱ��)������ 0 ��
        /// �������ʱ�����óɹ������� 1 ���� key �����ڻ�û�취��������ʱ�䣬���� 0 ��
        /// ���óɹ������� 1��key �����ڻ�����ʧ�ܣ����� 0��
        /// �������ʱ�����óɹ������� 1 ���� key �����ڻ�û�취��������ʱ��ʱ������ 0 ��(�鿴 EXPIRE �����ȡ������Ϣ)
        /// </returns>
        /// </summary>
        bool Expire(string keySuffix, DateTime expiry, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// Ϊ���� key ��������ʱ�䣬�� key ����ʱ(����ʱ��Ϊ 0 )�����ᱻ�Զ�ɾ����
        /// �� Redis �У���������ʱ��� key ����Ϊ����ʧ�ġ�(volatile)��
        /// ����ʱ�����ͨ��ʹ�� DEL ������ɾ������ key ���Ƴ������߱� SET �� GETSET ���д(overwrite)������ζ�ţ����һ������ֻ���޸�(alter)һ��������ʱ��� key ��ֵ��������һ���µ� key ֵ������(replace)���Ļ�����ô����ʱ�䲻�ᱻ�ı䡣
        /// ����˵����һ�� key ִ�� INCR �����һ���б���� LPUSH ������߶�һ����ϣ��ִ�� HSET �����������������޸� key ���������ʱ�䡣
        /// ��һ���棬���ʹ�� RENAME ��һ�� key ���и�������ô������� key ������ʱ��͸���ǰһ����
        /// RENAME �������һ�ֿ����ǣ����Խ�һ��������ʱ��� key ��������һ��������ʱ��� another_key ����ʱ�ɵ� another_key(�Լ���������ʱ��)�ᱻɾ����Ȼ��ɵ� key �����Ϊ another_key ����ˣ��µ� another_key ������ʱ��Ҳ��ԭ���� key һ����
        /// ʹ�� PERSIST ��������ڲ�ɾ�� key ������£��Ƴ� key ������ʱ�䣬�� key ���³�Ϊһ�����־õġ�(persistent) key ��
        /// ��������ʱ��
        /// ���Զ�һ���Ѿ���������ʱ��� key ִ�� EXPIRE �����ָ��������ʱ���ȡ���ɵ�����ʱ�䡣
        /// ����ʱ��ľ�ȷ��
        /// �� Redis 2.4 �汾�У�����ʱ����ӳ��� 1 ����֮�� ���� Ҳ���ǣ����� key �Ѿ����ڣ��������ǿ����ڹ���֮��һ����֮�ڱ����ʵ��������µ� Redis 2.6 �汾�У��ӳٱ����͵� 1 ����֮�ڡ�
        /// Redis 2.1.3 ֮ǰ�Ĳ�֮ͬ��
        /// �� Redis 2.1.3 ֮ǰ�İ汾�У��޸�һ����������ʱ��� key �ᵼ������ key ��ɾ������һ��Ϊ���ܵ�ʱ����(replication)������ƶ������ģ�������һ�����Ѿ����޸���
        /// ʱ�临�Ӷȣ�O(1)
        /// 
        /// EXPIREAT �����ú� EXPIRE ���ƣ�������Ϊ key ��������ʱ�䡣
        /// ��ͬ���� EXPIREAT ������ܵ�ʱ������� UNIX ʱ���(unix timestamp)��
        /// ʱ�临�Ӷȣ�O(1)
        /// 
        /// �������� EXPIRE ������������ƣ��������Ժ���Ϊ��λ���� key ������ʱ�䣬������ EXPIRE ��������������Ϊ��λ��
        /// ʱ�临�Ӷȣ�O(1)
        /// 
        /// �������� EXPIREAT �������ƣ������Ժ���Ϊ��λ���� key �Ĺ��� unix ʱ������������� EXPIREAT ����������Ϊ��λ��
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// EXPIRE key seconds http://redis.io/commands/expire
        /// EXPIREAT key timestamp http://redis.io/commands/expireat
        /// PEXPIRE key milliseconds http://redis.io/commands/pexpire
        /// PEXPIREAT key milliseconds-timestamp http://redis.io/commands/pexpireat
        /// </remarks>
        /// <returns>
        /// ���óɹ����� 1 �� �� key �����ڻ��߲���Ϊ key ��������ʱ��ʱ(�����ڵ��� 2.1.3 �汾�� Redis ���㳢�Ը��� key ������ʱ��)������ 0 ��
        /// �������ʱ�����óɹ������� 1 ���� key �����ڻ�û�취��������ʱ�䣬���� 0 ��
        /// ���óɹ������� 1��key �����ڻ�����ʧ�ܣ����� 0��
        /// �������ʱ�����óɹ������� 1 ���� key �����ڻ�û�취��������ʱ��ʱ������ 0 ��(�鿴 EXPIRE �����ȡ������Ϣ)
        /// </returns>
        /// </summary>
        bool Expire(string keySuffix, TimeSpan expiry, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �� key ԭ���Եشӵ�ǰʵ�����͵�Ŀ��ʵ����ָ�����ݿ��ϣ�һ�����ͳɹ��� key ��֤�������Ŀ��ʵ���ϣ�����ǰʵ���ϵ� key �ᱻɾ����
        /// ���������һ��ԭ�Ӳ���������ִ�е�ʱ�����������Ǩ�Ƶ�����ʵ����ֱ������������������Ǩ�Ƴɹ���Ǩ��ʧ�ܣ��ȴ���ʱ��
        /// ������ڲ�ʵ���������ģ����ڵ�ǰʵ���Ը��� key ִ�� DUMP ���� ���������л���Ȼ���͵�Ŀ��ʵ����Ŀ��ʵ����ʹ�� RESTORE �����ݽ��з����л������������л����õ�������ӵ����ݿ��У���ǰʵ������Ŀ��ʵ���Ŀͻ���������ֻҪ���� RESTORE ����� OK �����ͻ���� DEL ɾ���Լ����ݿ��ϵ� key ��
        /// timeout �����Ժ���Ϊ��ʽ��ָ����ǰʵ����Ŀ��ʵ�����й�ͨ�������ʱ�䡣��˵����������һ��Ҫ�� timeout ��������ɣ�ֻ��˵���ݴ��͵�ʱ�䲻�ܳ������ timeout ����
        /// MIGRATE ������Ҫ�ڸ�����ʱ��涨����� IO ����������ڴ�������ʱ���� IO ���󣬻��ߴﵽ�˳�ʱʱ�䣬��ô�����ִֹͣ�У�������һ������Ĵ��� IOERR ��
        /// �� IOERR ����ʱ�����������ֿ��ܣ�
        /// key ���ܴ���������ʵ��
        /// key ����ֻ�����ڵ�ǰʵ��
        /// Ψһ�����ܷ�����������Ƕ�ʧ key ����ˣ����һ���ͻ���ִ�� MIGRATE ������Ҳ������� IOERR ������ô����ͻ���ΨһҪ���ľ��Ǽ���Լ����ݿ��ϵ� key �Ƿ��Ѿ�����ȷ��ɾ����
        /// �������������������ô MIGRATE ��֤ key ֻ������ڵ�ǰʵ���С�����Ȼ��Ŀ��ʵ���ĸ������ݿ��Ͽ����к� key ͬ���ļ���������� MIGRATE ����û�й�ϵ����
        /// ��ѡ�
        /// COPY �����Ƴ�Դʵ���ϵ� key ��
        /// REPLACE ���滻Ŀ��ʵ�����Ѵ��ڵ� key ��
        /// ʱ�临�Ӷȣ����������Դʵ����ʵ��ִ�� DUMP ����� DEL �����Ŀ��ʵ��ִ�� RESTORE ����鿴����������ĵ����Կ�����ϸ�ĸ��Ӷ�˵����
        /// key ����������ʵ��֮�䴫��ĸ��Ӷ�Ϊ O(N) ��
        /// <remarks>
        /// MIGRATE host port key destination-db timeout [COPY] [REPLACE] http://redis.io/commands/migrate
        /// </remarks>
        /// <returns>
        /// Ǩ�Ƴɹ�ʱ���� OK �����򷵻���Ӧ�Ĵ���
        /// </returns>
        /// </summary>
        void Migrate(string keySuffix, EndPoint toServer, int toDatabase = 0, int timeoutMilliseconds = 0, MigrateOptions migrateOptions = MigrateOptions.None, CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// ����ǰ���ݿ�� key �ƶ������������ݿ� db ���С�
        /// �����ǰ���ݿ�(Դ���ݿ�)�͸������ݿ�(Ŀ�����ݿ�)����ͬ���ֵĸ��� key ������ key �������ڵ�ǰ���ݿ⣬��ô MOVE û���κ�Ч����
        /// ��ˣ�Ҳ����������һ���ԣ��� MOVE ������(locking)ԭ��(primitive)��
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// MOVE key db http://redis.io/commands/move
        /// </remarks>
        /// <returns>
        /// �ƶ��ɹ����� 1 ��ʧ���򷵻� 0 ��
        /// </returns>
        /// </summary>
        bool Move(string keySuffix, int database, CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// �Ƴ����� key ������ʱ�䣬����� key �ӡ���ʧ�ġ�(������ʱ�� key )ת���ɡ��־õġ�(һ����������ʱ�䡢�������ڵ� key )��
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// PERSIST key http://redis.io/commands/persist
        /// </remarks>
        /// <returns>
        /// ������ʱ���Ƴ��ɹ�ʱ������ 1 .��� key �����ڻ� key û����������ʱ�䣬���� 0 ��
        /// </returns>
        /// </summary>
        bool Persist(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �ӵ�ǰ���ݿ����������(��ɾ��)һ�� key ��
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// RANDOMKEY http://redis.io/commands/randomkey
        /// </remarks>
        /// <returns>
        /// �����ݿⲻΪ��ʱ������һ�� key �������ݿ�Ϊ��ʱ������ nil ��
        /// </returns>
        /// </summary>
        string Random(CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �� key ����Ϊ newkey ��
        /// �� key �� newkey ��ͬ������ key ������ʱ������һ������
        /// �� newkey �Ѿ�����ʱ�� RENAME ������Ǿ�ֵ��
        /// ʱ�临�Ӷȣ�O(1)
        /// ���ҽ��� newkey ������ʱ���� key ����Ϊ newkey ��
        /// �� key ������ʱ������һ������
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// RENAME key newkey http://redis.io/commands/rename
        /// RENAMENX key newkey http://redis.io/commands/renamenx
        /// </remarks>
        /// <returns>
        /// ����ֵ�������ɹ�ʱ��ʾ OK ��ʧ��ʱ�򷵻�һ������
        /// ����ֵ���޸ĳɹ�ʱ������ 1 ����� newkey �Ѿ����ڣ����� 0 ��
        /// </returns>
        /// </summary>
        bool Rename(string keySuffix, string newKey, When when = When.Always, CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// �����л����������л�ֵ���������͸����� key ������
        /// ���� ttl �Ժ���Ϊ��λΪ key ��������ʱ�䣻��� ttl Ϊ 0 ����ô����������ʱ�䡣
        /// RESTORE ��ִ�з����л�֮ǰ���ȶ����л�ֵ�� RDB �汾������У��ͽ��м�飬��� RDB �汾����ͬ�������ݲ������Ļ�����ô RESTORE ��ܾ����з����л���������һ������
        /// ����� key �Ѿ����ڣ� ���Ҹ����� REPLACE ѡ� ��ôʹ�÷����л��ó���ֵ������� key ԭ�е�ֵ�� �෴�أ� ����� key �Ѿ����ڣ� ����û�и��� REPLACE ѡ� ��ô�����һ������
        /// ������Ϣ���Բο� DUMP ���
        /// ʱ�临�Ӷȣ����Ҹ������ĸ��Ӷ�Ϊ O(1) ���Լ����з����л��ĸ��Ӷ�Ϊ O(N* M) ������ N �ǹ��� key �� Redis ������������� M ������Щ�����ƽ����С��
        /// ���򼯺�(sorted set)�ķ����л����Ӷ�Ϊ O(N* M*log(N)) ����Ϊ���򼯺�ÿ�β���ĸ��Ӷ�Ϊ O(log(N)) ��
        /// ��������л��Ķ����ǱȽ�С���ַ�������ô���Ӷ�Ϊ O(1) ��
        /// <remarks>
        /// RESTORE key ttl serialized-value [REPLACE] http://redis.io/commands/restore
        /// </remarks>
        /// <returns>
        /// ��������л��ɹ���ô���� OK �����򷵻�һ������
        /// </returns>
        /// </summary>
        void Restore(string keySuffix, byte[] value, TimeSpan? expiry = default(TimeSpan?),CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// ����Ϊ��λ�����ظ��� key ��ʣ������ʱ��(TTL, time to live)��
        /// ʱ�临�Ӷȣ�O(1)
        /// 
        /// ������������� TTL ��������Ժ���Ϊ��λ���� key ��ʣ������ʱ�䣬�������� TTL ��������������Ϊ��λ��
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// TTL key http://redis.io/commands/ttl
        /// PTTL key http://redis.io/commands/pttl
        /// </remarks>
        /// <returns>
        /// �� key ������ʱ������ -2 ���� key ���ڵ�û������ʣ������ʱ��ʱ������ -1 ����������Ϊ��λ������ key ��ʣ������ʱ�䡣
        /// �� key ������ʱ������ -2 ���� key ���ڵ�û������ʣ������ʱ��ʱ������ -1 �������Ժ���Ϊ��λ������ key ��ʣ������ʱ�䡣
        /// </returns>
        /// </summary>
        TimeSpan? TimeToLive(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ���� key �������ֵ�����͡�
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// TYPE key http://redis.io/commands/type
        /// </remarks>
        /// <returns>
        /// none(key������)��string (�ַ���)��list(�б�)��set(����)��zset(����)��hash(��ϣ��)
        /// </returns>
        /// </summary>
        string Type(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// http://redis.io/commands/eval
        /// EVALSHA http://redis.io/commands/evalsha
        /// </summary>
        int ScriptEvaluateAsInt(string script, RedisKey[] keys = null, RedisValue[] values = null, CommandFlags commandFlags = CommandFlags.None);
        string[] ScriptEvaluateAsList(string script, RedisKey[] keys = null, RedisValue[] values = null, CommandFlags commandFlags = CommandFlags.None);
        string ScriptEvaluateString(string script, RedisKey[] keys = null, RedisValue[] values = null, CommandFlags commandFlags = CommandFlags.None);

        int DeleteKeys(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        long[] GetRedisTime();
        long GetRedisTimeResult();
    }
    /// <summary>
    /// String���ַ�����
    /// 
    /// todo
    /// BITOP operation destkey key [key ...]
    /// DECR key
    /// GETRANGE key start end
    /// INCR key
    /// MGET key [key ...]
    /// MSET key value [key value ...]
    /// MSETNX key value [key value ...]
    /// PSETEX key milliseconds value
    /// SETEX key seconds value
    /// SETNX key value
    /// SETRANGE key offset value
    /// STRLEN key
    /// </summary>
    public interface IRedisString : IRedisStructure
    {
        /// <summary>
        /// ��� key �Ѿ����ڲ�����һ���ַ����� APPEND ��� value ׷�ӵ� key ԭ����ֵ��ĩβ��
        /// ��� key �����ڣ� APPEND �ͼ򵥵ؽ����� key ��Ϊ value ������ִ�� SET key value һ����
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// APPEND key value http://redis.io/commands/append
        /// </remarks>
        /// <returns>
        /// ׷�� value ֮�� key ���ַ����ĳ��ȡ�
        /// </returns>
        /// </summary>
        long Append(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ��������ַ����У�������Ϊ 1 �ı���λ��������
        /// һ������£������������ַ������ᱻ���м�����ͨ��ָ������� start �� end �����������ü���ֻ���ض���λ�Ͻ��С�
        /// start �� end ���������ú� GETRANGE �������ƣ�������ʹ�ø���ֵ�� ���� -1 ��ʾ���һ���ֽڣ� -2 ��ʾ�����ڶ����ֽڣ��Դ����ơ�
        /// �����ڵ� key �������ǿ��ַ������������˶�һ�������ڵ� key ���� BITCOUNT ���������Ϊ 0 ��
        /// ʱ�临�Ӷȣ�O(N)
        /// <remarks>
        /// BITCOUNT key [start] [end] http://redis.io/commands/bitcount
        /// </remarks>
        /// <returns>
        /// ������Ϊ 1 ��λ��������
        /// </returns>
        /// </summary>
        long BitCount(string keySuffix, long start = 0, long end = -1, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// http://redis.io/commands/bitop
        /// </summary>
        long BitOperation(Bitwise operation, RedisKey destination, RedisKey first, RedisKey second = default(RedisKey), CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// BITPOSITION http://redis.io/commands/bitpos
        /// </summary>
        long BitPosition(string keySuffix, bool bit, long start = 0, long end = -1,
            CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ���� key ���������ַ���ֵ��
        /// ��� key ��������ô��������ֵ nil ��
        /// ���� key �����ֵ�����ַ������ͣ�����һ��������Ϊ GET ֻ�����ڴ����ַ���ֵ��
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// GET key http://redis.io/commands/get
        /// http://redis.io/commands/mget
        /// </remarks>
        /// <returns>
        /// �� key ������ʱ������ nil �����򣬷��� key ��ֵ����� key �����ַ������ͣ���ô����һ������
        /// </returns>
        /// </summary>
        string Get(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ������ key ��ֵ��Ϊ value �������� key �ľ�ֵ(old value)��
        /// �� key ���ڵ������ַ�������ʱ������һ������
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// GETSET key value http://redis.io/commands/getset
        /// </remarks>
        /// <returns>
        /// ���ظ��� key �ľ�ֵ���� key û�о�ֵʱ��Ҳ���ǣ� key ������ʱ������ nil ��
        /// </returns>
        /// </summary>
        string GetSet(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ���ַ���ֵ value ������ key ��
        /// ��� key �Ѿ���������ֵ�� SET �͸�д��ֵ���������͡�
        /// ����ĳ��ԭ����������ʱ�䣨TTL���ļ���˵�� �� SET ����ɹ����������ִ��ʱ�� �����ԭ�е� TTL ���������
        /// ��ѡ����
        /// �� Redis 2.6.12 �汾��ʼ�� SET �������Ϊ����ͨ��һϵ�в������޸ģ�
        /// EX second �����ü��Ĺ���ʱ��Ϊ second �롣 SET key value EX second Ч����ͬ�� SETEX key second value ��
        /// PX millisecond �����ü��Ĺ���ʱ��Ϊ millisecond ���롣 SET key value PX millisecond Ч����ͬ�� PSETEX key millisecond value ��
        /// NX ��ֻ�ڼ�������ʱ���ŶԼ��������ò����� SET key value NX Ч����ͬ�� SETNX key value ��
        /// XX ��ֻ�ڼ��Ѿ�����ʱ���ŶԼ��������ò�����
        /// ��Ϊ SET �������ͨ��������ʵ�ֺ� SETNX �� SETEX �� PSETEX ���������Ч�������Խ����� Redis �汾���ܻ�����������Ƴ� SETNX �� SETEX �� PSETEX ���������
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// SET key value [EX seconds] [PX milliseconds] [NX|XX] http://redis.io/commands/set
        /// </remarks>
        /// <returns>
        /// ����ֵ���� Redis 2.6.12 �汾��ǰ�� SET �������Ƿ��� OK ��
        /// �� Redis 2.6.12 �汾��ʼ�� SET �����ò����ɹ����ʱ���ŷ��� OK ��
        /// ��������� NX ���� XX ������Ϊ����û�ﵽ��������ò���δִ�У���ô����ؿ������ظ���NULL Bulk Reply����
        /// </returns>
        /// </summary>
        bool Set(string keySuffix, string value, TimeSpan? expiry = null, When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �� key �������ֵ�������� increment ��
        /// ��� key �����ڣ���ô key ��ֵ���ȱ���ʼ��Ϊ 0 ��Ȼ����ִ�� INCRBY ���
        /// ���ֵ������������ͣ����ַ������͵�ֵ���ܱ�ʾΪ���֣���ô����һ������
        /// ��������ֵ������ 64 λ(bit)�з������ֱ�ʾ֮�ڡ�
        /// ���ڵ���(increment) / �ݼ�(decrement)�����ĸ�����Ϣ���μ� INCR ����
        /// ʱ�临�Ӷȣ�O(1)��
        /// <remarks>
        /// INCRBY key increment http://redis.io/commands/incrby
        /// http://redis.io/commands/incr
        /// </remarks>
        /// <returns>
        /// ���� increment ֮�� key ��ֵ�� 
        /// </returns>
        /// </summary>
        long Increment(string keySuffix, long value = 1, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// Ϊ key ���������ֵ���ϸ��������� increment ��
        /// ��� key �����ڣ���ô INCRBYFLOAT ���Ƚ� key ��ֵ��Ϊ 0 ����ִ�мӷ�������
        /// �������ִ�гɹ�����ô key ��ֵ�ᱻ����Ϊ��ִ�мӷ�֮��ģ���ֵ��������ֵ�����ַ�������ʽ���ظ������ߡ�
        /// ������ key ��ֵ���������� increment ��������ʹ���� 2.0e7 �� 3e5 �� 90e-2 ������ָ������(exponential notation)����ʾ�����ǣ�ִ�� INCRBYFLOAT ����֮���ֵ������ͬ������ʽ���棬Ҳ���ǣ�����������һ�����֣�һ������ѡ�ģ�С�����һ������λ��С��������ɣ����� 3.14 �� 69.768 ���������)��С������β��� 0 �ᱻ�Ƴ����������Ҫ�Ļ������Ὣ��������Ϊ���������� 3.0 �ᱻ����� 3 ����
        /// ����֮�⣬���ۼӷ��������õĸ�������ʵ�ʾ����ж೤�� INCRBYFLOAT �ļ�����Ҳ���ֻ�ܱ�ʾС����ĺ�ʮ��λ��
        /// ����������һ����������ʱ������һ������
        /// key ��ֵ�����ַ�������(��Ϊ Redis �е����ֺ͸����������ַ�������ʽ���棬�������Ƕ������ַ������ͣ�
        /// key ��ǰ��ֵ���߸��������� increment ���ܽ���(parse)Ϊ˫���ȸ�����(double precision floating point number��
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// INCRBYFLOAT key increment http://redis.io/commands/incrbyfloat
        /// </remarks>
        /// <returns>
        /// ִ������֮�� key ��ֵ��
        /// </returns>
        /// </summary>
        double Increment(string keySuffix, double value, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �� key �������ֵ��ȥ���� decrement ��
        /// ��� key �����ڣ���ô key ��ֵ���ȱ���ʼ��Ϊ 0 ��Ȼ����ִ�� DECRBY ������
        /// ���ֵ������������ͣ����ַ������͵�ֵ���ܱ�ʾΪ���֣���ô����һ������
        /// ��������ֵ������ 64 λ(bit)�з������ֱ�ʾ֮�ڡ�
        /// ���ڸ������(increment) / �ݼ�(decrement)�����ĸ�����Ϣ����μ� INCR ���
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// DECRBY key decrement http://redis.io/commands/decrby
        /// http://redis.io/commands/decr
        /// </remarks>
        /// <returns>
        /// ��ȥ decrement ֮�� key ��ֵ��
        /// </returns>
        /// </summary>
        long Decrement(string keySuffix, long value = 1, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// INCRBYFLOAT http://redis.io/commands/incrbyfloat
        /// </summary>
        double Decrement(string keySuffix, double value = 1, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �� key ��������ַ���ֵ�����û����ָ��ƫ�����ϵ�λ(bit)��
        /// λ�����û����ȡ���� value ������������ 0 Ҳ������ 1 ��
        /// �� key ������ʱ���Զ�����һ���µ��ַ���ֵ��
        /// �ַ����������չ(grown)��ȷ�������Խ� value ������ָ����ƫ�����ϡ����ַ���ֵ������չʱ���հ�λ���� 0 ��䡣
        /// offset ����������ڻ���� 0 ��С�� 2^32 (bit ӳ�䱻������ 512 MB ֮��)��
        /// ��ʹ�ô�� offset �� SETBIT ������˵���ڴ���������� Redis ������������������ο� SETRANGE ���warning(����)���֡�
        /// ʱ�临�Ӷ�:O(1)
        /// <remarks>
        /// SETBIT key offset value http://redis.io/commands/setbit
        /// </remarks>
        /// <returns>
        /// ָ��ƫ����ԭ�������λ��
        /// </returns>
        /// </summary>
        bool SetBit(string keySuffix, long offset, bool bit, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �� key ��������ַ���ֵ����ȡָ��ƫ�����ϵ�λ(bit)��
        /// �� offset ���ַ���ֵ�ĳ��ȴ󣬻��� key ������ʱ������ 0 ��
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// GETBIT key offset http://redis.io/commands/getbit
        /// </remarks>
        /// <returns>
        /// �ַ���ֵָ��ƫ�����ϵ�λ(bit)��
        /// </returns>
        /// </summary>
        bool GetBit(string keySuffix, long offset, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// LUA Script including incrby, set
        /// </summary>
        long IncrementLimitByMax(string keySuffix, long value, long max, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// LUA Script including incrby, set
        /// </summary>
        long IncrementLimitByMin(string keySuffix, long value, long min, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// LUA Script including incrbyfloat, set
        /// </summary>
        double IncrementLimitByMax(string keySuffix, double value, double max,
            CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// LUA Script including incrbyfloat, set
        /// </summary>
        double IncrementLimitByMin(string keySuffix, double value, double min,
            CommandFlags commandFlags = CommandFlags.None);
    }
    /// <summary>
    /// Hash����ϣ���
    /// </summary>
    public interface IRedisHash : IRedisStructure
    {
        /// <summary>
        /// ɾ����ϣ�� key �е�һ������ָ���򣬲����ڵ��򽫱����ԡ�
        /// ʱ�临�Ӷ�:O(N)�� N ΪҪɾ�������������
        /// <remarks>
        /// HDEL key field [field ...] http://redis.io/commands/hdel
        /// </remarks>
        /// <returns>
        /// ���ɹ��Ƴ�����������������������Ե���
        /// </returns>
        /// </summary>
        bool Delete(string keySuffix, string field, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ɾ����ϣ�� key �е�һ������ָ���򣬲����ڵ��򽫱����ԡ�
        /// ʱ�临�Ӷ�:O(N)�� N ΪҪɾ�������������
        /// <remarks>
        /// HDEL key field [field ...] http://redis.io/commands/hdel
        /// </remarks>
        /// <returns>
        /// ���ɹ��Ƴ�����������������������Ե���
        /// </returns>
        /// </summary>
        long Delete(string keySuffix, string[] fields, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �鿴��ϣ�� key �У������� field �Ƿ���ڡ�
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// HEXISTS key field http://redis.io/commands/hexists
        /// </remarks>
        /// <returns>
        /// �����ϣ����и����򣬷��� 1 ��
        /// �����ϣ������и����򣬻� key �����ڣ����� 0 ��
        /// </returns>
        /// </summary>
        bool Exists(string keySuffix, string field, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ���ع�ϣ�� key �и����� field ��ֵ��
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// HGET key field http://redis.io/commands/hget
        /// </remarks>
        /// <returns>
        /// �������ֵ���������򲻴��ڻ��Ǹ��� key ������ʱ������ nil ��
        /// </returns>
        /// </summary>
        string Get(string keySuffix, string field, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ���ع�ϣ�� key �У�һ�������������ֵ��
        /// ����������򲻴����ڹ�ϣ�����ô����һ�� nil ֵ��
        /// ��Ϊ�����ڵ� key ������һ���չ�ϣ������������Զ�һ�������ڵ� key ���� HMGET ����������һ��ֻ���� nil ֵ�ı��
        /// ʱ�临�Ӷȣ�O(N)�� N Ϊ�������������
        /// <remarks>
        /// HMGET key field [field ...] http://redis.io/commands/hmget
        /// </remarks>
        /// <returns>
        /// һ���������������Ĺ���ֵ�ı����ֵ������˳��͸��������������˳��һ����
        /// </returns>
        /// </summary>
        IDictionary<string, string> Get(string keySuffix, string[] fields, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ���ع�ϣ�� key �У����е����ֵ��
        /// �ڷ���ֵ����ÿ������(field name)֮�������ֵ(value)�����Է���ֵ�ĳ����ǹ�ϣ���С��������
        /// ʱ�临�Ӷȣ�O(N)�� N Ϊ��ϣ��Ĵ�С��
        /// <remarks>
        /// HGETALL key http://redis.io/commands/hgetall
        /// </remarks>
        /// <returns>
        /// ���б���ʽ���ع�ϣ���������ֵ���� key �����ڣ����ؿ��б��
        /// </returns>
        /// </summary>
        IDictionary<string, string> GetAll(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// Ϊ��ϣ�� key �е��� field ��ֵ�������� increment ��
        /// ����Ҳ����Ϊ�������൱�ڶԸ�������м���������
        /// ��� key �����ڣ�һ���µĹ�ϣ���������ִ�� HINCRBY ���
        /// ����� field �����ڣ���ô��ִ������ǰ�����ֵ����ʼ��Ϊ 0 ��
        /// ��һ�������ַ���ֵ���� field ִ�� HINCRBY ������һ������
        /// ��������ֵ�������� 64 λ(bit)�з������ֱ�ʾ֮�ڡ�
        /// ʱ�临�Ӷȣ�O(1)
        /// HINCRBY key field increment http://redis.io/commands/hincrby
        /// <remarks>
        /// </remarks>
        /// <returns> 
        /// ִ�� HINCRBY ����֮�󣬹�ϣ�� key ���� field ��ֵ��
        /// </returns>
        /// </summary>
        long Increment(string keySuffix, string field, long value = 1, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// Ϊ��ϣ�� key �е��� field ���ϸ��������� increment ��
        /// �����ϣ����û���� field ����ô HINCRBYFLOAT ���Ƚ��� field ��ֵ��Ϊ 0 ��Ȼ����ִ�мӷ�������
        /// ����� key �����ڣ���ô HINCRBYFLOAT ���ȴ���һ����ϣ����ٴ����� field �������ִ�мӷ�������
        /// ����������һ����������ʱ������һ������
        /// �� field ��ֵ�����ַ�������(��Ϊ redis �е����ֺ͸����������ַ�������ʽ���棬�������Ƕ������ַ������ͣ�
        /// �� field ��ǰ��ֵ����������� increment ���ܽ���(parse)Ϊ˫���ȸ�����(double precision floating point number)
        /// HINCRBYFLOAT �������ϸ���ܺ� INCRBYFLOAT �������ƣ���鿴 INCRBYFLOAT �����ȡ���������Ϣ��
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// HINCRBYFLOAT key field increment http://redis.io/commands/hincrbyfloat
        /// </remarks>
        /// <returns>
        /// ִ�мӷ�����֮�� field ���ֵ��
        /// </returns>
        /// </summary>
        double Increment(string keySuffix, string field, double value, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ���ع�ϣ�� key �е�������
        /// ʱ�临�Ӷȣ�O(N)�� N Ϊ��ϣ��Ĵ�С��
        /// <remarks>
        /// HKEYS key http://redis.io/commands/hkeys
        /// </remarks>
        /// <returns>
        /// һ��������ϣ����������ı���� key ������ʱ������һ���ձ��
        /// </returns>
        /// </summary>
        string[] Keys(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ���ع�ϣ�� key �����������
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// HLEN key http://redis.io/commands/hlen
        /// </remarks>
        /// <returns>
        /// ��ϣ��������������� key ������ʱ������ 0 ��
        /// </returns>
        /// </summary>
        long Length(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// http://redis.io/commands/hscan
        /// </summary>
        //IEnumerable<HashEntry> Scan(string keySuffix, RedisValue pattern = default(RedisValue), int pageSize = 10, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// ����ϣ�� key �е��� field ��ֵ��Ϊ value ��
        /// ��� key �����ڣ�һ���µĹ�ϣ������������� HSET ������
        /// ����� field �Ѿ������ڹ�ϣ���У���ֵ�������ǡ�
        /// ʱ�临�Ӷȣ�O(1)
        /// ����ϣ�� key �е��� field ��ֵ����Ϊ value �����ҽ����� field �����ڡ�
        /// ���� field �Ѿ����ڣ��ò�����Ч��
        /// ��� key �����ڣ�һ���¹�ϣ���������ִ�� HSETNX ���
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// HSET key field value http://redis.io/commands/hset
        /// HSETNX key field value http://redis.io/commands/hsetnx
        /// </remarks>
        /// <returns>
        /// ����ֵ����� field �ǹ�ϣ���е�һ���½��򣬲���ֵ���óɹ������� 1 �������ϣ������ field �Ѿ������Ҿ�ֵ�ѱ���ֵ���ǣ����� 0 ��
        /// ����ֵ�����óɹ������� 1 ������������Ѿ�������û�в�����ִ�У����� 0 ��
        /// </returns>
        /// </summary>
        bool Set(string keySuffix, string field, string value, When when = When.Always, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ͬʱ����� field-value (��-ֵ)�����õ���ϣ�� key �С�
        /// ������Ḳ�ǹ�ϣ�����Ѵ��ڵ���
        /// ��� key �����ڣ�һ���չ�ϣ���������ִ�� HMSET ������
        /// ʱ�临�Ӷȣ�O(N)�� N Ϊ field-value �Ե�������
        /// <remarks>
        /// HMSET key field value [field value ...] http://redis.io/commands/hmset
        /// </remarks>
        /// <returns>
        /// �������ִ�гɹ������� OK ���� key ���ǹ�ϣ��(hash)����ʱ������һ������
        /// </returns>
        /// </summary>
        void Set(string keySuffix, IDictionary<string, string> values,CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ���ع�ϣ�� key ���������ֵ��
        /// ʱ�临�Ӷȣ�O(N)�� N Ϊ��ϣ��Ĵ�С��
        /// <remarks>
        /// HVALS key http://redis.io/commands/hvals
        /// </remarks>
        /// <returns>
        /// һ��������ϣ��������ֵ�ı���� key ������ʱ������һ���ձ��
        /// </returns>
        /// </summary>
        string[] Values(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        long DecrementLimitByMin(string keySuffix, string field, long value, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// LUA Script including hincrby, hset
        /// </summary>
        long IncrementLimitByMax(string keySuffix, string field, long value, long max, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// LUA Script including hincrbyfloat, hset
        /// </summary>
        double IncrementLimitByMax(string keySuffix, string field, double value, double max, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// LUA Script including hincrby, hset
        /// </summary>
        long IncrementLimitByMin(string keySuffix, string field, long value, long max, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// LUA Script including hincrbyfloat, hset
        /// </summary>
        double IncrementLimitByMin(string keySuffix, string field, double value, double max, CommandFlags commandFlags = CommandFlags.None);
    }
    public interface IRedisHash<TEntity> : IRedisHash
    {
        string Get(Expression<Func<TEntity, object>> expression, int field, CommandFlags commandFlags = CommandFlags.None);
        bool Set(Expression<Func<TEntity, object>> expression, int field, string value, When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None);
        long Increment(Expression<Func<TEntity, object>> expression, int field, long value = 1,
            CommandFlags commandFlags = CommandFlags.None);
        bool Delete(Expression<Func<TEntity, object>> expression, int field,
            CommandFlags commandFlags = CommandFlags.None);
    }
    /// <summary>
    /// List���б��
    /// </summary>
    public interface IRedisList : IRedisStructure
    {
        /// <summary>
        /// �����б� key �У��±�Ϊ index ��Ԫ�ء�
        /// �±�(index)���� start �� stop ���� 0 Ϊ�ף�Ҳ����˵���� 0 ��ʾ�б�ĵ�һ��Ԫ�أ��� 1 ��ʾ�б�ĵڶ���Ԫ�أ��Դ����ơ�
        /// ��Ҳ����ʹ�ø����±꣬�� -1 ��ʾ�б�����һ��Ԫ�أ� -2 ��ʾ�б�ĵ����ڶ���Ԫ�أ��Դ����ơ�
        /// ��� key �����б����ͣ�����һ������
        /// ʱ�临�Ӷȣ�O(N)�� N Ϊ�����±� index �����о�����Ԫ�������� ��ˣ����б��ͷԪ�غ�βԪ��ִ�� LINDEX ������Ӷ�ΪO(1)��
        /// <remarks>
        /// LINDEX key index http://redis.io/commands/lindex
        /// </remarks>
        /// <returns>
        /// �б����±�Ϊ index ��Ԫ�ء���� index ������ֵ�����б�����䷶Χ��(out of range)������ nil ��
        /// </returns>
        /// </summary>
        string GetByIndex(string keySuffix, long index, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ��ֵ value ���뵽�б� key ���У�λ��ֵ pivot ֮ǰ��֮��
        /// �� pivot ���������б� key ʱ����ִ���κβ�����
        /// �� key ������ʱ�� key ����Ϊ���б����ִ���κβ�����
        /// ��� key �����б����ͣ�����һ������
        /// ʱ�临�Ӷ�:O(N)�� N ΪѰ�� pivot �����о�����Ԫ��������
        /// <remarks>
        /// LINSERT key BEFORE|AFTER pivot value http://redis.io/commands/linsert
        /// </remarks>
        /// <returns>
        /// �������ִ�гɹ������ز���������֮���б�ĳ��ȡ����û���ҵ� pivot ������ -1 ����� key �����ڻ�Ϊ���б������ 0 ��
        /// </returns>
        /// </summary>
        long InsertAfter(string keySuffix, string pivot, string value, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ��ֵ value ���뵽�б� key ���У�λ��ֵ pivot ֮ǰ��֮��
        /// �� pivot ���������б� key ʱ����ִ���κβ�����
        /// �� key ������ʱ�� key ����Ϊ���б����ִ���κβ�����
        /// ��� key �����б����ͣ�����һ������
        /// ʱ�临�Ӷ�:O(N)�� N ΪѰ�� pivot �����о�����Ԫ��������
        /// <remarks>
        /// LINSERT key BEFORE|AFTER pivot value http://redis.io/commands/linsert
        /// </remarks>
        /// <returns>
        /// �������ִ�гɹ������ز���������֮���б�ĳ��ȡ����û���ҵ� pivot ������ -1 ����� key �����ڻ�Ϊ���б������ 0 ��
        /// </returns>
        /// </summary>
        long InsertBefore(string keySuffix, string pivot, string value, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �Ƴ��������б� key ��ͷԪ�ء�
        /// ʱ�临�Ӷȣ�O(1)
        /// 
        /// BLPOP ���б������ʽ(blocking)����ԭ�
        /// ���� LPOP ����������汾���������б���û���κ�Ԫ�ؿɹ�������ʱ�����ӽ��� BLPOP ����������ֱ���ȴ���ʱ���ֿɵ���Ԫ��Ϊֹ��
        /// ��������� key ����ʱ�������� key ���Ⱥ�˳�����μ������б��������һ���ǿ��б��ͷԪ�ء�
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// LPOP key http://redis.io/commands/lpop
        /// BLPOP key [key ...] timeout http://redis.io/commands/blpop
        /// </remarks>
        /// <returns>
        /// �б��ͷԪ�ء��� key ������ʱ������ nil ��
        /// ����б�Ϊ�գ�����һ�� nil �����򣬷���һ����������Ԫ�ص��б����һ��Ԫ���Ǳ�����Ԫ������� key ���ڶ���Ԫ���Ǳ�����Ԫ�ص�ֵ��
        /// </returns>
        /// </summary>
        string LeftPop(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ��һ������ֵ value ���뵽�б� key �ı�ͷ
        /// ����ж�� value ֵ����ô���� value ֵ�������ҵ�˳�����β��뵽��ͷ�� ����˵���Կ��б� mylist ִ������ LPUSH mylist a b c ���б��ֵ���� c b a �����ͬ��ԭ���Ե�ִ�� LPUSH mylist a �� LPUSH mylist b �� LPUSH mylist c �������
        /// ��� key �����ڣ�һ�����б�ᱻ������ִ�� LPUSH ������
        /// �� key ���ڵ������б�����ʱ������һ������
        /// ʱ�临�Ӷȣ�O(1)
        /// 
        /// ��ֵ value ���뵽�б� key �ı�ͷ�����ҽ��� key ���ڲ�����һ���б��
        /// �� LPUSH �����෴���� key ������ʱ�� LPUSHX ����ʲôҲ������
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// LPUSH key value [value ...] http://redis.io/commands/lpush
        /// LPUSHX key value http://redis.io/commands/lpushx
        /// </remarks>
        /// <returns>
        /// ִ�� LPUSH ������б�ĳ��ȡ�
        /// LPUSHX ����ִ��֮�󣬱�ĳ��ȡ�
        /// </returns>
        /// </summary>
        long LeftPush(string keySuffix, string value, When when = When.Always, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ��һ������ֵ value ���뵽�б� key �ı�ͷ
        /// ����ж�� value ֵ����ô���� value ֵ�������ҵ�˳�����β��뵽��ͷ�� ����˵���Կ��б� mylist ִ������ LPUSH mylist a b c ���б��ֵ���� c b a �����ͬ��ԭ���Ե�ִ�� LPUSH mylist a �� LPUSH mylist b �� LPUSH mylist c �������
        /// ��� key �����ڣ�һ�����б�ᱻ������ִ�� LPUSH ������
        /// �� key ���ڵ������б�����ʱ������һ������
        /// ʱ�临�Ӷȣ�O(1)
        /// 
        /// ��ֵ value ���뵽�б� key �ı�ͷ�����ҽ��� key ���ڲ�����һ���б��
        /// �� LPUSH �����෴���� key ������ʱ�� LPUSHX ����ʲôҲ������
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// LPUSH key value [value ...] http://redis.io/commands/lpush
        /// LPUSHX key value http://redis.io/commands/lpushx
        /// </remarks>
        /// <returns>
        /// ִ�� LPUSH ������б�ĳ��ȡ�
        /// LPUSHX ����ִ��֮�󣬱�ĳ��ȡ�
        /// </returns>
        /// </summary>
        long LeftPush(string keySuffix, string[] values, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �����б� key �ĳ��ȡ�
        /// ��� key �����ڣ��� key ������Ϊһ�����б������ 0 .
        /// ��� key �����б����ͣ�����һ������
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// LLEN key http://redis.io/commands/llen
        /// </remarks>
        /// <returns>
        /// �б� key �ĳ��ȡ�
        /// </returns>
        /// </summary>
        long Length(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �����б� key ��ָ�������ڵ�Ԫ�أ�������ƫ���� start �� stop ָ����
        /// �±�(index)���� start �� stop ���� 0 Ϊ�ף�Ҳ����˵���� 0 ��ʾ�б�ĵ�һ��Ԫ�أ��� 1 ��ʾ�б�ĵڶ���Ԫ�أ��Դ����ơ�
        /// ��Ҳ����ʹ�ø����±꣬�� -1 ��ʾ�б�����һ��Ԫ�أ� -2 ��ʾ�б�ĵ����ڶ���Ԫ�أ��Դ����ơ�
        /// ע��LRANGE����ͱ���������亯��������
        /// ��������һ������һ�ٸ�Ԫ�ص��б���Ը��б�ִ�� LRANGE list 0 10 �������һ������11��Ԫ�ص��б������� stop �±�Ҳ�� LRANGE �����ȡֵ��Χ֮��(������)�����ĳЩ���Ե����亯�����ܲ�һ�£�����Ruby�� Range.new �� Array#slice ��Python�� range() ������
        /// ������Χ���±�
        /// ������Χ���±�ֵ�����������
        /// ��� start �±���б������±� end(LLEN list ��ȥ 1 )��Ҫ����ô LRANGE ����һ�����б��
        /// ��� stop �±�� end �±껹Ҫ��Redis�� stop ��ֵ����Ϊ end ��
        /// ʱ�临�Ӷ�:O(S+N)�� S Ϊƫ���� start �� N Ϊָ��������Ԫ�ص�������
        /// <remarks>
        /// LRANGE key start stop http://redis.io/commands/lrange
        /// </remarks>
        /// <returns>
        /// һ���б������ָ�������ڵ�Ԫ�ء�
        /// </returns>
        /// </summary>
        string[] Range(string keySuffix, long start = 0, long stop = -1, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ���ݲ��� count ��ֵ���Ƴ��б�������� value ��ȵ�Ԫ�ء�
        /// count ��ֵ���������¼��֣�
        /// count �� 0 : �ӱ�ͷ��ʼ���β�������Ƴ��� value ��ȵ�Ԫ�أ�����Ϊ count ��
        /// count �� 0 : �ӱ�β��ʼ���ͷ�������Ƴ��� value ��ȵ�Ԫ�أ�����Ϊ count �ľ���ֵ��
        /// count = 0 : �Ƴ����������� value ��ȵ�ֵ��
        /// ʱ�临�Ӷȣ�O(N)�� N Ϊ�б�ĳ��ȡ�
        /// <remarks>
        /// LREM key count value http://redis.io/commands/lrem
        /// </remarks>
        /// <returns>
        /// ���Ƴ�Ԫ�ص���������Ϊ�����ڵ� key �������ձ�(empty list)�����Ե� key ������ʱ�� LREM �������Ƿ��� 0 ��
        /// </returns>
        /// </summary>
        long Remove(string keySuffix, string value, long count = 0, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �Ƴ��������б� key ��βԪ�ء�
        /// ʱ�临�Ӷȣ�O(1)
        /// 
        /// BRPOP ���б������ʽ(blocking)����ԭ�
        /// ���� RPOP ����������汾���������б���û���κ�Ԫ�ؿɹ�������ʱ�����ӽ��� BRPOP ����������ֱ���ȴ���ʱ���ֿɵ���Ԫ��Ϊֹ��
        /// ��������� key ����ʱ�������� key ���Ⱥ�˳�����μ������б��������һ���ǿ��б��β��Ԫ�ء�
        /// �������������ĸ�����Ϣ����鿴 BLPOP ��� BRPOP ���˵���Ԫ�ص�λ�ú� BLPOP ��֮ͬ�⣬��������һ�¡�
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// RPOP key http://redis.io/commands/rpop
        /// BRPOP key [key ...] timeout http://redis.io/commands/brpop
        /// </remarks>
        /// <returns>
        /// �б��βԪ�ء��� key ������ʱ������ nil ��
        /// ������ָ��ʱ����û���κ�Ԫ�ر��������򷵻�һ�� nil �͵ȴ�ʱ������֮������һ����������Ԫ�ص��б����һ��Ԫ���Ǳ�����Ԫ������� key ���ڶ���Ԫ���Ǳ�����Ԫ�ص�ֵ��
        /// </returns>
        /// </summary>
        string RightPop(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ���� RPOPLPUSH ��һ��ԭ��ʱ���ڣ�ִ����������������
        /// ���б� source �е����һ��Ԫ��(βԪ��)�����������ظ��ͻ��ˡ�
        /// �� source ������Ԫ�ز��뵽�б� destination ����Ϊ destination �б�ĵ�ͷԪ�ء�
        /// �ٸ����ӣ����������б� source �� destination �� source �б���Ԫ�� a, b, c �� destination �б���Ԫ�� x, y, z ��ִ�� RPOPLPUSH source destination ֮�� source �б����Ԫ�� a, b �� destination �б����Ԫ�� c, x, y, z ������Ԫ�� c �ᱻ���ظ��ͻ��ˡ�
        /// ��� source �����ڣ�ֵ nil �����أ����Ҳ�ִ������������
        /// ��� source �� destination ��ͬ�����б��еı�βԪ�ر��ƶ�����ͷ�������ظ�Ԫ�أ����԰�����������������б����ת(rotation)������
        /// ʱ�临�Ӷȣ�O(1)
        /// 
        /// BRPOPLPUSH �� RPOPLPUSH �������汾���������б� source ��Ϊ��ʱ�� BRPOPLPUSH �ı��ֺ� RPOPLPUSH һ����
        /// ���б� source Ϊ��ʱ�� BRPOPLPUSH ����������ӣ�ֱ���ȴ���ʱ��������һ���ͻ��˶� source ִ�� LPUSH �� RPUSH ����Ϊֹ��
        /// ��ʱ���� timeout ����һ������Ϊ��λ��������Ϊֵ����ʱ������Ϊ 0 ��ʾ����ʱ������������ӳ�(block indefinitely) ��
        /// ���������Ϣ����ο� RPOPLPUSH ���
        /// ʱ�临�Ӷȣ�O(1)
        /// <remarks>
        /// RPOPLPUSH source destination http://redis.io/commands/rpoplpush
        /// BRPOPLPUSH source destination timeout http://redis.io/commands/brpoplpush
        /// </remarks>
        /// <returns>
        /// ��������Ԫ�ء�
        /// ������ָ��ʱ����û���κ�Ԫ�ر��������򷵻�һ�� nil �͵ȴ�ʱ������֮������һ����������Ԫ�ص��б����һ��Ԫ���Ǳ�����Ԫ�ص�ֵ���ڶ���Ԫ���ǵȴ�ʱ����
        /// </returns>
        /// </summary>
        string RightPopLeftPush(string keySuffix, string destination, CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// ��һ������ֵ value ���뵽�б� key �ı�β(���ұ�)��
        /// ����ж�� value ֵ����ô���� value ֵ�������ҵ�˳�����β��뵽��β�������һ�����б� mylist ִ�� RPUSH mylist a b c ���ó��Ľ���б�Ϊ a b c ����ͬ��ִ������ RPUSH mylist a �� RPUSH mylist b �� RPUSH mylist c ��
        /// ��� key �����ڣ�һ�����б�ᱻ������ִ�� RPUSH ������
        /// �� key ���ڵ������б�����ʱ������һ������
        /// ʱ�临�Ӷȣ�O(1)
        /// 
        /// ��ֵ value ���뵽�б� key �ı�β�����ҽ��� key ���ڲ�����һ���б��
        /// �� RPUSH �����෴���� key ������ʱ�� RPUSHX ����ʲôҲ������
        /// <remarks>
        /// RPUSH key value [value ...] http://redis.io/commands/rpush
        /// RPUSHX key value http://redis.io/commands/rpushx
        /// </remarks>
        /// <returns>
        /// ִ�� RPUSH �����󣬱�ĳ��ȡ�
        /// RPUSHX ����ִ��֮�󣬱�ĳ��ȡ�
        /// </returns>
        /// </summary>
        long RightPush(string keySuffix, string value, When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ��һ������ֵ value ���뵽�б� key �ı�β(���ұ�)��
        /// ����ж�� value ֵ����ô���� value ֵ�������ҵ�˳�����β��뵽��β�������һ�����б� mylist ִ�� RPUSH mylist a b c ���ó��Ľ���б�Ϊ a b c ����ͬ��ִ������ RPUSH mylist a �� RPUSH mylist b �� RPUSH mylist c ��
        /// ��� key �����ڣ�һ�����б�ᱻ������ִ�� RPUSH ������
        /// �� key ���ڵ������б�����ʱ������һ������
        /// ʱ�临�Ӷȣ�O(1)
        /// 
        /// ��ֵ value ���뵽�б� key �ı�β�����ҽ��� key ���ڲ�����һ���б��
        /// �� RPUSH �����෴���� key ������ʱ�� RPUSHX ����ʲôҲ������
        /// <remarks>
        /// RPUSH key value [value ...] http://redis.io/commands/rpush
        /// RPUSHX key value http://redis.io/commands/rpushx
        /// </remarks>
        /// <returns>
        /// ִ�� RPUSH �����󣬱�ĳ��ȡ�
        /// RPUSHX ����ִ��֮�󣬱�ĳ��ȡ�
        /// </returns>
        /// </summary>
        long RightPush(string keySuffix, string[] values, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ���б� key �±�Ϊ index ��Ԫ�ص�ֵ����Ϊ value ��
        /// �� index ����������Χ�����һ�����б�(key ������)���� LSET ʱ������һ������
        /// �����б��±�ĸ�����Ϣ����ο� LINDEX ���
        /// ʱ�临�Ӷȣ���ͷԪ�ػ�βԪ�ؽ��� LSET ���������Ӷ�Ϊ O(1)����������£�Ϊ O(N)�� N Ϊ�б�ĳ��ȡ�
        /// <remarks>
        /// LSET key index value http://redis.io/commands/lset
        /// </remarks>
        /// <returns>
        /// �����ɹ����� ok �����򷵻ش�����Ϣ��
        /// </returns>
        /// </summary>
        void SetByIndex(string keySuffix, int index, string value, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ��һ���б�����޼�(trim)������˵�����б�ֻ����ָ�������ڵ�Ԫ�أ�����ָ������֮�ڵ�Ԫ�ض�����ɾ����
        /// �ٸ����ӣ�ִ������ LTRIM list 0 2 ����ʾֻ�����б� list ��ǰ����Ԫ�أ�����Ԫ��ȫ��ɾ����
        /// �±�(index)���� start �� stop ���� 0 Ϊ�ף�Ҳ����˵���� 0 ��ʾ�б�ĵ�һ��Ԫ�أ��� 1 ��ʾ�б�ĵڶ���Ԫ�أ��Դ����ơ�
        /// ��Ҳ����ʹ�ø����±꣬�� -1 ��ʾ�б�����һ��Ԫ�أ� -2 ��ʾ�б�ĵ����ڶ���Ԫ�أ��Դ����ơ�
        /// �� key �����б�����ʱ������һ������
        /// LTRIM ����ͨ���� LPUSH ����� RPUSH �������ʹ�ã��ٸ����ӣ�
        /// LPUSH log newest_log LTRIM log 0 99
        /// �������ģ����һ����־����ÿ�ν�������־ newest_log �ŵ� log �б��У�����ֻ�������µ� 100 �ע�⵱����ʹ�� LTRIM ����ʱ��ʱ�临�Ӷ���O(1)����Ϊƽ������£�ÿ��ֻ��һ��Ԫ�ر��Ƴ���
        /// ע��LTRIM����ͱ���������亯��������
        /// ��������һ������һ�ٸ�Ԫ�ص��б� list ���Ը��б�ִ�� LTRIM list 0 10 �������һ������11��Ԫ�ص��б������� stop �±�Ҳ�� LTRIM �����ȡֵ��Χ֮��(������)�����ĳЩ���Ե����亯�����ܲ�һ�£�����Ruby�� Range.new �� Array#slice ��Python�� range() ������
        /// ������Χ���±�
        /// ������Χ���±�ֵ�����������
        /// ��� start �±���б������±� end(LLEN list ��ȥ 1 )��Ҫ�󣬻��� start > stop �� LTRIM ����һ�����б�(��Ϊ LTRIM �Ѿ��������б����)��
        /// ��� stop �±�� end �±껹Ҫ��Redis�� stop ��ֵ����Ϊ end ��
        /// ʱ�临�Ӷ�:O(N)�� N Ϊ���Ƴ���Ԫ�ص�������
        /// <remarks>
        /// LTRIM key start stop http://redis.io/commands/ltrim
        /// </remarks>
        /// <returns>
        /// ����ִ�гɹ�ʱ������ ok ��
        /// </returns>
        /// </summary>
        void Trim(string keySuffix, long start, long stop, CommandFlags commandFlags = CommandFlags.None);
    }
    /// <summary>
    /// Set�����ϣ�
    /// </summary>
    public interface IRedisSet : IRedisStructure
    {
        /// <summary>
        /// ��һ������ member Ԫ�ؼ��뵽���� key ���У��Ѿ������ڼ��ϵ� member Ԫ�ؽ������ԡ�
        /// ���� key �����ڣ��򴴽�һ��ֻ���� member Ԫ������Ա�ļ��ϡ�
        /// �� key ���Ǽ�������ʱ������һ������
        /// ʱ�临�Ӷ�:O(N)�� N �Ǳ���ӵ�Ԫ�ص�������
        /// <remarks>
        /// SADD key member [member ...] http://redis.io/commands/sadd
        /// </remarks>
        /// <returns>
        /// ����ӵ������е���Ԫ�ص������������������Ե�Ԫ�ء� 
        /// </returns>
        /// </summary>
        bool Add(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ��һ������ member Ԫ�ؼ��뵽���� key ���У��Ѿ������ڼ��ϵ� member Ԫ�ؽ������ԡ�
        /// ���� key �����ڣ��򴴽�һ��ֻ���� member Ԫ������Ա�ļ��ϡ�
        /// �� key ���Ǽ�������ʱ������һ������
        /// ʱ�临�Ӷ�:O(N)�� N �Ǳ���ӵ�Ԫ�ص�������
        /// <remarks>
        /// SADD key member [member ...] http://redis.io/commands/sadd
        /// </remarks>
        /// <returns>
        /// ����ӵ������е���Ԫ�ص������������������Ե�Ԫ�ء� 
        /// </returns>
        /// </summary>
        long Add(string keySuffix, string[] values, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ����һ�����ϵ�ȫ����Ա���ü��������и�������֮��Ĳ��
        /// �����ڵ� key ����Ϊ�ռ���
        /// ʱ�临�Ӷ�:O(N)�� N �����и������ϵĳ�Ա����֮�͡�
        /// 
        /// ����һ�����ϵ�ȫ����Ա���ü��������и������ϵĽ�����
        /// �����ڵ� key ����Ϊ�ռ���
        /// ���������ϵ�����һ���ռ�ʱ�����ҲΪ�ռ�(���ݼ������㶨��)��
        /// ʱ�临�Ӷ�:O(N* M)�� N Ϊ�������ϵ��л�����С�ļ��ϣ� M Ϊ�������ϵĸ�����
        /// 
        /// ����һ�����ϵ�ȫ����Ա���ü��������и������ϵĲ�����
        /// �����ڵ� key ����Ϊ�ռ���
        /// ʱ�临�Ӷ�:O(N)�� N �����и������ϵĳ�Ա����֮�͡�
        /// 
        /// <remarks>
        /// SDIFF key [key ...] http://redis.io/commands/sdiff
        /// SINTER key [key ...] http://redis.io/commands/sinter
        /// SUNION key [key ...] http://redis.io/commands/sunion
        /// </remarks>
        /// <returns>
        /// ���Ա���б��
        /// ������Ա���б��
        /// ������Ա���б��
        /// </returns>
        /// </summary>
        string[] Combine(SetOperation operation, string[] keys, CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// �����������ú� SDIFF ���ƣ�������������浽 destination ���ϣ������Ǽ򵥵ط��ؽ������
        /// ��� destination �����Ѿ����ڣ����串�ǡ�
        /// destination ������ key �����
        /// ʱ�临�Ӷ�:O(N)�� N �����и������ϵĳ�Ա����֮�͡�
        /// 
        /// ������������� SINTER ���������������浽 destination ���ϣ������Ǽ򵥵ط��ؽ������
        /// ��� destination �����Ѿ����ڣ����串�ǡ�
        /// destination ������ key �����
        /// ʱ�临�Ӷ�:O(N* M)�� N Ϊ�������ϵ��л�����С�ļ��ϣ� M Ϊ�������ϵĸ�����
        /// 
        /// ������������� SUNION ���������������浽 destination ���ϣ������Ǽ򵥵ط��ؽ������
        /// ��� destination �Ѿ����ڣ����串�ǡ�
        /// destination ������ key �����
        /// ʱ�临�Ӷ�:O(N)�� N �����и������ϵĳ�Ա����֮�͡�
        /// <remarks>
        /// SDIFFSTORE destination key [key ...] http://redis.io/commands/sdiffstore
        /// SINTERSTORE destination key [key ...] http://redis.io/commands/sinterstore
        /// SUNIONSTORE destination key [key ...] http://redis.io/commands/sunionstore
        /// </remarks>
        /// <returns>
        /// ������е�Ԫ��������
        /// ������е�Ԫ��������
        /// ������е�Ԫ��������
        /// </returns>
        /// </summary>
        long CombineAndStore(SetOperation operation, string destination, string[] keys,
            CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// �ж� member Ԫ���Ƿ񼯺� key �ĳ�Ա��
        /// ʱ�临�Ӷ�:O(1)
        /// <remarks>
        /// SISMEMBER key member http://redis.io/commands/sismember
        /// </remarks>
        /// <returns>
        /// ��� member Ԫ���Ǽ��ϵĳ�Ա������ 1 ����� member Ԫ�ز��Ǽ��ϵĳ�Ա���� key �����ڣ����� 0 ��
        /// </returns>
        /// </summary>
        bool Contains(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ���ؼ��� key �Ļ���(������Ԫ�ص�����)��
        /// ʱ�临�Ӷ�:O(1)
        /// <remarks>
        /// SCARD key http://redis.io/commands/scard
        /// </remarks>
        /// <returns>
        /// ���ϵĻ������� key ������ʱ������ 0 ��
        /// </returns>
        /// </summary>
        long Length(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ���ؼ��� key �е����г�Ա��
        /// �����ڵ� key ����Ϊ�ռ��ϡ�
        /// ʱ�临�Ӷ�:O(N)�� N Ϊ���ϵĻ�����
        /// <remarks>
        /// SMEMBERS key http://redis.io/commands/smembers
        /// </remarks>
        /// <returns>
        /// �����е����г�Ա��
        /// </returns>
        /// </summary>
        string[] Members(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �� member Ԫ�ش� source �����ƶ��� destination ���ϡ�
        /// SMOVE ��ԭ���Բ�����
        /// ��� source ���ϲ����ڻ򲻰���ָ���� member Ԫ�أ��� SMOVE ���ִ���κβ����������� 0 ������ member Ԫ�ش� source �����б��Ƴ�������ӵ� destination ������ȥ��
        /// �� destination �����Ѿ����� member Ԫ��ʱ�� SMOVE ����ֻ�Ǽ򵥵ؽ� source �����е� member Ԫ��ɾ����
        /// �� source �� destination ���Ǽ�������ʱ������һ������
        /// ʱ�临�Ӷ�:O(1)
        /// <remarks>
        /// SMOVE source destination member http://redis.io/commands/smove
        /// </remarks>
        /// <returns>
        /// ��� member Ԫ�ر��ɹ��Ƴ������� 1 ��
        /// ��� member Ԫ�ز��� source ���ϵĳ�Ա������û���κβ����� destination ����ִ�У���ô���� 0 ��</returns>
        /// </summary>
        bool Move(string keySuffix, RedisKey destination, RedisValue value, CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// �Ƴ������ؼ����е�һ�����Ԫ�ء�
        /// ���ֻ���ȡһ�����Ԫ�أ��������Ԫ�شӼ����б��Ƴ��Ļ�������ʹ�� SRANDMEMBER ���
        /// ʱ�临�Ӷ�:O(1)
        /// <remarks>
        /// SPOP key http://redis.io/commands/spop
        /// </remarks>
        /// <returns>
        /// ���Ƴ������Ԫ�ء��� key �����ڻ� key �ǿռ�ʱ������ nil ��
        /// </returns>
        /// </summary>
        string Pop(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �������ִ��ʱ��ֻ�ṩ�� key ��������ô���ؼ����е�һ�����Ԫ�ء�
        /// �� Redis 2.6 �汾��ʼ�� SRANDMEMBER ������ܿ�ѡ�� count ������
        /// ��� count Ϊ��������С�ڼ��ϻ�������ô�����һ������ count ��Ԫ�ص����飬�����е�Ԫ�ظ�����ͬ����� count ���ڵ��ڼ��ϻ�������ô�����������ϡ�
        /// ��� count Ϊ��������ô�����һ�����飬�����е�Ԫ�ؿ��ܻ��ظ����ֶ�Σ�������ĳ���Ϊ count �ľ���ֵ��
        /// �ò����� SPOP ���ƣ��� SPOP �����Ԫ�شӼ������Ƴ������أ��� SRANDMEMBER ������������Ԫ�أ������Լ��Ͻ����κθĶ���
        /// ʱ�临�Ӷ�:ֻ�ṩ key ����ʱΪ O(1) ��
        /// ����ṩ�� count ��������ôΪ O(N) ��N Ϊ���������Ԫ�ظ�����
        /// <remarks>
        /// SRANDMEMBER key [count] http://redis.io/commands/srandmember
        /// </remarks>
        /// <returns>
        /// ֻ�ṩ key ����ʱ������һ��Ԫ�أ��������Ϊ�գ����� nil ��
        /// ����ṩ�� count ��������ô����һ�����飻�������Ϊ�գ����ؿ����顣
        /// </returns>
        /// </summary>
        string RandomMember(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �������ִ��ʱ��ֻ�ṩ�� key ��������ô���ؼ����е�һ�����Ԫ�ء�
        /// �� Redis 2.6 �汾��ʼ�� SRANDMEMBER ������ܿ�ѡ�� count ������
        /// ��� count Ϊ��������С�ڼ��ϻ�������ô�����һ������ count ��Ԫ�ص����飬�����е�Ԫ�ظ�����ͬ����� count ���ڵ��ڼ��ϻ�������ô�����������ϡ�
        /// ��� count Ϊ��������ô�����һ�����飬�����е�Ԫ�ؿ��ܻ��ظ����ֶ�Σ�������ĳ���Ϊ count �ľ���ֵ��
        /// �ò����� SPOP ���ƣ��� SPOP �����Ԫ�شӼ������Ƴ������أ��� SRANDMEMBER ������������Ԫ�أ������Լ��Ͻ����κθĶ���
        /// ʱ�临�Ӷ�:ֻ�ṩ key ����ʱΪ O(1) ��
        /// ����ṩ�� count ��������ôΪ O(N) ��N Ϊ���������Ԫ�ظ�����
        /// <remarks>
        /// SRANDMEMBER key [count] http://redis.io/commands/srandmember
        /// </remarks>
        /// <returns>
        /// ֻ�ṩ key ����ʱ������һ��Ԫ�أ��������Ϊ�գ����� nil ��
        /// ����ṩ�� count ��������ô����һ�����飻�������Ϊ�գ����ؿ����顣
        /// </returns>
        /// </summary>
        string[] RandomMembers(string keySuffix, long count, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �Ƴ����� key �е�һ������ member Ԫ�أ������ڵ� member Ԫ�ػᱻ���ԡ�
        /// �� key ���Ǽ������ͣ�����һ������
        /// ʱ�临�Ӷ�:O(N)�� N Ϊ���� member Ԫ�ص�������
        /// <remarks>
        /// SREM key member [member ...] http://redis.io/commands/srem
        /// </remarks>
        /// <returns>
        /// ���ɹ��Ƴ���Ԫ�ص������������������Ե�Ԫ�ء�
        /// </returns>
        /// </summary>
        bool Remove(string keySuffix, string member, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �Ƴ����� key �е�һ������ member Ԫ�أ������ڵ� member Ԫ�ػᱻ���ԡ�
        /// �� key ���Ǽ������ͣ�����һ������
        /// ʱ�临�Ӷ�:O(N)�� N Ϊ���� member Ԫ�ص�������
        /// <remarks>
        /// SREM key member [member ...] http://redis.io/commands/srem
        /// </remarks>
        /// <returns>
        /// ���ɹ��Ƴ���Ԫ�ص������������������Ե�Ԫ�ء�
        /// </returns>
        /// </summary>
        long Remove(string keySuffix, string[] members, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// http://redis.io/commands/sscan
        /// </summary>
        IEnumerable<RedisValue> Scan(string keySuffix, RedisValue pattern = default(RedisValue), int pageSize = 10,
            long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None);
    }
    /// <summary>
    /// SortedSet�����򼯺ϣ�
    /// 
    /// todo
    /// ZLEXCOUNT key min max
    /// </summary>
    public interface IRedisSortedSet : IRedisStructure
    {
        /// <summary>
        /// ��һ������ member Ԫ�ؼ��� score ֵ���뵽���� key ���С�
        /// ���ĳ�� member �Ѿ������򼯵ĳ�Ա����ô������� member �� score ֵ����ͨ�����²������ member Ԫ�أ�����֤�� member ����ȷ��λ���ϡ�
        /// score ֵ����������ֵ��˫���ȸ�������
        /// ��� key �����ڣ��򴴽�һ���յ����򼯲�ִ�� ZADD ������
        /// �� key ���ڵ�������������ʱ������һ������
        /// ʱ�临�Ӷ�:O(M* log(N))�� N �����򼯵Ļ����� M Ϊ�ɹ���ӵ��³�Ա��������
        /// <remarks>
        /// ZADD key score member [[score member] [score member] ...] http://redis.io/commands/zadd
        /// </remarks>
        /// <returns>
        /// ���ɹ���ӵ��³�Ա����������������Щ�����µġ��Ѿ����ڵĳ�Ա��
        /// </returns>
        /// </summary>
        bool Add(string keySuffix, string value, double score, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ��һ������ member Ԫ�ؼ��� score ֵ���뵽���� key ���С�
        /// ���ĳ�� member �Ѿ������򼯵ĳ�Ա����ô������� member �� score ֵ����ͨ�����²������ member Ԫ�أ�����֤�� member ����ȷ��λ���ϡ�
        /// score ֵ����������ֵ��˫���ȸ�������
        /// ��� key �����ڣ��򴴽�һ���յ����򼯲�ִ�� ZADD ������
        /// �� key ���ڵ�������������ʱ������һ������
        /// ʱ�临�Ӷ�:O(M* log(N))�� N �����򼯵Ļ����� M Ϊ�ɹ���ӵ��³�Ա��������
        /// <remarks>
        /// ZADD key score member [[score member] [score member] ...] http://redis.io/commands/zadd
        /// </remarks>
        /// <returns>
        /// ���ɹ���ӵ��³�Ա����������������Щ�����µġ��Ѿ����ڵĳ�Ա��
        /// </returns>
        /// </summary>
        long Add(string keySuffix, IDictionary<string, double> values, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ���������һ���������򼯵Ĳ��������и��� key ������������ numkeys ����ָ���������ò���(�����)���浽 destination ��
        /// Ĭ������£��������ĳ����Ա�� score ֵ�����и������¸ó�Ա score ֵ֮�� ��
        /// WEIGHTS
        /// ʹ�� WEIGHTS ѡ������Ϊ ÿ�� �������� �ֱ� ָ��һ���˷�����(multiplication factor)��ÿ���������򼯵����г�Ա�� score ֵ�ڴ��ݸ��ۺϺ���(aggregation function)֮ǰ��Ҫ�ȳ��Ը����򼯵����ӡ�
        /// ���û��ָ�� WEIGHTS ѡ��˷�����Ĭ������Ϊ 1 ��
        /// AGGREGATE
        /// ʹ�� AGGREGATE ѡ������ָ�������Ľ�����ľۺϷ�ʽ��
        /// Ĭ��ʹ�õĲ��� SUM �����Խ����м�����ĳ����Ա�� score ֵ֮ �� ��Ϊ������иó�Ա�� score ֵ��ʹ�ò��� MIN �����Խ����м�����ĳ����Ա�� ��С score ֵ��Ϊ������иó�Ա�� score ֵ�������� MAX ���ǽ����м�����ĳ����Ա�� ��� score ֵ��Ϊ������иó�Ա�� score ֵ��
        /// ʱ�临�Ӷ�:O(N)+O(M log(M))�� N Ϊ�������򼯻������ܺͣ� M Ϊ������Ļ�����
        /// 
        /// ���������һ���������򼯵Ľ��������и��� key ������������ numkeys ����ָ���������ý���(�����)���浽 destination ��
        /// Ĭ������£��������ĳ����Ա�� score ֵ�����и������¸ó�Ա score ֵ֮�� ��
        /// ���� WEIGHTS �� AGGREGATE ѡ����������μ� ZUNIONSTORE ���
        /// ʱ�临�Ӷ�:O(N* K)+O(M* log(M))�� N Ϊ���� key �л�����С�����򼯣� K Ϊ�������򼯵������� M Ϊ������Ļ�����
        /// <remarks>
        /// ZUNIONSTORE destination numkeys key [key ...] [WEIGHTS weight [weight ...]] [AGGREGATE SUM|MIN|MAX] http://redis.io/commands/zunionstore
        /// ZINTERSTORE destination numkeys key [key ...] [WEIGHTS weight [weight ...]] [AGGREGATE SUM|MIN|MAX] http://redis.io/commands/zinterstore
        /// </remarks>
        /// <returns>
        /// ���浽 destination �Ľ�����Ļ�����
        /// ���浽 destination �Ľ�����Ļ�����
        /// </returns>
        /// </summary>
        long CombineAndStore(SetOperation operation, string destination, string[] keys,
            double[] weights = null, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// Ϊ���� key �ĳ�Ա member �� score ֵ�������� increment ��
        /// ����ͨ������һ������ֵ increment ���� score ��ȥ��Ӧ��ֵ������ ZINCRBY key -5 member �������� member �� score ֵ��ȥ 5 ��
        /// �� key �����ڣ��� member ���� key �ĳ�Աʱ�� ZINCRBY key increment member ��ͬ�� ZADD key increment member ��
        /// �� key ������������ʱ������һ������
        /// score ֵ����������ֵ��˫���ȸ�������
        /// ʱ�临�Ӷ�:O(log(N))
        /// <remarks>
        /// ZINCRBY key increment member http://redis.io/commands/zincrby
        /// </remarks>
        /// <returns>
        /// member ��Ա���� score ֵ�����ַ�����ʽ��ʾ��
        /// </returns>
        /// </summary>
        double Decrement(string keySuffix, string member, double value, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// Ϊ���� key �ĳ�Ա member �� score ֵ�������� increment ��
        /// ����ͨ������һ������ֵ increment ���� score ��ȥ��Ӧ��ֵ������ ZINCRBY key -5 member �������� member �� score ֵ��ȥ 5 ��
        /// �� key �����ڣ��� member ���� key �ĳ�Աʱ�� ZINCRBY key increment member ��ͬ�� ZADD key increment member ��
        /// �� key ������������ʱ������һ������
        /// score ֵ����������ֵ��˫���ȸ�������
        /// ʱ�临�Ӷ�:O(log(N))
        /// <remarks>
        /// ZINCRBY key increment member http://redis.io/commands/zincrby
        /// </remarks>
        /// <returns>
        /// member ��Ա���� score ֵ�����ַ�����ʽ��ʾ��
        /// </returns>
        /// </summary>
        double Increment(string keySuffix, string member, double value, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �������� key �Ļ�����
        /// ʱ�临�Ӷ�:O(1)
        /// �������� key �У� score ֵ�� min �� max ֮��(Ĭ�ϰ��� score ֵ���� min �� max )�ĳ�Ա��������
        /// ���ڲ��� min �� max ����ϸʹ�÷�������ο� ZRANGEBYSCORE ���
        /// ʱ�临�Ӷ�:O(log(N))�� N Ϊ���򼯵Ļ�����
        /// <remarks>
        /// ZCARD key http://redis.io/commands/zcard
        /// ZCOUNT key min max http://redis.io/commands/zcount
        /// </remarks>
        /// <returns>
        /// zcard����ֵ:�� key ����������������ʱ���������򼯵Ļ������� key ������ʱ������ 0 ��
        /// zcount����ֵ:score ֵ�� min �� max ֮��ĳ�Ա��������
        /// </returns>
        /// </summary>
        long Length(string keySuffix, double min = double.NegativeInfinity, double max = double.PositiveInfinity,
            Exclude exclude = Exclude.None, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �������� key �У�ָ�������ڵĳ�Ա��
        /// ���г�Ա��λ�ð� score ֵ����(��С����)������
        /// ������ͬ score ֵ�ĳ�Ա���ֵ���(lexicographical order)�����С�
        /// �������Ҫ��Ա�� score ֵ�ݼ�(�Ӵ�С)�����У���ʹ�� ZREVRANGE ���
        /// �±���� start �� stop ���� 0 Ϊ�ף�Ҳ����˵���� 0 ��ʾ���򼯵�һ����Ա���� 1 ��ʾ���򼯵ڶ�����Ա���Դ����ơ�
        /// ��Ҳ����ʹ�ø����±꣬�� -1 ��ʾ���һ����Ա�� -2 ��ʾ�����ڶ�����Ա���Դ����ơ�
        /// ������Χ���±겢�����������
        /// ����˵���� start ��ֵ�����򼯵�����±껹Ҫ�󣬻��� start > stop ʱ�� ZRANGE ����ֻ�Ǽ򵥵ط���һ�����б��
        /// ��һ���棬���� stop ������ֵ�����򼯵�����±껹Ҫ����ô Redis �� stop ��������±��������
        /// ����ͨ��ʹ�� WITHSCORES ѡ����ó�Ա������ score ֵһ�����أ������б��� value1, score1, ..., valueN, scoreN �ĸ�ʽ��ʾ��
        /// �ͻ��˿���ܻ᷵��һЩ�����ӵ��������ͣ��������顢Ԫ��ȡ�
        /// ʱ�临�Ӷ�:O(log(N)+M)�� N Ϊ���򼯵Ļ������� M Ϊ������Ļ�����
        /// 
        /// �������� key �У�ָ�������ڵĳ�Ա��
        /// ���г�Ա��λ�ð� score ֵ�ݼ�(�Ӵ�С)�����С�
        /// ������ͬ score ֵ�ĳ�Ա���ֵ��������(reverse lexicographical order)���С�
        /// ���˳�Ա�� score ֵ�ݼ��Ĵ���������һ���⣬ ZREVRANGE �������������� ZRANGE ����һ��
        /// ʱ�临�Ӷ�:O(log(N)+M)�� N Ϊ���򼯵Ļ������� M Ϊ������Ļ���
        /// <remarks>
        /// ZRANGE key start stop [WITHSCORES] http://redis.io/commands/zrange
        /// ZREVRANGE key start stop [WITHSCORES]  http://redis.io/commands/zrevrange
        /// </remarks>
        /// <returns>
        /// ����ֵ:ָ�������ڣ����� score ֵ(��ѡ)�����򼯳�Ա���б��
        /// ����ֵ:ָ�������ڣ����� score ֵ(��ѡ)�����򼯳�Ա���б��
        /// </returns>
        /// </summary>
        string[] RangeByRank(string keySuffix, long start = 0, long stop = -1, Order order = Order.Ascending,
            CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// �������� key �У�ָ�������ڵĳ�Ա��
        /// ���г�Ա��λ�ð� score ֵ����(��С����)������
        /// ������ͬ score ֵ�ĳ�Ա���ֵ���(lexicographical order)�����С�
        /// �������Ҫ��Ա�� score ֵ�ݼ�(�Ӵ�С)�����У���ʹ�� ZREVRANGE ���
        /// �±���� start �� stop ���� 0 Ϊ�ף�Ҳ����˵���� 0 ��ʾ���򼯵�һ����Ա���� 1 ��ʾ���򼯵ڶ�����Ա���Դ����ơ�
        /// ��Ҳ����ʹ�ø����±꣬�� -1 ��ʾ���һ����Ա�� -2 ��ʾ�����ڶ�����Ա���Դ����ơ�
        /// ������Χ���±겢�����������
        /// ����˵���� start ��ֵ�����򼯵�����±껹Ҫ�󣬻��� start > stop ʱ�� ZRANGE ����ֻ�Ǽ򵥵ط���һ�����б��
        /// ��һ���棬���� stop ������ֵ�����򼯵�����±껹Ҫ����ô Redis �� stop ��������±��������
        /// ����ͨ��ʹ�� WITHSCORES ѡ����ó�Ա������ score ֵһ�����أ������б��� value1, score1, ..., valueN, scoreN �ĸ�ʽ��ʾ��
        /// �ͻ��˿���ܻ᷵��һЩ�����ӵ��������ͣ��������顢Ԫ��ȡ�
        /// ʱ�临�Ӷ�:O(log(N)+M)�� N Ϊ���򼯵Ļ������� M Ϊ������Ļ�����
        /// 
        /// �������� key �У�ָ�������ڵĳ�Ա��
        /// ���г�Ա��λ�ð� score ֵ�ݼ�(�Ӵ�С)�����С�
        /// ������ͬ score ֵ�ĳ�Ա���ֵ��������(reverse lexicographical order)���С�
        /// ���˳�Ա�� score ֵ�ݼ��Ĵ���������һ���⣬ ZREVRANGE �������������� ZRANGE ����һ��
        /// ʱ�临�Ӷ�:O(log(N)+M)�� N Ϊ���򼯵Ļ������� M Ϊ������Ļ���
        /// <remarks>
        /// ZRANGE key start stop [WITHSCORES] http://redis.io/commands/zrange
        /// ZREVRANGE key start stop [WITHSCORES]  http://redis.io/commands/zrevrange
        /// </remarks>
        /// <returns>
        /// ����ֵ:ָ�������ڣ����� score ֵ(��ѡ)�����򼯳�Ա���б��
        /// ����ֵ:ָ�������ڣ����� score ֵ(��ѡ)�����򼯳�Ա���б��
        /// </returns>
        /// </summary>
        SortedSetEntry[] RangeByRankWithScores(string keySuffix, long start = 0, long stop = -1,
            Order order = Order.Ascending, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �������� key �У�ָ�������ڵĳ�Ա��
        /// ���г�Ա��λ�ð� score ֵ����(��С����)������
        /// ������ͬ score ֵ�ĳ�Ա���ֵ���(lexicographical order)�����С�
        /// �������Ҫ��Ա�� score ֵ�ݼ�(�Ӵ�С)�����У���ʹ�� ZREVRANGE ���
        /// �±���� start �� stop ���� 0 Ϊ�ף�Ҳ����˵���� 0 ��ʾ���򼯵�һ����Ա���� 1 ��ʾ���򼯵ڶ�����Ա���Դ����ơ�
        /// ��Ҳ����ʹ�ø����±꣬�� -1 ��ʾ���һ����Ա�� -2 ��ʾ�����ڶ�����Ա���Դ����ơ�
        /// ������Χ���±겢�����������
        /// ����˵���� start ��ֵ�����򼯵�����±껹Ҫ�󣬻��� start > stop ʱ�� ZRANGE ����ֻ�Ǽ򵥵ط���һ�����б��
        /// ��һ���棬���� stop ������ֵ�����򼯵�����±껹Ҫ����ô Redis �� stop ��������±��������
        /// ����ͨ��ʹ�� WITHSCORES ѡ����ó�Ա������ score ֵһ�����أ������б��� value1, score1, ..., valueN, scoreN �ĸ�ʽ��ʾ��
        /// �ͻ��˿���ܻ᷵��һЩ�����ӵ��������ͣ��������顢Ԫ��ȡ�
        /// ʱ�临�Ӷ�:O(log(N)+M)�� N Ϊ���򼯵Ļ������� M Ϊ������Ļ�����
        /// 
        /// �������� key �У�ָ�������ڵĳ�Ա��
        /// ���г�Ա��λ�ð� score ֵ�ݼ�(�Ӵ�С)�����С�
        /// ������ͬ score ֵ�ĳ�Ա���ֵ��������(reverse lexicographical order)���С�
        /// ���˳�Ա�� score ֵ�ݼ��Ĵ���������һ���⣬ ZREVRANGE �������������� ZRANGE ����һ��
        /// ʱ�临�Ӷ�:O(log(N)+M)�� N Ϊ���򼯵Ļ������� M Ϊ������Ļ���
        /// <remarks>
        /// ZRANGE key start stop [WITHSCORES] http://redis.io/commands/zrange
        /// ZREVRANGE key start stop [WITHSCORES]  http://redis.io/commands/zrevrange
        /// </remarks>
        /// <returns>
        /// ����ֵ:ָ�������ڣ����� score ֵ(��ѡ)�����򼯳�Ա���б��
        /// ����ֵ:ָ�������ڣ����� score ֵ(��ѡ)�����򼯳�Ա���б��
        /// </returns>
        /// </summary>
        Tuple<string, double, double>[] RangeByRankWithScoresAndRank(string keySuffix, long start = 0, long stop = -1,
            Order order = Order.Ascending, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// �������� key �У����� score ֵ���� min �� max ֮��(�������� min �� max )�ĳ�Ա�����򼯳�Ա�� score ֵ����(��С����)�������С�
        /// ������ͬ score ֵ�ĳ�Ա���ֵ���(lexicographical order)������(�������������ṩ�ģ�����Ҫ����ļ���)��
        /// ��ѡ�� LIMIT ����ָ�����ؽ��������������(����SQL�е� SELECT LIMIT offset, count )��ע�⵱ offset �ܴ�ʱ����λ offset �Ĳ���������Ҫ�����������򼯣��˹�������Ӷ�Ϊ O(N) ʱ�䡣
        /// ��ѡ�� WITHSCORES ��������������ǵ����������򼯵ĳ�Ա�����ǽ����򼯳�Ա���� score ֵһ�𷵻ء�
        /// ��ѡ���� Redis 2.0 �汾����á�
        /// ���估����
        /// min �� max ������ -inf �� +inf ������һ������Ϳ����ڲ�֪�����򼯵���ͺ���� score ֵ������£�ʹ�� ZRANGEBYSCORE �������
        /// Ĭ������£������ȡֵʹ�ñ�����(С�ڵ��ڻ���ڵ���)����Ҳ����ͨ��������ǰ����(������ʹ�ÿ�ѡ�Ŀ����� (С�ڻ����)��
        /// �ٸ����ӣ�
        /// ZRANGEBYSCORE zset(1 5 �������з������� 1 ��score ��= 5 �ĳ�Ա����ZRANGEBYSCORE zset (5 (10 �򷵻����з������� 5 �� score�� 10 �ĳ�Ա��
        /// ʱ�临�Ӷ�:O(log(N)+M)�� N Ϊ���򼯵Ļ����� M Ϊ��������Ļ�����
        /// 
        /// �������� key �У� score ֵ���� max �� min ֮��(Ĭ�ϰ������� max �� min )�����еĳ�Ա�����򼯳�Ա�� score ֵ�ݼ�(�Ӵ�С)�Ĵ������С�
        /// ������ͬ score ֵ�ĳ�Ա���ֵ��������(reverse lexicographical order )���С�
        /// ���˳�Ա�� score ֵ�ݼ��Ĵ���������һ���⣬ ZREVRANGEBYSCORE �������������� ZRANGEBYSCORE ����һ����
        /// ʱ�临�Ӷ�:O(log(N)+M)�� N Ϊ���򼯵Ļ����� M Ϊ������Ļ�����
        /// <remarks>
        /// ZRANGEBYSCORE key min max [WITHSCORES] [LIMIT offset count] http://redis.io/commands/zrangebyscore
        /// ZREVRANGEBYSCORE key max min [WITHSCORES] [LIMIT offset count]  http://redis.io/commands/zrevrangebyscore
        /// </remarks>
        /// <returns>
        /// ����ֵ:ָ�������ڣ����� score ֵ(��ѡ)�����򼯳�Ա���б��
        /// ����ֵ:ָ�������ڣ����� score ֵ(��ѡ)�����򼯳�Ա���б��
        /// </returns>
        /// </summary>
        string[] RangeByScore(string keySuffix, double start = double.NegativeInfinity,
            double stop = double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending,
            long skip = 0, long take = -1, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �������� key �У����� score ֵ���� min �� max ֮��(�������� min �� max )�ĳ�Ա�����򼯳�Ա�� score ֵ����(��С����)�������С�
        /// ������ͬ score ֵ�ĳ�Ա���ֵ���(lexicographical order)������(�������������ṩ�ģ�����Ҫ����ļ���)��
        /// ��ѡ�� LIMIT ����ָ�����ؽ��������������(����SQL�е� SELECT LIMIT offset, count )��ע�⵱ offset �ܴ�ʱ����λ offset �Ĳ���������Ҫ�����������򼯣��˹�������Ӷ�Ϊ O(N) ʱ�䡣
        /// ��ѡ�� WITHSCORES ��������������ǵ����������򼯵ĳ�Ա�����ǽ����򼯳�Ա���� score ֵһ�𷵻ء�
        /// ��ѡ���� Redis 2.0 �汾����á�
        /// ���估����
        /// min �� max ������ -inf �� +inf ������һ������Ϳ����ڲ�֪�����򼯵���ͺ���� score ֵ������£�ʹ�� ZRANGEBYSCORE �������
        /// Ĭ������£������ȡֵʹ�ñ�����(С�ڵ��ڻ���ڵ���)����Ҳ����ͨ��������ǰ����(������ʹ�ÿ�ѡ�Ŀ����� (С�ڻ����)��
        /// �ٸ����ӣ�
        /// ZRANGEBYSCORE zset(1 5 �������з������� 1 ��score ��= 5 �ĳ�Ա����ZRANGEBYSCORE zset (5 (10 �򷵻����з������� 5 �� score�� 10 �ĳ�Ա��
        /// ʱ�临�Ӷ�:O(log(N)+M)�� N Ϊ���򼯵Ļ����� M Ϊ��������Ļ�����
        /// 
        /// �������� key �У� score ֵ���� max �� min ֮��(Ĭ�ϰ������� max �� min )�����еĳ�Ա�����򼯳�Ա�� score ֵ�ݼ�(�Ӵ�С)�Ĵ������С�
        /// ������ͬ score ֵ�ĳ�Ա���ֵ��������(reverse lexicographical order )���С�
        /// ���˳�Ա�� score ֵ�ݼ��Ĵ���������һ���⣬ ZREVRANGEBYSCORE �������������� ZRANGEBYSCORE ����һ����
        /// ʱ�临�Ӷ�:O(log(N)+M)�� N Ϊ���򼯵Ļ����� M Ϊ������Ļ�����
        /// <remarks>
        /// ZRANGEBYSCORE key min max [WITHSCORES] [LIMIT offset count] http://redis.io/commands/zrangebyscore
        /// ZREVRANGEBYSCORE key max min [WITHSCORES] [LIMIT offset count]  http://redis.io/commands/zrevrangebyscore
        /// </remarks>
        /// <returns>
        /// ����ֵ:ָ�������ڣ����� score ֵ(��ѡ)�����򼯳�Ա���б��
        /// ����ֵ:ָ�������ڣ����� score ֵ(��ѡ)�����򼯳�Ա���б��
        /// </returns>
        /// </summary>
        IDictionary<string, double> RangeByScoreWithScores(string keySuffix, double start = double.NegativeInfinity,
            double stop = double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending,
            long skip = 0, long take = -1, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �����򼯺ϵ����г�Ա��������ͬ�ķ�ֵʱ�� ���򼯺ϵ�Ԫ�ػ���ݳ�Ա���ֵ���lexicographical ordering������������ �������������Է��ظ��������򼯺ϼ� key �У� ֵ���� min �� max ֮��ĳ�Ա��
        /// ������򼯺�����ĳ�Ա���в�ͬ�ķ�ֵ�� ��ô����صĽ����δָ���ģ�unspecified����
        /// �����ʹ�� C ���Ե� memcmp() ������ �Լ����е�ÿ����Ա��������ֽڵĶԱȣ�byte-by-byte compare���� �����մӵ͵��ߵ�˳�� ���������ļ��ϳ�Ա�� ��������ַ�����һ������������ͬ�Ļ��� ��ô�������Ϊ�ϳ����ַ����Ƚ϶̵��ַ���Ҫ��
        /// ��ѡ�� LIMIT offset count �������ڻ�ȡָ����Χ�ڵ�ƥ��Ԫ�� ������ SQL �е� SELECT LIMIT offset count ��䣩�� ��Ҫע���һ���ǣ� ��� offset ������ֵ�ǳ���Ļ��� ��ô�����ڷ��ؽ��֮ǰ�� ��Ҫ�ȱ����� offset ��ָ����λ�ã� ���������Ϊ���������� O(N) ���Ӷȡ�
        /// ���ָ����Χ����
        /// �Ϸ��� min �� max �����������(����[ �� ����(��ʾ�����䣨ָ����ֵ���ᱻ�����ڷ�Χ֮�ڣ��� ��[���ʾ�����䣨ָ����ֵ�ᱻ�����ڷ�Χ֮�ڣ���
        /// 
        /// ����ֵ + �� - �� min �����Լ� max �����о�����������壬 ���� + ��ʾ�����ޣ� �� - ��ʾ�����ޡ� ��ˣ� ��һ�����г�Ա�ķ�ֵ����ͬ�����򼯺Ϸ������� ZRANGEBYLEX �� zset �� -+ �� ����������򼯺��е�����Ԫ�ء�
        /// ʱ�临�Ӷȣ�O(log(N)+M)�� ���� N Ϊ���򼯺ϵ�Ԫ�������� �� M ��������ص�Ԫ�������� ��� M ��һ������������˵���û�����ʹ�� LIMIT �������������ȵ� 10 ��Ԫ�أ��� ��ô����ĸ��Ӷ�Ҳ���Կ����� O(log(N)) ��
        /// <remarks>
        /// ZRANGEBYLEX key min max [LIMIT offset count] http://redis.io/commands/zrangebylex
        /// </remarks>
        /// <returns>
        /// ����ظ���һ���б���б�������������򼯺���ָ����Χ�ڵĳ�Ա��
        /// </returns>
        /// </summary>
        string[] RangeByValue(string keySuffix, string min, string max, Exclude exclude = Exclude.None, long skip = 0,
            long take = -1, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �������� key �г�Ա member ���������������򼯳�Ա�� score ֵ����(��С����)˳�����С�
        /// ������ 0 Ϊ�ף�Ҳ����˵�� score ֵ��С�ĳ�Ա����Ϊ 0 ��
        /// ʹ�� ZREVRANK ������Ի�ó�Ա�� score ֵ�ݼ�(�Ӵ�С)���е�������
        /// ʱ�临�Ӷ�:O(log(N))
        /// 
        /// �������� key �г�Ա member ���������������򼯳�Ա�� score ֵ�ݼ�(�Ӵ�С)����
        /// ������ 0 Ϊ�ף�Ҳ����˵�� score ֵ���ĳ�Ա����Ϊ 0 ��
        /// ʹ�� ZRANK ������Ի�ó�Ա�� score ֵ����(��С����)���е�������
        /// ʱ�临�Ӷ�:O(log(N))
        /// <remarks>
        /// ZRANK key member http://redis.io/commands/zrank
        /// ZREVRANK key member http://redis.io/commands/zrevrank
        /// </remarks>
        /// <returns>
        /// ��� member ������ key �ĳ�Ա������ member ����������� member �������� key �ĳ�Ա������ nil ��
        /// ��� member ������ key �ĳ�Ա������ member ����������� member �������� key �ĳ�Ա������ nil ��
        /// </returns>
        /// </summary>
        long? Rank(string keySuffix, string member, Order order = Order.Ascending,
            CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �Ƴ����� key �е�һ��������Ա�������ڵĳ�Ա�������ԡ�
        /// �� key ���ڵ�������������ʱ������һ������
        /// ʱ�临�Ӷ�:O(M* log(N))�� N Ϊ���򼯵Ļ����� M Ϊ���ɹ��Ƴ��ĳ�Ա��������
        /// <remarks>
        /// ZREM key member [member ...] http://redis.io/commands/zrem
        /// </remarks>
        /// <returns>
        /// ���ɹ��Ƴ��ĳ�Ա�������������������Եĳ�Ա��
        /// </returns>
        /// </summary>
        bool Remove(string keySuffix, string member, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �Ƴ����� key �е�һ��������Ա�������ڵĳ�Ա�������ԡ�
        /// �� key ���ڵ�������������ʱ������һ������
        /// ʱ�临�Ӷ�:O(M* log(N))�� N Ϊ���򼯵Ļ����� M Ϊ���ɹ��Ƴ��ĳ�Ա��������
        /// <remarks>
        /// ZREM key member [member ...] http://redis.io/commands/zrem
        /// </remarks>
        /// <returns>
        /// ���ɹ��Ƴ��ĳ�Ա�������������������Եĳ�Ա��
        /// </returns>
        /// </summary>
        long Remove(string keySuffix, string[] members, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �Ƴ����� key �У�ָ������(rank)�����ڵ����г�Ա��
        /// ����ֱ����±���� start �� stop ָ�������� start �� stop ���ڡ�
        /// �±���� start �� stop ���� 0 Ϊ�ף�Ҳ����˵���� 0 ��ʾ���򼯵�һ����Ա���� 1 ��ʾ���򼯵ڶ�����Ա���Դ����ơ�
        /// ��Ҳ����ʹ�ø����±꣬�� -1 ��ʾ���һ����Ա�� -2 ��ʾ�����ڶ�����Ա���Դ����ơ�
        /// ʱ�临�Ӷ�:O(log(N)+M)�� N Ϊ���򼯵Ļ������� M Ϊ���Ƴ���Ա��������
        /// <remarks>
        /// ZREMRANGEBYRANK key start stop http://redis.io/commands/zremrangebyrank
        /// </remarks>
        /// <returns>
        /// ���Ƴ���Ա��������
        /// </returns>
        /// </summary>
        long RemoveRangeByRank(string keySuffix, long start, long stop, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �Ƴ����� key �У����� score ֵ���� min �� max ֮��(�������� min �� max )�ĳ�Ա��
        /// �԰汾2.1.6��ʼ�� score ֵ���� min �� max �ĳ�ԱҲ���Բ��������ڣ�������μ� ZRANGEBYSCORE ���
        /// ʱ�临�Ӷ�:O(log(N)+M)�� N Ϊ���򼯵Ļ������� M Ϊ���Ƴ���Ա��������
        /// <remarks>
        /// ZREMRANGEBYSCORE key min max http://redis.io/commands/zremrangebyscore
        /// </remarks>
        /// <returns>
        /// ���Ƴ���Ա��������
        /// </returns>
        /// </summary>
        long RemoveRangeByScore(string keySuffix, double start, double stop, Exclude exclude = Exclude.None,
            CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ����һ�����г�Ա�ķ�ֵ����ͬ�����򼯺ϼ� key ��˵�� ���������Ƴ��ü����У� ��Ա���� min �� max ��Χ�ڵ�����Ԫ�ء�
        /// �������� min ������ max ����������� ZRANGEBYLEX ����� min ������ max ����������һ����
        /// ʱ�临�Ӷȣ�O(log(N)+M)�� ���� N Ϊ���򼯺ϵ�Ԫ�������� �� M ��Ϊ���Ƴ���Ԫ��������
        /// <remarks>
        /// ZREMRANGEBYLEX key min max http://redis.io/commands/zremrangebylex
        /// </remarks>
        /// <returns>
        /// �����ظ������Ƴ���Ԫ��������
        /// </returns>
        /// </summary>
        long RemoveRangeByScore(string keySuffix, string min, string max, Exclude exclude = Exclude.None,
            CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// http://redis.io/commands/zscan
        /// </summary>
        IEnumerable<SortedSetEntry> Scan(string keySuffix, RedisValue pattern = default(RedisValue), int pageSize = 10,
            long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// �������� key �У���Ա member �� score ֵ��
        /// ��� member Ԫ�ز������� key �ĳ�Ա���� key �����ڣ����� nil ��
        /// ʱ�临�Ӷ�:O(1)
        /// <remarks>
        /// ZSCORE key member http://redis.io/commands/zscore
        /// </remarks>
        /// <returns>
        /// member ��Ա�� score ֵ�����ַ�����ʽ��ʾ��
        /// </returns>
        /// </summary>
        double Score(string keySuffix, string member, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// Get Rank and Score include ZRANK, ZSCORE. If not found return value is null.
        /// </summary>
        Tuple<double, long> Get(string keySuffix, string member, Order rankOrder = Order.Ascending,
            CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// LUA Script including zincrby, zadd
        /// </summary>
        double IncrementLimitByMax(string keySuffix, string member, double value, double max,
            CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// LUA Script including zincrby, zadd
        /// </summary>
        double IncrementLimitByMin(string keySuffix, string member, double value, double min,
            CommandFlags commandFlags = CommandFlags.None);
    }
    /// <summary>
    /// HyperLogLog
    /// </summary>
    public interface IRedisHyperLogLog : IRedisStructure
    {
        /// <summary>
        /// ������������Ԫ����ӵ�ָ���� HyperLogLog ���档
        /// ��Ϊ�������ĸ����ã� HyperLogLog �ڲ����ܻᱻ���£� �Ա㷴ӳһ����ͬ��ΨһԪ�ع���������Ҳ���Ǽ��ϵĻ�������
        /// ��� HyperLogLog ���ƵĽ��ƻ�����approximated cardinality��������ִ��֮������˱仯�� ��ô����� 1 �� ���򷵻� 0 �� �������ִ��ʱ�����ļ������ڣ� ��ô�����ȴ���һ���յ� HyperLogLog �ṹ�� Ȼ����ִ�����
        /// ���� PFADD ����ʱ����ֻ����������������Ԫ�أ�
        /// ����������Ѿ���һ�� HyperLogLog �� ��ô���ֵ��ò�������κ�Ч����
        /// ����������ļ������ڣ� ��ô����ᴴ��һ���յ� HyperLogLog �� ����ͻ��˷��� 1 ��
        /// Ҫ�˽������� HyperLogLog ���ݽṹ�Ľ���֪ʶ�� ����� PFCOUNT ������ĵ���
        /// ʱ�临�Ӷȣ�ÿ���һ��Ԫ�صĸ��Ӷ�Ϊ O(1) ��
        /// <remarks>
        /// PFADD key element [element ...] http://redis.io/commands/pfadd
        /// </remarks>
        /// <returns>
        /// �����ظ��� ��� HyperLogLog ���ڲ����汻�޸��ˣ� ��ô���� 1 �� ���򷵻� 0 ��
        /// </returns>
        /// </summary>
        bool Add(string keySuffix, string value, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ������������Ԫ����ӵ�ָ���� HyperLogLog ���档
        /// ��Ϊ�������ĸ����ã� HyperLogLog �ڲ����ܻᱻ���£� �Ա㷴ӳһ����ͬ��ΨһԪ�ع���������Ҳ���Ǽ��ϵĻ�������
        /// ��� HyperLogLog ���ƵĽ��ƻ�����approximated cardinality��������ִ��֮������˱仯�� ��ô����� 1 �� ���򷵻� 0 �� �������ִ��ʱ�����ļ������ڣ� ��ô�����ȴ���һ���յ� HyperLogLog �ṹ�� Ȼ����ִ�����
        /// ���� PFADD ����ʱ����ֻ����������������Ԫ�أ�
        /// ����������Ѿ���һ�� HyperLogLog �� ��ô���ֵ��ò�������κ�Ч����
        /// ����������ļ������ڣ� ��ô����ᴴ��һ���յ� HyperLogLog �� ����ͻ��˷��� 1 ��
        /// Ҫ�˽������� HyperLogLog ���ݽṹ�Ľ���֪ʶ�� ����� PFCOUNT ������ĵ���
        /// ʱ�临�Ӷȣ�ÿ���һ��Ԫ�صĸ��Ӷ�Ϊ O(1) ��
        /// <remarks>
        /// PFADD key element [element ...] http://redis.io/commands/pfadd
        /// </remarks>
        /// <returns>
        /// �����ظ��� ��� HyperLogLog ���ڲ����汻�޸��ˣ� ��ô���� 1 �� ���򷵻� 0 ��
        /// </returns>
        /// </summary>
        bool Add(string keySuffix, string[] values, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// �� PFCOUNT ���������ڵ�����ʱ�� ���ش����ڸ������� HyperLogLog �Ľ��ƻ����� ����������ڣ� ��ô���� 0 ��
        /// �� PFCOUNT ���������ڶ����ʱ�� �������и��� HyperLogLog �Ĳ����Ľ��ƻ����� ������ƻ�����ͨ�������и��� HyperLogLog �ϲ���һ����ʱ HyperLogLog ������ó��ġ�
        /// ͨ�� HyperLogLog ���ݽṹ�� �û�����ʹ�������̶���С���ڴ棬 �����漯���е�ΨһԪ�� ��ÿ�� HyperLogLog ֻ��ʹ�� 12k �ֽ��ڴ棬�Լ������ֽڵ��ڴ���������������
        /// ����صĿɼ����ϣ�observed set�����������Ǿ�ȷֵ�� ����һ������ 0.81% ��׼����standard error���Ľ���ֵ��
        /// �ٸ����ӣ� Ϊ�˼�¼һ���ִ�ж��ٴθ�����ͬ��������ѯ�� һ�����������ÿ��ִ��������ѯʱ����һ�� PFADD �� ��ͨ������ PFCOUNT ��������ȡ�����¼�Ľ��ƽ����
        /// ʱ�临�Ӷȣ������������ڵ��� HyperLogLog ʱ�� ���Ӷ�Ϊ O(1) �� ���Ҿ��зǳ��͵�ƽ������ʱ�䡣 ������������ N �� HyperLogLog ʱ�� ���Ӷ�Ϊ O(N) �� ����ʱ��Ҳ�ȴ������ HyperLogLog ʱҪ��öࡣ
        /// <remarks>
        /// PFCOUNT key [key ...] http://redis.io/commands/pfcount
        /// </remarks>
        /// <returns>
        /// �����ظ��� ���� HyperLogLog ������ΨһԪ�صĽ���������
        /// </returns>
        /// </summary>
        long Length(string keySuffix, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// ����� HyperLogLog �ϲ���merge��Ϊһ�� HyperLogLog �� �ϲ���� HyperLogLog �Ļ����ӽ����������� HyperLogLog �Ŀɼ����ϣ�observed set���Ĳ�����
        /// �ϲ��ó��� HyperLogLog �ᱻ������ destkey �����棬 ����ü��������ڣ� ��ô������ִ��֮ǰ�� ����Ϊ�ü�����һ���յ� HyperLogLog ��
        /// ʱ�临�Ӷȣ�O(N) �� ���� N Ϊ���ϲ��� HyperLogLog ������ �����������ĳ������ӶȱȽϸߡ�
        /// <remarks>
        /// PFMERGE destkey sourcekey [sourcekey ...] http://redis.io/commands/pfmerge
        /// </remarks>
        /// <returns>
        /// �ַ����ظ������� OK
        /// </returns>
        /// </summary>
        void HyperLogLogMerge(string destination, string[] sourceKeys, CommandFlags flags = CommandFlags.None);

    }

    public interface IRedisPubSub
    {

    }
}