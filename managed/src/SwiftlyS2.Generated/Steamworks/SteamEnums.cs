namespace SwiftlyS2.Shared.SteamAPI;

/// <summary>
/// <para>set of relationships to other users</para>
/// </summary>
public enum EFriendRelationship : int
{
	k_EFriendRelationshipNone = 0,
	/// <summary>
	/// <para>this doesn&apos;t get stored; the user has just done an Ignore on an friendship invite</para>
	/// </summary>
	k_EFriendRelationshipBlocked = 1,
	k_EFriendRelationshipRequestRecipient = 2,
	k_EFriendRelationshipFriend = 3,
	k_EFriendRelationshipRequestInitiator = 4,
	/// <summary>
	/// <para>this is stored; the user has explicit blocked this other user from comments/chat/etc</para>
	/// </summary>
	k_EFriendRelationshipIgnored = 5,
	k_EFriendRelationshipIgnoredFriend = 6,
	/// <summary>
	/// <para>was used by the original implementation of the facebook linking feature, but now unused.</para>
	/// </summary>
	k_EFriendRelationshipSuggested_DEPRECATED = 7,
	/// <summary>
	/// <para>keep this updated</para>
	/// </summary>
	k_EFriendRelationshipMax = 8,
}

/// <summary>
/// <para>list of states a friend can be in</para>
/// </summary>
public enum EPersonaState : int
{
	/// <summary>
	/// <para>friend is not currently logged on</para>
	/// </summary>
	k_EPersonaStateOffline = 0,
	/// <summary>
	/// <para>friend is logged on</para>
	/// </summary>
	k_EPersonaStateOnline = 1,
	/// <summary>
	/// <para>user is on, but busy</para>
	/// </summary>
	k_EPersonaStateBusy = 2,
	/// <summary>
	/// <para>auto-away feature</para>
	/// </summary>
	k_EPersonaStateAway = 3,
	/// <summary>
	/// <para>auto-away for a long time</para>
	/// </summary>
	k_EPersonaStateSnooze = 4,
	/// <summary>
	/// <para>Online, trading</para>
	/// </summary>
	k_EPersonaStateLookingToTrade = 5,
	/// <summary>
	/// <para>Online, wanting to play</para>
	/// </summary>
	k_EPersonaStateLookingToPlay = 6,
	/// <summary>
	/// <para>Online, but appears offline to friends.  This status is never published to clients.</para>
	/// </summary>
	k_EPersonaStateInvisible = 7,
	k_EPersonaStateMax,
}

/// <summary>
/// <para>flags for enumerating friends list, or quickly checking a the relationship between users</para>
/// </summary>
[Flags]
public enum EFriendFlags : int
{
	k_EFriendFlagNone			= 0x00,
	k_EFriendFlagBlocked		= 0x01,
	k_EFriendFlagFriendshipRequested	= 0x02,
	/// <summary>
	/// <para>&quot;regular&quot; friend</para>
	/// </summary>
	k_EFriendFlagImmediate		= 0x04,
	k_EFriendFlagClanMember		= 0x08,
	k_EFriendFlagOnGameServer	= 0x10,
	// k_EFriendFlagHasPlayedWith	= 0x20,	// not currently used
	// k_EFriendFlagFriendOfFriend	= 0x40, // not currently used
	k_EFriendFlagRequestingFriendship = 0x80,
	k_EFriendFlagRequestingInfo = 0x100,
	k_EFriendFlagIgnored		= 0x200,
	k_EFriendFlagIgnoredFriend	= 0x400,
	// k_EFriendFlagSuggested		= 0x800,	// not used
	k_EFriendFlagChatMember		= 0x1000,
	k_EFriendFlagAll			= 0xFFFF,
}

/// <summary>
/// <para>These values are passed as parameters to the store</para>
/// </summary>
public enum EOverlayToStoreFlag : int
{
	k_EOverlayToStoreFlag_None = 0,
	k_EOverlayToStoreFlag_AddToCart = 1,
	k_EOverlayToStoreFlag_AddToCartAndShow = 2,
}

/// <summary>
/// <para>Tells Steam where to place the browser window inside the overlay</para>
/// </summary>
public enum EActivateGameOverlayToWebPageMode : int
{
	/// <summary>
	/// <para>Browser will open next to all other windows that the user has open in the overlay.</para>
	/// </summary>
	k_EActivateGameOverlayToWebPageMode_Default = 0,
	/// <summary>
	/// <para>The window will remain open, even if the user closes then re-opens the overlay.</para>
	/// <para>Browser will be opened in a special overlay configuration which hides all other windows</para>
	/// </summary>
	k_EActivateGameOverlayToWebPageMode_Modal = 1
															// that the user has open in the overlay. When the user closes the overlay, the browser window
															// will also close. When the user closes the browser window, the overlay will automatically close.
}

/// <summary>
/// <para>See GetProfileItemPropertyString and GetProfileItemPropertyUint</para>
/// </summary>
public enum ECommunityProfileItemType : int
{
	k_ECommunityProfileItemType_AnimatedAvatar		 = 0,
	k_ECommunityProfileItemType_AvatarFrame			 = 1,
	k_ECommunityProfileItemType_ProfileModifier		 = 2,
	k_ECommunityProfileItemType_ProfileBackground	 = 3,
	k_ECommunityProfileItemType_MiniProfileBackground = 4,
}

public enum ECommunityProfileItemProperty : int
{
	/// <summary>
	/// <para>string</para>
	/// </summary>
	k_ECommunityProfileItemProperty_ImageSmall	   = 0,
	/// <summary>
	/// <para>string</para>
	/// </summary>
	k_ECommunityProfileItemProperty_ImageLarge	   = 1,
	/// <summary>
	/// <para>string</para>
	/// </summary>
	k_ECommunityProfileItemProperty_InternalName   = 2,
	/// <summary>
	/// <para>string</para>
	/// </summary>
	k_ECommunityProfileItemProperty_Title		   = 3,
	/// <summary>
	/// <para>string</para>
	/// </summary>
	k_ECommunityProfileItemProperty_Description	   = 4,
	/// <summary>
	/// <para>uint32</para>
	/// </summary>
	k_ECommunityProfileItemProperty_AppID		   = 5,
	/// <summary>
	/// <para>uint32</para>
	/// </summary>
	k_ECommunityProfileItemProperty_TypeID		   = 6,
	/// <summary>
	/// <para>uint32</para>
	/// </summary>
	k_ECommunityProfileItemProperty_Class		   = 7,
	/// <summary>
	/// <para>string</para>
	/// </summary>
	k_ECommunityProfileItemProperty_MovieWebM	   = 8,
	/// <summary>
	/// <para>string</para>
	/// </summary>
	k_ECommunityProfileItemProperty_MovieMP4	   = 9,
	/// <summary>
	/// <para>string</para>
	/// </summary>
	k_ECommunityProfileItemProperty_MovieWebMSmall = 10,
	/// <summary>
	/// <para>string</para>
	/// </summary>
	k_ECommunityProfileItemProperty_MovieMP4Small  = 11,
}

/// <summary>
/// <para>used in PersonaStateChange_t::m_nChangeFlags to describe what&apos;s changed about a user</para>
/// <para>these flags describe what the client has learned has changed recently, so on startup you&apos;ll see a name, avatar &amp; relationship change for every friend</para>
/// </summary>
[Flags]
public enum EPersonaChange : int
{
	k_EPersonaChangeName		= 0x0001,
	k_EPersonaChangeStatus		= 0x0002,
	k_EPersonaChangeComeOnline	= 0x0004,
	k_EPersonaChangeGoneOffline	= 0x0008,
	k_EPersonaChangeGamePlayed	= 0x0010,
	k_EPersonaChangeGameServer	= 0x0020,
	k_EPersonaChangeAvatar		= 0x0040,
	k_EPersonaChangeJoinedSource= 0x0080,
	k_EPersonaChangeLeftSource	= 0x0100,
	k_EPersonaChangeRelationshipChanged = 0x0200,
	k_EPersonaChangeNameFirstSet = 0x0400,
	k_EPersonaChangeBroadcast = 0x0800,
	k_EPersonaChangeNickname =	0x1000,
	k_EPersonaChangeSteamLevel = 0x2000,
	k_EPersonaChangeRichPresence = 0x4000,
}

/// <summary>
/// <para>list of possible return values from the ISteamGameCoordinator API</para>
/// </summary>
public enum EGCResults : int
{
	k_EGCResultOK = 0,
	/// <summary>
	/// <para>There is no message in the queue</para>
	/// </summary>
	k_EGCResultNoMessage = 1,
	/// <summary>
	/// <para>The buffer is too small for the requested message</para>
	/// </summary>
	k_EGCResultBufferTooSmall = 2,
	/// <summary>
	/// <para>The client is not logged onto Steam</para>
	/// </summary>
	k_EGCResultNotLoggedOn = 3,
	/// <summary>
	/// <para>Something was wrong with the message being sent with SendMessage</para>
	/// </summary>
	k_EGCResultInvalidMessage = 4,
}

public enum EHTMLMouseButton : int
{
	eHTMLMouseButton_Left = 0,
	eHTMLMouseButton_Right = 1,
	eHTMLMouseButton_Middle = 2,
}

public enum EHTMLMouseCursor : int
{
	k_EHTMLMouseCursor_User = 0,
	k_EHTMLMouseCursor_None,
	k_EHTMLMouseCursor_Arrow,
	k_EHTMLMouseCursor_IBeam,
	k_EHTMLMouseCursor_Hourglass,
	k_EHTMLMouseCursor_WaitArrow,
	k_EHTMLMouseCursor_Crosshair,
	k_EHTMLMouseCursor_Up,
	k_EHTMLMouseCursor_SizeNW,
	k_EHTMLMouseCursor_SizeSE,
	k_EHTMLMouseCursor_SizeNE,
	k_EHTMLMouseCursor_SizeSW,
	k_EHTMLMouseCursor_SizeW,
	k_EHTMLMouseCursor_SizeE,
	k_EHTMLMouseCursor_SizeN,
	k_EHTMLMouseCursor_SizeS,
	k_EHTMLMouseCursor_SizeWE,
	k_EHTMLMouseCursor_SizeNS,
	k_EHTMLMouseCursor_SizeAll,
	k_EHTMLMouseCursor_No,
	k_EHTMLMouseCursor_Hand,
	/// <summary>
	/// <para>don&apos;t show any custom cursor, just use your default</para>
	/// </summary>
	k_EHTMLMouseCursor_Blank,
	k_EHTMLMouseCursor_MiddlePan,
	k_EHTMLMouseCursor_NorthPan,
	k_EHTMLMouseCursor_NorthEastPan,
	k_EHTMLMouseCursor_EastPan,
	k_EHTMLMouseCursor_SouthEastPan,
	k_EHTMLMouseCursor_SouthPan,
	k_EHTMLMouseCursor_SouthWestPan,
	k_EHTMLMouseCursor_WestPan,
	k_EHTMLMouseCursor_NorthWestPan,
	k_EHTMLMouseCursor_Alias,
	k_EHTMLMouseCursor_Cell,
	k_EHTMLMouseCursor_ColResize,
	k_EHTMLMouseCursor_CopyCur,
	k_EHTMLMouseCursor_VerticalText,
	k_EHTMLMouseCursor_RowResize,
	k_EHTMLMouseCursor_ZoomIn,
	k_EHTMLMouseCursor_ZoomOut,
	k_EHTMLMouseCursor_Help,
	k_EHTMLMouseCursor_Custom,
	k_EHTMLMouseCursor_SizeNWSE,
	k_EHTMLMouseCursor_SizeNESW,
	/// <summary>
	/// <para>custom cursors start from this value and up</para>
	/// </summary>
	k_EHTMLMouseCursor_last,
}

[Flags]
public enum EHTMLKeyModifiers : int
{
	k_eHTMLKeyModifier_None = 0,
	k_eHTMLKeyModifier_AltDown = 1 << 0,
	k_eHTMLKeyModifier_CtrlDown = 1 << 1,
	k_eHTMLKeyModifier_ShiftDown = 1 << 2,
}

public enum EInputSourceMode : int
{
	k_EInputSourceMode_None,
	k_EInputSourceMode_Dpad,
	k_EInputSourceMode_Buttons,
	k_EInputSourceMode_FourButtons,
	k_EInputSourceMode_AbsoluteMouse,
	k_EInputSourceMode_RelativeMouse,
	k_EInputSourceMode_JoystickMove,
	k_EInputSourceMode_JoystickMouse,
	k_EInputSourceMode_JoystickCamera,
	k_EInputSourceMode_ScrollWheel,
	k_EInputSourceMode_Trigger,
	k_EInputSourceMode_TouchMenu,
	k_EInputSourceMode_MouseJoystick,
	k_EInputSourceMode_MouseRegion,
	k_EInputSourceMode_RadialMenu,
	k_EInputSourceMode_SingleButton,
	k_EInputSourceMode_Switches
}

/// <summary>
/// <para>Note: Please do not use action origins as a way to identify controller types. There is no</para>
/// <para>guarantee that they will be added in a contiguous manner - use GetInputTypeForHandle instead.</para>
/// <para>Versions of Steam that add new controller types in the future will extend this enum so if you&apos;re</para>
/// <para>using a lookup table please check the bounds of any origins returned by Steam.</para>
/// </summary>
public enum EInputActionOrigin : int
{
	/// <summary>
	/// <para>Steam Controller</para>
	/// </summary>
	k_EInputActionOrigin_None,
	k_EInputActionOrigin_SteamController_A,
	k_EInputActionOrigin_SteamController_B,
	k_EInputActionOrigin_SteamController_X,
	k_EInputActionOrigin_SteamController_Y,
	k_EInputActionOrigin_SteamController_LeftBumper,
	k_EInputActionOrigin_SteamController_RightBumper,
	k_EInputActionOrigin_SteamController_LeftGrip,
	k_EInputActionOrigin_SteamController_RightGrip,
	k_EInputActionOrigin_SteamController_Start,
	k_EInputActionOrigin_SteamController_Back,
	k_EInputActionOrigin_SteamController_LeftPad_Touch,
	k_EInputActionOrigin_SteamController_LeftPad_Swipe,
	k_EInputActionOrigin_SteamController_LeftPad_Click,
	k_EInputActionOrigin_SteamController_LeftPad_DPadNorth,
	k_EInputActionOrigin_SteamController_LeftPad_DPadSouth,
	k_EInputActionOrigin_SteamController_LeftPad_DPadWest,
	k_EInputActionOrigin_SteamController_LeftPad_DPadEast,
	k_EInputActionOrigin_SteamController_RightPad_Touch,
	k_EInputActionOrigin_SteamController_RightPad_Swipe,
	k_EInputActionOrigin_SteamController_RightPad_Click,
	k_EInputActionOrigin_SteamController_RightPad_DPadNorth,
	k_EInputActionOrigin_SteamController_RightPad_DPadSouth,
	k_EInputActionOrigin_SteamController_RightPad_DPadWest,
	k_EInputActionOrigin_SteamController_RightPad_DPadEast,
	k_EInputActionOrigin_SteamController_LeftTrigger_Pull,
	k_EInputActionOrigin_SteamController_LeftTrigger_Click,
	k_EInputActionOrigin_SteamController_RightTrigger_Pull,
	k_EInputActionOrigin_SteamController_RightTrigger_Click,
	k_EInputActionOrigin_SteamController_LeftStick_Move,
	k_EInputActionOrigin_SteamController_LeftStick_Click,
	k_EInputActionOrigin_SteamController_LeftStick_DPadNorth,
	k_EInputActionOrigin_SteamController_LeftStick_DPadSouth,
	k_EInputActionOrigin_SteamController_LeftStick_DPadWest,
	k_EInputActionOrigin_SteamController_LeftStick_DPadEast,
	k_EInputActionOrigin_SteamController_Gyro_Move,
	k_EInputActionOrigin_SteamController_Gyro_Pitch,
	k_EInputActionOrigin_SteamController_Gyro_Yaw,
	k_EInputActionOrigin_SteamController_Gyro_Roll,
	k_EInputActionOrigin_SteamController_Reserved0,
	k_EInputActionOrigin_SteamController_Reserved1,
	k_EInputActionOrigin_SteamController_Reserved2,
	k_EInputActionOrigin_SteamController_Reserved3,
	k_EInputActionOrigin_SteamController_Reserved4,
	k_EInputActionOrigin_SteamController_Reserved5,
	k_EInputActionOrigin_SteamController_Reserved6,
	k_EInputActionOrigin_SteamController_Reserved7,
	k_EInputActionOrigin_SteamController_Reserved8,
	k_EInputActionOrigin_SteamController_Reserved9,
	k_EInputActionOrigin_SteamController_Reserved10,
	/// <summary>
	/// <para>PS4 Dual Shock</para>
	/// </summary>
	k_EInputActionOrigin_PS4_X,
	k_EInputActionOrigin_PS4_Circle,
	k_EInputActionOrigin_PS4_Triangle,
	k_EInputActionOrigin_PS4_Square,
	k_EInputActionOrigin_PS4_LeftBumper,
	k_EInputActionOrigin_PS4_RightBumper,
	/// <summary>
	/// <para>Start</para>
	/// </summary>
	k_EInputActionOrigin_PS4_Options,
	/// <summary>
	/// <para>Back</para>
	/// </summary>
	k_EInputActionOrigin_PS4_Share,
	k_EInputActionOrigin_PS4_LeftPad_Touch,
	k_EInputActionOrigin_PS4_LeftPad_Swipe,
	k_EInputActionOrigin_PS4_LeftPad_Click,
	k_EInputActionOrigin_PS4_LeftPad_DPadNorth,
	k_EInputActionOrigin_PS4_LeftPad_DPadSouth,
	k_EInputActionOrigin_PS4_LeftPad_DPadWest,
	k_EInputActionOrigin_PS4_LeftPad_DPadEast,
	k_EInputActionOrigin_PS4_RightPad_Touch,
	k_EInputActionOrigin_PS4_RightPad_Swipe,
	k_EInputActionOrigin_PS4_RightPad_Click,
	k_EInputActionOrigin_PS4_RightPad_DPadNorth,
	k_EInputActionOrigin_PS4_RightPad_DPadSouth,
	k_EInputActionOrigin_PS4_RightPad_DPadWest,
	k_EInputActionOrigin_PS4_RightPad_DPadEast,
	k_EInputActionOrigin_PS4_CenterPad_Touch,
	k_EInputActionOrigin_PS4_CenterPad_Swipe,
	k_EInputActionOrigin_PS4_CenterPad_Click,
	k_EInputActionOrigin_PS4_CenterPad_DPadNorth,
	k_EInputActionOrigin_PS4_CenterPad_DPadSouth,
	k_EInputActionOrigin_PS4_CenterPad_DPadWest,
	k_EInputActionOrigin_PS4_CenterPad_DPadEast,
	k_EInputActionOrigin_PS4_LeftTrigger_Pull,
	k_EInputActionOrigin_PS4_LeftTrigger_Click,
	k_EInputActionOrigin_PS4_RightTrigger_Pull,
	k_EInputActionOrigin_PS4_RightTrigger_Click,
	k_EInputActionOrigin_PS4_LeftStick_Move,
	k_EInputActionOrigin_PS4_LeftStick_Click,
	k_EInputActionOrigin_PS4_LeftStick_DPadNorth,
	k_EInputActionOrigin_PS4_LeftStick_DPadSouth,
	k_EInputActionOrigin_PS4_LeftStick_DPadWest,
	k_EInputActionOrigin_PS4_LeftStick_DPadEast,
	k_EInputActionOrigin_PS4_RightStick_Move,
	k_EInputActionOrigin_PS4_RightStick_Click,
	k_EInputActionOrigin_PS4_RightStick_DPadNorth,
	k_EInputActionOrigin_PS4_RightStick_DPadSouth,
	k_EInputActionOrigin_PS4_RightStick_DPadWest,
	k_EInputActionOrigin_PS4_RightStick_DPadEast,
	k_EInputActionOrigin_PS4_DPad_North,
	k_EInputActionOrigin_PS4_DPad_South,
	k_EInputActionOrigin_PS4_DPad_West,
	k_EInputActionOrigin_PS4_DPad_East,
	k_EInputActionOrigin_PS4_Gyro_Move,
	k_EInputActionOrigin_PS4_Gyro_Pitch,
	k_EInputActionOrigin_PS4_Gyro_Yaw,
	k_EInputActionOrigin_PS4_Gyro_Roll,
	k_EInputActionOrigin_PS4_DPad_Move,
	k_EInputActionOrigin_PS4_Reserved1,
	k_EInputActionOrigin_PS4_Reserved2,
	k_EInputActionOrigin_PS4_Reserved3,
	k_EInputActionOrigin_PS4_Reserved4,
	k_EInputActionOrigin_PS4_Reserved5,
	k_EInputActionOrigin_PS4_Reserved6,
	k_EInputActionOrigin_PS4_Reserved7,
	k_EInputActionOrigin_PS4_Reserved8,
	k_EInputActionOrigin_PS4_Reserved9,
	k_EInputActionOrigin_PS4_Reserved10,
	/// <summary>
	/// <para>XBox One</para>
	/// </summary>
	k_EInputActionOrigin_XBoxOne_A,
	k_EInputActionOrigin_XBoxOne_B,
	k_EInputActionOrigin_XBoxOne_X,
	k_EInputActionOrigin_XBoxOne_Y,
	k_EInputActionOrigin_XBoxOne_LeftBumper,
	k_EInputActionOrigin_XBoxOne_RightBumper,
	/// <summary>
	/// <para>Start</para>
	/// </summary>
	k_EInputActionOrigin_XBoxOne_Menu,
	/// <summary>
	/// <para>Back</para>
	/// </summary>
	k_EInputActionOrigin_XBoxOne_View,
	k_EInputActionOrigin_XBoxOne_LeftTrigger_Pull,
	k_EInputActionOrigin_XBoxOne_LeftTrigger_Click,
	k_EInputActionOrigin_XBoxOne_RightTrigger_Pull,
	k_EInputActionOrigin_XBoxOne_RightTrigger_Click,
	k_EInputActionOrigin_XBoxOne_LeftStick_Move,
	k_EInputActionOrigin_XBoxOne_LeftStick_Click,
	k_EInputActionOrigin_XBoxOne_LeftStick_DPadNorth,
	k_EInputActionOrigin_XBoxOne_LeftStick_DPadSouth,
	k_EInputActionOrigin_XBoxOne_LeftStick_DPadWest,
	k_EInputActionOrigin_XBoxOne_LeftStick_DPadEast,
	k_EInputActionOrigin_XBoxOne_RightStick_Move,
	k_EInputActionOrigin_XBoxOne_RightStick_Click,
	k_EInputActionOrigin_XBoxOne_RightStick_DPadNorth,
	k_EInputActionOrigin_XBoxOne_RightStick_DPadSouth,
	k_EInputActionOrigin_XBoxOne_RightStick_DPadWest,
	k_EInputActionOrigin_XBoxOne_RightStick_DPadEast,
	k_EInputActionOrigin_XBoxOne_DPad_North,
	k_EInputActionOrigin_XBoxOne_DPad_South,
	k_EInputActionOrigin_XBoxOne_DPad_West,
	k_EInputActionOrigin_XBoxOne_DPad_East,
	k_EInputActionOrigin_XBoxOne_DPad_Move,
	k_EInputActionOrigin_XBoxOne_LeftGrip_Lower,
	k_EInputActionOrigin_XBoxOne_LeftGrip_Upper,
	k_EInputActionOrigin_XBoxOne_RightGrip_Lower,
	k_EInputActionOrigin_XBoxOne_RightGrip_Upper,
	/// <summary>
	/// <para>Xbox Series X controllers only</para>
	/// </summary>
	k_EInputActionOrigin_XBoxOne_Share,
	k_EInputActionOrigin_XBoxOne_Reserved6,
	k_EInputActionOrigin_XBoxOne_Reserved7,
	k_EInputActionOrigin_XBoxOne_Reserved8,
	k_EInputActionOrigin_XBoxOne_Reserved9,
	k_EInputActionOrigin_XBoxOne_Reserved10,
	/// <summary>
	/// <para>XBox 360</para>
	/// </summary>
	k_EInputActionOrigin_XBox360_A,
	k_EInputActionOrigin_XBox360_B,
	k_EInputActionOrigin_XBox360_X,
	k_EInputActionOrigin_XBox360_Y,
	k_EInputActionOrigin_XBox360_LeftBumper,
	k_EInputActionOrigin_XBox360_RightBumper,
	/// <summary>
	/// <para>Start</para>
	/// </summary>
	k_EInputActionOrigin_XBox360_Start,
	/// <summary>
	/// <para>Back</para>
	/// </summary>
	k_EInputActionOrigin_XBox360_Back,
	k_EInputActionOrigin_XBox360_LeftTrigger_Pull,
	k_EInputActionOrigin_XBox360_LeftTrigger_Click,
	k_EInputActionOrigin_XBox360_RightTrigger_Pull,
	k_EInputActionOrigin_XBox360_RightTrigger_Click,
	k_EInputActionOrigin_XBox360_LeftStick_Move,
	k_EInputActionOrigin_XBox360_LeftStick_Click,
	k_EInputActionOrigin_XBox360_LeftStick_DPadNorth,
	k_EInputActionOrigin_XBox360_LeftStick_DPadSouth,
	k_EInputActionOrigin_XBox360_LeftStick_DPadWest,
	k_EInputActionOrigin_XBox360_LeftStick_DPadEast,
	k_EInputActionOrigin_XBox360_RightStick_Move,
	k_EInputActionOrigin_XBox360_RightStick_Click,
	k_EInputActionOrigin_XBox360_RightStick_DPadNorth,
	k_EInputActionOrigin_XBox360_RightStick_DPadSouth,
	k_EInputActionOrigin_XBox360_RightStick_DPadWest,
	k_EInputActionOrigin_XBox360_RightStick_DPadEast,
	k_EInputActionOrigin_XBox360_DPad_North,
	k_EInputActionOrigin_XBox360_DPad_South,
	k_EInputActionOrigin_XBox360_DPad_West,
	k_EInputActionOrigin_XBox360_DPad_East,
	k_EInputActionOrigin_XBox360_DPad_Move,
	k_EInputActionOrigin_XBox360_Reserved1,
	k_EInputActionOrigin_XBox360_Reserved2,
	k_EInputActionOrigin_XBox360_Reserved3,
	k_EInputActionOrigin_XBox360_Reserved4,
	k_EInputActionOrigin_XBox360_Reserved5,
	k_EInputActionOrigin_XBox360_Reserved6,
	k_EInputActionOrigin_XBox360_Reserved7,
	k_EInputActionOrigin_XBox360_Reserved8,
	k_EInputActionOrigin_XBox360_Reserved9,
	k_EInputActionOrigin_XBox360_Reserved10,
	/// <summary>
	/// <para>Switch - Pro or Joycons used as a single input device.</para>
	/// <para>This does not apply to a single joycon</para>
	/// </summary>
	k_EInputActionOrigin_Switch_A,
	k_EInputActionOrigin_Switch_B,
	k_EInputActionOrigin_Switch_X,
	k_EInputActionOrigin_Switch_Y,
	k_EInputActionOrigin_Switch_LeftBumper,
	k_EInputActionOrigin_Switch_RightBumper,
	/// <summary>
	/// <para>Start</para>
	/// </summary>
	k_EInputActionOrigin_Switch_Plus,
	/// <summary>
	/// <para>Back</para>
	/// </summary>
	k_EInputActionOrigin_Switch_Minus,
	k_EInputActionOrigin_Switch_Capture,
	k_EInputActionOrigin_Switch_LeftTrigger_Pull,
	k_EInputActionOrigin_Switch_LeftTrigger_Click,
	k_EInputActionOrigin_Switch_RightTrigger_Pull,
	k_EInputActionOrigin_Switch_RightTrigger_Click,
	k_EInputActionOrigin_Switch_LeftStick_Move,
	k_EInputActionOrigin_Switch_LeftStick_Click,
	k_EInputActionOrigin_Switch_LeftStick_DPadNorth,
	k_EInputActionOrigin_Switch_LeftStick_DPadSouth,
	k_EInputActionOrigin_Switch_LeftStick_DPadWest,
	k_EInputActionOrigin_Switch_LeftStick_DPadEast,
	k_EInputActionOrigin_Switch_RightStick_Move,
	k_EInputActionOrigin_Switch_RightStick_Click,
	k_EInputActionOrigin_Switch_RightStick_DPadNorth,
	k_EInputActionOrigin_Switch_RightStick_DPadSouth,
	k_EInputActionOrigin_Switch_RightStick_DPadWest,
	k_EInputActionOrigin_Switch_RightStick_DPadEast,
	k_EInputActionOrigin_Switch_DPad_North,
	k_EInputActionOrigin_Switch_DPad_South,
	k_EInputActionOrigin_Switch_DPad_West,
	k_EInputActionOrigin_Switch_DPad_East,
	/// <summary>
	/// <para>Primary Gyro in Pro Controller, or Right JoyCon</para>
	/// </summary>
	k_EInputActionOrigin_Switch_ProGyro_Move,
	/// <summary>
	/// <para>Primary Gyro in Pro Controller, or Right JoyCon</para>
	/// </summary>
	k_EInputActionOrigin_Switch_ProGyro_Pitch,
	/// <summary>
	/// <para>Primary Gyro in Pro Controller, or Right JoyCon</para>
	/// </summary>
	k_EInputActionOrigin_Switch_ProGyro_Yaw,
	/// <summary>
	/// <para>Primary Gyro in Pro Controller, or Right JoyCon</para>
	/// </summary>
	k_EInputActionOrigin_Switch_ProGyro_Roll,
	k_EInputActionOrigin_Switch_DPad_Move,
	k_EInputActionOrigin_Switch_Reserved1,
	k_EInputActionOrigin_Switch_Reserved2,
	k_EInputActionOrigin_Switch_Reserved3,
	k_EInputActionOrigin_Switch_Reserved4,
	k_EInputActionOrigin_Switch_Reserved5,
	k_EInputActionOrigin_Switch_Reserved6,
	k_EInputActionOrigin_Switch_Reserved7,
	k_EInputActionOrigin_Switch_Reserved8,
	k_EInputActionOrigin_Switch_Reserved9,
	k_EInputActionOrigin_Switch_Reserved10,
	/// <summary>
	/// <para>Switch JoyCon Specific</para>
	/// <para>Right JoyCon Gyro generally should correspond to Pro&apos;s single gyro</para>
	/// </summary>
	k_EInputActionOrigin_Switch_RightGyro_Move,
	/// <summary>
	/// <para>Right JoyCon Gyro generally should correspond to Pro&apos;s single gyro</para>
	/// </summary>
	k_EInputActionOrigin_Switch_RightGyro_Pitch,
	/// <summary>
	/// <para>Right JoyCon Gyro generally should correspond to Pro&apos;s single gyro</para>
	/// </summary>
	k_EInputActionOrigin_Switch_RightGyro_Yaw,
	/// <summary>
	/// <para>Right JoyCon Gyro generally should correspond to Pro&apos;s single gyro</para>
	/// </summary>
	k_EInputActionOrigin_Switch_RightGyro_Roll,
	k_EInputActionOrigin_Switch_LeftGyro_Move,
	k_EInputActionOrigin_Switch_LeftGyro_Pitch,
	k_EInputActionOrigin_Switch_LeftGyro_Yaw,
	k_EInputActionOrigin_Switch_LeftGyro_Roll,
	/// <summary>
	/// <para>Left JoyCon SR Button</para>
	/// </summary>
	k_EInputActionOrigin_Switch_LeftGrip_Lower,
	/// <summary>
	/// <para>Left JoyCon SL Button</para>
	/// </summary>
	k_EInputActionOrigin_Switch_LeftGrip_Upper,
	/// <summary>
	/// <para>Right JoyCon SL Button</para>
	/// </summary>
	k_EInputActionOrigin_Switch_RightGrip_Lower,
	/// <summary>
	/// <para>Right JoyCon SR Button</para>
	/// </summary>
	k_EInputActionOrigin_Switch_RightGrip_Upper,
	/// <summary>
	/// <para>With a Horizontal JoyCon this will be Y or what would be Dpad Right when vertical</para>
	/// </summary>
	k_EInputActionOrigin_Switch_JoyConButton_N,
	/// <summary>
	/// <para>X</para>
	/// </summary>
	k_EInputActionOrigin_Switch_JoyConButton_E,
	/// <summary>
	/// <para>A</para>
	/// </summary>
	k_EInputActionOrigin_Switch_JoyConButton_S,
	/// <summary>
	/// <para>B</para>
	/// </summary>
	k_EInputActionOrigin_Switch_JoyConButton_W,
	k_EInputActionOrigin_Switch_Reserved15,
	k_EInputActionOrigin_Switch_Reserved16,
	k_EInputActionOrigin_Switch_Reserved17,
	k_EInputActionOrigin_Switch_Reserved18,
	k_EInputActionOrigin_Switch_Reserved19,
	k_EInputActionOrigin_Switch_Reserved20,
	/// <summary>
	/// <para>Added in SDK 1.51</para>
	/// </summary>
	k_EInputActionOrigin_PS5_X,
	k_EInputActionOrigin_PS5_Circle,
	k_EInputActionOrigin_PS5_Triangle,
	k_EInputActionOrigin_PS5_Square,
	k_EInputActionOrigin_PS5_LeftBumper,
	k_EInputActionOrigin_PS5_RightBumper,
	/// <summary>
	/// <para>Start</para>
	/// </summary>
	k_EInputActionOrigin_PS5_Option,
	/// <summary>
	/// <para>Back</para>
	/// </summary>
	k_EInputActionOrigin_PS5_Create,
	k_EInputActionOrigin_PS5_Mute,
	k_EInputActionOrigin_PS5_LeftPad_Touch,
	k_EInputActionOrigin_PS5_LeftPad_Swipe,
	k_EInputActionOrigin_PS5_LeftPad_Click,
	k_EInputActionOrigin_PS5_LeftPad_DPadNorth,
	k_EInputActionOrigin_PS5_LeftPad_DPadSouth,
	k_EInputActionOrigin_PS5_LeftPad_DPadWest,
	k_EInputActionOrigin_PS5_LeftPad_DPadEast,
	k_EInputActionOrigin_PS5_RightPad_Touch,
	k_EInputActionOrigin_PS5_RightPad_Swipe,
	k_EInputActionOrigin_PS5_RightPad_Click,
	k_EInputActionOrigin_PS5_RightPad_DPadNorth,
	k_EInputActionOrigin_PS5_RightPad_DPadSouth,
	k_EInputActionOrigin_PS5_RightPad_DPadWest,
	k_EInputActionOrigin_PS5_RightPad_DPadEast,
	k_EInputActionOrigin_PS5_CenterPad_Touch,
	k_EInputActionOrigin_PS5_CenterPad_Swipe,
	k_EInputActionOrigin_PS5_CenterPad_Click,
	k_EInputActionOrigin_PS5_CenterPad_DPadNorth,
	k_EInputActionOrigin_PS5_CenterPad_DPadSouth,
	k_EInputActionOrigin_PS5_CenterPad_DPadWest,
	k_EInputActionOrigin_PS5_CenterPad_DPadEast,
	k_EInputActionOrigin_PS5_LeftTrigger_Pull,
	k_EInputActionOrigin_PS5_LeftTrigger_Click,
	k_EInputActionOrigin_PS5_RightTrigger_Pull,
	k_EInputActionOrigin_PS5_RightTrigger_Click,
	k_EInputActionOrigin_PS5_LeftStick_Move,
	k_EInputActionOrigin_PS5_LeftStick_Click,
	k_EInputActionOrigin_PS5_LeftStick_DPadNorth,
	k_EInputActionOrigin_PS5_LeftStick_DPadSouth,
	k_EInputActionOrigin_PS5_LeftStick_DPadWest,
	k_EInputActionOrigin_PS5_LeftStick_DPadEast,
	k_EInputActionOrigin_PS5_RightStick_Move,
	k_EInputActionOrigin_PS5_RightStick_Click,
	k_EInputActionOrigin_PS5_RightStick_DPadNorth,
	k_EInputActionOrigin_PS5_RightStick_DPadSouth,
	k_EInputActionOrigin_PS5_RightStick_DPadWest,
	k_EInputActionOrigin_PS5_RightStick_DPadEast,
	k_EInputActionOrigin_PS5_DPad_North,
	k_EInputActionOrigin_PS5_DPad_South,
	k_EInputActionOrigin_PS5_DPad_West,
	k_EInputActionOrigin_PS5_DPad_East,
	k_EInputActionOrigin_PS5_Gyro_Move,
	k_EInputActionOrigin_PS5_Gyro_Pitch,
	k_EInputActionOrigin_PS5_Gyro_Yaw,
	k_EInputActionOrigin_PS5_Gyro_Roll,
	k_EInputActionOrigin_PS5_DPad_Move,
	k_EInputActionOrigin_PS5_LeftGrip,
	k_EInputActionOrigin_PS5_RightGrip,
	k_EInputActionOrigin_PS5_LeftFn,
	k_EInputActionOrigin_PS5_RightFn,
	k_EInputActionOrigin_PS5_Reserved5,
	k_EInputActionOrigin_PS5_Reserved6,
	k_EInputActionOrigin_PS5_Reserved7,
	k_EInputActionOrigin_PS5_Reserved8,
	k_EInputActionOrigin_PS5_Reserved9,
	k_EInputActionOrigin_PS5_Reserved10,
	k_EInputActionOrigin_PS5_Reserved11,
	k_EInputActionOrigin_PS5_Reserved12,
	k_EInputActionOrigin_PS5_Reserved13,
	k_EInputActionOrigin_PS5_Reserved14,
	k_EInputActionOrigin_PS5_Reserved15,
	k_EInputActionOrigin_PS5_Reserved16,
	k_EInputActionOrigin_PS5_Reserved17,
	k_EInputActionOrigin_PS5_Reserved18,
	k_EInputActionOrigin_PS5_Reserved19,
	k_EInputActionOrigin_PS5_Reserved20,
	/// <summary>
	/// <para>Added in SDK 1.53</para>
	/// </summary>
	k_EInputActionOrigin_SteamDeck_A,
	k_EInputActionOrigin_SteamDeck_B,
	k_EInputActionOrigin_SteamDeck_X,
	k_EInputActionOrigin_SteamDeck_Y,
	k_EInputActionOrigin_SteamDeck_L1,
	k_EInputActionOrigin_SteamDeck_R1,
	k_EInputActionOrigin_SteamDeck_Menu,
	k_EInputActionOrigin_SteamDeck_View,
	k_EInputActionOrigin_SteamDeck_LeftPad_Touch,
	k_EInputActionOrigin_SteamDeck_LeftPad_Swipe,
	k_EInputActionOrigin_SteamDeck_LeftPad_Click,
	k_EInputActionOrigin_SteamDeck_LeftPad_DPadNorth,
	k_EInputActionOrigin_SteamDeck_LeftPad_DPadSouth,
	k_EInputActionOrigin_SteamDeck_LeftPad_DPadWest,
	k_EInputActionOrigin_SteamDeck_LeftPad_DPadEast,
	k_EInputActionOrigin_SteamDeck_RightPad_Touch,
	k_EInputActionOrigin_SteamDeck_RightPad_Swipe,
	k_EInputActionOrigin_SteamDeck_RightPad_Click,
	k_EInputActionOrigin_SteamDeck_RightPad_DPadNorth,
	k_EInputActionOrigin_SteamDeck_RightPad_DPadSouth,
	k_EInputActionOrigin_SteamDeck_RightPad_DPadWest,
	k_EInputActionOrigin_SteamDeck_RightPad_DPadEast,
	k_EInputActionOrigin_SteamDeck_L2_SoftPull,
	k_EInputActionOrigin_SteamDeck_L2,
	k_EInputActionOrigin_SteamDeck_R2_SoftPull,
	k_EInputActionOrigin_SteamDeck_R2,
	k_EInputActionOrigin_SteamDeck_LeftStick_Move,
	k_EInputActionOrigin_SteamDeck_L3,
	k_EInputActionOrigin_SteamDeck_LeftStick_DPadNorth,
	k_EInputActionOrigin_SteamDeck_LeftStick_DPadSouth,
	k_EInputActionOrigin_SteamDeck_LeftStick_DPadWest,
	k_EInputActionOrigin_SteamDeck_LeftStick_DPadEast,
	k_EInputActionOrigin_SteamDeck_LeftStick_Touch,
	k_EInputActionOrigin_SteamDeck_RightStick_Move,
	k_EInputActionOrigin_SteamDeck_R3,
	k_EInputActionOrigin_SteamDeck_RightStick_DPadNorth,
	k_EInputActionOrigin_SteamDeck_RightStick_DPadSouth,
	k_EInputActionOrigin_SteamDeck_RightStick_DPadWest,
	k_EInputActionOrigin_SteamDeck_RightStick_DPadEast,
	k_EInputActionOrigin_SteamDeck_RightStick_Touch,
	k_EInputActionOrigin_SteamDeck_L4,
	k_EInputActionOrigin_SteamDeck_R4,
	k_EInputActionOrigin_SteamDeck_L5,
	k_EInputActionOrigin_SteamDeck_R5,
	k_EInputActionOrigin_SteamDeck_DPad_Move,
	k_EInputActionOrigin_SteamDeck_DPad_North,
	k_EInputActionOrigin_SteamDeck_DPad_South,
	k_EInputActionOrigin_SteamDeck_DPad_West,
	k_EInputActionOrigin_SteamDeck_DPad_East,
	k_EInputActionOrigin_SteamDeck_Gyro_Move,
	k_EInputActionOrigin_SteamDeck_Gyro_Pitch,
	k_EInputActionOrigin_SteamDeck_Gyro_Yaw,
	k_EInputActionOrigin_SteamDeck_Gyro_Roll,
	k_EInputActionOrigin_SteamDeck_Reserved1,
	k_EInputActionOrigin_SteamDeck_Reserved2,
	k_EInputActionOrigin_SteamDeck_Reserved3,
	k_EInputActionOrigin_SteamDeck_Reserved4,
	k_EInputActionOrigin_SteamDeck_Reserved5,
	k_EInputActionOrigin_SteamDeck_Reserved6,
	k_EInputActionOrigin_SteamDeck_Reserved7,
	k_EInputActionOrigin_SteamDeck_Reserved8,
	k_EInputActionOrigin_SteamDeck_Reserved9,
	k_EInputActionOrigin_SteamDeck_Reserved10,
	k_EInputActionOrigin_SteamDeck_Reserved11,
	k_EInputActionOrigin_SteamDeck_Reserved12,
	k_EInputActionOrigin_SteamDeck_Reserved13,
	k_EInputActionOrigin_SteamDeck_Reserved14,
	k_EInputActionOrigin_SteamDeck_Reserved15,
	k_EInputActionOrigin_SteamDeck_Reserved16,
	k_EInputActionOrigin_SteamDeck_Reserved17,
	k_EInputActionOrigin_SteamDeck_Reserved18,
	k_EInputActionOrigin_SteamDeck_Reserved19,
	k_EInputActionOrigin_SteamDeck_Reserved20,
	k_EInputActionOrigin_Horipad_M1,
	k_EInputActionOrigin_Horipad_M2,
	k_EInputActionOrigin_Horipad_L4,
	k_EInputActionOrigin_Horipad_R4,
	/// <summary>
	/// <para>If Steam has added support for new controllers origins will go here.</para>
	/// </summary>
	k_EInputActionOrigin_Count,
	/// <summary>
	/// <para>Origins are currently a maximum of 16 bits.</para>
	/// </summary>
	k_EInputActionOrigin_MaximumPossibleValue = 32767,
}

public enum EXboxOrigin : int
{
	k_EXboxOrigin_A,
	k_EXboxOrigin_B,
	k_EXboxOrigin_X,
	k_EXboxOrigin_Y,
	k_EXboxOrigin_LeftBumper,
	k_EXboxOrigin_RightBumper,
	/// <summary>
	/// <para>Start</para>
	/// </summary>
	k_EXboxOrigin_Menu,
	/// <summary>
	/// <para>Back</para>
	/// </summary>
	k_EXboxOrigin_View,
	k_EXboxOrigin_LeftTrigger_Pull,
	k_EXboxOrigin_LeftTrigger_Click,
	k_EXboxOrigin_RightTrigger_Pull,
	k_EXboxOrigin_RightTrigger_Click,
	k_EXboxOrigin_LeftStick_Move,
	k_EXboxOrigin_LeftStick_Click,
	k_EXboxOrigin_LeftStick_DPadNorth,
	k_EXboxOrigin_LeftStick_DPadSouth,
	k_EXboxOrigin_LeftStick_DPadWest,
	k_EXboxOrigin_LeftStick_DPadEast,
	k_EXboxOrigin_RightStick_Move,
	k_EXboxOrigin_RightStick_Click,
	k_EXboxOrigin_RightStick_DPadNorth,
	k_EXboxOrigin_RightStick_DPadSouth,
	k_EXboxOrigin_RightStick_DPadWest,
	k_EXboxOrigin_RightStick_DPadEast,
	k_EXboxOrigin_DPad_North,
	k_EXboxOrigin_DPad_South,
	k_EXboxOrigin_DPad_West,
	k_EXboxOrigin_DPad_East,
	k_EXboxOrigin_Count,
}

public enum ESteamControllerPad : int
{
	k_ESteamControllerPad_Left,
	k_ESteamControllerPad_Right
}

[Flags]
public enum EControllerHapticLocation : int
{
	k_EControllerHapticLocation_Left = ( 1 << ESteamControllerPad.k_ESteamControllerPad_Left ),
	k_EControllerHapticLocation_Right = ( 1 << ESteamControllerPad.k_ESteamControllerPad_Right ),
	k_EControllerHapticLocation_Both = ( 1 << ESteamControllerPad.k_ESteamControllerPad_Left | 1 << ESteamControllerPad.k_ESteamControllerPad_Right ),
}

public enum EControllerHapticType : int
{
	k_EControllerHapticType_Off,
	k_EControllerHapticType_Tick,
	k_EControllerHapticType_Click,
}

public enum ESteamInputType : int
{
	k_ESteamInputType_Unknown,
	k_ESteamInputType_SteamController,
	k_ESteamInputType_XBox360Controller,
	k_ESteamInputType_XBoxOneController,
	/// <summary>
	/// <para>DirectInput controllers</para>
	/// </summary>
	k_ESteamInputType_GenericGamepad,
	k_ESteamInputType_PS4Controller,
	/// <summary>
	/// <para>Unused</para>
	/// </summary>
	k_ESteamInputType_AppleMFiController,
	/// <summary>
	/// <para>Unused</para>
	/// </summary>
	k_ESteamInputType_AndroidController,
	/// <summary>
	/// <para>Unused</para>
	/// </summary>
	k_ESteamInputType_SwitchJoyConPair,
	/// <summary>
	/// <para>Unused</para>
	/// </summary>
	k_ESteamInputType_SwitchJoyConSingle,
	k_ESteamInputType_SwitchProController,
	/// <summary>
	/// <para>Steam Link App On-screen Virtual Controller</para>
	/// </summary>
	k_ESteamInputType_MobileTouch,
	/// <summary>
	/// <para>Currently uses PS4 Origins</para>
	/// </summary>
	k_ESteamInputType_PS3Controller,
	/// <summary>
	/// <para>Added in SDK 151</para>
	/// </summary>
	k_ESteamInputType_PS5Controller,
	/// <summary>
	/// <para>Added in SDK 153</para>
	/// </summary>
	k_ESteamInputType_SteamDeckController,
	k_ESteamInputType_Count,
	k_ESteamInputType_MaximumPossibleValue = 255,
}

/// <summary>
/// <para>Individual values are used by the GetSessionInputConfigurationSettings bitmask</para>
/// </summary>
public enum ESteamInputConfigurationEnableType : int
{
	k_ESteamInputConfigurationEnableType_None			= 0x0000,
	k_ESteamInputConfigurationEnableType_Playstation	= 0x0001,
	k_ESteamInputConfigurationEnableType_Xbox			= 0x0002,
	k_ESteamInputConfigurationEnableType_Generic		= 0x0004,
	k_ESteamInputConfigurationEnableType_Switch			= 0x0008,
}

/// <summary>
/// <para>These values are passed into SetLEDColor</para>
/// </summary>
public enum ESteamInputLEDFlag : int
{
	k_ESteamInputLEDFlag_SetColor,
	/// <summary>
	/// <para>Restore the LED color to the user&apos;s preference setting as set in the controller personalization menu.</para>
	/// <para>This also happens automatically on exit of your game.</para>
	/// </summary>
	k_ESteamInputLEDFlag_RestoreUserDefault
}

/// <summary>
/// <para>These values are passed into GetGlyphPNGForActionOrigin</para>
/// </summary>
public enum ESteamInputGlyphSize : int
{
	/// <summary>
	/// <para>32x32 pixels</para>
	/// </summary>
	k_ESteamInputGlyphSize_Small,
	/// <summary>
	/// <para>128x128 pixels</para>
	/// </summary>
	k_ESteamInputGlyphSize_Medium,
	/// <summary>
	/// <para>256x256 pixels</para>
	/// </summary>
	k_ESteamInputGlyphSize_Large,
	k_ESteamInputGlyphSize_Count,
}

public enum ESteamInputGlyphStyle : int
{
	/// <summary>
	/// <para>Base-styles - cannot mix</para>
	/// <para>Face buttons will have colored labels/outlines on a knocked out background</para>
	/// </summary>
	ESteamInputGlyphStyle_Knockout 	= 0x0,
	/// <summary>
	/// <para>Rest of inputs will have white detail/borders on a knocked out background</para>
	/// <para>Black detail/borders on a white background</para>
	/// </summary>
	ESteamInputGlyphStyle_Light		= 0x1,
	/// <summary>
	/// <para>White detail/borders on a black background</para>
	/// </summary>
	ESteamInputGlyphStyle_Dark 		= 0x2,
	/// <summary>
	/// <para>Modifiers</para>
	/// <para>Default ABXY/PS equivalent glyphs have a solid fill w/ color matching the physical buttons on the device</para>
	/// <para>ABXY Buttons will match the base style color instead of their normal associated color</para>
	/// </summary>
	ESteamInputGlyphStyle_NeutralColorABXY 	= 0x10,
	/// <summary>
	/// <para>ABXY Buttons will have a solid fill</para>
	/// </summary>
	ESteamInputGlyphStyle_SolidABXY 		= 0x20,
}

public enum ESteamInputActionEventType : int
{
	ESteamInputActionEventType_DigitalAction,
	ESteamInputActionEventType_AnalogAction,
}

[Flags]
public enum ESteamItemFlags : int
{
	/// <summary>
	/// <para>Item status flags - these flags are permanently attached to specific item instances</para>
	/// <para>This item is account-locked and cannot be traded or given away.</para>
	/// </summary>
	k_ESteamItemNoTrade = 1 << 0,
	/// <summary>
	/// <para>Action confirmation flags - these flags are set one time only, as part of a result set</para>
	/// <para>The item has been destroyed, traded away, expired, or otherwise invalidated</para>
	/// </summary>
	k_ESteamItemRemoved = 1 << 8,
	/// <summary>
	/// <para>The item quantity has been decreased by 1 via ConsumeItem API.</para>
	/// </summary>
	k_ESteamItemConsumed = 1 << 9,

	// All other flag bits are currently reserved for internal Steam use at this time.
	// Do not assume anything about the state of other flags which are not defined here.
}

/// <summary>
/// <para></para>
/// </summary>
public enum AudioPlayback_Status : int
{
	AudioPlayback_Undefined = 0,
	AudioPlayback_Playing = 1,
	AudioPlayback_Paused = 2,
	AudioPlayback_Idle = 3
}

/// <summary>
/// <para>list of possible errors returned by SendP2PPacket() API</para>
/// <para>these will be posted in the P2PSessionConnectFail_t callback</para>
/// </summary>
public enum EP2PSessionError : int
{
	k_EP2PSessionErrorNone = 0,
	/// <summary>
	/// <para>local user doesn&apos;t own the app that is running</para>
	/// </summary>
	k_EP2PSessionErrorNoRightsToApp = 2,
	/// <summary>
	/// <para>target isn&apos;t responding, perhaps not calling AcceptP2PSessionWithUser()</para>
	/// </summary>
	k_EP2PSessionErrorTimeout = 4,
	/// <summary>
	/// <para>corporate firewalls can also block this (NAT traversal is not firewall traversal)</para>
	/// <para>make sure that UDP ports 3478, 4379, and 4380 are open in an outbound direction</para>
	/// <para>The following error codes were removed and will never be sent.</para>
	/// <para>For privacy reasons, there is no reply if the user is offline or playing another game.</para>
	/// </summary>
	k_EP2PSessionErrorNotRunningApp_DELETED = 1,
	k_EP2PSessionErrorDestinationNotLoggedIn_DELETED = 3,
	k_EP2PSessionErrorMax = 5
}

/// <summary>
/// <para>SendP2PPacket() send types</para>
/// <para>Typically k_EP2PSendUnreliable is what you want for UDP-like packets, k_EP2PSendReliable for TCP-like packets</para>
/// </summary>
public enum EP2PSend : int
{
	/// <summary>
	/// <para>Basic UDP send. Packets can&apos;t be bigger than 1200 bytes (your typical MTU size). Can be lost, or arrive out of order (rare).</para>
	/// <para>The sending API does have some knowledge of the underlying connection, so if there is no NAT-traversal accomplished or</para>
	/// <para>there is a recognized adjustment happening on the connection, the packet will be batched until the connection is open again.</para>
	/// </summary>
	k_EP2PSendUnreliable = 0,
	/// <summary>
	/// <para>As above, but if the underlying p2p connection isn&apos;t yet established the packet will just be thrown away. Using this on the first</para>
	/// <para>packet sent to a remote host almost guarantees the packet will be dropped.</para>
	/// <para>This is only really useful for kinds of data that should never buffer up, i.e. voice payload packets</para>
	/// </summary>
	k_EP2PSendUnreliableNoDelay = 1,
	/// <summary>
	/// <para>Reliable message send. Can send up to 1MB of data in a single message.</para>
	/// <para>Does fragmentation/re-assembly of messages under the hood, as well as a sliding window for efficient sends of large chunks of data.</para>
	/// </summary>
	k_EP2PSendReliable = 2,
	/// <summary>
	/// <para>As above, but applies the Nagle algorithm to the send - sends will accumulate</para>
	/// <para>until the current MTU size (typically ~1200 bytes, but can change) or ~200ms has passed (Nagle algorithm).</para>
	/// <para>Useful if you want to send a set of smaller messages but have the coalesced into a single packet</para>
	/// <para>Since the reliable stream is all ordered, you can do several small message sends with k_EP2PSendReliableWithBuffering and then</para>
	/// <para>do a normal k_EP2PSendReliable to force all the buffered data to be sent.</para>
	/// </summary>
	k_EP2PSendReliableWithBuffering = 3,

}

/// <summary>
/// <para>connection progress indicators, used by CreateP2PConnectionSocket()</para>
/// </summary>
public enum ESNetSocketState : int
{
	k_ESNetSocketStateInvalid = 0,
	/// <summary>
	/// <para>communication is valid</para>
	/// </summary>
	k_ESNetSocketStateConnected = 1,
	/// <summary>
	/// <para>states while establishing a connection</para>
	/// <para>the connection state machine has started</para>
	/// </summary>
	k_ESNetSocketStateInitiated = 10,
	/// <summary>
	/// <para>p2p connections</para>
	/// <para>we&apos;ve found our local IP info</para>
	/// </summary>
	k_ESNetSocketStateLocalCandidatesFound = 11,
	/// <summary>
	/// <para>we&apos;ve received information from the remote machine, via the Steam back-end, about their IP info</para>
	/// </summary>
	k_ESNetSocketStateReceivedRemoteCandidates = 12,
	/// <summary>
	/// <para>direct connections</para>
	/// <para>we&apos;ve received a challenge packet from the server</para>
	/// </summary>
	k_ESNetSocketStateChallengeHandshake = 15,
	/// <summary>
	/// <para>failure states</para>
	/// <para>the API shut it down, and we&apos;re in the process of telling the other end</para>
	/// </summary>
	k_ESNetSocketStateDisconnecting = 21,
	/// <summary>
	/// <para>the API shut it down, and we&apos;ve completed shutdown</para>
	/// </summary>
	k_ESNetSocketStateLocalDisconnect = 22,
	/// <summary>
	/// <para>we timed out while trying to creating the connection</para>
	/// </summary>
	k_ESNetSocketStateTimeoutDuringConnect = 23,
	/// <summary>
	/// <para>the remote end has disconnected from us</para>
	/// </summary>
	k_ESNetSocketStateRemoteEndDisconnected = 24,
	/// <summary>
	/// <para>connection has been broken; either the other end has disappeared or our local network connection has broke</para>
	/// </summary>
	k_ESNetSocketStateConnectionBroken = 25,

}

/// <summary>
/// <para>describes how the socket is currently connected</para>
/// </summary>
public enum ESNetSocketConnectionType : int
{
	k_ESNetSocketConnectionTypeNotConnected = 0,
	k_ESNetSocketConnectionTypeUDP = 1,
	k_ESNetSocketConnectionTypeUDPRelay = 2,
}

/// <summary>
/// <para>Feature types for parental settings</para>
/// </summary>
public enum EParentalFeature : int
{
	k_EFeatureInvalid = 0,
	k_EFeatureStore = 1,
	k_EFeatureCommunity = 2,
	k_EFeatureProfile = 3,
	k_EFeatureFriends = 4,
	k_EFeatureNews = 5,
	k_EFeatureTrading = 6,
	k_EFeatureSettings = 7,
	k_EFeatureConsole = 8,
	k_EFeatureBrowser = 9,
	k_EFeatureParentalSetup = 10,
	k_EFeatureLibrary = 11,
	k_EFeatureTest = 12,
	k_EFeatureSiteLicense = 13,
	k_EFeatureKioskMode_Deprecated = 14,
	k_EFeatureBlockAlways = 15,
	k_EFeatureMax
}

/// <summary>
/// <para>The form factor of a device</para>
/// </summary>
public enum ESteamDeviceFormFactor : int
{
	k_ESteamDeviceFormFactorUnknown		= 0,
	k_ESteamDeviceFormFactorPhone		= 1,
	k_ESteamDeviceFormFactorTablet		= 2,
	k_ESteamDeviceFormFactorComputer	= 3,
	k_ESteamDeviceFormFactorTV			= 4,
	k_ESteamDeviceFormFactorVRHeadset	= 5,
}

/// <summary>
/// <para>The type of input in ERemotePlayInput_t</para>
/// </summary>
public enum ERemotePlayInputType : int
{
	k_ERemotePlayInputUnknown,
	k_ERemotePlayInputMouseMotion,
	k_ERemotePlayInputMouseButtonDown,
	k_ERemotePlayInputMouseButtonUp,
	k_ERemotePlayInputMouseWheel,
	k_ERemotePlayInputKeyDown,
	k_ERemotePlayInputKeyUp
}

/// <summary>
/// <para>Mouse buttons in ERemotePlayInput_t</para>
/// </summary>
public enum ERemotePlayMouseButton : int
{
	k_ERemotePlayMouseButtonLeft = 0x0001,
	k_ERemotePlayMouseButtonRight = 0x0002,
	k_ERemotePlayMouseButtonMiddle = 0x0010,
	k_ERemotePlayMouseButtonX1 = 0x0020,
	k_ERemotePlayMouseButtonX2 = 0x0040,
}

/// <summary>
/// <para>Mouse wheel direction in ERemotePlayInput_t</para>
/// </summary>
public enum ERemotePlayMouseWheelDirection : int
{
	k_ERemotePlayMouseWheelUp = 1,
	k_ERemotePlayMouseWheelDown = 2,
	k_ERemotePlayMouseWheelLeft = 3,
	k_ERemotePlayMouseWheelRight = 4,
}

/// <summary>
/// <para>Key scancode in ERemotePlayInput_t</para>
/// <para>This is a USB scancode value as defined for the Keyboard/Keypad Page (0x07)</para>
/// <para>This enumeration isn&apos;t a complete list, just the most commonly used keys.</para>
/// </summary>
public enum ERemotePlayScancode : int
{
	k_ERemotePlayScancodeUnknown = 0,
	k_ERemotePlayScancodeA = 4,
	k_ERemotePlayScancodeB = 5,
	k_ERemotePlayScancodeC = 6,
	k_ERemotePlayScancodeD = 7,
	k_ERemotePlayScancodeE = 8,
	k_ERemotePlayScancodeF = 9,
	k_ERemotePlayScancodeG = 10,
	k_ERemotePlayScancodeH = 11,
	k_ERemotePlayScancodeI = 12,
	k_ERemotePlayScancodeJ = 13,
	k_ERemotePlayScancodeK = 14,
	k_ERemotePlayScancodeL = 15,
	k_ERemotePlayScancodeM = 16,
	k_ERemotePlayScancodeN = 17,
	k_ERemotePlayScancodeO = 18,
	k_ERemotePlayScancodeP = 19,
	k_ERemotePlayScancodeQ = 20,
	k_ERemotePlayScancodeR = 21,
	k_ERemotePlayScancodeS = 22,
	k_ERemotePlayScancodeT = 23,
	k_ERemotePlayScancodeU = 24,
	k_ERemotePlayScancodeV = 25,
	k_ERemotePlayScancodeW = 26,
	k_ERemotePlayScancodeX = 27,
	k_ERemotePlayScancodeY = 28,
	k_ERemotePlayScancodeZ = 29,
	k_ERemotePlayScancode1 = 30,
	k_ERemotePlayScancode2 = 31,
	k_ERemotePlayScancode3 = 32,
	k_ERemotePlayScancode4 = 33,
	k_ERemotePlayScancode5 = 34,
	k_ERemotePlayScancode6 = 35,
	k_ERemotePlayScancode7 = 36,
	k_ERemotePlayScancode8 = 37,
	k_ERemotePlayScancode9 = 38,
	k_ERemotePlayScancode0 = 39,
	k_ERemotePlayScancodeReturn = 40,
	k_ERemotePlayScancodeEscape = 41,
	k_ERemotePlayScancodeBackspace = 42,
	k_ERemotePlayScancodeTab = 43,
	k_ERemotePlayScancodeSpace = 44,
	k_ERemotePlayScancodeMinus = 45,
	k_ERemotePlayScancodeEquals = 46,
	k_ERemotePlayScancodeLeftBracket = 47,
	k_ERemotePlayScancodeRightBracket = 48,
	k_ERemotePlayScancodeBackslash = 49,
	k_ERemotePlayScancodeSemicolon = 51,
	k_ERemotePlayScancodeApostrophe = 52,
	k_ERemotePlayScancodeGrave = 53,
	k_ERemotePlayScancodeComma = 54,
	k_ERemotePlayScancodePeriod = 55,
	k_ERemotePlayScancodeSlash = 56,
	k_ERemotePlayScancodeCapsLock = 57,
	k_ERemotePlayScancodeF1 = 58,
	k_ERemotePlayScancodeF2 = 59,
	k_ERemotePlayScancodeF3 = 60,
	k_ERemotePlayScancodeF4 = 61,
	k_ERemotePlayScancodeF5 = 62,
	k_ERemotePlayScancodeF6 = 63,
	k_ERemotePlayScancodeF7 = 64,
	k_ERemotePlayScancodeF8 = 65,
	k_ERemotePlayScancodeF9 = 66,
	k_ERemotePlayScancodeF10 = 67,
	k_ERemotePlayScancodeF11 = 68,
	k_ERemotePlayScancodeF12 = 69,
	k_ERemotePlayScancodeInsert = 73,
	k_ERemotePlayScancodeHome = 74,
	k_ERemotePlayScancodePageUp = 75,
	k_ERemotePlayScancodeDelete = 76,
	k_ERemotePlayScancodeEnd = 77,
	k_ERemotePlayScancodePageDown = 78,
	k_ERemotePlayScancodeRight = 79,
	k_ERemotePlayScancodeLeft = 80,
	k_ERemotePlayScancodeDown = 81,
	k_ERemotePlayScancodeUp = 82,
	k_ERemotePlayScancodeLeftControl = 224,
	k_ERemotePlayScancodeLeftShift = 225,
	k_ERemotePlayScancodeLeftAlt = 226,
	/// <summary>
	/// <para>windows, command (apple), meta</para>
	/// </summary>
	k_ERemotePlayScancodeLeftGUI = 227,
	k_ERemotePlayScancodeRightControl = 228,
	k_ERemotePlayScancodeRightShift = 229,
	k_ERemotePlayScancodeRightALT = 230,
	/// <summary>
	/// <para>windows, command (apple), meta</para>
	/// </summary>
	k_ERemotePlayScancodeRightGUI = 231,
}

/// <summary>
/// <para>Key modifier in ERemotePlayInput_t</para>
/// </summary>
public enum ERemotePlayKeyModifier : int
{
	k_ERemotePlayKeyModifierNone			= 0x0000,
	k_ERemotePlayKeyModifierLeftShift		= 0x0001,
	k_ERemotePlayKeyModifierRightShift		= 0x0002,
	k_ERemotePlayKeyModifierLeftControl		= 0x0040,
	k_ERemotePlayKeyModifierRightControl	= 0x0080,
	k_ERemotePlayKeyModifierLeftAlt			= 0x0100,
	k_ERemotePlayKeyModifierRightAlt		= 0x0200,
	k_ERemotePlayKeyModifierLeftGUI			= 0x0400,
	k_ERemotePlayKeyModifierRightGUI		= 0x0800,
	k_ERemotePlayKeyModifierNumLock			= 0x1000,
	k_ERemotePlayKeyModifierCapsLock		= 0x2000,
	k_ERemotePlayKeyModifierMask			= 0xFFFF,
}

[Flags]
public enum ERemoteStoragePlatform : int
{
	k_ERemoteStoragePlatformNone		= 0,
	k_ERemoteStoragePlatformWindows		= (1 << 0),
	k_ERemoteStoragePlatformOSX			= (1 << 1),
	k_ERemoteStoragePlatformPS3			= (1 << 2),
	k_ERemoteStoragePlatformLinux		= (1 << 3),
	k_ERemoteStoragePlatformSwitch		= (1 << 4),
	k_ERemoteStoragePlatformAndroid		= (1 << 5),
	k_ERemoteStoragePlatformIOS			= (1 << 6),
	/// <summary>
	/// <para>NB we get one more before we need to widen some things</para>
	/// </summary>
	k_ERemoteStoragePlatformAll = -1
}

public enum ERemoteStoragePublishedFileVisibility : int
{
	k_ERemoteStoragePublishedFileVisibilityPublic = 0,
	k_ERemoteStoragePublishedFileVisibilityFriendsOnly = 1,
	k_ERemoteStoragePublishedFileVisibilityPrivate = 2,
	k_ERemoteStoragePublishedFileVisibilityUnlisted = 3,
}

public enum EWorkshopFileType : int
{
	k_EWorkshopFileTypeFirst = 0,
	/// <summary>
	/// <para>normal Workshop item that can be subscribed to</para>
	/// </summary>
	k_EWorkshopFileTypeCommunity			  = 0,
	/// <summary>
	/// <para>Workshop item that is meant to be voted on for the purpose of selling in-game</para>
	/// </summary>
	k_EWorkshopFileTypeMicrotransaction		  = 1,
	/// <summary>
	/// <para>a collection of Workshop or Greenlight items</para>
	/// </summary>
	k_EWorkshopFileTypeCollection			  = 2,
	/// <summary>
	/// <para>artwork</para>
	/// </summary>
	k_EWorkshopFileTypeArt					  = 3,
	/// <summary>
	/// <para>external video</para>
	/// </summary>
	k_EWorkshopFileTypeVideo				  = 4,
	/// <summary>
	/// <para>screenshot</para>
	/// </summary>
	k_EWorkshopFileTypeScreenshot			  = 5,
	/// <summary>
	/// <para>Greenlight game entry</para>
	/// </summary>
	k_EWorkshopFileTypeGame					  = 6,
	/// <summary>
	/// <para>Greenlight software entry</para>
	/// </summary>
	k_EWorkshopFileTypeSoftware				  = 7,
	/// <summary>
	/// <para>Greenlight concept</para>
	/// </summary>
	k_EWorkshopFileTypeConcept				  = 8,
	/// <summary>
	/// <para>Steam web guide</para>
	/// </summary>
	k_EWorkshopFileTypeWebGuide				  = 9,
	/// <summary>
	/// <para>application integrated guide</para>
	/// </summary>
	k_EWorkshopFileTypeIntegratedGuide		  = 10,
	/// <summary>
	/// <para>Workshop merchandise meant to be voted on for the purpose of being sold</para>
	/// </summary>
	k_EWorkshopFileTypeMerch				  = 11,
	/// <summary>
	/// <para>Steam Controller bindings</para>
	/// </summary>
	k_EWorkshopFileTypeControllerBinding	  = 12,
	/// <summary>
	/// <para>internal</para>
	/// </summary>
	k_EWorkshopFileTypeSteamworksAccessInvite = 13,
	/// <summary>
	/// <para>Steam video</para>
	/// </summary>
	k_EWorkshopFileTypeSteamVideo			  = 14,
	/// <summary>
	/// <para>managed completely by the game, not the user, and not shown on the web</para>
	/// </summary>
	k_EWorkshopFileTypeGameManagedItem		  = 15,
	/// <summary>
	/// <para>internal</para>
	/// </summary>
	k_EWorkshopFileTypeClip					  = 16,
	/// <summary>
	/// <para>Update k_EWorkshopFileTypeMax if you add values.</para>
	/// </summary>
	k_EWorkshopFileTypeMax = 17

}

public enum EWorkshopVote : int
{
	k_EWorkshopVoteUnvoted = 0,
	k_EWorkshopVoteFor = 1,
	k_EWorkshopVoteAgainst = 2,
	k_EWorkshopVoteLater = 3,
}

public enum EWorkshopFileAction : int
{
	k_EWorkshopFileActionPlayed = 0,
	k_EWorkshopFileActionCompleted = 1,
}

public enum EWorkshopEnumerationType : int
{
	k_EWorkshopEnumerationTypeRankedByVote = 0,
	k_EWorkshopEnumerationTypeRecent = 1,
	k_EWorkshopEnumerationTypeTrending = 2,
	k_EWorkshopEnumerationTypeFavoritesOfFriends = 3,
	k_EWorkshopEnumerationTypeVotedByFriends = 4,
	k_EWorkshopEnumerationTypeContentByFriends = 5,
	k_EWorkshopEnumerationTypeRecentFromFollowedUsers = 6,
}

public enum EWorkshopVideoProvider : int
{
	k_EWorkshopVideoProviderNone = 0,
	k_EWorkshopVideoProviderYoutube = 1
}

public enum EUGCReadAction : int
{
	/// <summary>
	/// <para>Keeps the file handle open unless the last byte is read.  You can use this when reading large files (over 100MB) in sequential chunks.</para>
	/// <para>If the last byte is read, this will behave the same as k_EUGCRead_Close.  Otherwise, it behaves the same as k_EUGCRead_ContinueReading.</para>
	/// <para>This value maintains the same behavior as before the EUGCReadAction parameter was introduced.</para>
	/// </summary>
	k_EUGCRead_ContinueReadingUntilFinished = 0,
	/// <summary>
	/// <para>Keeps the file handle open.  Use this when using UGCRead to seek to different parts of the file.</para>
	/// <para>When you are done seeking around the file, make a final call with k_EUGCRead_Close to close it.</para>
	/// </summary>
	k_EUGCRead_ContinueReading = 1,
	/// <summary>
	/// <para>Frees the file handle.  Use this when you&apos;re done reading the content.</para>
	/// <para>To read the file from Steam again you will need to call UGCDownload again.</para>
	/// </summary>
	k_EUGCRead_Close = 2,
}

public enum ERemoteStorageLocalFileChange : int
{
	k_ERemoteStorageLocalFileChange_Invalid = 0,
	/// <summary>
	/// <para>The file was updated from another device</para>
	/// </summary>
	k_ERemoteStorageLocalFileChange_FileUpdated = 1,
	/// <summary>
	/// <para>The file was deleted by another device</para>
	/// </summary>
	k_ERemoteStorageLocalFileChange_FileDeleted = 2,
}

public enum ERemoteStorageFilePathType : int
{
	k_ERemoteStorageFilePathType_Invalid = 0,
	/// <summary>
	/// <para>The file is directly accessed by the game and this is the full path</para>
	/// </summary>
	k_ERemoteStorageFilePathType_Absolute = 1,
	/// <summary>
	/// <para>The file is accessed via the ISteamRemoteStorage API and this is the filename</para>
	/// </summary>
	k_ERemoteStorageFilePathType_APIFilename = 2,
}

public enum EVRScreenshotType : int
{
	k_EVRScreenshotType_None			= 0,
	k_EVRScreenshotType_Mono			= 1,
	k_EVRScreenshotType_Stereo			= 2,
	k_EVRScreenshotType_MonoCubemap		= 3,
	k_EVRScreenshotType_MonoPanorama	= 4,
	k_EVRScreenshotType_StereoPanorama	= 5
}

/// <summary>
/// <para>callbacks</para>
/// <para>Controls the color of the timeline bar segments. The value names listed here map to a multiplayer game, where</para>
/// <para>the user starts a game (in menus), then joins a multiplayer session that first has a character selection lobby</para>
/// <para>then finally the multiplayer session starts. However, you can also map these values to any type of game. In a single</para>
/// <para>player game where you visit towns &amp; dungeons, you could set k_ETimelineGameMode_Menus when the player is in a town</para>
/// <para>buying items, k_ETimelineGameMode_Staging for when a dungeon is loading and k_ETimelineGameMode_Playing for when</para>
/// <para>inside the dungeon fighting monsters.</para>
/// </summary>
public enum ETimelineGameMode : int
{
	k_ETimelineGameMode_Invalid = 0,
	k_ETimelineGameMode_Playing = 1,
	k_ETimelineGameMode_Staging = 2,
	k_ETimelineGameMode_Menus = 3,
	k_ETimelineGameMode_LoadingScreen = 4,
	/// <summary>
	/// <para>one past the last valid value</para>
	/// </summary>
	k_ETimelineGameMode_Max,
}

/// <summary>
/// <para>Used in AddTimelineEvent, where Featured events will be offered before Standard events</para>
/// </summary>
public enum ETimelineEventClipPriority : int
{
	k_ETimelineEventClipPriority_Invalid = 0,
	k_ETimelineEventClipPriority_None = 1,
	k_ETimelineEventClipPriority_Standard = 2,
	k_ETimelineEventClipPriority_Featured = 3,
}

/// <summary>
/// <para>Matching UGC types for queries</para>
/// </summary>
public enum EUGCMatchingUGCType : int
{
	/// <summary>
	/// <para>both mtx items and ready-to-use items</para>
	/// </summary>
	k_EUGCMatchingUGCType_Items				 = 0,
	k_EUGCMatchingUGCType_Items_Mtx			 = 1,
	k_EUGCMatchingUGCType_Items_ReadyToUse	 = 2,
	k_EUGCMatchingUGCType_Collections		 = 3,
	k_EUGCMatchingUGCType_Artwork			 = 4,
	k_EUGCMatchingUGCType_Videos			 = 5,
	k_EUGCMatchingUGCType_Screenshots		 = 6,
	/// <summary>
	/// <para>both web guides and integrated guides</para>
	/// </summary>
	k_EUGCMatchingUGCType_AllGuides			 = 7,
	k_EUGCMatchingUGCType_WebGuides			 = 8,
	k_EUGCMatchingUGCType_IntegratedGuides	 = 9,
	/// <summary>
	/// <para>ready-to-use items and integrated guides</para>
	/// </summary>
	k_EUGCMatchingUGCType_UsableInGame		 = 10,
	k_EUGCMatchingUGCType_ControllerBindings = 11,
	/// <summary>
	/// <para>game managed items (not managed by users)</para>
	/// </summary>
	k_EUGCMatchingUGCType_GameManagedItems	 = 12,
	/// <summary>
	/// <para>@note: will only be valid for CreateQueryUserUGCRequest requests</para>
	/// </summary>
	k_EUGCMatchingUGCType_All				 = ~0,
}

/// <summary>
/// <para>Different lists of published UGC for a user.</para>
/// <para>If the current logged in user is different than the specified user, then some options may not be allowed.</para>
/// </summary>
public enum EUserUGCList : int
{
	k_EUserUGCList_Published,
	k_EUserUGCList_VotedOn,
	k_EUserUGCList_VotedUp,
	k_EUserUGCList_VotedDown,
	k_EUserUGCList_WillVoteLater,
	k_EUserUGCList_Favorited,
	k_EUserUGCList_Subscribed,
	k_EUserUGCList_UsedOrPlayed,
	k_EUserUGCList_Followed,
}

/// <summary>
/// <para>Sort order for user published UGC lists (defaults to creation order descending)</para>
/// </summary>
public enum EUserUGCListSortOrder : int
{
	k_EUserUGCListSortOrder_CreationOrderDesc,
	k_EUserUGCListSortOrder_CreationOrderAsc,
	k_EUserUGCListSortOrder_TitleAsc,
	k_EUserUGCListSortOrder_LastUpdatedDesc,
	k_EUserUGCListSortOrder_SubscriptionDateDesc,
	k_EUserUGCListSortOrder_VoteScoreDesc,
	k_EUserUGCListSortOrder_ForModeration,
}

/// <summary>
/// <para>Combination of sorting and filtering for queries across all UGC</para>
/// </summary>
public enum EUGCQuery : int
{
	k_EUGCQuery_RankedByVote								  = 0,
	k_EUGCQuery_RankedByPublicationDate						  = 1,
	k_EUGCQuery_AcceptedForGameRankedByAcceptanceDate		  = 2,
	k_EUGCQuery_RankedByTrend								  = 3,
	k_EUGCQuery_FavoritedByFriendsRankedByPublicationDate	  = 4,
	k_EUGCQuery_CreatedByFriendsRankedByPublicationDate		  = 5,
	k_EUGCQuery_RankedByNumTimesReported					  = 6,
	k_EUGCQuery_CreatedByFollowedUsersRankedByPublicationDate = 7,
	k_EUGCQuery_NotYetRated									  = 8,
	k_EUGCQuery_RankedByTotalVotesAsc						  = 9,
	k_EUGCQuery_RankedByVotesUp								  = 10,
	k_EUGCQuery_RankedByTextSearch							  = 11,
	k_EUGCQuery_RankedByTotalUniqueSubscriptions			  = 12,
	k_EUGCQuery_RankedByPlaytimeTrend						  = 13,
	k_EUGCQuery_RankedByTotalPlaytime						  = 14,
	k_EUGCQuery_RankedByAveragePlaytimeTrend				  = 15,
	k_EUGCQuery_RankedByLifetimeAveragePlaytime				  = 16,
	k_EUGCQuery_RankedByPlaytimeSessionsTrend				  = 17,
	k_EUGCQuery_RankedByLifetimePlaytimeSessions			  = 18,
	k_EUGCQuery_RankedByLastUpdatedDate						  = 19,
}

public enum EItemUpdateStatus : int
{
	/// <summary>
	/// <para>The item update handle was invalid, job might be finished, listen too SubmitItemUpdateResult_t</para>
	/// </summary>
	k_EItemUpdateStatusInvalid 				= 0,
	/// <summary>
	/// <para>The item update is processing configuration data</para>
	/// </summary>
	k_EItemUpdateStatusPreparingConfig 		= 1,
	/// <summary>
	/// <para>The item update is reading and processing content files</para>
	/// </summary>
	k_EItemUpdateStatusPreparingContent		= 2,
	/// <summary>
	/// <para>The item update is uploading content changes to Steam</para>
	/// </summary>
	k_EItemUpdateStatusUploadingContent		= 3,
	/// <summary>
	/// <para>The item update is uploading new preview file image</para>
	/// </summary>
	k_EItemUpdateStatusUploadingPreviewFile	= 4,
	/// <summary>
	/// <para>The item update is committing all changes</para>
	/// </summary>
	k_EItemUpdateStatusCommittingChanges	= 5
}

[Flags]
public enum EItemState : int
{
	/// <summary>
	/// <para>item not tracked on client</para>
	/// </summary>
	k_EItemStateNone			= 0,
	/// <summary>
	/// <para>current user is subscribed to this item. Not just cached.</para>
	/// </summary>
	k_EItemStateSubscribed		= 1,
	/// <summary>
	/// <para>item was created with ISteamRemoteStorage</para>
	/// </summary>
	k_EItemStateLegacyItem		= 2,
	/// <summary>
	/// <para>item is installed and usable (but maybe out of date)</para>
	/// </summary>
	k_EItemStateInstalled		= 4,
	/// <summary>
	/// <para>items needs an update. Either because it&apos;s not installed yet or creator updated content</para>
	/// </summary>
	k_EItemStateNeedsUpdate		= 8,
	/// <summary>
	/// <para>item update is currently downloading</para>
	/// </summary>
	k_EItemStateDownloading		= 16,
	/// <summary>
	/// <para>DownloadItem() was called for this item, content isn&apos;t available until DownloadItemResult_t is fired</para>
	/// </summary>
	k_EItemStateDownloadPending	= 32,
	/// <summary>
	/// <para>Item is disabled locally, so it shouldn&apos;t be considered subscribed</para>
	/// </summary>
	k_EItemStateDisabledLocally = 64,
}

public enum EItemStatistic : int
{
	k_EItemStatistic_NumSubscriptions					 = 0,
	k_EItemStatistic_NumFavorites						 = 1,
	k_EItemStatistic_NumFollowers						 = 2,
	k_EItemStatistic_NumUniqueSubscriptions				 = 3,
	k_EItemStatistic_NumUniqueFavorites					 = 4,
	k_EItemStatistic_NumUniqueFollowers					 = 5,
	k_EItemStatistic_NumUniqueWebsiteViews				 = 6,
	k_EItemStatistic_ReportScore						 = 7,
	k_EItemStatistic_NumSecondsPlayed					 = 8,
	k_EItemStatistic_NumPlaytimeSessions				 = 9,
	k_EItemStatistic_NumComments						 = 10,
	k_EItemStatistic_NumSecondsPlayedDuringTimePeriod	 = 11,
	k_EItemStatistic_NumPlaytimeSessionsDuringTimePeriod = 12,
}

public enum EItemPreviewType : int
{
	/// <summary>
	/// <para>standard image file expected (e.g. jpg, png, gif, etc.)</para>
	/// </summary>
	k_EItemPreviewType_Image							= 0,
	/// <summary>
	/// <para>video id is stored</para>
	/// </summary>
	k_EItemPreviewType_YouTubeVideo						= 1,
	/// <summary>
	/// <para>model id is stored</para>
	/// </summary>
	k_EItemPreviewType_Sketchfab						= 2,
	/// <summary>
	/// <para>standard image file expected - cube map in the layout</para>
	/// </summary>
	k_EItemPreviewType_EnvironmentMap_HorizontalCross	= 3,
	/// <summary>
	/// <para>+---+---+-------+</para>
	/// <para>|   |Up |       |</para>
	/// <para>+---+---+---+---+</para>
	/// <para>| L | F | R | B |</para>
	/// <para>+---+---+---+---+</para>
	/// <para>|   |Dn |       |</para>
	/// <para>+---+---+---+---+</para>
	/// <para>standard image file expected</para>
	/// </summary>
	k_EItemPreviewType_EnvironmentMap_LatLong			= 4,
	/// <summary>
	/// <para>clip id is stored</para>
	/// </summary>
	k_EItemPreviewType_Clip								= 5,
	/// <summary>
	/// <para>you can specify your own types above this value</para>
	/// </summary>
	k_EItemPreviewType_ReservedMax						= 255,
}

public enum EUGCContentDescriptorID : int
{
	k_EUGCContentDescriptor_NudityOrSexualContent	= 1,
	k_EUGCContentDescriptor_FrequentViolenceOrGore	= 2,
	k_EUGCContentDescriptor_AdultOnlySexualContent	= 3,
	k_EUGCContentDescriptor_GratuitousSexualContent = 4,
	k_EUGCContentDescriptor_AnyMatureContent		= 5,
}

public enum EFailureType : int
{
	k_EFailureFlushedCallbackQueue,
	k_EFailurePipeFail,
}

/// <summary>
/// <para>type of data request, when downloading leaderboard entries</para>
/// </summary>
public enum ELeaderboardDataRequest : int
{
	k_ELeaderboardDataRequestGlobal = 0,
	k_ELeaderboardDataRequestGlobalAroundUser = 1,
	k_ELeaderboardDataRequestFriends = 2,
	k_ELeaderboardDataRequestUsers = 3
}

/// <summary>
/// <para>the sort order of a leaderboard</para>
/// </summary>
public enum ELeaderboardSortMethod : int
{
	k_ELeaderboardSortMethodNone = 0,
	/// <summary>
	/// <para>top-score is lowest number</para>
	/// </summary>
	k_ELeaderboardSortMethodAscending = 1,
	/// <summary>
	/// <para>top-score is highest number</para>
	/// </summary>
	k_ELeaderboardSortMethodDescending = 2,
}

/// <summary>
/// <para>the display type (used by the Steam Community web site) for a leaderboard</para>
/// </summary>
public enum ELeaderboardDisplayType : int
{
	k_ELeaderboardDisplayTypeNone = 0,
	/// <summary>
	/// <para>simple numerical score</para>
	/// </summary>
	k_ELeaderboardDisplayTypeNumeric = 1,
	/// <summary>
	/// <para>the score represents a time, in seconds</para>
	/// </summary>
	k_ELeaderboardDisplayTypeTimeSeconds = 2,
	/// <summary>
	/// <para>the score represents a time, in milliseconds</para>
	/// </summary>
	k_ELeaderboardDisplayTypeTimeMilliSeconds = 3,
}

public enum ELeaderboardUploadScoreMethod : int
{
	k_ELeaderboardUploadScoreMethodNone = 0,
	/// <summary>
	/// <para>Leaderboard will keep user&apos;s best score</para>
	/// </summary>
	k_ELeaderboardUploadScoreMethodKeepBest = 1,
	/// <summary>
	/// <para>Leaderboard will always replace score with specified</para>
	/// </summary>
	k_ELeaderboardUploadScoreMethodForceUpdate = 2,
}

/// <summary>
/// <para>Steam API call failure results</para>
/// </summary>
public enum ESteamAPICallFailure : int
{
	/// <summary>
	/// <para>no failure</para>
	/// </summary>
	k_ESteamAPICallFailureNone = -1,
	/// <summary>
	/// <para>the local Steam process has gone away</para>
	/// </summary>
	k_ESteamAPICallFailureSteamGone = 0,
	/// <summary>
	/// <para>the network connection to Steam has been broken, or was already broken</para>
	/// </summary>
	k_ESteamAPICallFailureNetworkFailure = 1,
	/// <summary>
	/// <para>SteamServersDisconnected_t callback will be sent around the same time</para>
	/// <para>SteamServersConnected_t will be sent when the client is able to talk to the Steam servers again</para>
	/// <para>the SteamAPICall_t handle passed in no longer exists</para>
	/// </summary>
	k_ESteamAPICallFailureInvalidHandle = 2,
	/// <summary>
	/// <para>GetAPICallResult() was called with the wrong callback type for this API call</para>
	/// </summary>
	k_ESteamAPICallFailureMismatchedCallback = 3,
}

/// <summary>
/// <para>Input modes for the Big Picture gamepad text entry</para>
/// </summary>
public enum EGamepadTextInputMode : int
{
	k_EGamepadTextInputModeNormal = 0,
	k_EGamepadTextInputModePassword = 1
}

/// <summary>
/// <para>Controls number of allowed lines for the Big Picture gamepad text entry</para>
/// </summary>
public enum EGamepadTextInputLineMode : int
{
	k_EGamepadTextInputLineModeSingleLine = 0,
	k_EGamepadTextInputLineModeMultipleLines = 1
}

public enum EFloatingGamepadTextInputMode : int
{
	/// <summary>
	/// <para>Enter dismisses the keyboard</para>
	/// </summary>
	k_EFloatingGamepadTextInputModeModeSingleLine = 0,
	/// <summary>
	/// <para>User needs to explictly close the keyboard</para>
	/// </summary>
	k_EFloatingGamepadTextInputModeModeMultipleLines = 1,
	/// <summary>
	/// <para>Keyboard layout is email, enter dismisses the keyboard</para>
	/// </summary>
	k_EFloatingGamepadTextInputModeModeEmail = 2,
	/// <summary>
	/// <para>Keyboard layout is numeric, enter dismisses the keyboard</para>
	/// </summary>
	k_EFloatingGamepadTextInputModeModeNumeric = 3,

}

/// <summary>
/// <para>The context where text filtering is being done</para>
/// </summary>
public enum ETextFilteringContext : int
{
	/// <summary>
	/// <para>Unknown context</para>
	/// </summary>
	k_ETextFilteringContextUnknown = 0,
	/// <summary>
	/// <para>Game content, only legally required filtering is performed</para>
	/// </summary>
	k_ETextFilteringContextGameContent = 1,
	/// <summary>
	/// <para>Chat from another player</para>
	/// </summary>
	k_ETextFilteringContextChat = 2,
	/// <summary>
	/// <para>Character or item name</para>
	/// </summary>
	k_ETextFilteringContextName = 3,
}

/// <summary>
/// <para>results for CheckFileSignature</para>
/// </summary>
public enum ECheckFileSignature : int
{
	k_ECheckFileSignatureInvalidSignature = 0,
	k_ECheckFileSignatureValidSignature = 1,
	k_ECheckFileSignatureFileNotFound = 2,
	k_ECheckFileSignatureNoSignaturesFoundForThisApp = 3,
	k_ECheckFileSignatureNoSignaturesFoundForThisFile = 4,
}

/// <summary>
/// <para>Steam API setup &amp; shutdown</para>
/// <para>These functions manage loading, initializing and shutdown of the steamclient.dll</para>
/// </summary>
public enum ESteamAPIInitResult : int
{
	k_ESteamAPIInitResult_OK = 0,
	/// <summary>
	/// <para>Some other failure</para>
	/// </summary>
	k_ESteamAPIInitResult_FailedGeneric = 1,
	/// <summary>
	/// <para>We cannot connect to Steam, steam probably isn&apos;t running</para>
	/// </summary>
	k_ESteamAPIInitResult_NoSteamClient = 2,
	/// <summary>
	/// <para>Steam client appears to be out of date</para>
	/// </summary>
	k_ESteamAPIInitResult_VersionMismatch = 3,
}

public enum EServerMode : int
{
	/// <summary>
	/// <para>DO NOT USE</para>
	/// </summary>
	eServerModeInvalid = 0,
	/// <summary>
	/// <para>Don&apos;t authenticate user logins and don&apos;t list on the server list</para>
	/// </summary>
	eServerModeNoAuthentication = 1,
	/// <summary>
	/// <para>Authenticate users, list on the server list, don&apos;t run VAC on clients that connect</para>
	/// </summary>
	eServerModeAuthentication = 2,
	/// <summary>
	/// <para>Authenticate users, list on the server list and VAC protect clients</para>
	/// </summary>
	eServerModeAuthenticationAndSecure = 3,
}

/// <summary>
/// <para>General result codes</para>
/// </summary>
public enum EResult : int
{
	/// <summary>
	/// <para>no result</para>
	/// </summary>
	k_EResultNone = 0,
	/// <summary>
	/// <para>success</para>
	/// </summary>
	k_EResultOK	= 1,
	/// <summary>
	/// <para>generic failure</para>
	/// </summary>
	k_EResultFail = 2,
	/// <summary>
	/// <para>no/failed network connection</para>
	/// </summary>
	k_EResultNoConnection = 3,
	// k_EResultNoConnectionRetry = 4,				// OBSOLETE - removed
	/// <summary>
	/// <para>password/ticket is invalid</para>
	/// </summary>
	k_EResultInvalidPassword = 5,
	/// <summary>
	/// <para>same user logged in elsewhere</para>
	/// </summary>
	k_EResultLoggedInElsewhere = 6,
	/// <summary>
	/// <para>protocol version is incorrect</para>
	/// </summary>
	k_EResultInvalidProtocolVer = 7,
	/// <summary>
	/// <para>a parameter is incorrect</para>
	/// </summary>
	k_EResultInvalidParam = 8,
	/// <summary>
	/// <para>file was not found</para>
	/// </summary>
	k_EResultFileNotFound = 9,
	/// <summary>
	/// <para>called method busy - action not taken</para>
	/// </summary>
	k_EResultBusy = 10,
	/// <summary>
	/// <para>called object was in an invalid state</para>
	/// </summary>
	k_EResultInvalidState = 11,
	/// <summary>
	/// <para>name is invalid</para>
	/// </summary>
	k_EResultInvalidName = 12,
	/// <summary>
	/// <para>email is invalid</para>
	/// </summary>
	k_EResultInvalidEmail = 13,
	/// <summary>
	/// <para>name is not unique</para>
	/// </summary>
	k_EResultDuplicateName = 14,
	/// <summary>
	/// <para>access is denied</para>
	/// </summary>
	k_EResultAccessDenied = 15,
	/// <summary>
	/// <para>operation timed out</para>
	/// </summary>
	k_EResultTimeout = 16,
	/// <summary>
	/// <para>VAC2 banned</para>
	/// </summary>
	k_EResultBanned = 17,
	/// <summary>
	/// <para>account not found</para>
	/// </summary>
	k_EResultAccountNotFound = 18,
	/// <summary>
	/// <para>steamID is invalid</para>
	/// </summary>
	k_EResultInvalidSteamID = 19,
	/// <summary>
	/// <para>The requested service is currently unavailable</para>
	/// </summary>
	k_EResultServiceUnavailable = 20,
	/// <summary>
	/// <para>The user is not logged on</para>
	/// </summary>
	k_EResultNotLoggedOn = 21,
	/// <summary>
	/// <para>Request is pending (may be in process, or waiting on third party)</para>
	/// </summary>
	k_EResultPending = 22,
	/// <summary>
	/// <para>Encryption or Decryption failed</para>
	/// </summary>
	k_EResultEncryptionFailure = 23,
	/// <summary>
	/// <para>Insufficient privilege</para>
	/// </summary>
	k_EResultInsufficientPrivilege = 24,
	/// <summary>
	/// <para>Too much of a good thing</para>
	/// </summary>
	k_EResultLimitExceeded = 25,
	/// <summary>
	/// <para>Access has been revoked (used for revoked guest passes)</para>
	/// </summary>
	k_EResultRevoked = 26,
	/// <summary>
	/// <para>License/Guest pass the user is trying to access is expired</para>
	/// </summary>
	k_EResultExpired = 27,
	/// <summary>
	/// <para>Guest pass has already been redeemed by account, cannot be acked again</para>
	/// </summary>
	k_EResultAlreadyRedeemed = 28,
	/// <summary>
	/// <para>The request is a duplicate and the action has already occurred in the past, ignored this time</para>
	/// </summary>
	k_EResultDuplicateRequest = 29,
	/// <summary>
	/// <para>All the games in this guest pass redemption request are already owned by the user</para>
	/// </summary>
	k_EResultAlreadyOwned = 30,
	/// <summary>
	/// <para>IP address not found</para>
	/// </summary>
	k_EResultIPNotFound = 31,
	/// <summary>
	/// <para>failed to write change to the data store</para>
	/// </summary>
	k_EResultPersistFailed = 32,
	/// <summary>
	/// <para>failed to acquire access lock for this operation</para>
	/// </summary>
	k_EResultLockingFailed = 33,
	k_EResultLogonSessionReplaced = 34,
	k_EResultConnectFailed = 35,
	k_EResultHandshakeFailed = 36,
	k_EResultIOFailure = 37,
	k_EResultRemoteDisconnect = 38,
	/// <summary>
	/// <para>failed to find the shopping cart requested</para>
	/// </summary>
	k_EResultShoppingCartNotFound = 39,
	/// <summary>
	/// <para>a user didn&apos;t allow it</para>
	/// </summary>
	k_EResultBlocked = 40,
	/// <summary>
	/// <para>target is ignoring sender</para>
	/// </summary>
	k_EResultIgnored = 41,
	/// <summary>
	/// <para>nothing matching the request found</para>
	/// </summary>
	k_EResultNoMatch = 42,
	k_EResultAccountDisabled = 43,
	/// <summary>
	/// <para>this service is not accepting content changes right now</para>
	/// </summary>
	k_EResultServiceReadOnly = 44,
	/// <summary>
	/// <para>account doesn&apos;t have value, so this feature isn&apos;t available</para>
	/// </summary>
	k_EResultAccountNotFeatured = 45,
	/// <summary>
	/// <para>allowed to take this action, but only because requester is admin</para>
	/// </summary>
	k_EResultAdministratorOK = 46,
	/// <summary>
	/// <para>A Version mismatch in content transmitted within the Steam protocol.</para>
	/// </summary>
	k_EResultContentVersion = 47,
	/// <summary>
	/// <para>The current CM can&apos;t service the user making a request, user should try another.</para>
	/// </summary>
	k_EResultTryAnotherCM = 48,
	/// <summary>
	/// <para>You are already logged in elsewhere, this cached credential login has failed.</para>
	/// </summary>
	k_EResultPasswordRequiredToKickSession = 49,
	/// <summary>
	/// <para>You are already logged in elsewhere, you must wait</para>
	/// </summary>
	k_EResultAlreadyLoggedInElsewhere = 50,
	/// <summary>
	/// <para>Long running operation (content download) suspended/paused</para>
	/// </summary>
	k_EResultSuspended = 51,
	/// <summary>
	/// <para>Operation canceled (typically by user: content download)</para>
	/// </summary>
	k_EResultCancelled = 52,
	/// <summary>
	/// <para>Operation canceled because data is ill formed or unrecoverable</para>
	/// </summary>
	k_EResultDataCorruption = 53,
	/// <summary>
	/// <para>Operation canceled - not enough disk space.</para>
	/// </summary>
	k_EResultDiskFull = 54,
	/// <summary>
	/// <para>an remote call or IPC call failed</para>
	/// </summary>
	k_EResultRemoteCallFailed = 55,
	/// <summary>
	/// <para>Password could not be verified as it&apos;s unset server side</para>
	/// </summary>
	k_EResultPasswordUnset = 56,
	/// <summary>
	/// <para>External account (PSN, Facebook...) is not linked to a Steam account</para>
	/// </summary>
	k_EResultExternalAccountUnlinked = 57,
	/// <summary>
	/// <para>PSN ticket was invalid</para>
	/// </summary>
	k_EResultPSNTicketInvalid = 58,
	/// <summary>
	/// <para>External account (PSN, Facebook...) is already linked to some other account, must explicitly request to replace/delete the link first</para>
	/// </summary>
	k_EResultExternalAccountAlreadyLinked = 59,
	/// <summary>
	/// <para>The sync cannot resume due to a conflict between the local and remote files</para>
	/// </summary>
	k_EResultRemoteFileConflict = 60,
	/// <summary>
	/// <para>The requested new password is not legal</para>
	/// </summary>
	k_EResultIllegalPassword = 61,
	/// <summary>
	/// <para>new value is the same as the old one ( secret question and answer )</para>
	/// </summary>
	k_EResultSameAsPreviousValue = 62,
	/// <summary>
	/// <para>account login denied due to 2nd factor authentication failure</para>
	/// </summary>
	k_EResultAccountLogonDenied = 63,
	/// <summary>
	/// <para>The requested new password is not legal</para>
	/// </summary>
	k_EResultCannotUseOldPassword = 64,
	/// <summary>
	/// <para>account login denied due to auth code invalid</para>
	/// </summary>
	k_EResultInvalidLoginAuthCode = 65,
	/// <summary>
	/// <para>account login denied due to 2nd factor auth failure - and no mail has been sent - partner site specific</para>
	/// </summary>
	k_EResultAccountLogonDeniedNoMail = 66,
	k_EResultHardwareNotCapableOfIPT = 67,
	k_EResultIPTInitError = 68,
	/// <summary>
	/// <para>operation failed due to parental control restrictions for current user</para>
	/// </summary>
	k_EResultParentalControlRestricted = 69,
	/// <summary>
	/// <para>Facebook query returned an error</para>
	/// </summary>
	k_EResultFacebookQueryError = 70,
	/// <summary>
	/// <para>account login denied due to auth code expired</para>
	/// </summary>
	k_EResultExpiredLoginAuthCode = 71,
	k_EResultIPLoginRestrictionFailed = 72,
	k_EResultAccountLockedDown = 73,
	k_EResultAccountLogonDeniedVerifiedEmailRequired = 74,
	k_EResultNoMatchingURL = 75,
	/// <summary>
	/// <para>parse failure, missing field, etc.</para>
	/// </summary>
	k_EResultBadResponse = 76,
	/// <summary>
	/// <para>The user cannot complete the action until they re-enter their password</para>
	/// </summary>
	k_EResultRequirePasswordReEntry = 77,
	/// <summary>
	/// <para>the value entered is outside the acceptable range</para>
	/// </summary>
	k_EResultValueOutOfRange = 78,
	/// <summary>
	/// <para>something happened that we didn&apos;t expect to ever happen</para>
	/// </summary>
	k_EResultUnexpectedError = 79,
	/// <summary>
	/// <para>The requested service has been configured to be unavailable</para>
	/// </summary>
	k_EResultDisabled = 80,
	/// <summary>
	/// <para>The set of files submitted to the CEG server are not valid !</para>
	/// </summary>
	k_EResultInvalidCEGSubmission = 81,
	/// <summary>
	/// <para>The device being used is not allowed to perform this action</para>
	/// </summary>
	k_EResultRestrictedDevice = 82,
	/// <summary>
	/// <para>The action could not be complete because it is region restricted</para>
	/// </summary>
	k_EResultRegionLocked = 83,
	/// <summary>
	/// <para>Temporary rate limit exceeded, try again later, different from k_EResultLimitExceeded which may be permanent</para>
	/// </summary>
	k_EResultRateLimitExceeded = 84,
	/// <summary>
	/// <para>Need two-factor code to login</para>
	/// </summary>
	k_EResultAccountLoginDeniedNeedTwoFactor = 85,
	/// <summary>
	/// <para>The thing we&apos;re trying to access has been deleted</para>
	/// </summary>
	k_EResultItemDeleted = 86,
	/// <summary>
	/// <para>login attempt failed, try to throttle response to possible attacker</para>
	/// </summary>
	k_EResultAccountLoginDeniedThrottle = 87,
	/// <summary>
	/// <para>two factor code mismatch</para>
	/// </summary>
	k_EResultTwoFactorCodeMismatch = 88,
	/// <summary>
	/// <para>activation code for two-factor didn&apos;t match</para>
	/// </summary>
	k_EResultTwoFactorActivationCodeMismatch = 89,
	/// <summary>
	/// <para>account has been associated with multiple partners</para>
	/// </summary>
	k_EResultAccountAssociatedToMultiplePartners = 90,
	/// <summary>
	/// <para>data not modified</para>
	/// </summary>
	k_EResultNotModified = 91,
	/// <summary>
	/// <para>the account does not have a mobile device associated with it</para>
	/// </summary>
	k_EResultNoMobileDevice = 92,
	/// <summary>
	/// <para>the time presented is out of range or tolerance</para>
	/// </summary>
	k_EResultTimeNotSynced = 93,
	/// <summary>
	/// <para>SMS code failure (no match, none pending, etc.)</para>
	/// </summary>
	k_EResultSmsCodeFailed = 94,
	/// <summary>
	/// <para>Too many accounts access this resource</para>
	/// </summary>
	k_EResultAccountLimitExceeded = 95,
	/// <summary>
	/// <para>Too many changes to this account</para>
	/// </summary>
	k_EResultAccountActivityLimitExceeded = 96,
	/// <summary>
	/// <para>Too many changes to this phone</para>
	/// </summary>
	k_EResultPhoneActivityLimitExceeded = 97,
	/// <summary>
	/// <para>Cannot refund to payment method, must use wallet</para>
	/// </summary>
	k_EResultRefundToWallet = 98,
	/// <summary>
	/// <para>Cannot send an email</para>
	/// </summary>
	k_EResultEmailSendFailure = 99,
	/// <summary>
	/// <para>Can&apos;t perform operation till payment has settled</para>
	/// </summary>
	k_EResultNotSettled = 100,
	/// <summary>
	/// <para>Needs to provide a valid captcha</para>
	/// </summary>
	k_EResultNeedCaptcha = 101,
	/// <summary>
	/// <para>a game server login token owned by this token&apos;s owner has been banned</para>
	/// </summary>
	k_EResultGSLTDenied = 102,
	/// <summary>
	/// <para>game server owner is denied for other reason (account lock, community ban, vac ban, missing phone)</para>
	/// </summary>
	k_EResultGSOwnerDenied = 103,
	/// <summary>
	/// <para>the type of thing we were requested to act on is invalid</para>
	/// </summary>
	k_EResultInvalidItemType = 104,
	/// <summary>
	/// <para>the ip address has been banned from taking this action</para>
	/// </summary>
	k_EResultIPBanned = 105,
	/// <summary>
	/// <para>this token has expired from disuse; can be reset for use</para>
	/// </summary>
	k_EResultGSLTExpired = 106,
	/// <summary>
	/// <para>user doesn&apos;t have enough wallet funds to complete the action</para>
	/// </summary>
	k_EResultInsufficientFunds = 107,
	/// <summary>
	/// <para>There are too many of this thing pending already</para>
	/// </summary>
	k_EResultTooManyPending = 108,
	/// <summary>
	/// <para>No site licenses found</para>
	/// </summary>
	k_EResultNoSiteLicensesFound = 109,
	/// <summary>
	/// <para>the WG couldn&apos;t send a response because we exceeded max network send size</para>
	/// </summary>
	k_EResultWGNetworkSendExceeded = 110,
	/// <summary>
	/// <para>the user is not mutually friends</para>
	/// </summary>
	k_EResultAccountNotFriends = 111,
	/// <summary>
	/// <para>the user is limited</para>
	/// </summary>
	k_EResultLimitedUserAccount = 112,
	/// <summary>
	/// <para>item can&apos;t be removed</para>
	/// </summary>
	k_EResultCantRemoveItem = 113,
	/// <summary>
	/// <para>account has been deleted</para>
	/// </summary>
	k_EResultAccountDeleted = 114,
	/// <summary>
	/// <para>A license for this already exists, but cancelled</para>
	/// </summary>
	k_EResultExistingUserCancelledLicense = 115,
	/// <summary>
	/// <para>access is denied because of a community cooldown (probably from support profile data resets)</para>
	/// </summary>
	k_EResultCommunityCooldown = 116,
	/// <summary>
	/// <para>No launcher was specified, but a launcher was needed to choose correct realm for operation.</para>
	/// </summary>
	k_EResultNoLauncherSpecified = 117,
	/// <summary>
	/// <para>User must agree to china SSA or global SSA before login</para>
	/// </summary>
	k_EResultMustAgreeToSSA = 118,
	/// <summary>
	/// <para>The specified launcher type is no longer supported; the user should be directed elsewhere</para>
	/// </summary>
	k_EResultLauncherMigrated = 119,
	/// <summary>
	/// <para>The user&apos;s realm does not match the realm of the requested resource</para>
	/// </summary>
	k_EResultSteamRealmMismatch = 120,
	/// <summary>
	/// <para>signature check did not match</para>
	/// </summary>
	k_EResultInvalidSignature = 121,
	/// <summary>
	/// <para>Failed to parse input</para>
	/// </summary>
	k_EResultParseFailure = 122,
	/// <summary>
	/// <para>account does not have a verified phone number</para>
	/// </summary>
	k_EResultNoVerifiedPhone = 123,
	/// <summary>
	/// <para>user device doesn&apos;t have enough battery charge currently to complete the action</para>
	/// </summary>
	k_EResultInsufficientBattery = 124,
	/// <summary>
	/// <para>The operation requires a charger to be plugged in, which wasn&apos;t present</para>
	/// </summary>
	k_EResultChargerRequired = 125,
	/// <summary>
	/// <para>Cached credential was invalid - user must reauthenticate</para>
	/// </summary>
	k_EResultCachedCredentialInvalid = 126,
	/// <summary>
	/// <para>The phone number provided is a Voice Over IP number</para>
	/// </summary>
	K_EResultPhoneNumberIsVOIP = 127,
	/// <summary>
	/// <para>The data being accessed is not supported by this API</para>
	/// </summary>
	k_EResultNotSupported = 128,
	/// <summary>
	/// <para>Reached the maximum size of the family</para>
	/// </summary>
	k_EResultFamilySizeLimitExceeded = 129,
	/// <summary>
	/// <para>The local data for the offline mode cache is insufficient to login</para>
	/// </summary>
	k_EResultOfflineAppCacheInvalid = 130,
}

/// <summary>
/// <para>Error codes for use with the voice functions</para>
/// </summary>
public enum EVoiceResult : int
{
	k_EVoiceResultOK = 0,
	k_EVoiceResultNotInitialized = 1,
	k_EVoiceResultNotRecording = 2,
	k_EVoiceResultNoData = 3,
	k_EVoiceResultBufferTooSmall = 4,
	k_EVoiceResultDataCorrupted = 5,
	k_EVoiceResultRestricted = 6,
	k_EVoiceResultUnsupportedCodec = 7,
	k_EVoiceResultReceiverOutOfDate = 8,
	k_EVoiceResultReceiverDidNotAnswer = 9,

}

/// <summary>
/// <para>Result codes to GSHandleClientDeny/Kick</para>
/// </summary>
public enum EDenyReason : int
{
	k_EDenyInvalid = 0,
	k_EDenyInvalidVersion = 1,
	k_EDenyGeneric = 2,
	k_EDenyNotLoggedOn = 3,
	k_EDenyNoLicense = 4,
	k_EDenyCheater = 5,
	k_EDenyLoggedInElseWhere = 6,
	k_EDenyUnknownText = 7,
	k_EDenyIncompatibleAnticheat = 8,
	k_EDenyMemoryCorruption = 9,
	k_EDenyIncompatibleSoftware = 10,
	k_EDenySteamConnectionLost = 11,
	k_EDenySteamConnectionError = 12,
	k_EDenySteamResponseTimedOut = 13,
	k_EDenySteamValidationStalled = 14,
	k_EDenySteamOwnerLeftGuestUser = 15,
}

/// <summary>
/// <para>results from BeginAuthSession</para>
/// </summary>
public enum EBeginAuthSessionResult : int
{
	/// <summary>
	/// <para>Ticket is valid for this game and this steamID.</para>
	/// </summary>
	k_EBeginAuthSessionResultOK = 0,
	/// <summary>
	/// <para>Ticket is not valid.</para>
	/// </summary>
	k_EBeginAuthSessionResultInvalidTicket = 1,
	/// <summary>
	/// <para>A ticket has already been submitted for this steamID</para>
	/// </summary>
	k_EBeginAuthSessionResultDuplicateRequest = 2,
	/// <summary>
	/// <para>Ticket is from an incompatible interface version</para>
	/// </summary>
	k_EBeginAuthSessionResultInvalidVersion = 3,
	/// <summary>
	/// <para>Ticket is not for this game</para>
	/// </summary>
	k_EBeginAuthSessionResultGameMismatch = 4,
	/// <summary>
	/// <para>Ticket has expired</para>
	/// </summary>
	k_EBeginAuthSessionResultExpiredTicket = 5,
}

/// <summary>
/// <para>Callback values for callback ValidateAuthTicketResponse_t which is a response to BeginAuthSession</para>
/// </summary>
public enum EAuthSessionResponse : int
{
	/// <summary>
	/// <para>Steam has verified the user is online, the ticket is valid and ticket has not been reused.</para>
	/// </summary>
	k_EAuthSessionResponseOK = 0,
	/// <summary>
	/// <para>The user in question is not connected to steam</para>
	/// </summary>
	k_EAuthSessionResponseUserNotConnectedToSteam = 1,
	/// <summary>
	/// <para>The license has expired.</para>
	/// </summary>
	k_EAuthSessionResponseNoLicenseOrExpired = 2,
	/// <summary>
	/// <para>The user is VAC banned for this game.</para>
	/// </summary>
	k_EAuthSessionResponseVACBanned = 3,
	/// <summary>
	/// <para>The user account has logged in elsewhere and the session containing the game instance has been disconnected.</para>
	/// </summary>
	k_EAuthSessionResponseLoggedInElseWhere = 4,
	/// <summary>
	/// <para>VAC has been unable to perform anti-cheat checks on this user</para>
	/// </summary>
	k_EAuthSessionResponseVACCheckTimedOut = 5,
	/// <summary>
	/// <para>The ticket has been canceled by the issuer</para>
	/// </summary>
	k_EAuthSessionResponseAuthTicketCanceled = 6,
	/// <summary>
	/// <para>This ticket has already been used, it is not valid.</para>
	/// </summary>
	k_EAuthSessionResponseAuthTicketInvalidAlreadyUsed = 7,
	/// <summary>
	/// <para>This ticket is not from a user instance currently connected to steam.</para>
	/// </summary>
	k_EAuthSessionResponseAuthTicketInvalid = 8,
	/// <summary>
	/// <para>The user is banned for this game. The ban came via the web api and not VAC</para>
	/// </summary>
	k_EAuthSessionResponsePublisherIssuedBan = 9,
	/// <summary>
	/// <para>The network identity in the ticket does not match the server authenticating the ticket</para>
	/// </summary>
	k_EAuthSessionResponseAuthTicketNetworkIdentityFailure = 10,
}

/// <summary>
/// <para>results from UserHasLicenseForApp</para>
/// </summary>
public enum EUserHasLicenseForAppResult : int
{
	/// <summary>
	/// <para>User has a license for specified app</para>
	/// </summary>
	k_EUserHasLicenseResultHasLicense = 0,
	/// <summary>
	/// <para>User does not have a license for the specified app</para>
	/// </summary>
	k_EUserHasLicenseResultDoesNotHaveLicense = 1,
	/// <summary>
	/// <para>User has not been authenticated</para>
	/// </summary>
	k_EUserHasLicenseResultNoAuth = 2,
}

/// <summary>
/// <para>Steam account types</para>
/// </summary>
public enum EAccountType : int
{
	k_EAccountTypeInvalid = 0,
	/// <summary>
	/// <para>single user account</para>
	/// </summary>
	k_EAccountTypeIndividual = 1,
	/// <summary>
	/// <para>multiseat (e.g. cybercafe) account</para>
	/// </summary>
	k_EAccountTypeMultiseat = 2,
	/// <summary>
	/// <para>game server account</para>
	/// </summary>
	k_EAccountTypeGameServer = 3,
	/// <summary>
	/// <para>anonymous game server account</para>
	/// </summary>
	k_EAccountTypeAnonGameServer = 4,
	/// <summary>
	/// <para>pending</para>
	/// </summary>
	k_EAccountTypePending = 5,
	/// <summary>
	/// <para>content server</para>
	/// </summary>
	k_EAccountTypeContentServer = 6,
	k_EAccountTypeClan = 7,
	k_EAccountTypeChat = 8,
	/// <summary>
	/// <para>Fake SteamID for local PSN account on PS3 or Live account on 360, etc.</para>
	/// </summary>
	k_EAccountTypeConsoleUser = 9,
	k_EAccountTypeAnonUser = 10,
	/// <summary>
	/// <para>Max of 16 items in this field</para>
	/// </summary>
	k_EAccountTypeMax
}

/// <summary>
/// <para>Chat Entry Types (previously was only friend-to-friend message types)</para>
/// </summary>
public enum EChatEntryType : int
{
	k_EChatEntryTypeInvalid = 0,
	/// <summary>
	/// <para>Normal text message from another user</para>
	/// </summary>
	k_EChatEntryTypeChatMsg = 1,
	/// <summary>
	/// <para>Another user is typing (not used in multi-user chat)</para>
	/// </summary>
	k_EChatEntryTypeTyping = 2,
	/// <summary>
	/// <para>Invite from other user into that users current game</para>
	/// </summary>
	k_EChatEntryTypeInviteGame = 3,
	/// <summary>
	/// <para>text emote message (deprecated, should be treated as ChatMsg)</para>
	/// </summary>
	k_EChatEntryTypeEmote = 4,
	// k_EChatEntryTypeLobbyGameStart = 5,	// lobby game is starting (dead - listen for LobbyGameCreated_t callback instead)
	/// <summary>
	/// <para>user has left the conversation ( closed chat window )</para>
	/// </summary>
	k_EChatEntryTypeLeftConversation = 6,
	/// <summary>
	/// <para>Above are previous FriendMsgType entries, now merged into more generic chat entry types</para>
	/// <para>user has entered the conversation (used in multi-user chat and group chat)</para>
	/// </summary>
	k_EChatEntryTypeEntered = 7,
	/// <summary>
	/// <para>user was kicked (data: 64-bit steamid of actor performing the kick)</para>
	/// </summary>
	k_EChatEntryTypeWasKicked = 8,
	/// <summary>
	/// <para>user was banned (data: 64-bit steamid of actor performing the ban)</para>
	/// </summary>
	k_EChatEntryTypeWasBanned = 9,
	/// <summary>
	/// <para>user disconnected</para>
	/// </summary>
	k_EChatEntryTypeDisconnected = 10,
	/// <summary>
	/// <para>a chat message from user&apos;s chat history or offilne message</para>
	/// </summary>
	k_EChatEntryTypeHistoricalChat = 11,
	// k_EChatEntryTypeReserved1 = 12, // No longer used
	// k_EChatEntryTypeReserved2 = 13, // No longer used
	/// <summary>
	/// <para>a link was removed by the chat filter.</para>
	/// </summary>
	k_EChatEntryTypeLinkBlocked = 14,
}

/// <summary>
/// <para>Chat Room Enter Responses</para>
/// </summary>
public enum EChatRoomEnterResponse : int
{
	/// <summary>
	/// <para>Success</para>
	/// </summary>
	k_EChatRoomEnterResponseSuccess = 1,
	/// <summary>
	/// <para>Chat doesn&apos;t exist (probably closed)</para>
	/// </summary>
	k_EChatRoomEnterResponseDoesntExist = 2,
	/// <summary>
	/// <para>General Denied - You don&apos;t have the permissions needed to join the chat</para>
	/// </summary>
	k_EChatRoomEnterResponseNotAllowed = 3,
	/// <summary>
	/// <para>Chat room has reached its maximum size</para>
	/// </summary>
	k_EChatRoomEnterResponseFull = 4,
	/// <summary>
	/// <para>Unexpected Error</para>
	/// </summary>
	k_EChatRoomEnterResponseError = 5,
	/// <summary>
	/// <para>You are banned from this chat room and may not join</para>
	/// </summary>
	k_EChatRoomEnterResponseBanned = 6,
	/// <summary>
	/// <para>Joining this chat is not allowed because you are a limited user (no value on account)</para>
	/// </summary>
	k_EChatRoomEnterResponseLimited = 7,
	/// <summary>
	/// <para>Attempt to join a clan chat when the clan is locked or disabled</para>
	/// </summary>
	k_EChatRoomEnterResponseClanDisabled = 8,
	/// <summary>
	/// <para>Attempt to join a chat when the user has a community lock on their account</para>
	/// </summary>
	k_EChatRoomEnterResponseCommunityBan = 9,
	/// <summary>
	/// <para>Join failed - some member in the chat has blocked you from joining</para>
	/// </summary>
	k_EChatRoomEnterResponseMemberBlockedYou = 10,
	/// <summary>
	/// <para>Join failed - you have blocked some member already in the chat</para>
	/// </summary>
	k_EChatRoomEnterResponseYouBlockedMember = 11,
	// k_EChatRoomEnterResponseNoRankingDataLobby = 12,  // No longer used
	// k_EChatRoomEnterResponseNoRankingDataUser = 13,  //  No longer used
	// k_EChatRoomEnterResponseRankOutOfRange = 14, //  No longer used
	/// <summary>
	/// <para>Join failed - to many join attempts in a very short period of time</para>
	/// </summary>
	k_EChatRoomEnterResponseRatelimitExceeded = 15,
}

/// <summary>
/// <para>Special flags for Chat accounts - they go in the top 8 bits</para>
/// <para>of the steam ID&apos;s &quot;instance&quot;, leaving 12 for the actual instances</para>
/// </summary>
[Flags]
public enum EChatSteamIDInstanceFlags : int
{
	/// <summary>
	/// <para>top 8 bits are flags</para>
	/// </summary>
	k_EChatAccountInstanceMask = 0x00000FFF,
	/// <summary>
	/// <para>top bit</para>
	/// </summary>
	k_EChatInstanceFlagClan = ( Constants.k_unSteamAccountInstanceMask + 1 ) >> 1,
	/// <summary>
	/// <para>next one down, etc</para>
	/// </summary>
	k_EChatInstanceFlagLobby = ( Constants.k_unSteamAccountInstanceMask + 1 ) >> 2,
	/// <summary>
	/// <para>next one down, etc</para>
	/// </summary>
	k_EChatInstanceFlagMMSLobby = ( Constants.k_unSteamAccountInstanceMask + 1 ) >> 3,

	// Max of 8 flags
}

/// <summary>
/// <para>Possible positions to tell the overlay to show notifications in</para>
/// </summary>
public enum ENotificationPosition : int
{
	k_EPositionInvalid = -1,
	k_EPositionTopLeft = 0,
	k_EPositionTopRight = 1,
	k_EPositionBottomLeft = 2,
	k_EPositionBottomRight = 3,
}

/// <summary>
/// <para>Broadcast upload result details</para>
/// </summary>
public enum EBroadcastUploadResult : int
{
	/// <summary>
	/// <para>broadcast state unknown</para>
	/// </summary>
	k_EBroadcastUploadResultNone = 0,
	/// <summary>
	/// <para>broadcast was good, no problems</para>
	/// </summary>
	k_EBroadcastUploadResultOK = 1,
	/// <summary>
	/// <para>broadcast init failed</para>
	/// </summary>
	k_EBroadcastUploadResultInitFailed = 2,
	/// <summary>
	/// <para>broadcast frame upload failed</para>
	/// </summary>
	k_EBroadcastUploadResultFrameFailed = 3,
	/// <summary>
	/// <para>broadcast upload timed out</para>
	/// </summary>
	k_EBroadcastUploadResultTimeout = 4,
	/// <summary>
	/// <para>broadcast send too much data</para>
	/// </summary>
	k_EBroadcastUploadResultBandwidthExceeded = 5,
	/// <summary>
	/// <para>broadcast FPS too low</para>
	/// </summary>
	k_EBroadcastUploadResultLowFPS = 6,
	/// <summary>
	/// <para>broadcast sending not enough key frames</para>
	/// </summary>
	k_EBroadcastUploadResultMissingKeyFrames = 7,
	/// <summary>
	/// <para>broadcast client failed to connect to relay</para>
	/// </summary>
	k_EBroadcastUploadResultNoConnection = 8,
	/// <summary>
	/// <para>relay dropped the upload</para>
	/// </summary>
	k_EBroadcastUploadResultRelayFailed = 9,
	/// <summary>
	/// <para>the client changed broadcast settings</para>
	/// </summary>
	k_EBroadcastUploadResultSettingsChanged = 10,
	/// <summary>
	/// <para>client failed to send audio data</para>
	/// </summary>
	k_EBroadcastUploadResultMissingAudio = 11,
	/// <summary>
	/// <para>clients was too slow uploading</para>
	/// </summary>
	k_EBroadcastUploadResultTooFarBehind = 12,
	/// <summary>
	/// <para>server failed to keep up with transcode</para>
	/// </summary>
	k_EBroadcastUploadResultTranscodeBehind = 13,
	/// <summary>
	/// <para>Broadcast does not have permissions to play game</para>
	/// </summary>
	k_EBroadcastUploadResultNotAllowedToPlay = 14,
	/// <summary>
	/// <para>RTMP host to busy to take new broadcast stream, choose another</para>
	/// </summary>
	k_EBroadcastUploadResultBusy = 15,
	/// <summary>
	/// <para>Account banned from community broadcast</para>
	/// </summary>
	k_EBroadcastUploadResultBanned = 16,
	/// <summary>
	/// <para>We already already have an stream running.</para>
	/// </summary>
	k_EBroadcastUploadResultAlreadyActive = 17,
	/// <summary>
	/// <para>We explicitly shutting down a broadcast</para>
	/// </summary>
	k_EBroadcastUploadResultForcedOff = 18,
	/// <summary>
	/// <para>Audio stream was too far behind video</para>
	/// </summary>
	k_EBroadcastUploadResultAudioBehind = 19,
	/// <summary>
	/// <para>Broadcast Server was shut down</para>
	/// </summary>
	k_EBroadcastUploadResultShutdown = 20,
	/// <summary>
	/// <para>broadcast uploader TCP disconnected</para>
	/// </summary>
	k_EBroadcastUploadResultDisconnect = 21,
	/// <summary>
	/// <para>invalid video settings</para>
	/// </summary>
	k_EBroadcastUploadResultVideoInitFailed = 22,
	/// <summary>
	/// <para>invalid audio settings</para>
	/// </summary>
	k_EBroadcastUploadResultAudioInitFailed = 23,
}

/// <summary>
/// <para>Reasons a user may not use the Community Market.</para>
/// <para>Used in MarketEligibilityResponse_t.</para>
/// </summary>
[Flags]
public enum EMarketNotAllowedReasonFlags : int
{
	k_EMarketNotAllowedReason_None = 0,
	/// <summary>
	/// <para>A back-end call failed or something that might work again on retry</para>
	/// </summary>
	k_EMarketNotAllowedReason_TemporaryFailure = (1 << 0),
	/// <summary>
	/// <para>Disabled account</para>
	/// </summary>
	k_EMarketNotAllowedReason_AccountDisabled = (1 << 1),
	/// <summary>
	/// <para>Locked account</para>
	/// </summary>
	k_EMarketNotAllowedReason_AccountLockedDown = (1 << 2),
	/// <summary>
	/// <para>Limited account (no purchases)</para>
	/// </summary>
	k_EMarketNotAllowedReason_AccountLimited = (1 << 3),
	/// <summary>
	/// <para>The account is banned from trading items</para>
	/// </summary>
	k_EMarketNotAllowedReason_TradeBanned = (1 << 4),
	/// <summary>
	/// <para>Wallet funds aren&apos;t tradable because the user has had no purchase</para>
	/// <para>activity in the last year or has had no purchases prior to last month</para>
	/// </summary>
	k_EMarketNotAllowedReason_AccountNotTrusted = (1 << 5),
	/// <summary>
	/// <para>The user doesn&apos;t have Steam Guard enabled</para>
	/// </summary>
	k_EMarketNotAllowedReason_SteamGuardNotEnabled = (1 << 6),
	/// <summary>
	/// <para>The user has Steam Guard, but it hasn&apos;t been enabled for the required</para>
	/// <para>number of days</para>
	/// </summary>
	k_EMarketNotAllowedReason_SteamGuardOnlyRecentlyEnabled = (1 << 7),
	/// <summary>
	/// <para>The user has recently forgotten their password and reset it</para>
	/// </summary>
	k_EMarketNotAllowedReason_RecentPasswordReset = (1 << 8),
	/// <summary>
	/// <para>The user has recently funded his or her wallet with a new payment method</para>
	/// </summary>
	k_EMarketNotAllowedReason_NewPaymentMethod = (1 << 9),
	/// <summary>
	/// <para>An invalid cookie was sent by the user</para>
	/// </summary>
	k_EMarketNotAllowedReason_InvalidCookie = (1 << 10),
	/// <summary>
	/// <para>The user has Steam Guard, but is using a new computer or web browser</para>
	/// </summary>
	k_EMarketNotAllowedReason_UsingNewDevice = (1 << 11),
	/// <summary>
	/// <para>The user has recently refunded a store purchase by his or herself</para>
	/// </summary>
	k_EMarketNotAllowedReason_RecentSelfRefund = (1 << 12),
	/// <summary>
	/// <para>The user has recently funded his or her wallet with a new payment method that cannot be verified</para>
	/// </summary>
	k_EMarketNotAllowedReason_NewPaymentMethodCannotBeVerified = (1 << 13),
	/// <summary>
	/// <para>Not only is the account not trusted, but they have no recent purchases at all</para>
	/// </summary>
	k_EMarketNotAllowedReason_NoRecentPurchases = (1 << 14),
	/// <summary>
	/// <para>User accepted a wallet gift that was recently purchased</para>
	/// </summary>
	k_EMarketNotAllowedReason_AcceptedWalletGift = (1 << 15),
}

/// <summary>
/// <para>describes XP / progress restrictions to apply for games with duration control /</para>
/// <para>anti-indulgence enabled for minor Steam China users.</para>
/// <para>WARNING: DO NOT RENUMBER</para>
/// </summary>
public enum EDurationControlProgress : int
{
	/// <summary>
	/// <para>Full progress</para>
	/// </summary>
	k_EDurationControlProgress_Full = 0,
	/// <summary>
	/// <para>deprecated - XP or persistent rewards should be halved</para>
	/// </summary>
	k_EDurationControlProgress_Half = 1,
	/// <summary>
	/// <para>deprecated - XP or persistent rewards should be stopped</para>
	/// </summary>
	k_EDurationControlProgress_None = 2,
	/// <summary>
	/// <para>allowed 3h time since 5h gap/break has elapsed, game should exit - steam will terminate the game soon</para>
	/// </summary>
	k_EDurationControl_ExitSoon_3h = 3,
	/// <summary>
	/// <para>allowed 5h time in calendar day has elapsed, game should exit - steam will terminate the game soon</para>
	/// </summary>
	k_EDurationControl_ExitSoon_5h = 4,
	/// <summary>
	/// <para>game running after day period, game should exit - steam will terminate the game soon</para>
	/// </summary>
	k_EDurationControl_ExitSoon_Night = 5,
}

/// <summary>
/// <para>describes which notification timer has expired, for steam china duration control feature</para>
/// <para>WARNING: DO NOT RENUMBER</para>
/// </summary>
public enum EDurationControlNotification : int
{
	/// <summary>
	/// <para>just informing you about progress, no notification to show</para>
	/// </summary>
	k_EDurationControlNotification_None = 0,
	/// <summary>
	/// <para>&quot;you&apos;ve been playing for N hours&quot;</para>
	/// </summary>
	k_EDurationControlNotification_1Hour = 1,
	/// <summary>
	/// <para>deprecated - &quot;you&apos;ve been playing for 3 hours; take a break&quot;</para>
	/// </summary>
	k_EDurationControlNotification_3Hours = 2,
	/// <summary>
	/// <para>deprecated - &quot;your XP / progress is half normal&quot;</para>
	/// </summary>
	k_EDurationControlNotification_HalfProgress = 3,
	/// <summary>
	/// <para>deprecated - &quot;your XP / progress is zero&quot;</para>
	/// </summary>
	k_EDurationControlNotification_NoProgress = 4,
	/// <summary>
	/// <para>allowed 3h time since 5h gap/break has elapsed, game should exit - steam will terminate the game soon</para>
	/// </summary>
	k_EDurationControlNotification_ExitSoon_3h = 5,
	/// <summary>
	/// <para>allowed 5h time in calendar day has elapsed, game should exit - steam will terminate the game soon</para>
	/// </summary>
	k_EDurationControlNotification_ExitSoon_5h = 6,
	/// <summary>
	/// <para>game running after day period, game should exit - steam will terminate the game soon</para>
	/// </summary>
	k_EDurationControlNotification_ExitSoon_Night = 7,
}

/// <summary>
/// <para>Specifies a game&apos;s online state in relation to duration control</para>
/// </summary>
public enum EDurationControlOnlineState : int
{
	/// <summary>
	/// <para>nil value</para>
	/// </summary>
	k_EDurationControlOnlineState_Invalid = 0,
	/// <summary>
	/// <para>currently in offline play - single-player, offline co-op, etc.</para>
	/// </summary>
	k_EDurationControlOnlineState_Offline = 1,
	/// <summary>
	/// <para>currently in online play</para>
	/// </summary>
	k_EDurationControlOnlineState_Online = 2,
	/// <summary>
	/// <para>currently in online play and requests not to be interrupted</para>
	/// </summary>
	k_EDurationControlOnlineState_OnlineHighPri = 3,
}

public enum EBetaBranchFlags : int
{
	k_EBetaBranch_None			= 0,
	/// <summary>
	/// <para>this is the default branch (&quot;public&quot;)</para>
	/// </summary>
	k_EBetaBranch_Default		= 1,
	/// <summary>
	/// <para>this branch can be selected (available)</para>
	/// </summary>
	k_EBetaBranch_Available		= 2,
	/// <summary>
	/// <para>this is a private branch (password protected)</para>
	/// </summary>
	k_EBetaBranch_Private		= 4,
	/// <summary>
	/// <para>this is the currently selected branch (active)</para>
	/// </summary>
	k_EBetaBranch_Selected		= 8,
	/// <summary>
	/// <para>this is the currently installed branch (mounted)</para>
	/// </summary>
	k_EBetaBranch_Installed		= 16,
}

public enum EGameSearchErrorCode_t : int
{
	k_EGameSearchErrorCode_OK = 1,
	k_EGameSearchErrorCode_Failed_Search_Already_In_Progress = 2,
	k_EGameSearchErrorCode_Failed_No_Search_In_Progress = 3,
	/// <summary>
	/// <para>if not the lobby leader can not call SearchForGameWithLobby</para>
	/// </summary>
	k_EGameSearchErrorCode_Failed_Not_Lobby_Leader = 4,
	/// <summary>
	/// <para>no host is available that matches those search params</para>
	/// </summary>
	k_EGameSearchErrorCode_Failed_No_Host_Available = 5,
	/// <summary>
	/// <para>search params are invalid</para>
	/// </summary>
	k_EGameSearchErrorCode_Failed_Search_Params_Invalid = 6,
	/// <summary>
	/// <para>offline, could not communicate with server</para>
	/// </summary>
	k_EGameSearchErrorCode_Failed_Offline = 7,
	/// <summary>
	/// <para>either the user or the application does not have priveledges to do this</para>
	/// </summary>
	k_EGameSearchErrorCode_Failed_NotAuthorized = 8,
	/// <summary>
	/// <para>unknown error</para>
	/// </summary>
	k_EGameSearchErrorCode_Failed_Unknown_Error = 9,
}

public enum EPlayerResult_t : int
{
	/// <summary>
	/// <para>failed to connect after confirming</para>
	/// </summary>
	k_EPlayerResultFailedToConnect = 1,
	/// <summary>
	/// <para>quit game without completing it</para>
	/// </summary>
	k_EPlayerResultAbandoned = 2,
	/// <summary>
	/// <para>kicked by other players/moderator/server rules</para>
	/// </summary>
	k_EPlayerResultKicked = 3,
	/// <summary>
	/// <para>player stayed to end but game did not conclude successfully ( nofault to player )</para>
	/// </summary>
	k_EPlayerResultIncomplete = 4,
	/// <summary>
	/// <para>player completed game</para>
	/// </summary>
	k_EPlayerResultCompleted = 5,
}

public enum ESteamIPv6ConnectivityProtocol : int
{
	k_ESteamIPv6ConnectivityProtocol_Invalid = 0,
	/// <summary>
	/// <para>because a proxy may make this different than other protocols</para>
	/// </summary>
	k_ESteamIPv6ConnectivityProtocol_HTTP = 1,
	/// <summary>
	/// <para>test UDP connectivity. Uses a port that is commonly needed for other Steam stuff. If UDP works, TCP probably works.</para>
	/// </summary>
	k_ESteamIPv6ConnectivityProtocol_UDP = 2,
}

/// <summary>
/// <para>For the above transport protocol, what do we think the local machine&apos;s connectivity to the internet over ipv6 is like</para>
/// </summary>
public enum ESteamIPv6ConnectivityState : int
{
	/// <summary>
	/// <para>We haven&apos;t run a test yet</para>
	/// </summary>
	k_ESteamIPv6ConnectivityState_Unknown = 0,
	/// <summary>
	/// <para>We have recently been able to make a request on ipv6 for the given protocol</para>
	/// </summary>
	k_ESteamIPv6ConnectivityState_Good = 1,
	/// <summary>
	/// <para>We failed to make a request, either because this machine has no ipv6 address assigned, or it has no upstream connectivity</para>
	/// </summary>
	k_ESteamIPv6ConnectivityState_Bad = 2,
}

/// <summary>
/// <para>HTTP related types</para>
/// <para>This enum is used in client API methods, do not re-number existing values.</para>
/// </summary>
public enum EHTTPMethod : int
{
	k_EHTTPMethodInvalid = 0,
	k_EHTTPMethodGET,
	k_EHTTPMethodHEAD,
	k_EHTTPMethodPOST,
	k_EHTTPMethodPUT,
	k_EHTTPMethodDELETE,
	k_EHTTPMethodOPTIONS,
	k_EHTTPMethodPATCH,

	// The remaining HTTP methods are not yet supported, per rfc2616 section 5.1.1 only GET and HEAD are required for
	// a compliant general purpose server.  We'll likely add more as we find uses for them.

	// k_EHTTPMethodTRACE,
	// k_EHTTPMethodCONNECT
}

/// <summary>
/// <para>HTTP Status codes that the server can send in response to a request, see rfc2616 section 10.3 for descriptions</para>
/// <para>of each of these.</para>
/// </summary>
public enum EHTTPStatusCode : int
{
	/// <summary>
	/// <para>Invalid status code (this isn&apos;t defined in HTTP, used to indicate unset in our code)</para>
	/// </summary>
	k_EHTTPStatusCodeInvalid =					0,
	/// <summary>
	/// <para>Informational codes</para>
	/// </summary>
	k_EHTTPStatusCode100Continue =				100,
	k_EHTTPStatusCode101SwitchingProtocols =	101,
	/// <summary>
	/// <para>Success codes</para>
	/// </summary>
	k_EHTTPStatusCode200OK =					200,
	k_EHTTPStatusCode201Created =				201,
	k_EHTTPStatusCode202Accepted =				202,
	k_EHTTPStatusCode203NonAuthoritative =		203,
	k_EHTTPStatusCode204NoContent =				204,
	k_EHTTPStatusCode205ResetContent =			205,
	k_EHTTPStatusCode206PartialContent =		206,
	/// <summary>
	/// <para>Redirection codes</para>
	/// </summary>
	k_EHTTPStatusCode300MultipleChoices =		300,
	k_EHTTPStatusCode301MovedPermanently =		301,
	k_EHTTPStatusCode302Found =					302,
	k_EHTTPStatusCode303SeeOther =				303,
	k_EHTTPStatusCode304NotModified =			304,
	k_EHTTPStatusCode305UseProxy =				305,
	// k_EHTTPStatusCode306Unused =				306, (used in old HTTP spec, now unused in 1.1)
	k_EHTTPStatusCode307TemporaryRedirect =		307,
	k_EHTTPStatusCode308PermanentRedirect =		308,
	/// <summary>
	/// <para>Error codes</para>
	/// </summary>
	k_EHTTPStatusCode400BadRequest =			400,
	/// <summary>
	/// <para>You probably want 403 or something else. 401 implies you&apos;re sending a WWW-Authenticate header and the client can sent an Authorization header in response.</para>
	/// </summary>
	k_EHTTPStatusCode401Unauthorized =			401,
	/// <summary>
	/// <para>This is reserved for future HTTP specs, not really supported by clients</para>
	/// </summary>
	k_EHTTPStatusCode402PaymentRequired =		402,
	k_EHTTPStatusCode403Forbidden =				403,
	k_EHTTPStatusCode404NotFound =				404,
	k_EHTTPStatusCode405MethodNotAllowed =		405,
	k_EHTTPStatusCode406NotAcceptable =			406,
	k_EHTTPStatusCode407ProxyAuthRequired =		407,
	k_EHTTPStatusCode408RequestTimeout =		408,
	k_EHTTPStatusCode409Conflict =				409,
	k_EHTTPStatusCode410Gone =					410,
	k_EHTTPStatusCode411LengthRequired =		411,
	k_EHTTPStatusCode412PreconditionFailed =	412,
	k_EHTTPStatusCode413RequestEntityTooLarge =	413,
	k_EHTTPStatusCode414RequestURITooLong =		414,
	k_EHTTPStatusCode415UnsupportedMediaType =	415,
	k_EHTTPStatusCode416RequestedRangeNotSatisfiable = 416,
	k_EHTTPStatusCode417ExpectationFailed =		417,
	/// <summary>
	/// <para>418 is reserved, so we&apos;ll use it to mean unknown</para>
	/// </summary>
	k_EHTTPStatusCode4xxUnknown = 				418,
	k_EHTTPStatusCode429TooManyRequests	=		429,
	/// <summary>
	/// <para>nginx only?</para>
	/// </summary>
	k_EHTTPStatusCode444ConnectionClosed =		444,
	/// <summary>
	/// <para>Server error codes</para>
	/// </summary>
	k_EHTTPStatusCode500InternalServerError =	500,
	k_EHTTPStatusCode501NotImplemented =		501,
	k_EHTTPStatusCode502BadGateway =			502,
	k_EHTTPStatusCode503ServiceUnavailable =	503,
	k_EHTTPStatusCode504GatewayTimeout =		504,
	k_EHTTPStatusCode505HTTPVersionNotSupported = 505,
	k_EHTTPStatusCode5xxUnknown =				599,
}

/// <summary>
/// <para>Describe the status of a particular network resource</para>
/// </summary>
public enum ESteamNetworkingAvailability : int
{
	/// <summary>
	/// <para>Negative values indicate a problem.</para>
	/// <para>In general, we will not automatically retry unless you take some action that</para>
	/// <para>depends on of requests this resource, such as querying the status, attempting</para>
	/// <para>to initiate a connection, receive a connection, etc.  If you do not take any</para>
	/// <para>action at all, we do not automatically retry in the background.</para>
	/// <para>A dependent resource is missing, so this service is unavailable.  (E.g. we cannot talk to routers because Internet is down or we don&apos;t have the network config.)</para>
	/// </summary>
	k_ESteamNetworkingAvailability_CannotTry = -102,
	/// <summary>
	/// <para>We have tried for enough time that we would expect to have been successful by now.  We have never been successful</para>
	/// </summary>
	k_ESteamNetworkingAvailability_Failed = -101,
	/// <summary>
	/// <para>We tried and were successful at one time, but now it looks like we have a problem</para>
	/// </summary>
	k_ESteamNetworkingAvailability_Previously = -100,
	/// <summary>
	/// <para>We previously failed and are currently retrying</para>
	/// </summary>
	k_ESteamNetworkingAvailability_Retrying = -10,
	/// <summary>
	/// <para>Not a problem, but not ready either</para>
	/// <para>We don&apos;t know because we haven&apos;t ever checked/tried</para>
	/// </summary>
	k_ESteamNetworkingAvailability_NeverTried = 1,
	/// <summary>
	/// <para>We&apos;re waiting on a dependent resource to be acquired.  (E.g. we cannot obtain a cert until we are logged into Steam.  We cannot measure latency to relays until we have the network config.)</para>
	/// </summary>
	k_ESteamNetworkingAvailability_Waiting = 2,
	/// <summary>
	/// <para>We&apos;re actively trying now, but are not yet successful.</para>
	/// </summary>
	k_ESteamNetworkingAvailability_Attempting = 3,
	/// <summary>
	/// <para>Resource is online/available</para>
	/// </summary>
	k_ESteamNetworkingAvailability_Current = 100,
	/// <summary>
	/// <para>Internal dummy/sentinel, or value is not applicable in this context</para>
	/// </summary>
	k_ESteamNetworkingAvailability_Unknown = 0,
	k_ESteamNetworkingAvailability__Force32bit = 0x7fffffff,
}

/// <summary>
/// <para>Describing network hosts</para>
/// <para>Different methods of describing the identity of a network host</para>
/// </summary>
public enum ESteamNetworkingIdentityType : int
{
	/// <summary>
	/// <para>Dummy/empty/invalid.</para>
	/// <para>Please note that if we parse a string that we don&apos;t recognize</para>
	/// <para>but that appears reasonable, we will NOT use this type.  Instead</para>
	/// <para>we&apos;ll use k_ESteamNetworkingIdentityType_UnknownType.</para>
	/// </summary>
	k_ESteamNetworkingIdentityType_Invalid = 0,
	/// <summary>
	/// <para>Basic platform-specific identifiers.</para>
	/// <para>64-bit CSteamID</para>
	/// </summary>
	k_ESteamNetworkingIdentityType_SteamID = 16,
	/// <summary>
	/// <para>Publisher-specific user identity, as string</para>
	/// </summary>
	k_ESteamNetworkingIdentityType_XboxPairwiseID = 17,
	/// <summary>
	/// <para>64-bit ID</para>
	/// </summary>
	k_ESteamNetworkingIdentityType_SonyPSN = 18,
	/// <summary>
	/// <para>Special identifiers.</para>
	/// <para>Use their IP address (and port) as their &quot;identity&quot;.</para>
	/// <para>These types of identities are always unauthenticated.</para>
	/// <para>They are useful for porting plain sockets code, and other</para>
	/// <para>situations where you don&apos;t care about authentication.  In this</para>
	/// <para>case, the local identity will be &quot;localhost&quot;,</para>
	/// <para>and the remote address will be their network address.</para>
	/// <para>We use the same type for either IPv4 or IPv6, and</para>
	/// <para>the address is always store as IPv6.  We use IPv4</para>
	/// <para>mapped addresses to handle IPv4.</para>
	/// </summary>
	k_ESteamNetworkingIdentityType_IPAddress = 1,
	/// <summary>
	/// <para>Generic string/binary blobs.  It&apos;s up to your app to interpret this.</para>
	/// <para>This library can tell you if the remote host presented a certificate</para>
	/// <para>signed by somebody you have chosen to trust, with this identity on it.</para>
	/// <para>It&apos;s up to you to ultimately decide what this identity means.</para>
	/// </summary>
	k_ESteamNetworkingIdentityType_GenericString = 2,
	k_ESteamNetworkingIdentityType_GenericBytes = 3,
	/// <summary>
	/// <para>This identity type is used when we parse a string that looks like is a</para>
	/// <para>valid identity, just of a kind that we don&apos;t recognize.  In this case, we</para>
	/// <para>can often still communicate with the peer!  Allowing such identities</para>
	/// <para>for types we do not recognize useful is very useful for forward</para>
	/// <para>compatibility.</para>
	/// </summary>
	k_ESteamNetworkingIdentityType_UnknownType = 4,
	/// <summary>
	/// <para>Make sure this enum is stored in an int.</para>
	/// </summary>
	k_ESteamNetworkingIdentityType__Force32bit = 0x7fffffff,
}

/// <summary>
/// <para>&quot;Fake IPs&quot; are assigned to hosts, to make it easier to interface with</para>
/// <para>older code that assumed all hosts will have an IPv4 address</para>
/// </summary>
public enum ESteamNetworkingFakeIPType : int
{
	/// <summary>
	/// <para>Error, argument was not even an IP address, etc.</para>
	/// </summary>
	k_ESteamNetworkingFakeIPType_Invalid,
	/// <summary>
	/// <para>Argument was a valid IP, but was not from the reserved &quot;fake&quot; range</para>
	/// </summary>
	k_ESteamNetworkingFakeIPType_NotFake,
	/// <summary>
	/// <para>Globally unique (for a given app) IPv4 address.  Address space managed by Steam</para>
	/// </summary>
	k_ESteamNetworkingFakeIPType_GlobalIPv4,
	/// <summary>
	/// <para>Locally unique IPv4 address.  Address space managed by the local process.  For internal use only; should not be shared!</para>
	/// </summary>
	k_ESteamNetworkingFakeIPType_LocalIPv4,
	k_ESteamNetworkingFakeIPType__Force32Bit = 0x7fffffff
}

/// <summary>
/// <para>Connection status</para>
/// <para>High level connection status</para>
/// </summary>
public enum ESteamNetworkingConnectionState : int
{
	/// <summary>
	/// <para>Dummy value used to indicate an error condition in the API.</para>
	/// <para>Specified connection doesn&apos;t exist or has already been closed.</para>
	/// </summary>
	k_ESteamNetworkingConnectionState_None = 0,
	/// <summary>
	/// <para>We are trying to establish whether peers can talk to each other,</para>
	/// <para>whether they WANT to talk to each other, perform basic auth,</para>
	/// <para>and exchange crypt keys.</para>
	/// <para>- For connections on the &quot;client&quot; side (initiated locally):</para>
	/// <para>We&apos;re in the process of trying to establish a connection.</para>
	/// <para>Depending on the connection type, we might not know who they are.</para>
	/// <para>Note that it is not possible to tell if we are waiting on the</para>
	/// <para>network to complete handshake packets, or for the application layer</para>
	/// <para>to accept the connection.</para>
	/// <para>- For connections on the &quot;server&quot; side (accepted through listen socket):</para>
	/// <para>We have completed some basic handshake and the client has presented</para>
	/// <para>some proof of identity.  The connection is ready to be accepted</para>
	/// <para>using AcceptConnection().</para>
	/// <para>In either case, any unreliable packets sent now are almost certain</para>
	/// <para>to be dropped.  Attempts to receive packets are guaranteed to fail.</para>
	/// <para>You may send messages if the send mode allows for them to be queued.</para>
	/// <para>but if you close the connection before the connection is actually</para>
	/// <para>established, any queued messages will be discarded immediately.</para>
	/// <para>(We will not attempt to flush the queue and confirm delivery to the</para>
	/// <para>remote host, which ordinarily happens when a connection is closed.)</para>
	/// </summary>
	k_ESteamNetworkingConnectionState_Connecting = 1,
	/// <summary>
	/// <para>Some connection types use a back channel or trusted 3rd party</para>
	/// <para>for earliest communication.  If the server accepts the connection,</para>
	/// <para>then these connections switch into the rendezvous state.  During this</para>
	/// <para>state, we still have not yet established an end-to-end route (through</para>
	/// <para>the relay network), and so if you send any messages unreliable, they</para>
	/// <para>are going to be discarded.</para>
	/// </summary>
	k_ESteamNetworkingConnectionState_FindingRoute = 2,
	/// <summary>
	/// <para>We&apos;ve received communications from our peer (and we know</para>
	/// <para>who they are) and are all good.  If you close the connection now,</para>
	/// <para>we will make our best effort to flush out any reliable sent data that</para>
	/// <para>has not been acknowledged by the peer.  (But note that this happens</para>
	/// <para>from within the application process, so unlike a TCP connection, you are</para>
	/// <para>not totally handing it off to the operating system to deal with it.)</para>
	/// </summary>
	k_ESteamNetworkingConnectionState_Connected = 3,
	/// <summary>
	/// <para>Connection has been closed by our peer, but not closed locally.</para>
	/// <para>The connection still exists from an API perspective.  You must close the</para>
	/// <para>handle to free up resources.  If there are any messages in the inbound queue,</para>
	/// <para>you may retrieve them.  Otherwise, nothing may be done with the connection</para>
	/// <para>except to close it.</para>
	/// <para>This stats is similar to CLOSE_WAIT in the TCP state machine.</para>
	/// </summary>
	k_ESteamNetworkingConnectionState_ClosedByPeer = 4,
	/// <summary>
	/// <para>A disruption in the connection has been detected locally.  (E.g. timeout,</para>
	/// <para>local internet connection disrupted, etc.)</para>
	/// <para>The connection still exists from an API perspective.  You must close the</para>
	/// <para>handle to free up resources.</para>
	/// <para>Attempts to send further messages will fail.  Any remaining received messages</para>
	/// <para>in the queue are available.</para>
	/// </summary>
	k_ESteamNetworkingConnectionState_ProblemDetectedLocally = 5,
	/// <summary>
	/// <para>The following values are used internally and will not be returned by any API.</para>
	/// <para>We document them here to provide a little insight into the state machine that is used</para>
	/// <para>under the hood.</para>
	/// <para>We&apos;ve disconnected on our side, and from an API perspective the connection is closed.</para>
	/// <para>No more data may be sent or received.  All reliable data has been flushed, or else</para>
	/// <para>we&apos;ve given up and discarded it.  We do not yet know for sure that the peer knows</para>
	/// <para>the connection has been closed, however, so we&apos;re just hanging around so that if we do</para>
	/// <para>get a packet from them, we can send them the appropriate packets so that they can</para>
	/// <para>know why the connection was closed (and not have to rely on a timeout, which makes</para>
	/// <para>it appear as if something is wrong).</para>
	/// </summary>
	k_ESteamNetworkingConnectionState_FinWait = -1,
	/// <summary>
	/// <para>We&apos;ve disconnected on our side, and from an API perspective the connection is closed.</para>
	/// <para>No more data may be sent or received.  From a network perspective, however, on the wire,</para>
	/// <para>we have not yet given any indication to the peer that the connection is closed.</para>
	/// <para>We are in the process of flushing out the last bit of reliable data.  Once that is done,</para>
	/// <para>we will inform the peer that the connection has been closed, and transition to the</para>
	/// <para>FinWait state.</para>
	/// <para>Note that no indication is given to the remote host that we have closed the connection,</para>
	/// <para>until the data has been flushed.  If the remote host attempts to send us data, we will</para>
	/// <para>do whatever is necessary to keep the connection alive until it can be closed properly.</para>
	/// <para>But in fact the data will be discarded, since there is no way for the application to</para>
	/// <para>read it back.  Typically this is not a problem, as application protocols that utilize</para>
	/// <para>the lingering functionality are designed for the remote host to wait for the response</para>
	/// <para>before sending any more data.</para>
	/// </summary>
	k_ESteamNetworkingConnectionState_Linger = -2,
	/// <summary>
	/// <para>Connection is completely inactive and ready to be destroyed</para>
	/// </summary>
	k_ESteamNetworkingConnectionState_Dead = -3,
	k_ESteamNetworkingConnectionState__Force32Bit = 0x7fffffff
}

/// <summary>
/// <para>Enumerate various causes of connection termination.  These are designed to work similar</para>
/// <para>to HTTP error codes: the numeric range gives you a rough classification as to the source</para>
/// <para>of the problem.</para>
/// </summary>
public enum ESteamNetConnectionEnd : int
{
	/// <summary>
	/// <para>Invalid/sentinel value</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Invalid = 0,
	/// <summary>
	/// <para>Application codes.  These are the values you will pass to</para>
	/// <para>ISteamNetworkingSockets::CloseConnection.  You can use these codes if</para>
	/// <para>you want to plumb through application-specific reason codes.  If you don&apos;t</para>
	/// <para>need this facility, feel free to always pass</para>
	/// <para>k_ESteamNetConnectionEnd_App_Generic.</para>
	/// <para>The distinction between &quot;normal&quot; and &quot;exceptional&quot; termination is</para>
	/// <para>one you may use if you find useful, but it&apos;s not necessary for you</para>
	/// <para>to do so.  The only place where we distinguish between normal and</para>
	/// <para>exceptional is in connection analytics.  If a significant</para>
	/// <para>proportion of connections terminates in an exceptional manner,</para>
	/// <para>this can trigger an alert.</para>
	/// <para>1xxx: Application ended the connection in a &quot;usual&quot; manner.</para>
	/// <para>E.g.: user intentionally disconnected from the server,</para>
	/// <para>gameplay ended normally, etc</para>
	/// </summary>
	k_ESteamNetConnectionEnd_App_Min = 1000,
	k_ESteamNetConnectionEnd_App_Generic = k_ESteamNetConnectionEnd_App_Min,
	/// <summary>
	/// <para>Use codes in this range for &quot;normal&quot; disconnection</para>
	/// </summary>
	k_ESteamNetConnectionEnd_App_Max = 1999,
	/// <summary>
	/// <para>2xxx: Application ended the connection in some sort of exceptional</para>
	/// <para>or unusual manner that might indicate a bug or configuration</para>
	/// <para>issue.</para>
	/// </summary>
	k_ESteamNetConnectionEnd_AppException_Min = 2000,
	k_ESteamNetConnectionEnd_AppException_Generic = k_ESteamNetConnectionEnd_AppException_Min,
	/// <summary>
	/// <para>Use codes in this range for &quot;unusual&quot; disconnection</para>
	/// </summary>
	k_ESteamNetConnectionEnd_AppException_Max = 2999,
	/// <summary>
	/// <para>System codes.  These will be returned by the system when</para>
	/// <para>the connection state is k_ESteamNetworkingConnectionState_ClosedByPeer</para>
	/// <para>or k_ESteamNetworkingConnectionState_ProblemDetectedLocally.  It is</para>
	/// <para>illegal to pass a code in this range to ISteamNetworkingSockets::CloseConnection</para>
	/// <para>3xxx: Connection failed or ended because of problem with the</para>
	/// <para>local host or their connection to the Internet.</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Local_Min = 3000,
	/// <summary>
	/// <para>You cannot do what you want to do because you&apos;re running in offline mode.</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Local_OfflineMode = 3001,
	/// <summary>
	/// <para>We&apos;re having trouble contacting many (perhaps all) relays.</para>
	/// <para>Since it&apos;s unlikely that they all went offline at once, the best</para>
	/// <para>explanation is that we have a problem on our end.  Note that we don&apos;t</para>
	/// <para>bother distinguishing between &quot;many&quot; and &quot;all&quot;, because in practice,</para>
	/// <para>it takes time to detect a connection problem, and by the time</para>
	/// <para>the connection has timed out, we might not have been able to</para>
	/// <para>actively probe all of the relay clusters, even if we were able to</para>
	/// <para>contact them at one time.  So this code just means that:</para>
	/// <para>* We don&apos;t have any recent successful communication with any relay.</para>
	/// <para>* We have evidence of recent failures to communicate with multiple relays.</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Local_ManyRelayConnectivity = 3002,
	/// <summary>
	/// <para>A hosted server is having trouble talking to the relay</para>
	/// <para>that the client was using, so the problem is most likely</para>
	/// <para>on our end</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Local_HostedServerPrimaryRelay = 3003,
	/// <summary>
	/// <para>We&apos;re not able to get the SDR network config.  This is</para>
	/// <para>*almost* always a local issue, since the network config</para>
	/// <para>comes from the CDN, which is pretty darn reliable.</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Local_NetworkConfig = 3004,
	/// <summary>
	/// <para>Steam rejected our request because we don&apos;t have rights</para>
	/// <para>to do this.</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Local_Rights = 3005,
	/// <summary>
	/// <para>ICE P2P rendezvous failed because we were not able to</para>
	/// <para>determine our &quot;public&quot; address (e.g. reflexive address via STUN)</para>
	/// <para>If relay fallback is available (it always is on Steam), then</para>
	/// <para>this is only used internally and will not be returned as a high</para>
	/// <para>level failure.</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Local_P2P_ICE_NoPublicAddresses = 3006,
	k_ESteamNetConnectionEnd_Local_Max = 3999,
	/// <summary>
	/// <para>4xxx: Connection failed or ended, and it appears that the</para>
	/// <para>cause does NOT have to do with the local host or their</para>
	/// <para>connection to the Internet.  It could be caused by the</para>
	/// <para>remote host, or it could be somewhere in between.</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Remote_Min = 4000,
	/// <summary>
	/// <para>The connection was lost, and as far as we can tell our connection</para>
	/// <para>to relevant services (relays) has not been disrupted.  This doesn&apos;t</para>
	/// <para>mean that the problem is &quot;their fault&quot;, it just means that it doesn&apos;t</para>
	/// <para>appear that we are having network issues on our end.</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Remote_Timeout = 4001,
	/// <summary>
	/// <para>Something was invalid with the cert or crypt handshake</para>
	/// <para>info you gave me, I don&apos;t understand or like your key types,</para>
	/// <para>etc.</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Remote_BadCrypt = 4002,
	/// <summary>
	/// <para>You presented me with a cert that was I was able to parse</para>
	/// <para>and *technically* we could use encrypted communication.</para>
	/// <para>But there was a problem that prevents me from checking your identity</para>
	/// <para>or ensuring that somebody int he middle can&apos;t observe our communication.</para>
	/// <para>E.g.: - the CA key was missing (and I don&apos;t accept unsigned certs)</para>
	/// <para>- The CA key isn&apos;t one that I trust,</para>
	/// <para>- The cert doesn&apos;t was appropriately restricted by app, user, time, data center, etc.</para>
	/// <para>- The cert wasn&apos;t issued to you.</para>
	/// <para>- etc</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Remote_BadCert = 4003,
	// k_ESteamNetConnectionEnd_Remote_NotLoggedIn_DEPRECATED = 4004,
	// k_ESteamNetConnectionEnd_Remote_NotRunningApp_DEPRECATED = 4005,
	/// <summary>
	/// <para>These will never be returned</para>
	/// <para>Something wrong with the protocol version you are using.</para>
	/// <para>(Probably the code you are running is too old.)</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Remote_BadProtocolVersion = 4006,
	/// <summary>
	/// <para>NAT punch failed failed because we never received any public</para>
	/// <para>addresses from the remote host.  (But we did receive some</para>
	/// <para>signals form them.)</para>
	/// <para>If relay fallback is available (it always is on Steam), then</para>
	/// <para>this is only used internally and will not be returned as a high</para>
	/// <para>level failure.</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Remote_P2P_ICE_NoPublicAddresses = 4007,
	k_ESteamNetConnectionEnd_Remote_Max = 4999,
	/// <summary>
	/// <para>5xxx: Connection failed for some other reason.</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Misc_Min = 5000,
	/// <summary>
	/// <para>A failure that isn&apos;t necessarily the result of a software bug,</para>
	/// <para>but that should happen rarely enough that it isn&apos;t worth specifically</para>
	/// <para>writing UI or making a localized message for.</para>
	/// <para>The debug string should contain further details.</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Misc_Generic = 5001,
	/// <summary>
	/// <para>Generic failure that is most likely a software bug.</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Misc_InternalError = 5002,
	/// <summary>
	/// <para>The connection to the remote host timed out, but we</para>
	/// <para>don&apos;t know if the problem is on our end, in the middle,</para>
	/// <para>or on their end.</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Misc_Timeout = 5003,
	// k_ESteamNetConnectionEnd_Misc_RelayConnectivity_DEPRECATED = 5004,
	/// <summary>
	/// <para>There&apos;s some trouble talking to Steam.</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Misc_SteamConnectivity = 5005,
	/// <summary>
	/// <para>A server in a dedicated hosting situation has no relay sessions</para>
	/// <para>active with which to talk back to a client.  (It&apos;s the client&apos;s</para>
	/// <para>job to open and maintain those sessions.)</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Misc_NoRelaySessionsToClient = 5006,
	// k_ESteamNetConnectionEnd_Misc_ServerNeverReplied = 5007,
	/// <summary>
	/// <para>While trying to initiate a connection, we never received</para>
	/// <para>*any* communication from the peer.</para>
	/// <para>P2P rendezvous failed in a way that we don&apos;t have more specific</para>
	/// <para>information</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Misc_P2P_Rendezvous = 5008,
	/// <summary>
	/// <para>NAT punch failed, probably due to NAT/firewall configuration.</para>
	/// <para>If relay fallback is available (it always is on Steam), then</para>
	/// <para>this is only used internally and will not be returned as a high</para>
	/// <para>level failure.</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Misc_P2P_NAT_Firewall = 5009,
	/// <summary>
	/// <para>Our peer replied that it has no record of the connection.</para>
	/// <para>This should not happen ordinarily, but can happen in a few</para>
	/// <para>exception cases:</para>
	/// <para>- This is an old connection, and the peer has already cleaned</para>
	/// <para>up and forgotten about it.  (Perhaps it timed out and they</para>
	/// <para>closed it and were not able to communicate this to us.)</para>
	/// <para>- A bug or internal protocol error has caused us to try to</para>
	/// <para>talk to the peer about the connection before we received</para>
	/// <para>confirmation that the peer has accepted the connection.</para>
	/// <para>- The peer thinks that we have closed the connection for some</para>
	/// <para>reason (perhaps a bug), and believes that is it is</para>
	/// <para>acknowledging our closure.</para>
	/// </summary>
	k_ESteamNetConnectionEnd_Misc_PeerSentNoConnection = 5010,
	k_ESteamNetConnectionEnd_Misc_Max = 5999,
	k_ESteamNetConnectionEnd__Force32Bit = 0x7fffffff
}

/// <summary>
/// <para>Configuration values</para>
/// <para>Configuration values can be applied to different types of objects.</para>
/// </summary>
public enum ESteamNetworkingConfigScope : int
{
	/// <summary>
	/// <para>Get/set global option, or defaults.  Even options that apply to more specific scopes</para>
	/// <para>have global scope, and you may be able to just change the global defaults.  If you</para>
	/// <para>need different settings per connection (for example), then you will need to set those</para>
	/// <para>options at the more specific scope.</para>
	/// </summary>
	k_ESteamNetworkingConfig_Global = 1,
	/// <summary>
	/// <para>Some options are specific to a particular interface.  Note that all connection</para>
	/// <para>and listen socket settings can also be set at the interface level, and they will</para>
	/// <para>apply to objects created through those interfaces.</para>
	/// </summary>
	k_ESteamNetworkingConfig_SocketsInterface = 2,
	/// <summary>
	/// <para>Options for a listen socket.  Listen socket options can be set at the interface layer,</para>
	/// <para>if  you have multiple listen sockets and they all use the same options.</para>
	/// <para>You can also set connection options on a listen socket, and they set the defaults</para>
	/// <para>for all connections accepted through this listen socket.  (They will be used if you don&apos;t</para>
	/// <para>set a connection option.)</para>
	/// </summary>
	k_ESteamNetworkingConfig_ListenSocket = 3,
	/// <summary>
	/// <para>Options for a specific connection.</para>
	/// </summary>
	k_ESteamNetworkingConfig_Connection = 4,
	k_ESteamNetworkingConfigScope__Force32Bit = 0x7fffffff
}

/// <summary>
/// <para>Different configuration values have different data types</para>
/// </summary>
public enum ESteamNetworkingConfigDataType : int
{
	k_ESteamNetworkingConfig_Int32 = 1,
	k_ESteamNetworkingConfig_Int64 = 2,
	k_ESteamNetworkingConfig_Float = 3,
	k_ESteamNetworkingConfig_String = 4,
	k_ESteamNetworkingConfig_Ptr = 5,
	k_ESteamNetworkingConfigDataType__Force32Bit = 0x7fffffff
}

/// <summary>
/// <para>Configuration options</para>
/// </summary>
public enum ESteamNetworkingConfigValue : int
{
	k_ESteamNetworkingConfig_Invalid = 0,
	/// <summary>
	/// <para>Connection options</para>
	/// <para>[connection int32] Timeout value (in ms) to use when first connecting</para>
	/// </summary>
	k_ESteamNetworkingConfig_TimeoutInitial = 24,
	/// <summary>
	/// <para>[connection int32] Timeout value (in ms) to use after connection is established</para>
	/// </summary>
	k_ESteamNetworkingConfig_TimeoutConnected = 25,
	/// <summary>
	/// <para>[connection int32] Upper limit of buffered pending bytes to be sent,</para>
	/// <para>if this is reached SendMessage will return k_EResultLimitExceeded</para>
	/// <para>Default is 512k (524288 bytes)</para>
	/// </summary>
	k_ESteamNetworkingConfig_SendBufferSize = 9,
	/// <summary>
	/// <para>[connection int32] Upper limit on total size (in bytes) of received messages</para>
	/// <para>that will be buffered waiting to be processed by the application.  If this limit</para>
	/// <para>is exceeded, packets will be dropped.  This is to protect us from a malicious</para>
	/// <para>peer flooding us with messages faster than we can process them.</para>
	/// <para>This must be bigger than k_ESteamNetworkingConfig_RecvMaxMessageSize</para>
	/// </summary>
	k_ESteamNetworkingConfig_RecvBufferSize = 47,
	/// <summary>
	/// <para>[connection int32] Upper limit on the number of received messages that will</para>
	/// <para>that will be buffered waiting to be processed by the application.  If this limit</para>
	/// <para>is exceeded, packets will be dropped.  This is to protect us from a malicious</para>
	/// <para>peer flooding us with messages faster than we can pull them off the wire.</para>
	/// </summary>
	k_ESteamNetworkingConfig_RecvBufferMessages = 48,
	/// <summary>
	/// <para>[connection int32] Maximum message size that we are willing to receive.</para>
	/// <para>if a client attempts to send us a message larger than this, the connection</para>
	/// <para>will be immediately closed.</para>
	/// <para>Default is 512k (524288 bytes).  Note that the peer needs to be able to</para>
	/// <para>send a message this big.  (See k_cbMaxSteamNetworkingSocketsMessageSizeSend.)</para>
	/// </summary>
	k_ESteamNetworkingConfig_RecvMaxMessageSize = 49,
	/// <summary>
	/// <para>[connection int32] Max number of message segments that can be received</para>
	/// <para>in a single UDP packet.  While decoding a packet, if the number of segments</para>
	/// <para>exceeds this, we will abort further packet processing.</para>
	/// <para>The default is effectively unlimited.  If you know that you very rarely</para>
	/// <para>send small packets, you can protect yourself from malicious senders by</para>
	/// <para>lowering this number.</para>
	/// <para>In particular, if you are NOT using the reliability layer and are only using</para>
	/// <para>SteamNetworkingSockets for datagram transport, setting this to a very low</para>
	/// <para>number may be beneficial.  (We recommend a value of 2.)  Make sure your sender</para>
	/// <para>disables Nagle!</para>
	/// </summary>
	k_ESteamNetworkingConfig_RecvMaxSegmentsPerPacket = 50,
	/// <summary>
	/// <para>[connection int64] Get/set userdata as a configuration option.</para>
	/// <para>The default value is -1.   You may want to set the user data as</para>
	/// <para>a config value, instead of using ISteamNetworkingSockets::SetConnectionUserData</para>
	/// <para>in two specific instances:</para>
	/// <para>- You wish to set the userdata atomically when creating</para>
	/// <para>an outbound connection, so that the userdata is filled in properly</para>
	/// <para>for any callbacks that happen.  However, note that this trick</para>
	/// <para>only works for connections initiated locally!  For incoming</para>
	/// <para>connections, multiple state transitions may happen and</para>
	/// <para>callbacks be queued, before you are able to service the first</para>
	/// <para>callback!  Be careful!</para>
	/// <para>- You can set the default userdata for all newly created connections</para>
	/// <para>by setting this value at a higher level (e.g. on the listen</para>
	/// <para>socket or at the global level.)  Then this default</para>
	/// <para>value will be inherited when the connection is created.</para>
	/// <para>This is useful in case -1 is a valid userdata value, and you</para>
	/// <para>wish to use something else as the default value so you can</para>
	/// <para>tell if it has been set or not.</para>
	/// <para>HOWEVER: once a connection is created, the effective value is</para>
	/// <para>then bound to the connection.  Unlike other connection options,</para>
	/// <para>if you change it again at a higher level, the new value will not</para>
	/// <para>be inherited by connections.</para>
	/// <para>Using the userdata field in callback structs is not advised because</para>
	/// <para>of tricky race conditions.  Instead, you might try one of these methods:</para>
	/// <para>- Use a separate map with the HSteamNetConnection as the key.</para>
	/// <para>- Fetch the userdata from the connection in your callback</para>
	/// <para>using ISteamNetworkingSockets::GetConnectionUserData, to</para>
	/// <para>ensure you have the current value.</para>
	/// </summary>
	k_ESteamNetworkingConfig_ConnectionUserData = 40,
	/// <summary>
	/// <para>[connection int32] Minimum/maximum send rate clamp, in bytes/sec.</para>
	/// <para>At the time of this writing these two options should always be set to</para>
	/// <para>the same value, to manually configure a specific send rate.  The default</para>
	/// <para>value is 256K.  Eventually we hope to have the library estimate the bandwidth</para>
	/// <para>of the channel and set the send rate to that estimated bandwidth, and these</para>
	/// <para>values will only set limits on that send rate.</para>
	/// </summary>
	k_ESteamNetworkingConfig_SendRateMin = 10,
	k_ESteamNetworkingConfig_SendRateMax = 11,
	/// <summary>
	/// <para>[connection int32] Nagle time, in microseconds.  When SendMessage is called, if</para>
	/// <para>the outgoing message is less than the size of the MTU, it will be</para>
	/// <para>queued for a delay equal to the Nagle timer value.  This is to ensure</para>
	/// <para>that if the application sends several small messages rapidly, they are</para>
	/// <para>coalesced into a single packet.</para>
	/// <para>See historical RFC 896.  Value is in microseconds.</para>
	/// <para>Default is 5000us (5ms).</para>
	/// </summary>
	k_ESteamNetworkingConfig_NagleTime = 12,
	/// <summary>
	/// <para>[connection int32] Don&apos;t automatically fail IP connections that don&apos;t have</para>
	/// <para>strong auth.  On clients, this means we will attempt the connection even if</para>
	/// <para>we don&apos;t know our identity or can&apos;t get a cert.  On the server, it means that</para>
	/// <para>we won&apos;t automatically reject a connection due to a failure to authenticate.</para>
	/// <para>(You can examine the incoming connection and decide whether to accept it.)</para>
	/// <para>0: Don&apos;t attempt or accept unauthorized connections</para>
	/// <para>1: Attempt authorization when connecting, and allow unauthorized peers, but emit warnings</para>
	/// <para>2: don&apos;t attempt authentication, or complain if peer is unauthenticated</para>
	/// <para>This is a dev configuration value, and you should not let users modify it in</para>
	/// <para>production.</para>
	/// </summary>
	k_ESteamNetworkingConfig_IP_AllowWithoutAuth = 23,
	/// <summary>
	/// <para>[connection int32] The same as IP_AllowWithoutAuth, but will only apply</para>
	/// <para>for connections to/from localhost addresses.  Whichever value is larger</para>
	/// <para>(more permissive) will be used.</para>
	/// </summary>
	k_ESteamNetworkingConfig_IPLocalHost_AllowWithoutAuth = 52,
	/// <summary>
	/// <para>[connection int32] Do not send UDP packets with a payload of</para>
	/// <para>larger than N bytes.  If you set this, k_ESteamNetworkingConfig_MTU_DataSize</para>
	/// <para>is automatically adjusted</para>
	/// </summary>
	k_ESteamNetworkingConfig_MTU_PacketSize = 32,
	/// <summary>
	/// <para>[connection int32] (read only) Maximum message size you can send that</para>
	/// <para>will not fragment, based on k_ESteamNetworkingConfig_MTU_PacketSize</para>
	/// </summary>
	k_ESteamNetworkingConfig_MTU_DataSize = 33,
	/// <summary>
	/// <para>[connection int32] Allow unencrypted (and unauthenticated) communication.</para>
	/// <para>0: Not allowed (the default)</para>
	/// <para>1: Allowed, but prefer encrypted</para>
	/// <para>2: Allowed, and preferred</para>
	/// <para>3: Required.  (Fail the connection if the peer requires encryption.)</para>
	/// <para>This is a dev configuration value, since its purpose is to disable encryption.</para>
	/// <para>You should not let users modify it in production.  (But note that it requires</para>
	/// <para>the peer to also modify their value in order for encryption to be disabled.)</para>
	/// </summary>
	k_ESteamNetworkingConfig_Unencrypted = 34,
	/// <summary>
	/// <para>[connection int32] Set this to 1 on outbound connections and listen sockets,</para>
	/// <para>to enable &quot;symmetric connect mode&quot;, which is useful in the following</para>
	/// <para>common peer-to-peer use case:</para>
	/// <para>- The two peers are &quot;equal&quot; to each other.  (Neither is clearly the &quot;client&quot;</para>
	/// <para>or &quot;server&quot;.)</para>
	/// <para>- Either peer may initiate the connection, and indeed they may do this</para>
	/// <para>at the same time</para>
	/// <para>- The peers only desire a single connection to each other, and if both</para>
	/// <para>peers initiate connections simultaneously, a protocol is needed for them</para>
	/// <para>to resolve the conflict, so that we end up with a single connection.</para>
	/// <para>This use case is both common, and involves subtle race conditions and tricky</para>
	/// <para>pitfalls, which is why the API has support for dealing with it.</para>
	/// <para>If an incoming connection arrives on a listen socket or via custom signaling,</para>
	/// <para>and the application has not attempted to make a matching outbound connection</para>
	/// <para>in symmetric mode, then the incoming connection can be accepted as usual.</para>
	/// <para>A &quot;matching&quot; connection means that the relevant endpoint information matches.</para>
	/// <para>(At the time this comment is being written, this is only supported for P2P</para>
	/// <para>connections, which means that the peer identities must match, and the virtual</para>
	/// <para>port must match.  At a later time, symmetric mode may be supported for other</para>
	/// <para>connection types.)</para>
	/// <para>If connections are initiated by both peers simultaneously, race conditions</para>
	/// <para>can arise, but fortunately, most of them are handled internally and do not</para>
	/// <para>require any special awareness from the application.  However, there</para>
	/// <para>is one important case that application code must be aware of:</para>
	/// <para>If application code attempts an outbound connection using a ConnectXxx</para>
	/// <para>function in symmetric mode, and a matching incoming connection is already</para>
	/// <para>waiting on a listen socket, then instead of forming a new connection,</para>
	/// <para>the ConnectXxx call will accept the existing incoming connection, and return</para>
	/// <para>a connection handle to this accepted connection.</para>
	/// <para>IMPORTANT: in this case, a SteamNetConnectionStatusChangedCallback_t</para>
	/// <para>has probably *already* been posted to the queue for the incoming connection!</para>
	/// <para>(Once callbacks are posted to the queue, they are not modified.)  It doesn&apos;t</para>
	/// <para>matter if the callback has not been consumed by the app.  Thus, application</para>
	/// <para>code that makes use of symmetric connections must be aware that, when processing a</para>
	/// <para>SteamNetConnectionStatusChangedCallback_t for an incoming connection, the</para>
	/// <para>m_hConn may refer to a new connection that the app has has not</para>
	/// <para>seen before (the usual case), but it may also refer to a connection that</para>
	/// <para>has already been accepted implicitly through a call to Connect()!  In this</para>
	/// <para>case, AcceptConnection() will return k_EResultDuplicateRequest.</para>
	/// <para>Only one symmetric connection to a given peer (on a given virtual port)</para>
	/// <para>may exist at any given time.  If client code attempts to create a connection,</para>
	/// <para>and a (live) connection already exists on the local host, then either the</para>
	/// <para>existing connection will be accepted as described above, or the attempt</para>
	/// <para>to create a new connection will fail.  Furthermore, linger mode functionality</para>
	/// <para>is not supported on symmetric connections.</para>
	/// <para>A more complicated race condition can arise if both peers initiate a connection</para>
	/// <para>at roughly the same time.  In this situation, each peer will receive an incoming</para>
	/// <para>connection from the other peer, when the application code has already initiated</para>
	/// <para>an outgoing connection to that peer.  The peers must resolve this conflict and</para>
	/// <para>decide who is going to act as the &quot;server&quot; and who will act as the &quot;client&quot;.</para>
	/// <para>Typically the application does not need to be aware of this case as it is handled</para>
	/// <para>internally.  On both sides, the will observe their outbound connection being</para>
	/// <para>&quot;accepted&quot;, although one of them one have been converted internally to act</para>
	/// <para>as the &quot;server&quot;.</para>
	/// <para>In general, symmetric mode should be all-or-nothing: do not mix symmetric</para>
	/// <para>connections with a non-symmetric connection that it might possible &quot;match&quot;</para>
	/// <para>with.  If you use symmetric mode on any connections, then both peers should</para>
	/// <para>use it on all connections, and the corresponding listen socket, if any.  The</para>
	/// <para>behaviour when symmetric and ordinary connections are mixed is not defined by</para>
	/// <para>this API, and you should not rely on it.  (This advice only applies when connections</para>
	/// <para>might possibly &quot;match&quot;.  For example, it&apos;s OK to use all symmetric mode</para>
	/// <para>connections on one virtual port, and all ordinary, non-symmetric connections</para>
	/// <para>on a different virtual port, as there is no potential for ambiguity.)</para>
	/// <para>When using the feature, you should set it in the following situations on</para>
	/// <para>applicable objects:</para>
	/// <para>- When creating an outbound connection using ConnectXxx function</para>
	/// <para>- When creating a listen socket.  (Note that this will automatically cause</para>
	/// <para>any accepted connections to inherit the flag.)</para>
	/// <para>- When using custom signaling, before accepting an incoming connection.</para>
	/// <para>Setting the flag on listen socket and accepted connections will enable the</para>
	/// <para>API to automatically deal with duplicate incoming connections, even if the</para>
	/// <para>local host has not made any outbound requests.  (In general, such duplicate</para>
	/// <para>requests from a peer are ignored internally and will not be visible to the</para>
	/// <para>application code.  The previous connection must be closed or resolved first.)</para>
	/// </summary>
	k_ESteamNetworkingConfig_SymmetricConnect = 37,
	/// <summary>
	/// <para>[connection int32] For connection types that use &quot;virtual ports&quot;, this can be used</para>
	/// <para>to assign a local virtual port.  For incoming connections, this will always be the</para>
	/// <para>virtual port of the listen socket (or the port requested by the remote host if custom</para>
	/// <para>signaling is used and the connection is accepted), and cannot be changed.  For</para>
	/// <para>connections initiated locally, the local virtual port will default to the same as the</para>
	/// <para>requested remote virtual port, if you do not specify a different option when creating</para>
	/// <para>the connection.  The local port is only relevant for symmetric connections, when</para>
	/// <para>determining if two connections &quot;match.&quot;  In this case, if you need the local and remote</para>
	/// <para>port to differ, you can set this value.</para>
	/// <para>You can also read back this value on listen sockets.</para>
	/// <para>This value should not be read or written in any other context.</para>
	/// </summary>
	k_ESteamNetworkingConfig_LocalVirtualPort = 38,
	/// <summary>
	/// <para>[connection int32] Enable Dual wifi band support for this connection</para>
	/// <para>0 = no, 1 = yes, 2 = simulate it for debugging, even if dual wifi not available</para>
	/// </summary>
	k_ESteamNetworkingConfig_DualWifi_Enable = 39,
	/// <summary>
	/// <para>[connection int32] True to enable diagnostics reporting through</para>
	/// <para>generic platform UI.  (Only available on Steam.)</para>
	/// </summary>
	k_ESteamNetworkingConfig_EnableDiagnosticsUI = 46,
	/// <summary>
	/// <para>[connection int32] Send of time-since-previous-packet values in each UDP packet.</para>
	/// <para>This add a small amount of packet overhead but allows for detailed jitter measurements</para>
	/// <para>to be made by the receiver.</para>
	/// <para>-  0: disables the sending</para>
	/// <para>-  1: enables sending</para>
	/// <para>- -1: (the default) Use the default for the connection type.  For plain UDP connections,</para>
	/// <para>this is disabled, and for relayed connections, it is enabled.  Note that relays</para>
	/// <para>always send the value.</para>
	/// </summary>
	k_ESteamNetworkingConfig_SendTimeSincePreviousPacket = 59,
	/// <summary>
	/// <para>Simulating network conditions</para>
	/// <para>These are global (not per-connection) because they apply at</para>
	/// <para>a relatively low UDP layer.</para>
	/// <para>[global float, 0--100] Randomly discard N pct of packets instead of sending/recv</para>
	/// <para>This is a global option only, since it is applied at a low level</para>
	/// <para>where we don&apos;t have much context</para>
	/// </summary>
	k_ESteamNetworkingConfig_FakePacketLoss_Send = 2,
	k_ESteamNetworkingConfig_FakePacketLoss_Recv = 3,
	/// <summary>
	/// <para>[global int32].  Delay all outbound/inbound packets by N ms</para>
	/// </summary>
	k_ESteamNetworkingConfig_FakePacketLag_Send = 4,
	k_ESteamNetworkingConfig_FakePacketLag_Recv = 5,
	/// <summary>
	/// <para>Simulated jitter/clumping.</para>
	/// <para>For each packet, a jitter value is determined (which may</para>
	/// <para>be zero).  This amount is added as extra delay to the</para>
	/// <para>packet.  When a subsequent packet is queued, it receives its</para>
	/// <para>own random jitter amount from the current time.  if this would</para>
	/// <para>result in the packets being delivered out of order, the later</para>
	/// <para>packet queue time is adjusted to happen after the first packet.</para>
	/// <para>Thus simulating jitter by itself will not reorder packets, but it</para>
	/// <para>can &quot;clump&quot; them.</para>
	/// <para>- Avg: A random jitter time is generated using an exponential</para>
	/// <para>distribution using this value as the mean (ms).  The default</para>
	/// <para>is zero, which disables random jitter.</para>
	/// <para>- Max: Limit the random jitter time to this value (ms).</para>
	/// <para>- Pct: odds (0-100) that a random jitter value for the packet</para>
	/// <para>will be generated.  Otherwise, a jitter value of zero</para>
	/// <para>is used, and the packet will only be delayed by the jitter</para>
	/// <para>system if necessary to retain order, due to the jitter of a</para>
	/// <para>previous packet.</para>
	/// <para>All values are [global float]</para>
	/// <para>Fake jitter is simulated after fake lag, but before reordering.</para>
	/// </summary>
	k_ESteamNetworkingConfig_FakePacketJitter_Send_Avg = 53,
	k_ESteamNetworkingConfig_FakePacketJitter_Send_Max = 54,
	k_ESteamNetworkingConfig_FakePacketJitter_Send_Pct = 55,
	k_ESteamNetworkingConfig_FakePacketJitter_Recv_Avg = 56,
	k_ESteamNetworkingConfig_FakePacketJitter_Recv_Max = 57,
	k_ESteamNetworkingConfig_FakePacketJitter_Recv_Pct = 58,
	/// <summary>
	/// <para>[global float] 0-100 Percentage of packets we will add additional</para>
	/// <para>delay to.  If other packet(s) are sent/received within this delay</para>
	/// <para>window (that doesn&apos;t also randomly receive the same extra delay),</para>
	/// <para>then the packets become reordered.</para>
	/// <para>This mechanism is primarily intended to generate out-of-order</para>
	/// <para>packets.  To simulate random jitter, use the FakePacketJitter.</para>
	/// <para>Fake packet reordering is applied after fake lag and jitter</para>
	/// </summary>
	k_ESteamNetworkingConfig_FakePacketReorder_Send = 6,
	k_ESteamNetworkingConfig_FakePacketReorder_Recv = 7,
	/// <summary>
	/// <para>[global int32] Extra delay, in ms, to apply to reordered</para>
	/// <para>packets.  The same time value is used for sending and receiving.</para>
	/// </summary>
	k_ESteamNetworkingConfig_FakePacketReorder_Time = 8,
	/// <summary>
	/// <para>[global float 0--100] Globally duplicate some percentage of packets.</para>
	/// </summary>
	k_ESteamNetworkingConfig_FakePacketDup_Send = 26,
	k_ESteamNetworkingConfig_FakePacketDup_Recv = 27,
	/// <summary>
	/// <para>[global int32] Amount of delay, in ms, to delay duplicated packets.</para>
	/// <para>(We chose a random delay between 0 and this value)</para>
	/// </summary>
	k_ESteamNetworkingConfig_FakePacketDup_TimeMax = 28,
	/// <summary>
	/// <para>[global int32] Trace every UDP packet, similar to Wireshark or tcpdump.</para>
	/// <para>Value is max number of bytes to dump.  -1 disables tracing.</para>
	/// <para>0 only traces the info but no actual data bytes</para>
	/// </summary>
	k_ESteamNetworkingConfig_PacketTraceMaxBytes = 41,
	// Rate=0 disables the limiter entirely, which is the default.
	// Burst=0 disables burst.  (This is not realistic.  A
	/// <summary>
	/// <para>[global int32] Global UDP token bucket rate limits.</para>
	/// <para>&quot;Rate&quot; refers to the steady state rate. (Bytes/sec, the</para>
	/// <para>rate that tokens are put into the bucket.)  &quot;Burst&quot;</para>
	/// <para>refers to the max amount that could be sent in a single</para>
	/// <para>burst.  (In bytes, the max capacity of the bucket.)</para>
	/// <para>burst of at least 4K is recommended; the default is higher.)</para>
	/// </summary>
	k_ESteamNetworkingConfig_FakeRateLimit_Send_Rate = 42,
	k_ESteamNetworkingConfig_FakeRateLimit_Send_Burst = 43,
	k_ESteamNetworkingConfig_FakeRateLimit_Recv_Rate = 44,
	k_ESteamNetworkingConfig_FakeRateLimit_Recv_Burst = 45,
	/// <summary>
	/// <para>Timeout used for out-of-order correction.  This is used when we see a small</para>
	/// <para>gap in the sequence number on a packet flow.  For example let&apos;s say we are</para>
	/// <para>processing packet 105 when the most recent one was 103.  104 might have dropped,</para>
	/// <para>but there is also a chance that packets are simply being reordered.  It is very</para>
	/// <para>common on certain types of connections for packet 104 to arrive very soon after 105,</para>
	/// <para>especially if 104 was large and 104 was small.  In this case, when we see packet 105</para>
	/// <para>we will shunt it aside and pend it, in the hopes of seeing 104 soon after.  If 104</para>
	/// <para>arrives before the a timeout occurs, then we can deliver the packets in order to the</para>
	/// <para>remainder of packet processing, and we will record this as a &quot;correctable&quot; out-of-order</para>
	/// <para>situation.  If the timer expires, then we will process packet 105, and assume for now</para>
	/// <para>that 104 has dropped.  (If 104 later arrives, we will process it, but that will be</para>
	/// <para>accounted for as uncorrected.)</para>
	/// <para>The default value is 1000 microseconds.  Note that the Windows scheduler does not</para>
	/// <para>have microsecond precision.</para>
	/// <para>Set the value to 0 to disable out of order correction at the packet layer.</para>
	/// <para>In many cases we are still effectively able to correct the situation because</para>
	/// <para>reassembly of message fragments is tolerant of fragments packets arriving out of</para>
	/// <para>order.  Also, when messages are decoded and inserted into the queue for the app</para>
	/// <para>to receive them, we will correct out of order messages that have not been</para>
	/// <para>dequeued by the app yet.  However, when out-of-order packets are corrected</para>
	/// <para>at the packet layer, they will not reduce the connection quality measure.</para>
	/// <para>(E.g. SteamNetConnectionRealTimeStatus_t::m_flConnectionQualityLocal)</para>
	/// </summary>
	k_ESteamNetworkingConfig_OutOfOrderCorrectionWindowMicroseconds = 51,
	/// <summary>
	/// <para>Callbacks</para>
	/// <para>On Steam, you may use the default Steam callback dispatch mechanism.  If you prefer</para>
	/// <para>to not use this dispatch mechanism (or you are not running with Steam), or you want</para>
	/// <para>to associate specific functions with specific listen sockets or connections, you can</para>
	/// <para>register them as configuration values.</para>
	/// <para>Note also that ISteamNetworkingUtils has some helpers to set these globally.</para>
	/// <para>[connection FnSteamNetConnectionStatusChanged] Callback that will be invoked</para>
	/// <para>when the state of a connection changes.</para>
	/// <para>IMPORTANT: callbacks are dispatched to the handler that is in effect at the time</para>
	/// <para>the event occurs, which might be in another thread.  For example, immediately after</para>
	/// <para>creating a listen socket, you may receive an incoming connection.  And then immediately</para>
	/// <para>after this, the remote host may close the connection.  All of this could happen</para>
	/// <para>before the function to create the listen socket has returned.  For this reason,</para>
	/// <para>callbacks usually must be in effect at the time of object creation.  This means</para>
	/// <para>you should set them when you are creating the listen socket or connection, or have</para>
	/// <para>them in effect so they will be inherited at the time of object creation.</para>
	/// <para>For example:</para>
	/// <para>exterm void MyStatusChangedFunc( SteamNetConnectionStatusChangedCallback_t *info );</para>
	/// <para>SteamNetworkingConfigValue_t opt; opt.SetPtr( k_ESteamNetworkingConfig_Callback_ConnectionStatusChanged, MyStatusChangedFunc );</para>
	/// <para>SteamNetworkingIPAddr localAddress; localAddress.Clear();</para>
	/// <para>HSteamListenSocket hListenSock = SteamNetworkingSockets()-&gt;CreateListenSocketIP( localAddress, 1, &amp;opt );</para>
	/// <para>When accepting an incoming connection, there is no atomic way to switch the</para>
	/// <para>callback.  However, if the connection is DOA, AcceptConnection() will fail, and</para>
	/// <para>you can fetch the state of the connection at that time.</para>
	/// <para>If all connections and listen sockets can use the same callback, the simplest</para>
	/// <para>method is to set it globally before you create any listen sockets or connections.</para>
	/// </summary>
	k_ESteamNetworkingConfig_Callback_ConnectionStatusChanged = 201,
	/// <summary>
	/// <para>[global FnSteamNetAuthenticationStatusChanged] Callback that will be invoked</para>
	/// <para>when our auth state changes.  If you use this, install the callback before creating</para>
	/// <para>any connections or listen sockets, and don&apos;t change it.</para>
	/// <para>See: ISteamNetworkingUtils::SetGlobalCallback_SteamNetAuthenticationStatusChanged</para>
	/// </summary>
	k_ESteamNetworkingConfig_Callback_AuthStatusChanged = 202,
	/// <summary>
	/// <para>[global FnSteamRelayNetworkStatusChanged] Callback that will be invoked</para>
	/// <para>when our auth state changes.  If you use this, install the callback before creating</para>
	/// <para>any connections or listen sockets, and don&apos;t change it.</para>
	/// <para>See: ISteamNetworkingUtils::SetGlobalCallback_SteamRelayNetworkStatusChanged</para>
	/// </summary>
	k_ESteamNetworkingConfig_Callback_RelayNetworkStatusChanged = 203,
	/// <summary>
	/// <para>[global FnSteamNetworkingMessagesSessionRequest] Callback that will be invoked</para>
	/// <para>when a peer wants to initiate a SteamNetworkingMessagesSessionRequest.</para>
	/// <para>See: ISteamNetworkingUtils::SetGlobalCallback_MessagesSessionRequest</para>
	/// </summary>
	k_ESteamNetworkingConfig_Callback_MessagesSessionRequest = 204,
	/// <summary>
	/// <para>[global FnSteamNetworkingMessagesSessionFailed] Callback that will be invoked</para>
	/// <para>when a session you have initiated, or accepted either fails to connect, or loses</para>
	/// <para>connection in some unexpected way.</para>
	/// <para>See: ISteamNetworkingUtils::SetGlobalCallback_MessagesSessionFailed</para>
	/// </summary>
	k_ESteamNetworkingConfig_Callback_MessagesSessionFailed = 205,
	/// <summary>
	/// <para>[global FnSteamNetworkingSocketsCreateConnectionSignaling] Callback that will</para>
	/// <para>be invoked when we need to create a signaling object for a connection</para>
	/// <para>initiated locally.  See: ISteamNetworkingSockets::ConnectP2P,</para>
	/// <para>ISteamNetworkingMessages.</para>
	/// </summary>
	k_ESteamNetworkingConfig_Callback_CreateConnectionSignaling = 206,
	/// <summary>
	/// <para>[global FnSteamNetworkingFakeIPResult] Callback that&apos;s invoked when</para>
	/// <para>a FakeIP allocation finishes.  See: ISteamNetworkingSockets::BeginAsyncRequestFakeIP,</para>
	/// <para>ISteamNetworkingUtils::SetGlobalCallback_FakeIPResult</para>
	/// </summary>
	k_ESteamNetworkingConfig_Callback_FakeIPResult = 207,
	// k_ESteamNetworkingConfig_P2P_Discovery_Server_LocalPort = 101,
	// k_ESteamNetworkingConfig_P2P_Discovery_Client_RemotePort = 102,
	/// <summary>
	/// <para>P2P connection settings</para>
	/// <para>[listen socket int32] When you create a P2P listen socket, we will automatically</para>
	/// <para>open up a UDP port to listen for LAN connections.  LAN connections can be made</para>
	/// <para>without any signaling: both sides can be disconnected from the Internet.</para>
	/// <para>This value can be set to zero to disable the feature.</para>
	/// <para>[connection int32] P2P connections can perform broadcasts looking for the peer</para>
	/// <para>on the LAN.</para>
	/// <para>[connection string] Comma-separated list of STUN servers that can be used</para>
	/// <para>for NAT piercing.  If you set this to an empty string, NAT piercing will</para>
	/// <para>not be attempted.  Also if &quot;public&quot; candidates are not allowed for</para>
	/// <para>P2P_Transport_ICE_Enable, then this is ignored.</para>
	/// </summary>
	k_ESteamNetworkingConfig_P2P_STUN_ServerList = 103,
	/// <summary>
	/// <para>[connection int32] What types of ICE candidates to share with the peer.</para>
	/// <para>See k_nSteamNetworkingConfig_P2P_Transport_ICE_Enable_xxx values</para>
	/// </summary>
	k_ESteamNetworkingConfig_P2P_Transport_ICE_Enable = 104,
	/// <summary>
	/// <para>[connection int32] When selecting P2P transport, add various</para>
	/// <para>penalties to the scores for selected transports.  (Route selection</para>
	/// <para>scores are on a scale of milliseconds.  The score begins with the</para>
	/// <para>route ping time and is then adjusted.)</para>
	/// </summary>
	k_ESteamNetworkingConfig_P2P_Transport_ICE_Penalty = 105,
	k_ESteamNetworkingConfig_P2P_Transport_SDR_Penalty = 106,
	k_ESteamNetworkingConfig_P2P_TURN_ServerList = 107,
	k_ESteamNetworkingConfig_P2P_TURN_UserList = 108,
	k_ESteamNetworkingConfig_P2P_TURN_PassList = 109,
	// k_ESteamNetworkingConfig_P2P_Transport_LANBeacon_Penalty = 107,
	k_ESteamNetworkingConfig_P2P_Transport_ICE_Implementation = 110,
	/// <summary>
	/// <para>Settings for SDR relayed connections</para>
	/// <para>[global int32] If the first N pings to a port all fail, mark that port as unavailable for</para>
	/// <para>a while, and try a different one.  Some ISPs and routers may drop the first</para>
	/// <para>packet, so setting this to 1 may greatly disrupt communications.</para>
	/// </summary>
	k_ESteamNetworkingConfig_SDRClient_ConsecutitivePingTimeoutsFailInitial = 19,
	/// <summary>
	/// <para>[global int32] If N consecutive pings to a port fail, after having received successful</para>
	/// <para>communication, mark that port as unavailable for a while, and try a</para>
	/// <para>different one.</para>
	/// </summary>
	k_ESteamNetworkingConfig_SDRClient_ConsecutitivePingTimeoutsFail = 20,
	/// <summary>
	/// <para>[global int32] Minimum number of lifetime pings we need to send, before we think our estimate</para>
	/// <para>is solid.  The first ping to each cluster is very often delayed because of NAT,</para>
	/// <para>routers not having the best route, etc.  Until we&apos;ve sent a sufficient number</para>
	/// <para>of pings, our estimate is often inaccurate.  Keep pinging until we get this</para>
	/// <para>many pings.</para>
	/// </summary>
	k_ESteamNetworkingConfig_SDRClient_MinPingsBeforePingAccurate = 21,
	/// <summary>
	/// <para>[global int32] Set all steam datagram traffic to originate from the same</para>
	/// <para>local port. By default, we open up a new UDP socket (on a different local</para>
	/// <para>port) for each relay.  This is slightly less optimal, but it works around</para>
	/// <para>some routers that don&apos;t implement NAT properly.  If you have intermittent</para>
	/// <para>problems talking to relays that might be NAT related, try toggling</para>
	/// <para>this flag</para>
	/// </summary>
	k_ESteamNetworkingConfig_SDRClient_SingleSocket = 22,
	/// <summary>
	/// <para>[global string] Code of relay cluster to force use.  If not empty, we will</para>
	/// <para>only use relays in that cluster.  E.g. &apos;iad&apos;</para>
	/// </summary>
	k_ESteamNetworkingConfig_SDRClient_ForceRelayCluster = 29,
	/// <summary>
	/// <para>[connection string] For development, a base-64 encoded ticket generated</para>
	/// <para>using the cert tool.  This can be used to connect to a gameserver via SDR</para>
	/// <para>without a ticket generated using the game coordinator.  (You will still</para>
	/// <para>need a key that is trusted for your app, however.)</para>
	/// <para>This can also be passed using the SDR_DEVTICKET environment variable</para>
	/// </summary>
	k_ESteamNetworkingConfig_SDRClient_DevTicket = 30,
	/// <summary>
	/// <para>[global string] For debugging.  Override list of relays from the config with</para>
	/// <para>this set (maybe just one).  Comma-separated list.</para>
	/// </summary>
	k_ESteamNetworkingConfig_SDRClient_ForceProxyAddr = 31,
	/// <summary>
	/// <para>[global string] For debugging.  Force ping times to clusters to be the specified</para>
	/// <para>values.  A comma separated list of &lt;cluster&gt;=&lt;ms&gt; values.  E.g. &quot;sto=32,iad=100&quot;</para>
	/// <para>This is a dev configuration value, you probably should not let users modify it</para>
	/// <para>in production.</para>
	/// </summary>
	k_ESteamNetworkingConfig_SDRClient_FakeClusterPing = 36,
	/// <summary>
	/// <para>[global int32] When probing the SteamDatagram network, we limit exploration</para>
	/// <para>to the closest N POPs, based on our current best approximated ping to that POP.</para>
	/// </summary>
	k_ESteamNetworkingConfig_SDRClient_LimitPingProbesToNearestN = 60,
	/// <summary>
	/// <para>Log levels for debugging information of various subsystems.</para>
	/// <para>Higher numeric values will cause more stuff to be printed.</para>
	/// <para>See ISteamNetworkingUtils::SetDebugOutputFunction for more</para>
	/// <para>information</para>
	/// <para>The default for all values is k_ESteamNetworkingSocketsDebugOutputType_Warning.</para>
	/// <para>[connection int32] RTT calculations for inline pings and replies</para>
	/// </summary>
	k_ESteamNetworkingConfig_LogLevel_AckRTT = 13,
	/// <summary>
	/// <para>[connection int32] log SNP packets send/recv</para>
	/// </summary>
	k_ESteamNetworkingConfig_LogLevel_PacketDecode = 14,
	/// <summary>
	/// <para>[connection int32] log each message send/recv</para>
	/// </summary>
	k_ESteamNetworkingConfig_LogLevel_Message = 15,
	/// <summary>
	/// <para>[connection int32] dropped packets</para>
	/// </summary>
	k_ESteamNetworkingConfig_LogLevel_PacketGaps = 16,
	/// <summary>
	/// <para>[connection int32] P2P rendezvous messages</para>
	/// </summary>
	k_ESteamNetworkingConfig_LogLevel_P2PRendezvous = 17,
	/// <summary>
	/// <para>[global int32] Ping relays</para>
	/// </summary>
	k_ESteamNetworkingConfig_LogLevel_SDRRelayPings = 18,
	/// <summary>
	/// <para>Experimental.  Set the ECN header field on all outbound UDP packets</para>
	/// <para>-1 = the default, and means &quot;don&apos;t set anything&quot;.</para>
	/// <para>0..3 = set that value.  (Even though 0 is the default UDP ECN value, a 0 here means &quot;explicitly set a 0&quot;.)</para>
	/// </summary>
	k_ESteamNetworkingConfig_ECN = 999,
	/// <summary>
	/// <para>Deleted, do not use</para>
	/// </summary>
	k_ESteamNetworkingConfig_DELETED_EnumerateDevVars = 35,
	k_ESteamNetworkingConfigValue__Force32Bit = 0x7fffffff
}

/// <summary>
/// <para>Return value of ISteamNetworkintgUtils::GetConfigValue</para>
/// </summary>
public enum ESteamNetworkingGetConfigValueResult : int
{
	/// <summary>
	/// <para>No such configuration value</para>
	/// </summary>
	k_ESteamNetworkingGetConfigValue_BadValue = -1,
	/// <summary>
	/// <para>Bad connection handle, etc</para>
	/// </summary>
	k_ESteamNetworkingGetConfigValue_BadScopeObj = -2,
	/// <summary>
	/// <para>Couldn&apos;t fit the result in your buffer</para>
	/// </summary>
	k_ESteamNetworkingGetConfigValue_BufferTooSmall = -3,
	k_ESteamNetworkingGetConfigValue_OK = 1,
	/// <summary>
	/// <para>A value was not set at this level, but the effective (inherited) value was returned.</para>
	/// </summary>
	k_ESteamNetworkingGetConfigValue_OKInherited = 2,
	k_ESteamNetworkingGetConfigValueResult__Force32Bit = 0x7fffffff
}

/// <summary>
/// <para>Debug output</para>
/// <para>Detail level for diagnostic output callback.</para>
/// <para>See ISteamNetworkingUtils::SetDebugOutputFunction</para>
/// </summary>
public enum ESteamNetworkingSocketsDebugOutputType : int
{
	k_ESteamNetworkingSocketsDebugOutputType_None = 0,
	/// <summary>
	/// <para>You used the API incorrectly, or an internal error happened</para>
	/// </summary>
	k_ESteamNetworkingSocketsDebugOutputType_Bug = 1,
	/// <summary>
	/// <para>Run-time error condition that isn&apos;t the result of a bug.  (E.g. we are offline, cannot bind a port, etc)</para>
	/// </summary>
	k_ESteamNetworkingSocketsDebugOutputType_Error = 2,
	/// <summary>
	/// <para>Nothing is wrong, but this is an important notification</para>
	/// </summary>
	k_ESteamNetworkingSocketsDebugOutputType_Important = 3,
	k_ESteamNetworkingSocketsDebugOutputType_Warning = 4,
	/// <summary>
	/// <para>Recommended amount</para>
	/// </summary>
	k_ESteamNetworkingSocketsDebugOutputType_Msg = 5,
	/// <summary>
	/// <para>Quite a bit</para>
	/// </summary>
	k_ESteamNetworkingSocketsDebugOutputType_Verbose = 6,
	/// <summary>
	/// <para>Practically everything</para>
	/// </summary>
	k_ESteamNetworkingSocketsDebugOutputType_Debug = 7,
	/// <summary>
	/// <para>Wall of text, detailed packet contents breakdown, etc</para>
	/// </summary>
	k_ESteamNetworkingSocketsDebugOutputType_Everything = 8,
	k_ESteamNetworkingSocketsDebugOutputType__Force32Bit = 0x7fffffff
}

public enum ESteamIPType : int
{
	k_ESteamIPTypeIPv4 = 0,
	k_ESteamIPTypeIPv6 = 1,
}

/// <summary>
/// <para>Steam universes.  Each universe is a self-contained Steam instance.</para>
/// </summary>
public enum EUniverse : int
{
	k_EUniverseInvalid = 0,
	k_EUniversePublic = 1,
	k_EUniverseBeta = 2,
	k_EUniverseInternal = 3,
	k_EUniverseDev = 4,
	// k_EUniverseRC = 5,				// no such universe anymore
	k_EUniverseMax
}

