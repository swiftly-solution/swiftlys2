namespace SwiftlyS2.Shared.SchemaDefinitions
{
    [Obsolete("EntityTouchType is deprecated. Use separate OnEntityStartTouch, OnEntityTouch, and OnEntityEndTouch events instead.")]
    public enum EntityTouchType : byte
    {
        StartTouch = 0,
        Touch = 1,
        EndTouch = 2
    }
}

namespace SwiftlyS2.Shared.Events
{
    using SwiftlyS2.Shared.SchemaDefinitions;

    [Obsolete("IOnEntityTouchHookEvent is deprecated. Use IOnEntityStartTouchEvent, IOnEntityTouchEvent, or IOnEntityEndTouchEvent instead.")]
    public interface IOnEntityTouchHookEvent
    {

        /// <summary>
        /// Gets the entity that initiated the touch.
        /// </summary>
        public CBaseEntity Entity { get; }

        /// <summary>
        /// Gets the entity being touched.
        /// </summary>
        public CBaseEntity OtherEntity { get; }

        /// <summary>
        /// Gets the type of touch interaction.
        /// </summary>
        public EntityTouchType TouchType { get; }
    }

    /// <summary>
    /// Called when an entity starts touching another entity.
    /// </summary>
    public interface IOnEntityStartTouchEvent
    {

        /// <summary>
        /// Gets the entity that initiated the touch.
        /// </summary>
        public CBaseEntity Entity { get; }

        /// <summary>
        /// Gets the entity being touched.
        /// </summary>
        public CBaseEntity OtherEntity { get; }
    }

    /// <summary>
    /// Called when an entity is touching another entity.
    /// </summary>
    public interface IOnEntityTouchEvent
    {

        /// <summary>
        /// Gets the entity that initiated the touch.
        /// </summary>
        public CBaseEntity Entity { get; }

        /// <summary>
        /// Gets the entity being touched.
        /// </summary>
        public CBaseEntity OtherEntity { get; }
    }

    /// <summary>
    /// Called when an entity ends touching another entity.
    /// </summary>
    public interface IOnEntityEndTouchEvent
    {

        /// <summary>
        /// Gets the entity that initiated the touch.
        /// </summary>
        public CBaseEntity Entity { get; }

        /// <summary>
        /// Gets the entity being touched.
        /// </summary>
        public CBaseEntity OtherEntity { get; }
    }
}