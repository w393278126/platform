using System;
using System.Threading.Tasks;
using  Xn.Platform.Infrastructure.Web.Events.Factory;
using  Xn.Platform.Infrastructure.Web.Events.Handlers;

namespace Xn.Platform.Infrastructure.Web.Events
{
    /// <summary>
    /// 消息总线类的接口.
    /// </summary>
    public interface IEventBus
    {
        #region Register

        /// <summary>
        /// 注册事件.
        /// 该Action会被用于处理所有 <see cref="TEventData"/> 对应的事件.
        /// </summary>
        /// <param name="action">用于处理事件的Action</param>
        /// <typeparam name="TEventData">事件类型</typeparam>
        IDisposable Register<TEventData>(Action<TEventData> action) where TEventData : IEventData;

        /// <summary>
        /// 注册事件.
        /// 该 <see cref="IEventHandler{TEventData}"/> 实例会被用于处理所有 <see cref="TEventData"/> 对应的事件.
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="handler">事件处理器实例</param>
        IDisposable Register<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData;

        /// <summary>
        /// 注册事件.
        /// 每次创建一个新的 <see cref="THandler"/> 实例处理 <see cref="TEventData"/> 对应的事件.
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <typeparam name="THandler">事件处理类</typeparam>
        IDisposable Register<TEventData, THandler>() where TEventData : IEventData where THandler : IEventHandler<TEventData>, new();

        /// <summary>
        /// 注册事件.
        /// 使用工厂模式创建事件处理器
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="handlerFactory">用于创建事件处理器的工厂</param>
        IDisposable Register<TEventData>(IEventHandlerFactory handlerFactory) where TEventData : IEventData;

        #endregion

        #region Unregister

        /// <summary>
        /// 注销事件处理器.
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="action"></param>
        void Unregister<TEventData>(Action<TEventData> action) where TEventData : IEventData;

        /// <summary>
        /// 注销事件处理器.
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="handler">之前注册的事件处理器</param>
        void Unregister<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData;

        /// <summary>
        /// 注销事件处理器工厂类.
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="factory">之前注册的事件处理器工厂</param>
        void Unregister<TEventData>(IEventHandlerFactory factory) where TEventData : IEventData;

        /// <summary>
        /// 注销 <see cref="TEventData"/> 对应的所有事件处理器
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        void UnregisterAll<TEventData>() where TEventData : IEventData;

        #endregion

        #region Trigger

        /// <summary>
        /// 触发事件.
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="eventData">事件相关数据</param>
        void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// 触发事件.
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="eventSource">事件源</param>
        /// <param name="eventData">事件相关数据</param>
        void Trigger<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// 异步触发事件.
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="eventData">事件相关数据</param>
        Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// 异步触发事件.
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="eventSource">事件源</param>
        /// <param name="eventData">事件相关数据</param>
        Task TriggerAsync<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData;

        #endregion
    }
}