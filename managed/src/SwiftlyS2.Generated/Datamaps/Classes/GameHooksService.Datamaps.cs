using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed partial class GameHooksService
{
    internal readonly GameHookDatamaps DatamapsHook = new();
    public IGameHookDatamaps Datamaps => DatamapsHook;

    internal void SubscribeDatamapsHooks()
    {
        DatamapHooksPublisher.Subscribe(this);
    }

    internal void DisposeDatamapsHooks()
    {
        DatamapsHook.UnregisterAllListeners();
        DatamapHooksPublisher.Unsubscribe(this);
    }

    internal void InvokeCAmbientGenericRampThinkPre(ref CAmbientGenericRampThinkPreContext ctx)
    {
        if (!DatamapsHook.CAmbientGenericHook.CAmbientGenericRampThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CAmbientGenericHook.CAmbientGenericRampThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CAmbientGeneric::RampThink::Pre.");
            }
        }
    }

    internal void InvokeCAmbientGenericRampThinkPost(ref CAmbientGenericRampThinkPostContext ctx)
    {
        if (!DatamapsHook.CAmbientGenericHook.CAmbientGenericRampThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CAmbientGenericHook.CAmbientGenericRampThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CAmbientGeneric::RampThink::Post.");
            }
        }
    }

    internal void InvokeCBarnLightThink_ApplyLightStylesToTargetsPre(ref CBarnLightThink_ApplyLightStylesToTargetsPreContext ctx)
    {
        if (!DatamapsHook.CBarnLightHook.CBarnLightThink_ApplyLightStylesToTargetsHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBarnLightHook.CBarnLightThink_ApplyLightStylesToTargetsHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBarnLight::Think_ApplyLightStylesToTargets::Pre.");
            }
        }
    }

    internal void InvokeCBarnLightThink_ApplyLightStylesToTargetsPost(ref CBarnLightThink_ApplyLightStylesToTargetsPostContext ctx)
    {
        if (!DatamapsHook.CBarnLightHook.CBarnLightThink_ApplyLightStylesToTargetsHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBarnLightHook.CBarnLightThink_ApplyLightStylesToTargetsHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBarnLight::Think_ApplyLightStylesToTargets::Post.");
            }
        }
    }

    internal void InvokeCBarnLightThink_LightStyleEventPre(ref CBarnLightThink_LightStyleEventPreContext ctx)
    {
        if (!DatamapsHook.CBarnLightHook.CBarnLightThink_LightStyleEventHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBarnLightHook.CBarnLightThink_LightStyleEventHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBarnLight::Think_LightStyleEvent::Pre.");
            }
        }
    }

    internal void InvokeCBarnLightThink_LightStyleEventPost(ref CBarnLightThink_LightStyleEventPostContext ctx)
    {
        if (!DatamapsHook.CBarnLightHook.CBarnLightThink_LightStyleEventHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBarnLightHook.CBarnLightThink_LightStyleEventHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBarnLight::Think_LightStyleEvent::Post.");
            }
        }
    }

    internal void InvokeCBarnLightThink_SetNextQueuedLightStylePre(ref CBarnLightThink_SetNextQueuedLightStylePreContext ctx)
    {
        if (!DatamapsHook.CBarnLightHook.CBarnLightThink_SetNextQueuedLightStyleHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBarnLightHook.CBarnLightThink_SetNextQueuedLightStyleHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBarnLight::Think_SetNextQueuedLightStyle::Pre.");
            }
        }
    }

    internal void InvokeCBarnLightThink_SetNextQueuedLightStylePost(ref CBarnLightThink_SetNextQueuedLightStylePostContext ctx)
    {
        if (!DatamapsHook.CBarnLightHook.CBarnLightThink_SetNextQueuedLightStyleHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBarnLightHook.CBarnLightThink_SetNextQueuedLightStyleHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBarnLight::Think_SetNextQueuedLightStyle::Post.");
            }
        }
    }

    internal void InvokeCBaseAnimGraphChoreoServicesThinkPre(ref CBaseAnimGraphChoreoServicesThinkPreContext ctx)
    {
        if (!DatamapsHook.CBaseAnimGraphHook.CBaseAnimGraphChoreoServicesThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseAnimGraphHook.CBaseAnimGraphChoreoServicesThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseAnimGraph::ChoreoServicesThink::Pre.");
            }
        }
    }

    internal void InvokeCBaseAnimGraphChoreoServicesThinkPost(ref CBaseAnimGraphChoreoServicesThinkPostContext ctx)
    {
        if (!DatamapsHook.CBaseAnimGraphHook.CBaseAnimGraphChoreoServicesThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseAnimGraphHook.CBaseAnimGraphChoreoServicesThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseAnimGraph::ChoreoServicesThink::Post.");
            }
        }
    }

    internal void InvokeCBaseButtonActivateTouchPre(ref CBaseButtonActivateTouchPreContext ctx)
    {
        if (!DatamapsHook.CBaseButtonHook.CBaseButtonActivateTouchHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseButtonHook.CBaseButtonActivateTouchHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseButton::ActivateTouch::Pre.");
            }
        }
    }

    internal void InvokeCBaseButtonActivateTouchPost(ref CBaseButtonActivateTouchPostContext ctx)
    {
        if (!DatamapsHook.CBaseButtonHook.CBaseButtonActivateTouchHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseButtonHook.CBaseButtonActivateTouchHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseButton::ActivateTouch::Post.");
            }
        }
    }

    internal void InvokeCBaseButtonButtonBackHomePre(ref CBaseButtonButtonBackHomePreContext ctx)
    {
        if (!DatamapsHook.CBaseButtonHook.CBaseButtonButtonBackHomeHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseButtonHook.CBaseButtonButtonBackHomeHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseButton::ButtonBackHome::Pre.");
            }
        }
    }

    internal void InvokeCBaseButtonButtonBackHomePost(ref CBaseButtonButtonBackHomePostContext ctx)
    {
        if (!DatamapsHook.CBaseButtonHook.CBaseButtonButtonBackHomeHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseButtonHook.CBaseButtonButtonBackHomeHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseButton::ButtonBackHome::Post.");
            }
        }
    }

    internal void InvokeCBaseButtonButtonReturnPre(ref CBaseButtonButtonReturnPreContext ctx)
    {
        if (!DatamapsHook.CBaseButtonHook.CBaseButtonButtonReturnHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseButtonHook.CBaseButtonButtonReturnHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseButton::ButtonReturn::Pre.");
            }
        }
    }

    internal void InvokeCBaseButtonButtonReturnPost(ref CBaseButtonButtonReturnPostContext ctx)
    {
        if (!DatamapsHook.CBaseButtonHook.CBaseButtonButtonReturnHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseButtonHook.CBaseButtonButtonReturnHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseButton::ButtonReturn::Post.");
            }
        }
    }

    internal void InvokeCBaseButtonButtonSparkPre(ref CBaseButtonButtonSparkPreContext ctx)
    {
        if (!DatamapsHook.CBaseButtonHook.CBaseButtonButtonSparkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseButtonHook.CBaseButtonButtonSparkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseButton::ButtonSpark::Pre.");
            }
        }
    }

    internal void InvokeCBaseButtonButtonSparkPost(ref CBaseButtonButtonSparkPostContext ctx)
    {
        if (!DatamapsHook.CBaseButtonHook.CBaseButtonButtonSparkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseButtonHook.CBaseButtonButtonSparkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseButton::ButtonSpark::Post.");
            }
        }
    }

    internal void InvokeCBaseButtonButtonTouchPre(ref CBaseButtonButtonTouchPreContext ctx)
    {
        if (!DatamapsHook.CBaseButtonHook.CBaseButtonButtonTouchHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseButtonHook.CBaseButtonButtonTouchHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseButton::ButtonTouch::Pre.");
            }
        }
    }

    internal void InvokeCBaseButtonButtonTouchPost(ref CBaseButtonButtonTouchPostContext ctx)
    {
        if (!DatamapsHook.CBaseButtonHook.CBaseButtonButtonTouchHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseButtonHook.CBaseButtonButtonTouchHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseButton::ButtonTouch::Post.");
            }
        }
    }

    internal void InvokeCBaseButtonButtonUsePre(ref CBaseButtonButtonUsePreContext ctx)
    {
        if (!DatamapsHook.CBaseButtonHook.CBaseButtonButtonUseHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseButtonHook.CBaseButtonButtonUseHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseButton::ButtonUse::Pre.");
            }
        }
    }

    internal void InvokeCBaseButtonButtonUsePost(ref CBaseButtonButtonUsePostContext ctx)
    {
        if (!DatamapsHook.CBaseButtonHook.CBaseButtonButtonUseHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseButtonHook.CBaseButtonButtonUseHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseButton::ButtonUse::Post.");
            }
        }
    }

    internal void InvokeCBaseButtonTriggerAndWaitPre(ref CBaseButtonTriggerAndWaitPreContext ctx)
    {
        if (!DatamapsHook.CBaseButtonHook.CBaseButtonTriggerAndWaitHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseButtonHook.CBaseButtonTriggerAndWaitHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseButton::TriggerAndWait::Pre.");
            }
        }
    }

    internal void InvokeCBaseButtonTriggerAndWaitPost(ref CBaseButtonTriggerAndWaitPostContext ctx)
    {
        if (!DatamapsHook.CBaseButtonHook.CBaseButtonTriggerAndWaitHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseButtonHook.CBaseButtonTriggerAndWaitHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseButton::TriggerAndWait::Post.");
            }
        }
    }

    internal void InvokeCBaseCSGrenadeProjectileDangerSoundThinkPre(ref CBaseCSGrenadeProjectileDangerSoundThinkPreContext ctx)
    {
        if (!DatamapsHook.CBaseCSGrenadeProjectileHook.CBaseCSGrenadeProjectileDangerSoundThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseCSGrenadeProjectileHook.CBaseCSGrenadeProjectileDangerSoundThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseCSGrenadeProjectile::DangerSoundThink::Pre.");
            }
        }
    }

    internal void InvokeCBaseCSGrenadeProjectileDangerSoundThinkPost(ref CBaseCSGrenadeProjectileDangerSoundThinkPostContext ctx)
    {
        if (!DatamapsHook.CBaseCSGrenadeProjectileHook.CBaseCSGrenadeProjectileDangerSoundThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseCSGrenadeProjectileHook.CBaseCSGrenadeProjectileDangerSoundThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseCSGrenadeProjectile::DangerSoundThink::Post.");
            }
        }
    }

    internal void InvokeCBaseDoorCloseAreaPortalsThinkPre(ref CBaseDoorCloseAreaPortalsThinkPreContext ctx)
    {
        if (!DatamapsHook.CBaseDoorHook.CBaseDoorCloseAreaPortalsThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseDoorHook.CBaseDoorCloseAreaPortalsThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseDoor::CloseAreaPortalsThink::Pre.");
            }
        }
    }

    internal void InvokeCBaseDoorCloseAreaPortalsThinkPost(ref CBaseDoorCloseAreaPortalsThinkPostContext ctx)
    {
        if (!DatamapsHook.CBaseDoorHook.CBaseDoorCloseAreaPortalsThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseDoorHook.CBaseDoorCloseAreaPortalsThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseDoor::CloseAreaPortalsThink::Post.");
            }
        }
    }

    internal void InvokeCBaseDoorDoorGoDownPre(ref CBaseDoorDoorGoDownPreContext ctx)
    {
        if (!DatamapsHook.CBaseDoorHook.CBaseDoorDoorGoDownHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseDoorHook.CBaseDoorDoorGoDownHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseDoor::DoorGoDown::Pre.");
            }
        }
    }

    internal void InvokeCBaseDoorDoorGoDownPost(ref CBaseDoorDoorGoDownPostContext ctx)
    {
        if (!DatamapsHook.CBaseDoorHook.CBaseDoorDoorGoDownHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseDoorHook.CBaseDoorDoorGoDownHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseDoor::DoorGoDown::Post.");
            }
        }
    }

    internal void InvokeCBaseDoorDoorGoUpPre(ref CBaseDoorDoorGoUpPreContext ctx)
    {
        if (!DatamapsHook.CBaseDoorHook.CBaseDoorDoorGoUpHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseDoorHook.CBaseDoorDoorGoUpHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseDoor::DoorGoUp::Pre.");
            }
        }
    }

    internal void InvokeCBaseDoorDoorGoUpPost(ref CBaseDoorDoorGoUpPostContext ctx)
    {
        if (!DatamapsHook.CBaseDoorHook.CBaseDoorDoorGoUpHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseDoorHook.CBaseDoorDoorGoUpHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseDoor::DoorGoUp::Post.");
            }
        }
    }

    internal void InvokeCBaseDoorDoorHitBottomPre(ref CBaseDoorDoorHitBottomPreContext ctx)
    {
        if (!DatamapsHook.CBaseDoorHook.CBaseDoorDoorHitBottomHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseDoorHook.CBaseDoorDoorHitBottomHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseDoor::DoorHitBottom::Pre.");
            }
        }
    }

    internal void InvokeCBaseDoorDoorHitBottomPost(ref CBaseDoorDoorHitBottomPostContext ctx)
    {
        if (!DatamapsHook.CBaseDoorHook.CBaseDoorDoorHitBottomHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseDoorHook.CBaseDoorDoorHitBottomHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseDoor::DoorHitBottom::Post.");
            }
        }
    }

    internal void InvokeCBaseDoorDoorHitTopPre(ref CBaseDoorDoorHitTopPreContext ctx)
    {
        if (!DatamapsHook.CBaseDoorHook.CBaseDoorDoorHitTopHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseDoorHook.CBaseDoorDoorHitTopHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseDoor::DoorHitTop::Pre.");
            }
        }
    }

    internal void InvokeCBaseDoorDoorHitTopPost(ref CBaseDoorDoorHitTopPostContext ctx)
    {
        if (!DatamapsHook.CBaseDoorHook.CBaseDoorDoorHitTopHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseDoorHook.CBaseDoorDoorHitTopHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseDoor::DoorHitTop::Post.");
            }
        }
    }

    internal void InvokeCBaseDoorDoorTouchPre(ref CBaseDoorDoorTouchPreContext ctx)
    {
        if (!DatamapsHook.CBaseDoorHook.CBaseDoorDoorTouchHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseDoorHook.CBaseDoorDoorTouchHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseDoor::DoorTouch::Pre.");
            }
        }
    }

    internal void InvokeCBaseDoorDoorTouchPost(ref CBaseDoorDoorTouchPostContext ctx)
    {
        if (!DatamapsHook.CBaseDoorHook.CBaseDoorDoorTouchHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseDoorHook.CBaseDoorDoorTouchHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseDoor::DoorTouch::Post.");
            }
        }
    }

    internal void InvokeCBaseDoorMovingSoundThinkPre(ref CBaseDoorMovingSoundThinkPreContext ctx)
    {
        if (!DatamapsHook.CBaseDoorHook.CBaseDoorMovingSoundThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseDoorHook.CBaseDoorMovingSoundThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseDoor::MovingSoundThink::Pre.");
            }
        }
    }

    internal void InvokeCBaseDoorMovingSoundThinkPost(ref CBaseDoorMovingSoundThinkPostContext ctx)
    {
        if (!DatamapsHook.CBaseDoorHook.CBaseDoorMovingSoundThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseDoorHook.CBaseDoorMovingSoundThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseDoor::MovingSoundThink::Post.");
            }
        }
    }

    internal void InvokeCBaseEntityClearNavIgnoreContentsThinkPre(ref CBaseEntityClearNavIgnoreContentsThinkPreContext ctx)
    {
        if (!DatamapsHook.CBaseEntityHook.CBaseEntityClearNavIgnoreContentsThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseEntityHook.CBaseEntityClearNavIgnoreContentsThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseEntity::ClearNavIgnoreContentsThink::Pre.");
            }
        }
    }

    internal void InvokeCBaseEntityClearNavIgnoreContentsThinkPost(ref CBaseEntityClearNavIgnoreContentsThinkPostContext ctx)
    {
        if (!DatamapsHook.CBaseEntityHook.CBaseEntityClearNavIgnoreContentsThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseEntityHook.CBaseEntityClearNavIgnoreContentsThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseEntity::ClearNavIgnoreContentsThink::Post.");
            }
        }
    }

    internal void InvokeCBaseEntityFakeScriptThinkFuncPre(ref CBaseEntityFakeScriptThinkFuncPreContext ctx)
    {
        if (!DatamapsHook.CBaseEntityHook.CBaseEntityFakeScriptThinkFuncHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseEntityHook.CBaseEntityFakeScriptThinkFuncHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseEntity::FakeScriptThinkFunc::Pre.");
            }
        }
    }

    internal void InvokeCBaseEntityFakeScriptThinkFuncPost(ref CBaseEntityFakeScriptThinkFuncPostContext ctx)
    {
        if (!DatamapsHook.CBaseEntityHook.CBaseEntityFakeScriptThinkFuncHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseEntityHook.CBaseEntityFakeScriptThinkFuncHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseEntity::FakeScriptThinkFunc::Post.");
            }
        }
    }

    internal void InvokeCBaseEntitySUB_CallUseTogglePre(ref CBaseEntitySUB_CallUseTogglePreContext ctx)
    {
        if (!DatamapsHook.CBaseEntityHook.CBaseEntitySUB_CallUseToggleHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseEntityHook.CBaseEntitySUB_CallUseToggleHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseEntity::SUB_CallUseToggle::Pre.");
            }
        }
    }

    internal void InvokeCBaseEntitySUB_CallUseTogglePost(ref CBaseEntitySUB_CallUseTogglePostContext ctx)
    {
        if (!DatamapsHook.CBaseEntityHook.CBaseEntitySUB_CallUseToggleHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseEntityHook.CBaseEntitySUB_CallUseToggleHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseEntity::SUB_CallUseToggle::Post.");
            }
        }
    }

    internal void InvokeCBaseEntitySUB_DoNothingPre(ref CBaseEntitySUB_DoNothingPreContext ctx)
    {
        if (!DatamapsHook.CBaseEntityHook.CBaseEntitySUB_DoNothingHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseEntityHook.CBaseEntitySUB_DoNothingHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseEntity::SUB_DoNothing::Pre.");
            }
        }
    }

    internal void InvokeCBaseEntitySUB_DoNothingPost(ref CBaseEntitySUB_DoNothingPostContext ctx)
    {
        if (!DatamapsHook.CBaseEntityHook.CBaseEntitySUB_DoNothingHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseEntityHook.CBaseEntitySUB_DoNothingHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseEntity::SUB_DoNothing::Post.");
            }
        }
    }

    internal void InvokeCBaseEntitySUB_KillSelfPre(ref CBaseEntitySUB_KillSelfPreContext ctx)
    {
        if (!DatamapsHook.CBaseEntityHook.CBaseEntitySUB_KillSelfHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseEntityHook.CBaseEntitySUB_KillSelfHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseEntity::SUB_KillSelf::Pre.");
            }
        }
    }

    internal void InvokeCBaseEntitySUB_KillSelfPost(ref CBaseEntitySUB_KillSelfPostContext ctx)
    {
        if (!DatamapsHook.CBaseEntityHook.CBaseEntitySUB_KillSelfHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseEntityHook.CBaseEntitySUB_KillSelfHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseEntity::SUB_KillSelf::Post.");
            }
        }
    }

    internal void InvokeCBaseEntitySUB_RemovePre(ref CBaseEntitySUB_RemovePreContext ctx)
    {
        if (!DatamapsHook.CBaseEntityHook.CBaseEntitySUB_RemoveHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseEntityHook.CBaseEntitySUB_RemoveHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseEntity::SUB_Remove::Pre.");
            }
        }
    }

    internal void InvokeCBaseEntitySUB_RemovePost(ref CBaseEntitySUB_RemovePostContext ctx)
    {
        if (!DatamapsHook.CBaseEntityHook.CBaseEntitySUB_RemoveHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseEntityHook.CBaseEntitySUB_RemoveHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseEntity::SUB_Remove::Post.");
            }
        }
    }

    internal void InvokeCBaseGrenadeBounceTouchPre(ref CBaseGrenadeBounceTouchPreContext ctx)
    {
        if (!DatamapsHook.CBaseGrenadeHook.CBaseGrenadeBounceTouchHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseGrenadeHook.CBaseGrenadeBounceTouchHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseGrenade::BounceTouch::Pre.");
            }
        }
    }

    internal void InvokeCBaseGrenadeBounceTouchPost(ref CBaseGrenadeBounceTouchPostContext ctx)
    {
        if (!DatamapsHook.CBaseGrenadeHook.CBaseGrenadeBounceTouchHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseGrenadeHook.CBaseGrenadeBounceTouchHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseGrenade::BounceTouch::Post.");
            }
        }
    }

    internal void InvokeCBaseGrenadeDangerSoundThinkPre(ref CBaseGrenadeDangerSoundThinkPreContext ctx)
    {
        if (!DatamapsHook.CBaseGrenadeHook.CBaseGrenadeDangerSoundThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseGrenadeHook.CBaseGrenadeDangerSoundThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseGrenade::DangerSoundThink::Pre.");
            }
        }
    }

    internal void InvokeCBaseGrenadeDangerSoundThinkPost(ref CBaseGrenadeDangerSoundThinkPostContext ctx)
    {
        if (!DatamapsHook.CBaseGrenadeHook.CBaseGrenadeDangerSoundThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseGrenadeHook.CBaseGrenadeDangerSoundThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseGrenade::DangerSoundThink::Post.");
            }
        }
    }

    internal void InvokeCBaseGrenadeDetonatePre(ref CBaseGrenadeDetonatePreContext ctx)
    {
        if (!DatamapsHook.CBaseGrenadeHook.CBaseGrenadeDetonateHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseGrenadeHook.CBaseGrenadeDetonateHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseGrenade::Detonate::Pre.");
            }
        }
    }

    internal void InvokeCBaseGrenadeDetonatePost(ref CBaseGrenadeDetonatePostContext ctx)
    {
        if (!DatamapsHook.CBaseGrenadeHook.CBaseGrenadeDetonateHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseGrenadeHook.CBaseGrenadeDetonateHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseGrenade::Detonate::Post.");
            }
        }
    }

    internal void InvokeCBaseGrenadeDetonateUsePre(ref CBaseGrenadeDetonateUsePreContext ctx)
    {
        if (!DatamapsHook.CBaseGrenadeHook.CBaseGrenadeDetonateUseHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseGrenadeHook.CBaseGrenadeDetonateUseHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseGrenade::DetonateUse::Pre.");
            }
        }
    }

    internal void InvokeCBaseGrenadeDetonateUsePost(ref CBaseGrenadeDetonateUsePostContext ctx)
    {
        if (!DatamapsHook.CBaseGrenadeHook.CBaseGrenadeDetonateUseHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseGrenadeHook.CBaseGrenadeDetonateUseHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseGrenade::DetonateUse::Post.");
            }
        }
    }

    internal void InvokeCBaseGrenadeExplodeTouchPre(ref CBaseGrenadeExplodeTouchPreContext ctx)
    {
        if (!DatamapsHook.CBaseGrenadeHook.CBaseGrenadeExplodeTouchHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseGrenadeHook.CBaseGrenadeExplodeTouchHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseGrenade::ExplodeTouch::Pre.");
            }
        }
    }

    internal void InvokeCBaseGrenadeExplodeTouchPost(ref CBaseGrenadeExplodeTouchPostContext ctx)
    {
        if (!DatamapsHook.CBaseGrenadeHook.CBaseGrenadeExplodeTouchHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseGrenadeHook.CBaseGrenadeExplodeTouchHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseGrenade::ExplodeTouch::Post.");
            }
        }
    }

    internal void InvokeCBaseGrenadePreDetonatePre(ref CBaseGrenadePreDetonatePreContext ctx)
    {
        if (!DatamapsHook.CBaseGrenadeHook.CBaseGrenadePreDetonateHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseGrenadeHook.CBaseGrenadePreDetonateHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseGrenade::PreDetonate::Pre.");
            }
        }
    }

    internal void InvokeCBaseGrenadePreDetonatePost(ref CBaseGrenadePreDetonatePostContext ctx)
    {
        if (!DatamapsHook.CBaseGrenadeHook.CBaseGrenadePreDetonateHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseGrenadeHook.CBaseGrenadePreDetonateHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseGrenade::PreDetonate::Post.");
            }
        }
    }

    internal void InvokeCBaseGrenadeSlideTouchPre(ref CBaseGrenadeSlideTouchPreContext ctx)
    {
        if (!DatamapsHook.CBaseGrenadeHook.CBaseGrenadeSlideTouchHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseGrenadeHook.CBaseGrenadeSlideTouchHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseGrenade::SlideTouch::Pre.");
            }
        }
    }

    internal void InvokeCBaseGrenadeSlideTouchPost(ref CBaseGrenadeSlideTouchPostContext ctx)
    {
        if (!DatamapsHook.CBaseGrenadeHook.CBaseGrenadeSlideTouchHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseGrenadeHook.CBaseGrenadeSlideTouchHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseGrenade::SlideTouch::Post.");
            }
        }
    }

    internal void InvokeCBaseGrenadeSmokePre(ref CBaseGrenadeSmokePreContext ctx)
    {
        if (!DatamapsHook.CBaseGrenadeHook.CBaseGrenadeSmokeHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseGrenadeHook.CBaseGrenadeSmokeHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseGrenade::Smoke::Pre.");
            }
        }
    }

    internal void InvokeCBaseGrenadeSmokePost(ref CBaseGrenadeSmokePostContext ctx)
    {
        if (!DatamapsHook.CBaseGrenadeHook.CBaseGrenadeSmokeHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseGrenadeHook.CBaseGrenadeSmokeHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseGrenade::Smoke::Post.");
            }
        }
    }

    internal void InvokeCBaseGrenadeTumbleThinkPre(ref CBaseGrenadeTumbleThinkPreContext ctx)
    {
        if (!DatamapsHook.CBaseGrenadeHook.CBaseGrenadeTumbleThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseGrenadeHook.CBaseGrenadeTumbleThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseGrenade::TumbleThink::Pre.");
            }
        }
    }

    internal void InvokeCBaseGrenadeTumbleThinkPost(ref CBaseGrenadeTumbleThinkPostContext ctx)
    {
        if (!DatamapsHook.CBaseGrenadeHook.CBaseGrenadeTumbleThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseGrenadeHook.CBaseGrenadeTumbleThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseGrenade::TumbleThink::Post.");
            }
        }
    }

    internal void InvokeCBaseModelEntityProcessSceneEventsThinkPre(ref CBaseModelEntityProcessSceneEventsThinkPreContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntityProcessSceneEventsThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntityProcessSceneEventsThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::ProcessSceneEventsThink::Pre.");
            }
        }
    }

    internal void InvokeCBaseModelEntityProcessSceneEventsThinkPost(ref CBaseModelEntityProcessSceneEventsThinkPostContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntityProcessSceneEventsThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntityProcessSceneEventsThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::ProcessSceneEventsThink::Post.");
            }
        }
    }

    internal void InvokeCBaseModelEntitySUB_DissolveIfUncarriedPre(ref CBaseModelEntitySUB_DissolveIfUncarriedPreContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_DissolveIfUncarriedHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_DissolveIfUncarriedHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::SUB_DissolveIfUncarried::Pre.");
            }
        }
    }

    internal void InvokeCBaseModelEntitySUB_DissolveIfUncarriedPost(ref CBaseModelEntitySUB_DissolveIfUncarriedPostContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_DissolveIfUncarriedHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_DissolveIfUncarriedHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::SUB_DissolveIfUncarried::Post.");
            }
        }
    }

    internal void InvokeCBaseModelEntitySUB_FadeOutPre(ref CBaseModelEntitySUB_FadeOutPreContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_FadeOutHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_FadeOutHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::SUB_FadeOut::Pre.");
            }
        }
    }

    internal void InvokeCBaseModelEntitySUB_FadeOutPost(ref CBaseModelEntitySUB_FadeOutPostContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_FadeOutHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_FadeOutHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::SUB_FadeOut::Post.");
            }
        }
    }

    internal void InvokeCBaseModelEntitySUB_PerformShadowFadeInPre(ref CBaseModelEntitySUB_PerformShadowFadeInPreContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_PerformShadowFadeInHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_PerformShadowFadeInHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::SUB_PerformShadowFadeIn::Pre.");
            }
        }
    }

    internal void InvokeCBaseModelEntitySUB_PerformShadowFadeInPost(ref CBaseModelEntitySUB_PerformShadowFadeInPostContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_PerformShadowFadeInHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_PerformShadowFadeInHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::SUB_PerformShadowFadeIn::Post.");
            }
        }
    }

    internal void InvokeCBaseModelEntitySUB_PerformShadowFadeOutPre(ref CBaseModelEntitySUB_PerformShadowFadeOutPreContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_PerformShadowFadeOutHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_PerformShadowFadeOutHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::SUB_PerformShadowFadeOut::Pre.");
            }
        }
    }

    internal void InvokeCBaseModelEntitySUB_PerformShadowFadeOutPost(ref CBaseModelEntitySUB_PerformShadowFadeOutPostContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_PerformShadowFadeOutHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_PerformShadowFadeOutHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::SUB_PerformShadowFadeOut::Post.");
            }
        }
    }

    internal void InvokeCBaseModelEntitySUB_StartFadeOutPre(ref CBaseModelEntitySUB_StartFadeOutPreContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StartFadeOutHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StartFadeOutHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::SUB_StartFadeOut::Pre.");
            }
        }
    }

    internal void InvokeCBaseModelEntitySUB_StartFadeOutPost(ref CBaseModelEntitySUB_StartFadeOutPostContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StartFadeOutHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StartFadeOutHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::SUB_StartFadeOut::Post.");
            }
        }
    }

    internal void InvokeCBaseModelEntitySUB_StartFadeOutInstantPre(ref CBaseModelEntitySUB_StartFadeOutInstantPreContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StartFadeOutInstantHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StartFadeOutInstantHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::SUB_StartFadeOutInstant::Pre.");
            }
        }
    }

    internal void InvokeCBaseModelEntitySUB_StartFadeOutInstantPost(ref CBaseModelEntitySUB_StartFadeOutInstantPostContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StartFadeOutInstantHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StartFadeOutInstantHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::SUB_StartFadeOutInstant::Post.");
            }
        }
    }

    internal void InvokeCBaseModelEntitySUB_StartShadowFadeInPre(ref CBaseModelEntitySUB_StartShadowFadeInPreContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StartShadowFadeInHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StartShadowFadeInHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::SUB_StartShadowFadeIn::Pre.");
            }
        }
    }

    internal void InvokeCBaseModelEntitySUB_StartShadowFadeInPost(ref CBaseModelEntitySUB_StartShadowFadeInPostContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StartShadowFadeInHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StartShadowFadeInHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::SUB_StartShadowFadeIn::Post.");
            }
        }
    }

    internal void InvokeCBaseModelEntitySUB_StartShadowFadeOutPre(ref CBaseModelEntitySUB_StartShadowFadeOutPreContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StartShadowFadeOutHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StartShadowFadeOutHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::SUB_StartShadowFadeOut::Pre.");
            }
        }
    }

    internal void InvokeCBaseModelEntitySUB_StartShadowFadeOutPost(ref CBaseModelEntitySUB_StartShadowFadeOutPostContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StartShadowFadeOutHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StartShadowFadeOutHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::SUB_StartShadowFadeOut::Post.");
            }
        }
    }

    internal void InvokeCBaseModelEntitySUB_StopShadowFadePre(ref CBaseModelEntitySUB_StopShadowFadePreContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StopShadowFadeHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StopShadowFadeHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::SUB_StopShadowFade::Pre.");
            }
        }
    }

    internal void InvokeCBaseModelEntitySUB_StopShadowFadePost(ref CBaseModelEntitySUB_StopShadowFadePostContext ctx)
    {
        if (!DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StopShadowFadeHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBaseModelEntityHook.CBaseModelEntitySUB_StopShadowFadeHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBaseModelEntity::SUB_StopShadowFade::Post.");
            }
        }
    }

    internal void InvokeCBasePropDoorDisableAreaPortalThinkPre(ref CBasePropDoorDisableAreaPortalThinkPreContext ctx)
    {
        if (!DatamapsHook.CBasePropDoorHook.CBasePropDoorDisableAreaPortalThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBasePropDoorHook.CBasePropDoorDisableAreaPortalThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBasePropDoor::DisableAreaPortalThink::Pre.");
            }
        }
    }

    internal void InvokeCBasePropDoorDisableAreaPortalThinkPost(ref CBasePropDoorDisableAreaPortalThinkPostContext ctx)
    {
        if (!DatamapsHook.CBasePropDoorHook.CBasePropDoorDisableAreaPortalThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBasePropDoorHook.CBasePropDoorDisableAreaPortalThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBasePropDoor::DisableAreaPortalThink::Post.");
            }
        }
    }

    internal void InvokeCBasePropDoorDoorAutoCloseThinkPre(ref CBasePropDoorDoorAutoCloseThinkPreContext ctx)
    {
        if (!DatamapsHook.CBasePropDoorHook.CBasePropDoorDoorAutoCloseThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBasePropDoorHook.CBasePropDoorDoorAutoCloseThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBasePropDoor::DoorAutoCloseThink::Pre.");
            }
        }
    }

    internal void InvokeCBasePropDoorDoorAutoCloseThinkPost(ref CBasePropDoorDoorAutoCloseThinkPostContext ctx)
    {
        if (!DatamapsHook.CBasePropDoorHook.CBasePropDoorDoorAutoCloseThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBasePropDoorHook.CBasePropDoorDoorAutoCloseThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBasePropDoor::DoorAutoCloseThink::Post.");
            }
        }
    }

    internal void InvokeCBasePropDoorDoorCloseMoveDonePre(ref CBasePropDoorDoorCloseMoveDonePreContext ctx)
    {
        if (!DatamapsHook.CBasePropDoorHook.CBasePropDoorDoorCloseMoveDoneHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBasePropDoorHook.CBasePropDoorDoorCloseMoveDoneHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBasePropDoor::DoorCloseMoveDone::Pre.");
            }
        }
    }

    internal void InvokeCBasePropDoorDoorCloseMoveDonePost(ref CBasePropDoorDoorCloseMoveDonePostContext ctx)
    {
        if (!DatamapsHook.CBasePropDoorHook.CBasePropDoorDoorCloseMoveDoneHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBasePropDoorHook.CBasePropDoorDoorCloseMoveDoneHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBasePropDoor::DoorCloseMoveDone::Post.");
            }
        }
    }

    internal void InvokeCBasePropDoorDoorOpenMoveDonePre(ref CBasePropDoorDoorOpenMoveDonePreContext ctx)
    {
        if (!DatamapsHook.CBasePropDoorHook.CBasePropDoorDoorOpenMoveDoneHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBasePropDoorHook.CBasePropDoorDoorOpenMoveDoneHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBasePropDoor::DoorOpenMoveDone::Pre.");
            }
        }
    }

    internal void InvokeCBasePropDoorDoorOpenMoveDonePost(ref CBasePropDoorDoorOpenMoveDonePostContext ctx)
    {
        if (!DatamapsHook.CBasePropDoorHook.CBasePropDoorDoorOpenMoveDoneHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBasePropDoorHook.CBasePropDoorDoorOpenMoveDoneHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBasePropDoor::DoorOpenMoveDone::Post.");
            }
        }
    }

    internal void InvokeCBombTargetBombTargetTouchPre(ref CBombTargetBombTargetTouchPreContext ctx)
    {
        if (!DatamapsHook.CBombTargetHook.CBombTargetBombTargetTouchHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBombTargetHook.CBombTargetBombTargetTouchHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBombTarget::BombTargetTouch::Pre.");
            }
        }
    }

    internal void InvokeCBombTargetBombTargetTouchPost(ref CBombTargetBombTargetTouchPostContext ctx)
    {
        if (!DatamapsHook.CBombTargetHook.CBombTargetBombTargetTouchHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBombTargetHook.CBombTargetBombTargetTouchHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBombTarget::BombTargetTouch::Post.");
            }
        }
    }

    internal void InvokeCBombTargetBombTargetUsePre(ref CBombTargetBombTargetUsePreContext ctx)
    {
        if (!DatamapsHook.CBombTargetHook.CBombTargetBombTargetUseHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBombTargetHook.CBombTargetBombTargetUseHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBombTarget::BombTargetUse::Pre.");
            }
        }
    }

    internal void InvokeCBombTargetBombTargetUsePost(ref CBombTargetBombTargetUsePostContext ctx)
    {
        if (!DatamapsHook.CBombTargetHook.CBombTargetBombTargetUseHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBombTargetHook.CBombTargetBombTargetUseHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBombTarget::BombTargetUse::Post.");
            }
        }
    }

    internal void InvokeCBreakableDiePre(ref CBreakableDiePreContext ctx)
    {
        if (!DatamapsHook.CBreakableHook.CBreakableDieHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBreakableHook.CBreakableDieHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBreakable::Die::Pre.");
            }
        }
    }

    internal void InvokeCBreakableDiePost(ref CBreakableDiePostContext ctx)
    {
        if (!DatamapsHook.CBreakableHook.CBreakableDieHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBreakableHook.CBreakableDieHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBreakable::Die::Post.");
            }
        }
    }

    internal void InvokeCBreakablePropBreakThinkPre(ref CBreakablePropBreakThinkPreContext ctx)
    {
        if (!DatamapsHook.CBreakablePropHook.CBreakablePropBreakThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBreakablePropHook.CBreakablePropBreakThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBreakableProp::BreakThink::Pre.");
            }
        }
    }

    internal void InvokeCBreakablePropBreakThinkPost(ref CBreakablePropBreakThinkPostContext ctx)
    {
        if (!DatamapsHook.CBreakablePropHook.CBreakablePropBreakThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBreakablePropHook.CBreakablePropBreakThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBreakableProp::BreakThink::Post.");
            }
        }
    }

    internal void InvokeCBreakablePropRampToDefaultFadeScalePre(ref CBreakablePropRampToDefaultFadeScalePreContext ctx)
    {
        if (!DatamapsHook.CBreakablePropHook.CBreakablePropRampToDefaultFadeScaleHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBreakablePropHook.CBreakablePropRampToDefaultFadeScaleHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBreakableProp::RampToDefaultFadeScale::Pre.");
            }
        }
    }

    internal void InvokeCBreakablePropRampToDefaultFadeScalePost(ref CBreakablePropRampToDefaultFadeScalePostContext ctx)
    {
        if (!DatamapsHook.CBreakablePropHook.CBreakablePropRampToDefaultFadeScaleHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CBreakablePropHook.CBreakablePropRampToDefaultFadeScaleHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CBreakableProp::RampToDefaultFadeScale::Post.");
            }
        }
    }

    internal void InvokeCCSPlayerControllerInventoryUpdateThinkPre(ref CCSPlayerControllerInventoryUpdateThinkPreContext ctx)
    {
        if (!DatamapsHook.CCSPlayerControllerHook.CCSPlayerControllerInventoryUpdateThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CCSPlayerControllerHook.CCSPlayerControllerInventoryUpdateThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CCSPlayerController::InventoryUpdateThink::Pre.");
            }
        }
    }

    internal void InvokeCCSPlayerControllerInventoryUpdateThinkPost(ref CCSPlayerControllerInventoryUpdateThinkPostContext ctx)
    {
        if (!DatamapsHook.CCSPlayerControllerHook.CCSPlayerControllerInventoryUpdateThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CCSPlayerControllerHook.CCSPlayerControllerInventoryUpdateThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CCSPlayerController::InventoryUpdateThink::Post.");
            }
        }
    }

    internal void InvokeCCSPlayerControllerPlayerForceTeamThinkPre(ref CCSPlayerControllerPlayerForceTeamThinkPreContext ctx)
    {
        if (!DatamapsHook.CCSPlayerControllerHook.CCSPlayerControllerPlayerForceTeamThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CCSPlayerControllerHook.CCSPlayerControllerPlayerForceTeamThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CCSPlayerController::PlayerForceTeamThink::Pre.");
            }
        }
    }

    internal void InvokeCCSPlayerControllerPlayerForceTeamThinkPost(ref CCSPlayerControllerPlayerForceTeamThinkPostContext ctx)
    {
        if (!DatamapsHook.CCSPlayerControllerHook.CCSPlayerControllerPlayerForceTeamThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CCSPlayerControllerHook.CCSPlayerControllerPlayerForceTeamThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CCSPlayerController::PlayerForceTeamThink::Post.");
            }
        }
    }

    internal void InvokeCCSPlayerControllerResetForceTeamThinkPre(ref CCSPlayerControllerResetForceTeamThinkPreContext ctx)
    {
        if (!DatamapsHook.CCSPlayerControllerHook.CCSPlayerControllerResetForceTeamThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CCSPlayerControllerHook.CCSPlayerControllerResetForceTeamThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CCSPlayerController::ResetForceTeamThink::Pre.");
            }
        }
    }

    internal void InvokeCCSPlayerControllerResetForceTeamThinkPost(ref CCSPlayerControllerResetForceTeamThinkPostContext ctx)
    {
        if (!DatamapsHook.CCSPlayerControllerHook.CCSPlayerControllerResetForceTeamThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CCSPlayerControllerHook.CCSPlayerControllerResetForceTeamThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CCSPlayerController::ResetForceTeamThink::Post.");
            }
        }
    }

    internal void InvokeCCSPlayerControllerResourceDataThinkPre(ref CCSPlayerControllerResourceDataThinkPreContext ctx)
    {
        if (!DatamapsHook.CCSPlayerControllerHook.CCSPlayerControllerResourceDataThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CCSPlayerControllerHook.CCSPlayerControllerResourceDataThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CCSPlayerController::ResourceDataThink::Pre.");
            }
        }
    }

    internal void InvokeCCSPlayerControllerResourceDataThinkPost(ref CCSPlayerControllerResourceDataThinkPostContext ctx)
    {
        if (!DatamapsHook.CCSPlayerControllerHook.CCSPlayerControllerResourceDataThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CCSPlayerControllerHook.CCSPlayerControllerResourceDataThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CCSPlayerController::ResourceDataThink::Post.");
            }
        }
    }

    internal void InvokeCCSPlayerPawnCheckStuffThinkPre(ref CCSPlayerPawnCheckStuffThinkPreContext ctx)
    {
        if (!DatamapsHook.CCSPlayerPawnHook.CCSPlayerPawnCheckStuffThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CCSPlayerPawnHook.CCSPlayerPawnCheckStuffThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CCSPlayerPawn::CheckStuffThink::Pre.");
            }
        }
    }

    internal void InvokeCCSPlayerPawnCheckStuffThinkPost(ref CCSPlayerPawnCheckStuffThinkPostContext ctx)
    {
        if (!DatamapsHook.CCSPlayerPawnHook.CCSPlayerPawnCheckStuffThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CCSPlayerPawnHook.CCSPlayerPawnCheckStuffThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CCSPlayerPawn::CheckStuffThink::Post.");
            }
        }
    }

    internal void InvokeCCSPlayerPawnPushawayThinkPre(ref CCSPlayerPawnPushawayThinkPreContext ctx)
    {
        if (!DatamapsHook.CCSPlayerPawnHook.CCSPlayerPawnPushawayThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CCSPlayerPawnHook.CCSPlayerPawnPushawayThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CCSPlayerPawn::PushawayThink::Pre.");
            }
        }
    }

    internal void InvokeCCSPlayerPawnPushawayThinkPost(ref CCSPlayerPawnPushawayThinkPostContext ctx)
    {
        if (!DatamapsHook.CCSPlayerPawnHook.CCSPlayerPawnPushawayThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CCSPlayerPawnHook.CCSPlayerPawnPushawayThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CCSPlayerPawn::PushawayThink::Post.");
            }
        }
    }

    internal void InvokeCCSPlayerResourceResourceThinkPre(ref CCSPlayerResourceResourceThinkPreContext ctx)
    {
        if (!DatamapsHook.CCSPlayerResourceHook.CCSPlayerResourceResourceThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CCSPlayerResourceHook.CCSPlayerResourceResourceThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CCSPlayerResource::ResourceThink::Pre.");
            }
        }
    }

    internal void InvokeCCSPlayerResourceResourceThinkPost(ref CCSPlayerResourceResourceThinkPostContext ctx)
    {
        if (!DatamapsHook.CCSPlayerResourceHook.CCSPlayerResourceResourceThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CCSPlayerResourceHook.CCSPlayerResourceResourceThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CCSPlayerResource::ResourceThink::Post.");
            }
        }
    }

    internal void InvokeCCSWeaponBaseDefaultTouchPre(ref CCSWeaponBaseDefaultTouchPreContext ctx)
    {
        if (!DatamapsHook.CCSWeaponBaseHook.CCSWeaponBaseDefaultTouchHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CCSWeaponBaseHook.CCSWeaponBaseDefaultTouchHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CCSWeaponBase::DefaultTouch::Pre.");
            }
        }
    }

    internal void InvokeCCSWeaponBaseDefaultTouchPost(ref CCSWeaponBaseDefaultTouchPostContext ctx)
    {
        if (!DatamapsHook.CCSWeaponBaseHook.CCSWeaponBaseDefaultTouchHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CCSWeaponBaseHook.CCSWeaponBaseDefaultTouchHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CCSWeaponBase::DefaultTouch::Post.");
            }
        }
    }

    internal void InvokeCCSWeaponBaseRemoveUnownedWeaponThinkPre(ref CCSWeaponBaseRemoveUnownedWeaponThinkPreContext ctx)
    {
        if (!DatamapsHook.CCSWeaponBaseHook.CCSWeaponBaseRemoveUnownedWeaponThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CCSWeaponBaseHook.CCSWeaponBaseRemoveUnownedWeaponThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CCSWeaponBase::RemoveUnownedWeaponThink::Pre.");
            }
        }
    }

    internal void InvokeCCSWeaponBaseRemoveUnownedWeaponThinkPost(ref CCSWeaponBaseRemoveUnownedWeaponThinkPostContext ctx)
    {
        if (!DatamapsHook.CCSWeaponBaseHook.CCSWeaponBaseRemoveUnownedWeaponThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CCSWeaponBaseHook.CCSWeaponBaseRemoveUnownedWeaponThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CCSWeaponBase::RemoveUnownedWeaponThink::Post.");
            }
        }
    }

    internal void InvokeCChickenChickenThinkPre(ref CChickenChickenThinkPreContext ctx)
    {
        if (!DatamapsHook.CChickenHook.CChickenChickenThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CChickenHook.CChickenChickenThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CChicken::ChickenThink::Pre.");
            }
        }
    }

    internal void InvokeCChickenChickenThinkPost(ref CChickenChickenThinkPostContext ctx)
    {
        if (!DatamapsHook.CChickenHook.CChickenChickenThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CChickenHook.CChickenChickenThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CChicken::ChickenThink::Post.");
            }
        }
    }

    internal void InvokeCChickenChickenTouchPre(ref CChickenChickenTouchPreContext ctx)
    {
        if (!DatamapsHook.CChickenHook.CChickenChickenTouchHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CChickenHook.CChickenChickenTouchHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CChicken::ChickenTouch::Pre.");
            }
        }
    }

    internal void InvokeCChickenChickenTouchPost(ref CChickenChickenTouchPostContext ctx)
    {
        if (!DatamapsHook.CChickenHook.CChickenChickenTouchHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CChickenHook.CChickenChickenTouchHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CChicken::ChickenTouch::Post.");
            }
        }
    }

    internal void InvokeCChickenChickenUsePre(ref CChickenChickenUsePreContext ctx)
    {
        if (!DatamapsHook.CChickenHook.CChickenChickenUseHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CChickenHook.CChickenChickenUseHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CChicken::ChickenUse::Pre.");
            }
        }
    }

    internal void InvokeCChickenChickenUsePost(ref CChickenChickenUsePostContext ctx)
    {
        if (!DatamapsHook.CChickenHook.CChickenChickenUseHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CChickenHook.CChickenChickenUseHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CChicken::ChickenUse::Post.");
            }
        }
    }

    internal void InvokeCColorCorrectionFadeInThinkPre(ref CColorCorrectionFadeInThinkPreContext ctx)
    {
        if (!DatamapsHook.CColorCorrectionHook.CColorCorrectionFadeInThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CColorCorrectionHook.CColorCorrectionFadeInThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CColorCorrection::FadeInThink::Pre.");
            }
        }
    }

    internal void InvokeCColorCorrectionFadeInThinkPost(ref CColorCorrectionFadeInThinkPostContext ctx)
    {
        if (!DatamapsHook.CColorCorrectionHook.CColorCorrectionFadeInThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CColorCorrectionHook.CColorCorrectionFadeInThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CColorCorrection::FadeInThink::Post.");
            }
        }
    }

    internal void InvokeCColorCorrectionFadeOutThinkPre(ref CColorCorrectionFadeOutThinkPreContext ctx)
    {
        if (!DatamapsHook.CColorCorrectionHook.CColorCorrectionFadeOutThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CColorCorrectionHook.CColorCorrectionFadeOutThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CColorCorrection::FadeOutThink::Pre.");
            }
        }
    }

    internal void InvokeCColorCorrectionFadeOutThinkPost(ref CColorCorrectionFadeOutThinkPostContext ctx)
    {
        if (!DatamapsHook.CColorCorrectionHook.CColorCorrectionFadeOutThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CColorCorrectionHook.CColorCorrectionFadeOutThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CColorCorrection::FadeOutThink::Post.");
            }
        }
    }

    internal void InvokeCColorCorrectionVolumeThinkFuncPre(ref CColorCorrectionVolumeThinkFuncPreContext ctx)
    {
        if (!DatamapsHook.CColorCorrectionVolumeHook.CColorCorrectionVolumeThinkFuncHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CColorCorrectionVolumeHook.CColorCorrectionVolumeThinkFuncHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CColorCorrectionVolume::ThinkFunc::Pre.");
            }
        }
    }

    internal void InvokeCColorCorrectionVolumeThinkFuncPost(ref CColorCorrectionVolumeThinkFuncPostContext ctx)
    {
        if (!DatamapsHook.CColorCorrectionVolumeHook.CColorCorrectionVolumeThinkFuncHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CColorCorrectionVolumeHook.CColorCorrectionVolumeThinkFuncHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CColorCorrectionVolume::ThinkFunc::Post.");
            }
        }
    }

    internal void InvokeCDecoyProjectileGunfireThinkPre(ref CDecoyProjectileGunfireThinkPreContext ctx)
    {
        if (!DatamapsHook.CDecoyProjectileHook.CDecoyProjectileGunfireThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CDecoyProjectileHook.CDecoyProjectileGunfireThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CDecoyProjectile::GunfireThink::Pre.");
            }
        }
    }

    internal void InvokeCDecoyProjectileGunfireThinkPost(ref CDecoyProjectileGunfireThinkPostContext ctx)
    {
        if (!DatamapsHook.CDecoyProjectileHook.CDecoyProjectileGunfireThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CDecoyProjectileHook.CDecoyProjectileGunfireThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CDecoyProjectile::GunfireThink::Post.");
            }
        }
    }

    internal void InvokeCDecoyProjectileThink_DetonatePre(ref CDecoyProjectileThink_DetonatePreContext ctx)
    {
        if (!DatamapsHook.CDecoyProjectileHook.CDecoyProjectileThink_DetonateHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CDecoyProjectileHook.CDecoyProjectileThink_DetonateHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CDecoyProjectile::Think_Detonate::Pre.");
            }
        }
    }

    internal void InvokeCDecoyProjectileThink_DetonatePost(ref CDecoyProjectileThink_DetonatePostContext ctx)
    {
        if (!DatamapsHook.CDecoyProjectileHook.CDecoyProjectileThink_DetonateHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CDecoyProjectileHook.CDecoyProjectileThink_DetonateHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CDecoyProjectile::Think_Detonate::Post.");
            }
        }
    }

    internal void InvokeCDynamicLightDynamicLightThinkPre(ref CDynamicLightDynamicLightThinkPreContext ctx)
    {
        if (!DatamapsHook.CDynamicLightHook.CDynamicLightDynamicLightThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CDynamicLightHook.CDynamicLightDynamicLightThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CDynamicLight::DynamicLightThink::Pre.");
            }
        }
    }

    internal void InvokeCDynamicLightDynamicLightThinkPost(ref CDynamicLightDynamicLightThinkPostContext ctx)
    {
        if (!DatamapsHook.CDynamicLightHook.CDynamicLightDynamicLightThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CDynamicLightHook.CDynamicLightDynamicLightThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CDynamicLight::DynamicLightThink::Post.");
            }
        }
    }

    internal void InvokeCDynamicPropAnimThinkPre(ref CDynamicPropAnimThinkPreContext ctx)
    {
        if (!DatamapsHook.CDynamicPropHook.CDynamicPropAnimThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CDynamicPropHook.CDynamicPropAnimThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CDynamicProp::AnimThink::Pre.");
            }
        }
    }

    internal void InvokeCDynamicPropAnimThinkPost(ref CDynamicPropAnimThinkPostContext ctx)
    {
        if (!DatamapsHook.CDynamicPropHook.CDynamicPropAnimThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CDynamicPropHook.CDynamicPropAnimThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CDynamicProp::AnimThink::Post.");
            }
        }
    }

    internal void InvokeCEntityDissolveDissolveThinkPre(ref CEntityDissolveDissolveThinkPreContext ctx)
    {
        if (!DatamapsHook.CEntityDissolveHook.CEntityDissolveDissolveThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CEntityDissolveHook.CEntityDissolveDissolveThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CEntityDissolve::DissolveThink::Pre.");
            }
        }
    }

    internal void InvokeCEntityDissolveDissolveThinkPost(ref CEntityDissolveDissolveThinkPostContext ctx)
    {
        if (!DatamapsHook.CEntityDissolveHook.CEntityDissolveDissolveThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CEntityDissolveHook.CEntityDissolveDissolveThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CEntityDissolve::DissolveThink::Post.");
            }
        }
    }

    internal void InvokeCEntityDissolveElectrocuteThinkPre(ref CEntityDissolveElectrocuteThinkPreContext ctx)
    {
        if (!DatamapsHook.CEntityDissolveHook.CEntityDissolveElectrocuteThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CEntityDissolveHook.CEntityDissolveElectrocuteThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CEntityDissolve::ElectrocuteThink::Pre.");
            }
        }
    }

    internal void InvokeCEntityDissolveElectrocuteThinkPost(ref CEntityDissolveElectrocuteThinkPostContext ctx)
    {
        if (!DatamapsHook.CEntityDissolveHook.CEntityDissolveElectrocuteThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CEntityDissolveHook.CEntityDissolveElectrocuteThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CEntityDissolve::ElectrocuteThink::Post.");
            }
        }
    }

    internal void InvokeCEnvBeamStrikeThinkPre(ref CEnvBeamStrikeThinkPreContext ctx)
    {
        if (!DatamapsHook.CEnvBeamHook.CEnvBeamStrikeThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CEnvBeamHook.CEnvBeamStrikeThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CEnvBeam::StrikeThink::Pre.");
            }
        }
    }

    internal void InvokeCEnvBeamStrikeThinkPost(ref CEnvBeamStrikeThinkPostContext ctx)
    {
        if (!DatamapsHook.CEnvBeamHook.CEnvBeamStrikeThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CEnvBeamHook.CEnvBeamStrikeThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CEnvBeam::StrikeThink::Post.");
            }
        }
    }

    internal void InvokeCEnvBeamUpdateThinkPre(ref CEnvBeamUpdateThinkPreContext ctx)
    {
        if (!DatamapsHook.CEnvBeamHook.CEnvBeamUpdateThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CEnvBeamHook.CEnvBeamUpdateThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CEnvBeam::UpdateThink::Pre.");
            }
        }
    }

    internal void InvokeCEnvBeamUpdateThinkPost(ref CEnvBeamUpdateThinkPostContext ctx)
    {
        if (!DatamapsHook.CEnvBeamHook.CEnvBeamUpdateThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CEnvBeamHook.CEnvBeamUpdateThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CEnvBeam::UpdateThink::Post.");
            }
        }
    }

    internal void InvokeCEnvEntityMakerCheckSpawnThinkPre(ref CEnvEntityMakerCheckSpawnThinkPreContext ctx)
    {
        if (!DatamapsHook.CEnvEntityMakerHook.CEnvEntityMakerCheckSpawnThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CEnvEntityMakerHook.CEnvEntityMakerCheckSpawnThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CEnvEntityMaker::CheckSpawnThink::Pre.");
            }
        }
    }

    internal void InvokeCEnvEntityMakerCheckSpawnThinkPost(ref CEnvEntityMakerCheckSpawnThinkPostContext ctx)
    {
        if (!DatamapsHook.CEnvEntityMakerHook.CEnvEntityMakerCheckSpawnThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CEnvEntityMakerHook.CEnvEntityMakerCheckSpawnThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CEnvEntityMaker::CheckSpawnThink::Post.");
            }
        }
    }

    internal void InvokeCEnvLaserStrikeThinkPre(ref CEnvLaserStrikeThinkPreContext ctx)
    {
        if (!DatamapsHook.CEnvLaserHook.CEnvLaserStrikeThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CEnvLaserHook.CEnvLaserStrikeThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CEnvLaser::StrikeThink::Pre.");
            }
        }
    }

    internal void InvokeCEnvLaserStrikeThinkPost(ref CEnvLaserStrikeThinkPostContext ctx)
    {
        if (!DatamapsHook.CEnvLaserHook.CEnvLaserStrikeThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CEnvLaserHook.CEnvLaserStrikeThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CEnvLaser::StrikeThink::Post.");
            }
        }
    }

    internal void InvokeCEnvSparkSparkThinkPre(ref CEnvSparkSparkThinkPreContext ctx)
    {
        if (!DatamapsHook.CEnvSparkHook.CEnvSparkSparkThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CEnvSparkHook.CEnvSparkSparkThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CEnvSpark::SparkThink::Pre.");
            }
        }
    }

    internal void InvokeCEnvSparkSparkThinkPost(ref CEnvSparkSparkThinkPostContext ctx)
    {
        if (!DatamapsHook.CEnvSparkHook.CEnvSparkSparkThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CEnvSparkHook.CEnvSparkSparkThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CEnvSpark::SparkThink::Post.");
            }
        }
    }

    internal void InvokeCEnvWindWindThinkPre(ref CEnvWindWindThinkPreContext ctx)
    {
        if (!DatamapsHook.CEnvWindHook.CEnvWindWindThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CEnvWindHook.CEnvWindWindThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CEnvWind::WindThink::Pre.");
            }
        }
    }

    internal void InvokeCEnvWindWindThinkPost(ref CEnvWindWindThinkPostContext ctx)
    {
        if (!DatamapsHook.CEnvWindHook.CEnvWindWindThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CEnvWindHook.CEnvWindWindThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CEnvWind::WindThink::Post.");
            }
        }
    }

    internal void InvokeCEnvWindControllerWindThinkPre(ref CEnvWindControllerWindThinkPreContext ctx)
    {
        if (!DatamapsHook.CEnvWindControllerHook.CEnvWindControllerWindThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CEnvWindControllerHook.CEnvWindControllerWindThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CEnvWindController::WindThink::Pre.");
            }
        }
    }

    internal void InvokeCEnvWindControllerWindThinkPost(ref CEnvWindControllerWindThinkPostContext ctx)
    {
        if (!DatamapsHook.CEnvWindControllerHook.CEnvWindControllerWindThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CEnvWindControllerHook.CEnvWindControllerWindThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CEnvWindController::WindThink::Post.");
            }
        }
    }

    internal void InvokeCFishPoolUpdatePre(ref CFishPoolUpdatePreContext ctx)
    {
        if (!DatamapsHook.CFishPoolHook.CFishPoolUpdateHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFishPoolHook.CFishPoolUpdateHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFishPool::Update::Pre.");
            }
        }
    }

    internal void InvokeCFishPoolUpdatePost(ref CFishPoolUpdatePostContext ctx)
    {
        if (!DatamapsHook.CFishPoolHook.CFishPoolUpdateHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFishPoolHook.CFishPoolUpdateHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFishPool::Update::Post.");
            }
        }
    }

    internal void InvokeCFogControllerSetLerpValuesPre(ref CFogControllerSetLerpValuesPreContext ctx)
    {
        if (!DatamapsHook.CFogControllerHook.CFogControllerSetLerpValuesHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFogControllerHook.CFogControllerSetLerpValuesHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFogController::SetLerpValues::Pre.");
            }
        }
    }

    internal void InvokeCFogControllerSetLerpValuesPost(ref CFogControllerSetLerpValuesPostContext ctx)
    {
        if (!DatamapsHook.CFogControllerHook.CFogControllerSetLerpValuesHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFogControllerHook.CFogControllerSetLerpValuesHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFogController::SetLerpValues::Post.");
            }
        }
    }

    internal void InvokeCFuncMoveLinearNavMovableThinkPre(ref CFuncMoveLinearNavMovableThinkPreContext ctx)
    {
        if (!DatamapsHook.CFuncMoveLinearHook.CFuncMoveLinearNavMovableThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncMoveLinearHook.CFuncMoveLinearNavMovableThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncMoveLinear::NavMovableThink::Pre.");
            }
        }
    }

    internal void InvokeCFuncMoveLinearNavMovableThinkPost(ref CFuncMoveLinearNavMovableThinkPostContext ctx)
    {
        if (!DatamapsHook.CFuncMoveLinearHook.CFuncMoveLinearNavMovableThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncMoveLinearHook.CFuncMoveLinearNavMovableThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncMoveLinear::NavMovableThink::Post.");
            }
        }
    }

    internal void InvokeCFuncMoveLinearNavObstacleThinkPre(ref CFuncMoveLinearNavObstacleThinkPreContext ctx)
    {
        if (!DatamapsHook.CFuncMoveLinearHook.CFuncMoveLinearNavObstacleThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncMoveLinearHook.CFuncMoveLinearNavObstacleThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncMoveLinear::NavObstacleThink::Pre.");
            }
        }
    }

    internal void InvokeCFuncMoveLinearNavObstacleThinkPost(ref CFuncMoveLinearNavObstacleThinkPostContext ctx)
    {
        if (!DatamapsHook.CFuncMoveLinearHook.CFuncMoveLinearNavObstacleThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncMoveLinearHook.CFuncMoveLinearNavObstacleThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncMoveLinear::NavObstacleThink::Post.");
            }
        }
    }

    internal void InvokeCFuncMoveLinearStopMoveSoundPre(ref CFuncMoveLinearStopMoveSoundPreContext ctx)
    {
        if (!DatamapsHook.CFuncMoveLinearHook.CFuncMoveLinearStopMoveSoundHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncMoveLinearHook.CFuncMoveLinearStopMoveSoundHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncMoveLinear::StopMoveSound::Pre.");
            }
        }
    }

    internal void InvokeCFuncMoveLinearStopMoveSoundPost(ref CFuncMoveLinearStopMoveSoundPostContext ctx)
    {
        if (!DatamapsHook.CFuncMoveLinearHook.CFuncMoveLinearStopMoveSoundHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncMoveLinearHook.CFuncMoveLinearStopMoveSoundHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncMoveLinear::StopMoveSound::Post.");
            }
        }
    }

    internal void InvokeCFuncPlatCallGoDownPre(ref CFuncPlatCallGoDownPreContext ctx)
    {
        if (!DatamapsHook.CFuncPlatHook.CFuncPlatCallGoDownHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncPlatHook.CFuncPlatCallGoDownHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncPlat::CallGoDown::Pre.");
            }
        }
    }

    internal void InvokeCFuncPlatCallGoDownPost(ref CFuncPlatCallGoDownPostContext ctx)
    {
        if (!DatamapsHook.CFuncPlatHook.CFuncPlatCallGoDownHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncPlatHook.CFuncPlatCallGoDownHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncPlat::CallGoDown::Post.");
            }
        }
    }

    internal void InvokeCFuncPlatCallHitBottomPre(ref CFuncPlatCallHitBottomPreContext ctx)
    {
        if (!DatamapsHook.CFuncPlatHook.CFuncPlatCallHitBottomHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncPlatHook.CFuncPlatCallHitBottomHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncPlat::CallHitBottom::Pre.");
            }
        }
    }

    internal void InvokeCFuncPlatCallHitBottomPost(ref CFuncPlatCallHitBottomPostContext ctx)
    {
        if (!DatamapsHook.CFuncPlatHook.CFuncPlatCallHitBottomHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncPlatHook.CFuncPlatCallHitBottomHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncPlat::CallHitBottom::Post.");
            }
        }
    }

    internal void InvokeCFuncPlatCallHitTopPre(ref CFuncPlatCallHitTopPreContext ctx)
    {
        if (!DatamapsHook.CFuncPlatHook.CFuncPlatCallHitTopHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncPlatHook.CFuncPlatCallHitTopHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncPlat::CallHitTop::Pre.");
            }
        }
    }

    internal void InvokeCFuncPlatCallHitTopPost(ref CFuncPlatCallHitTopPostContext ctx)
    {
        if (!DatamapsHook.CFuncPlatHook.CFuncPlatCallHitTopHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncPlatHook.CFuncPlatCallHitTopHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncPlat::CallHitTop::Post.");
            }
        }
    }

    internal void InvokeCFuncPlatPlatUsePre(ref CFuncPlatPlatUsePreContext ctx)
    {
        if (!DatamapsHook.CFuncPlatHook.CFuncPlatPlatUseHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncPlatHook.CFuncPlatPlatUseHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncPlat::PlatUse::Pre.");
            }
        }
    }

    internal void InvokeCFuncPlatPlatUsePost(ref CFuncPlatPlatUsePostContext ctx)
    {
        if (!DatamapsHook.CFuncPlatHook.CFuncPlatPlatUseHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncPlatHook.CFuncPlatPlatUseHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncPlat::PlatUse::Post.");
            }
        }
    }

    internal void InvokeCFuncRotatingHurtTouchPre(ref CFuncRotatingHurtTouchPreContext ctx)
    {
        if (!DatamapsHook.CFuncRotatingHook.CFuncRotatingHurtTouchHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncRotatingHook.CFuncRotatingHurtTouchHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncRotating::HurtTouch::Pre.");
            }
        }
    }

    internal void InvokeCFuncRotatingHurtTouchPost(ref CFuncRotatingHurtTouchPostContext ctx)
    {
        if (!DatamapsHook.CFuncRotatingHook.CFuncRotatingHurtTouchHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncRotatingHook.CFuncRotatingHurtTouchHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncRotating::HurtTouch::Post.");
            }
        }
    }

    internal void InvokeCFuncRotatingReverseMovePre(ref CFuncRotatingReverseMovePreContext ctx)
    {
        if (!DatamapsHook.CFuncRotatingHook.CFuncRotatingReverseMoveHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncRotatingHook.CFuncRotatingReverseMoveHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncRotating::ReverseMove::Pre.");
            }
        }
    }

    internal void InvokeCFuncRotatingReverseMovePost(ref CFuncRotatingReverseMovePostContext ctx)
    {
        if (!DatamapsHook.CFuncRotatingHook.CFuncRotatingReverseMoveHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncRotatingHook.CFuncRotatingReverseMoveHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncRotating::ReverseMove::Post.");
            }
        }
    }

    internal void InvokeCFuncRotatingRotateMovePre(ref CFuncRotatingRotateMovePreContext ctx)
    {
        if (!DatamapsHook.CFuncRotatingHook.CFuncRotatingRotateMoveHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncRotatingHook.CFuncRotatingRotateMoveHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncRotating::RotateMove::Pre.");
            }
        }
    }

    internal void InvokeCFuncRotatingRotateMovePost(ref CFuncRotatingRotateMovePostContext ctx)
    {
        if (!DatamapsHook.CFuncRotatingHook.CFuncRotatingRotateMoveHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncRotatingHook.CFuncRotatingRotateMoveHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncRotating::RotateMove::Post.");
            }
        }
    }

    internal void InvokeCFuncRotatingRotatingUsePre(ref CFuncRotatingRotatingUsePreContext ctx)
    {
        if (!DatamapsHook.CFuncRotatingHook.CFuncRotatingRotatingUseHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncRotatingHook.CFuncRotatingRotatingUseHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncRotating::RotatingUse::Pre.");
            }
        }
    }

    internal void InvokeCFuncRotatingRotatingUsePost(ref CFuncRotatingRotatingUsePostContext ctx)
    {
        if (!DatamapsHook.CFuncRotatingHook.CFuncRotatingRotatingUseHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncRotatingHook.CFuncRotatingRotatingUseHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncRotating::RotatingUse::Post.");
            }
        }
    }

    internal void InvokeCFuncRotatingSpinDownMovePre(ref CFuncRotatingSpinDownMovePreContext ctx)
    {
        if (!DatamapsHook.CFuncRotatingHook.CFuncRotatingSpinDownMoveHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncRotatingHook.CFuncRotatingSpinDownMoveHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncRotating::SpinDownMove::Pre.");
            }
        }
    }

    internal void InvokeCFuncRotatingSpinDownMovePost(ref CFuncRotatingSpinDownMovePostContext ctx)
    {
        if (!DatamapsHook.CFuncRotatingHook.CFuncRotatingSpinDownMoveHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncRotatingHook.CFuncRotatingSpinDownMoveHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncRotating::SpinDownMove::Post.");
            }
        }
    }

    internal void InvokeCFuncRotatingSpinUpMovePre(ref CFuncRotatingSpinUpMovePreContext ctx)
    {
        if (!DatamapsHook.CFuncRotatingHook.CFuncRotatingSpinUpMoveHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncRotatingHook.CFuncRotatingSpinUpMoveHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncRotating::SpinUpMove::Pre.");
            }
        }
    }

    internal void InvokeCFuncRotatingSpinUpMovePost(ref CFuncRotatingSpinUpMovePostContext ctx)
    {
        if (!DatamapsHook.CFuncRotatingHook.CFuncRotatingSpinUpMoveHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncRotatingHook.CFuncRotatingSpinUpMoveHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncRotating::SpinUpMove::Post.");
            }
        }
    }

    internal void InvokeCFuncShatterglassGlassThinkPre(ref CFuncShatterglassGlassThinkPreContext ctx)
    {
        if (!DatamapsHook.CFuncShatterglassHook.CFuncShatterglassGlassThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncShatterglassHook.CFuncShatterglassGlassThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncShatterglass::GlassThink::Pre.");
            }
        }
    }

    internal void InvokeCFuncShatterglassGlassThinkPost(ref CFuncShatterglassGlassThinkPostContext ctx)
    {
        if (!DatamapsHook.CFuncShatterglassHook.CFuncShatterglassGlassThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncShatterglassHook.CFuncShatterglassGlassThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncShatterglass::GlassThink::Post.");
            }
        }
    }

    internal void InvokeCFuncTrackChangeFindPre(ref CFuncTrackChangeFindPreContext ctx)
    {
        if (!DatamapsHook.CFuncTrackChangeHook.CFuncTrackChangeFindHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncTrackChangeHook.CFuncTrackChangeFindHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncTrackChange::Find::Pre.");
            }
        }
    }

    internal void InvokeCFuncTrackChangeFindPost(ref CFuncTrackChangeFindPostContext ctx)
    {
        if (!DatamapsHook.CFuncTrackChangeHook.CFuncTrackChangeFindHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncTrackChangeHook.CFuncTrackChangeFindHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncTrackChange::Find::Post.");
            }
        }
    }

    internal void InvokeCFuncTrackTrainDeadEndPre(ref CFuncTrackTrainDeadEndPreContext ctx)
    {
        if (!DatamapsHook.CFuncTrackTrainHook.CFuncTrackTrainDeadEndHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncTrackTrainHook.CFuncTrackTrainDeadEndHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncTrackTrain::DeadEnd::Pre.");
            }
        }
    }

    internal void InvokeCFuncTrackTrainDeadEndPost(ref CFuncTrackTrainDeadEndPostContext ctx)
    {
        if (!DatamapsHook.CFuncTrackTrainHook.CFuncTrackTrainDeadEndHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncTrackTrainHook.CFuncTrackTrainDeadEndHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncTrackTrain::DeadEnd::Post.");
            }
        }
    }

    internal void InvokeCFuncTrackTrainFindPre(ref CFuncTrackTrainFindPreContext ctx)
    {
        if (!DatamapsHook.CFuncTrackTrainHook.CFuncTrackTrainFindHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncTrackTrainHook.CFuncTrackTrainFindHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncTrackTrain::Find::Pre.");
            }
        }
    }

    internal void InvokeCFuncTrackTrainFindPost(ref CFuncTrackTrainFindPostContext ctx)
    {
        if (!DatamapsHook.CFuncTrackTrainHook.CFuncTrackTrainFindHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncTrackTrainHook.CFuncTrackTrainFindHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncTrackTrain::Find::Post.");
            }
        }
    }

    internal void InvokeCFuncTrackTrainNearestPathPre(ref CFuncTrackTrainNearestPathPreContext ctx)
    {
        if (!DatamapsHook.CFuncTrackTrainHook.CFuncTrackTrainNearestPathHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncTrackTrainHook.CFuncTrackTrainNearestPathHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncTrackTrain::NearestPath::Pre.");
            }
        }
    }

    internal void InvokeCFuncTrackTrainNearestPathPost(ref CFuncTrackTrainNearestPathPostContext ctx)
    {
        if (!DatamapsHook.CFuncTrackTrainHook.CFuncTrackTrainNearestPathHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncTrackTrainHook.CFuncTrackTrainNearestPathHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncTrackTrain::NearestPath::Post.");
            }
        }
    }

    internal void InvokeCFuncTrackTrainNextPre(ref CFuncTrackTrainNextPreContext ctx)
    {
        if (!DatamapsHook.CFuncTrackTrainHook.CFuncTrackTrainNextHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncTrackTrainHook.CFuncTrackTrainNextHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncTrackTrain::Next::Pre.");
            }
        }
    }

    internal void InvokeCFuncTrackTrainNextPost(ref CFuncTrackTrainNextPostContext ctx)
    {
        if (!DatamapsHook.CFuncTrackTrainHook.CFuncTrackTrainNextHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncTrackTrainHook.CFuncTrackTrainNextHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncTrackTrain::Next::Post.");
            }
        }
    }

    internal void InvokeCFuncTrainNextPre(ref CFuncTrainNextPreContext ctx)
    {
        if (!DatamapsHook.CFuncTrainHook.CFuncTrainNextHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncTrainHook.CFuncTrainNextHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncTrain::Next::Pre.");
            }
        }
    }

    internal void InvokeCFuncTrainNextPost(ref CFuncTrainNextPostContext ctx)
    {
        if (!DatamapsHook.CFuncTrainHook.CFuncTrainNextHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncTrainHook.CFuncTrainNextHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncTrain::Next::Post.");
            }
        }
    }

    internal void InvokeCFuncTrainWaitPre(ref CFuncTrainWaitPreContext ctx)
    {
        if (!DatamapsHook.CFuncTrainHook.CFuncTrainWaitHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncTrainHook.CFuncTrainWaitHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncTrain::Wait::Pre.");
            }
        }
    }

    internal void InvokeCFuncTrainWaitPost(ref CFuncTrainWaitPostContext ctx)
    {
        if (!DatamapsHook.CFuncTrainHook.CFuncTrainWaitHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CFuncTrainHook.CFuncTrainWaitHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CFuncTrain::Wait::Post.");
            }
        }
    }

    internal void InvokeCGenericConstraintUpdateThinkPre(ref CGenericConstraintUpdateThinkPreContext ctx)
    {
        if (!DatamapsHook.CGenericConstraintHook.CGenericConstraintUpdateThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CGenericConstraintHook.CGenericConstraintUpdateThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CGenericConstraint::UpdateThink::Pre.");
            }
        }
    }

    internal void InvokeCGenericConstraintUpdateThinkPost(ref CGenericConstraintUpdateThinkPostContext ctx)
    {
        if (!DatamapsHook.CGenericConstraintHook.CGenericConstraintUpdateThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CGenericConstraintHook.CGenericConstraintUpdateThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CGenericConstraint::UpdateThink::Post.");
            }
        }
    }

    internal void InvokeCGunTargetNextPre(ref CGunTargetNextPreContext ctx)
    {
        if (!DatamapsHook.CGunTargetHook.CGunTargetNextHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CGunTargetHook.CGunTargetNextHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CGunTarget::Next::Pre.");
            }
        }
    }

    internal void InvokeCGunTargetNextPost(ref CGunTargetNextPostContext ctx)
    {
        if (!DatamapsHook.CGunTargetHook.CGunTargetNextHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CGunTargetHook.CGunTargetNextHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CGunTarget::Next::Post.");
            }
        }
    }

    internal void InvokeCGunTargetStartPre(ref CGunTargetStartPreContext ctx)
    {
        if (!DatamapsHook.CGunTargetHook.CGunTargetStartHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CGunTargetHook.CGunTargetStartHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CGunTarget::Start::Pre.");
            }
        }
    }

    internal void InvokeCGunTargetStartPost(ref CGunTargetStartPostContext ctx)
    {
        if (!DatamapsHook.CGunTargetHook.CGunTargetStartHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CGunTargetHook.CGunTargetStartHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CGunTarget::Start::Post.");
            }
        }
    }

    internal void InvokeCGunTargetWaitPre(ref CGunTargetWaitPreContext ctx)
    {
        if (!DatamapsHook.CGunTargetHook.CGunTargetWaitHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CGunTargetHook.CGunTargetWaitHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CGunTarget::Wait::Pre.");
            }
        }
    }

    internal void InvokeCGunTargetWaitPost(ref CGunTargetWaitPostContext ctx)
    {
        if (!DatamapsHook.CGunTargetHook.CGunTargetWaitHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CGunTargetHook.CGunTargetWaitHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CGunTarget::Wait::Post.");
            }
        }
    }

    internal void InvokeCHostageHostageThinkPre(ref CHostageHostageThinkPreContext ctx)
    {
        if (!DatamapsHook.CHostageHook.CHostageHostageThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CHostageHook.CHostageHostageThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CHostage::HostageThink::Pre.");
            }
        }
    }

    internal void InvokeCHostageHostageThinkPost(ref CHostageHostageThinkPostContext ctx)
    {
        if (!DatamapsHook.CHostageHook.CHostageHostageThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CHostageHook.CHostageHostageThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CHostage::HostageThink::Post.");
            }
        }
    }

    internal void InvokeCHostageHostageUsePre(ref CHostageHostageUsePreContext ctx)
    {
        if (!DatamapsHook.CHostageHook.CHostageHostageUseHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CHostageHook.CHostageHostageUseHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CHostage::HostageUse::Pre.");
            }
        }
    }

    internal void InvokeCHostageHostageUsePost(ref CHostageHostageUsePostContext ctx)
    {
        if (!DatamapsHook.CHostageHook.CHostageHostageUseHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CHostageHook.CHostageHostageUseHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CHostage::HostageUse::Post.");
            }
        }
    }

    internal void InvokeCHostageRescueZoneHostageRescueTouchPre(ref CHostageRescueZoneHostageRescueTouchPreContext ctx)
    {
        if (!DatamapsHook.CHostageRescueZoneHook.CHostageRescueZoneHostageRescueTouchHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CHostageRescueZoneHook.CHostageRescueZoneHostageRescueTouchHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CHostageRescueZone::HostageRescueTouch::Pre.");
            }
        }
    }

    internal void InvokeCHostageRescueZoneHostageRescueTouchPost(ref CHostageRescueZoneHostageRescueTouchPostContext ctx)
    {
        if (!DatamapsHook.CHostageRescueZoneHook.CHostageRescueZoneHostageRescueTouchHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CHostageRescueZoneHook.CHostageRescueZoneHostageRescueTouchHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CHostageRescueZone::HostageRescueTouch::Post.");
            }
        }
    }

    internal void InvokeCInfernoInfernoThinkPre(ref CInfernoInfernoThinkPreContext ctx)
    {
        if (!DatamapsHook.CInfernoHook.CInfernoInfernoThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CInfernoHook.CInfernoInfernoThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CInferno::InfernoThink::Pre.");
            }
        }
    }

    internal void InvokeCInfernoInfernoThinkPost(ref CInfernoInfernoThinkPostContext ctx)
    {
        if (!DatamapsHook.CInfernoHook.CInfernoInfernoThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CInfernoHook.CInfernoInfernoThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CInferno::InfernoThink::Post.");
            }
        }
    }

    internal void InvokeCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPre(ref CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPreContext ctx)
    {
        if (!DatamapsHook.CInfoSpawnGroupLoadUnloadHook.CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CInfoSpawnGroupLoadUnloadHook.CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CInfoSpawnGroupLoadUnload::SpawnGroupLoadingThink::Pre.");
            }
        }
    }

    internal void InvokeCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPost(ref CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPostContext ctx)
    {
        if (!DatamapsHook.CInfoSpawnGroupLoadUnloadHook.CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CInfoSpawnGroupLoadUnloadHook.CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CInfoSpawnGroupLoadUnload::SpawnGroupLoadingThink::Post.");
            }
        }
    }

    internal void InvokeCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPre(ref CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPreContext ctx)
    {
        if (!DatamapsHook.CInfoSpawnGroupLoadUnloadHook.CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CInfoSpawnGroupLoadUnloadHook.CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CInfoSpawnGroupLoadUnload::SpawnGroupUnloadingThink::Pre.");
            }
        }
    }

    internal void InvokeCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPost(ref CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPostContext ctx)
    {
        if (!DatamapsHook.CInfoSpawnGroupLoadUnloadHook.CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CInfoSpawnGroupLoadUnloadHook.CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CInfoSpawnGroupLoadUnload::SpawnGroupUnloadingThink::Post.");
            }
        }
    }

    internal void InvokeCItemComeToRestPre(ref CItemComeToRestPreContext ctx)
    {
        if (!DatamapsHook.CItemHook.CItemComeToRestHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CItemHook.CItemComeToRestHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CItem::ComeToRest::Pre.");
            }
        }
    }

    internal void InvokeCItemComeToRestPost(ref CItemComeToRestPostContext ctx)
    {
        if (!DatamapsHook.CItemHook.CItemComeToRestHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CItemHook.CItemComeToRestHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CItem::ComeToRest::Post.");
            }
        }
    }

    internal void InvokeCItemItemTouchPre(ref CItemItemTouchPreContext ctx)
    {
        if (!DatamapsHook.CItemHook.CItemItemTouchHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CItemHook.CItemItemTouchHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CItem::ItemTouch::Pre.");
            }
        }
    }

    internal void InvokeCItemItemTouchPost(ref CItemItemTouchPostContext ctx)
    {
        if (!DatamapsHook.CItemHook.CItemItemTouchHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CItemHook.CItemItemTouchHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CItem::ItemTouch::Post.");
            }
        }
    }

    internal void InvokeCItemMaterializePre(ref CItemMaterializePreContext ctx)
    {
        if (!DatamapsHook.CItemHook.CItemMaterializeHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CItemHook.CItemMaterializeHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CItem::Materialize::Pre.");
            }
        }
    }

    internal void InvokeCItemMaterializePost(ref CItemMaterializePostContext ctx)
    {
        if (!DatamapsHook.CItemHook.CItemMaterializeHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CItemHook.CItemMaterializeHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CItem::Materialize::Post.");
            }
        }
    }

    internal void InvokeCItemDefuserActivateThinkPre(ref CItemDefuserActivateThinkPreContext ctx)
    {
        if (!DatamapsHook.CItemDefuserHook.CItemDefuserActivateThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CItemDefuserHook.CItemDefuserActivateThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CItemDefuser::ActivateThink::Pre.");
            }
        }
    }

    internal void InvokeCItemDefuserActivateThinkPost(ref CItemDefuserActivateThinkPostContext ctx)
    {
        if (!DatamapsHook.CItemDefuserHook.CItemDefuserActivateThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CItemDefuserHook.CItemDefuserActivateThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CItemDefuser::ActivateThink::Post.");
            }
        }
    }

    internal void InvokeCItemDefuserDefuserTouchPre(ref CItemDefuserDefuserTouchPreContext ctx)
    {
        if (!DatamapsHook.CItemDefuserHook.CItemDefuserDefuserTouchHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CItemDefuserHook.CItemDefuserDefuserTouchHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CItemDefuser::DefuserTouch::Pre.");
            }
        }
    }

    internal void InvokeCItemDefuserDefuserTouchPost(ref CItemDefuserDefuserTouchPostContext ctx)
    {
        if (!DatamapsHook.CItemDefuserHook.CItemDefuserDefuserTouchHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CItemDefuserHook.CItemDefuserDefuserTouchHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CItemDefuser::DefuserTouch::Post.");
            }
        }
    }

    internal void InvokeCItemGenericItemGenericTouchPre(ref CItemGenericItemGenericTouchPreContext ctx)
    {
        if (!DatamapsHook.CItemGenericHook.CItemGenericItemGenericTouchHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CItemGenericHook.CItemGenericItemGenericTouchHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CItemGeneric::ItemGenericTouch::Pre.");
            }
        }
    }

    internal void InvokeCItemGenericItemGenericTouchPost(ref CItemGenericItemGenericTouchPostContext ctx)
    {
        if (!DatamapsHook.CItemGenericHook.CItemGenericItemGenericTouchHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CItemGenericHook.CItemGenericItemGenericTouchHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CItemGeneric::ItemGenericTouch::Post.");
            }
        }
    }

    internal void InvokeCLogicActiveAutosaveSaveThinkPre(ref CLogicActiveAutosaveSaveThinkPreContext ctx)
    {
        if (!DatamapsHook.CLogicActiveAutosaveHook.CLogicActiveAutosaveSaveThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CLogicActiveAutosaveHook.CLogicActiveAutosaveSaveThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CLogicActiveAutosave::SaveThink::Pre.");
            }
        }
    }

    internal void InvokeCLogicActiveAutosaveSaveThinkPost(ref CLogicActiveAutosaveSaveThinkPostContext ctx)
    {
        if (!DatamapsHook.CLogicActiveAutosaveHook.CLogicActiveAutosaveSaveThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CLogicActiveAutosaveHook.CLogicActiveAutosaveSaveThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CLogicActiveAutosave::SaveThink::Post.");
            }
        }
    }

    internal void InvokeCLogicDistanceAutosaveSaveThinkPre(ref CLogicDistanceAutosaveSaveThinkPreContext ctx)
    {
        if (!DatamapsHook.CLogicDistanceAutosaveHook.CLogicDistanceAutosaveSaveThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CLogicDistanceAutosaveHook.CLogicDistanceAutosaveSaveThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CLogicDistanceAutosave::SaveThink::Pre.");
            }
        }
    }

    internal void InvokeCLogicDistanceAutosaveSaveThinkPost(ref CLogicDistanceAutosaveSaveThinkPostContext ctx)
    {
        if (!DatamapsHook.CLogicDistanceAutosaveHook.CLogicDistanceAutosaveSaveThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CLogicDistanceAutosaveHook.CLogicDistanceAutosaveSaveThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CLogicDistanceAutosave::SaveThink::Post.");
            }
        }
    }

    internal void InvokeCLogicGameStateReportSetGameStateReportThinkPre(ref CLogicGameStateReportSetGameStateReportThinkPreContext ctx)
    {
        if (!DatamapsHook.CLogicGameStateReportHook.CLogicGameStateReportSetGameStateReportThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CLogicGameStateReportHook.CLogicGameStateReportSetGameStateReportThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CLogicGameStateReport::SetGameStateReportThink::Pre.");
            }
        }
    }

    internal void InvokeCLogicGameStateReportSetGameStateReportThinkPost(ref CLogicGameStateReportSetGameStateReportThinkPostContext ctx)
    {
        if (!DatamapsHook.CLogicGameStateReportHook.CLogicGameStateReportSetGameStateReportThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CLogicGameStateReportHook.CLogicGameStateReportSetGameStateReportThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CLogicGameStateReport::SetGameStateReportThink::Post.");
            }
        }
    }

    internal void InvokeCLogicMeasureMovementMeasureThinkPre(ref CLogicMeasureMovementMeasureThinkPreContext ctx)
    {
        if (!DatamapsHook.CLogicMeasureMovementHook.CLogicMeasureMovementMeasureThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CLogicMeasureMovementHook.CLogicMeasureMovementMeasureThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CLogicMeasureMovement::MeasureThink::Pre.");
            }
        }
    }

    internal void InvokeCLogicMeasureMovementMeasureThinkPost(ref CLogicMeasureMovementMeasureThinkPostContext ctx)
    {
        if (!DatamapsHook.CLogicMeasureMovementHook.CLogicMeasureMovementMeasureThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CLogicMeasureMovementHook.CLogicMeasureMovementMeasureThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CLogicMeasureMovement::MeasureThink::Post.");
            }
        }
    }

    internal void InvokeCLogicNPCCounterSetNPCCounterThinkPre(ref CLogicNPCCounterSetNPCCounterThinkPreContext ctx)
    {
        if (!DatamapsHook.CLogicNPCCounterHook.CLogicNPCCounterSetNPCCounterThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CLogicNPCCounterHook.CLogicNPCCounterSetNPCCounterThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CLogicNPCCounter::SetNPCCounterThink::Pre.");
            }
        }
    }

    internal void InvokeCLogicNPCCounterSetNPCCounterThinkPost(ref CLogicNPCCounterSetNPCCounterThinkPostContext ctx)
    {
        if (!DatamapsHook.CLogicNPCCounterHook.CLogicNPCCounterSetNPCCounterThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CLogicNPCCounterHook.CLogicNPCCounterSetNPCCounterThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CLogicNPCCounter::SetNPCCounterThink::Post.");
            }
        }
    }

    internal void InvokeCMapVetoPickControllerVoteControllerThinkPre(ref CMapVetoPickControllerVoteControllerThinkPreContext ctx)
    {
        if (!DatamapsHook.CMapVetoPickControllerHook.CMapVetoPickControllerVoteControllerThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CMapVetoPickControllerHook.CMapVetoPickControllerVoteControllerThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CMapVetoPickController::VoteControllerThink::Pre.");
            }
        }
    }

    internal void InvokeCMapVetoPickControllerVoteControllerThinkPost(ref CMapVetoPickControllerVoteControllerThinkPostContext ctx)
    {
        if (!DatamapsHook.CMapVetoPickControllerHook.CMapVetoPickControllerVoteControllerThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CMapVetoPickControllerHook.CMapVetoPickControllerVoteControllerThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CMapVetoPickController::VoteControllerThink::Post.");
            }
        }
    }

    internal void InvokeCMomentaryRotButtonReturnMoveDonePre(ref CMomentaryRotButtonReturnMoveDonePreContext ctx)
    {
        if (!DatamapsHook.CMomentaryRotButtonHook.CMomentaryRotButtonReturnMoveDoneHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CMomentaryRotButtonHook.CMomentaryRotButtonReturnMoveDoneHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CMomentaryRotButton::ReturnMoveDone::Pre.");
            }
        }
    }

    internal void InvokeCMomentaryRotButtonReturnMoveDonePost(ref CMomentaryRotButtonReturnMoveDonePostContext ctx)
    {
        if (!DatamapsHook.CMomentaryRotButtonHook.CMomentaryRotButtonReturnMoveDoneHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CMomentaryRotButtonHook.CMomentaryRotButtonReturnMoveDoneHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CMomentaryRotButton::ReturnMoveDone::Post.");
            }
        }
    }

    internal void InvokeCMomentaryRotButtonSetPositionMoveDonePre(ref CMomentaryRotButtonSetPositionMoveDonePreContext ctx)
    {
        if (!DatamapsHook.CMomentaryRotButtonHook.CMomentaryRotButtonSetPositionMoveDoneHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CMomentaryRotButtonHook.CMomentaryRotButtonSetPositionMoveDoneHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CMomentaryRotButton::SetPositionMoveDone::Pre.");
            }
        }
    }

    internal void InvokeCMomentaryRotButtonSetPositionMoveDonePost(ref CMomentaryRotButtonSetPositionMoveDonePostContext ctx)
    {
        if (!DatamapsHook.CMomentaryRotButtonHook.CMomentaryRotButtonSetPositionMoveDoneHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CMomentaryRotButtonHook.CMomentaryRotButtonSetPositionMoveDoneHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CMomentaryRotButton::SetPositionMoveDone::Post.");
            }
        }
    }

    internal void InvokeCMomentaryRotButtonUpdateThinkPre(ref CMomentaryRotButtonUpdateThinkPreContext ctx)
    {
        if (!DatamapsHook.CMomentaryRotButtonHook.CMomentaryRotButtonUpdateThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CMomentaryRotButtonHook.CMomentaryRotButtonUpdateThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CMomentaryRotButton::UpdateThink::Pre.");
            }
        }
    }

    internal void InvokeCMomentaryRotButtonUpdateThinkPost(ref CMomentaryRotButtonUpdateThinkPostContext ctx)
    {
        if (!DatamapsHook.CMomentaryRotButtonHook.CMomentaryRotButtonUpdateThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CMomentaryRotButtonHook.CMomentaryRotButtonUpdateThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CMomentaryRotButton::UpdateThink::Post.");
            }
        }
    }

    internal void InvokeCMomentaryRotButtonUseMoveDonePre(ref CMomentaryRotButtonUseMoveDonePreContext ctx)
    {
        if (!DatamapsHook.CMomentaryRotButtonHook.CMomentaryRotButtonUseMoveDoneHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CMomentaryRotButtonHook.CMomentaryRotButtonUseMoveDoneHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CMomentaryRotButton::UseMoveDone::Pre.");
            }
        }
    }

    internal void InvokeCMomentaryRotButtonUseMoveDonePost(ref CMomentaryRotButtonUseMoveDonePostContext ctx)
    {
        if (!DatamapsHook.CMomentaryRotButtonHook.CMomentaryRotButtonUseMoveDoneHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CMomentaryRotButtonHook.CMomentaryRotButtonUseMoveDoneHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CMomentaryRotButton::UseMoveDone::Post.");
            }
        }
    }

    internal void InvokeCMultiLightProxyApproachBrightnessThinkPre(ref CMultiLightProxyApproachBrightnessThinkPreContext ctx)
    {
        if (!DatamapsHook.CMultiLightProxyHook.CMultiLightProxyApproachBrightnessThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CMultiLightProxyHook.CMultiLightProxyApproachBrightnessThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CMultiLightProxy::ApproachBrightnessThink::Pre.");
            }
        }
    }

    internal void InvokeCMultiLightProxyApproachBrightnessThinkPost(ref CMultiLightProxyApproachBrightnessThinkPostContext ctx)
    {
        if (!DatamapsHook.CMultiLightProxyHook.CMultiLightProxyApproachBrightnessThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CMultiLightProxyHook.CMultiLightProxyApproachBrightnessThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CMultiLightProxy::ApproachBrightnessThink::Post.");
            }
        }
    }

    internal void InvokeCMultiLightProxyRestoreFlashlightThinkPre(ref CMultiLightProxyRestoreFlashlightThinkPreContext ctx)
    {
        if (!DatamapsHook.CMultiLightProxyHook.CMultiLightProxyRestoreFlashlightThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CMultiLightProxyHook.CMultiLightProxyRestoreFlashlightThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CMultiLightProxy::RestoreFlashlightThink::Pre.");
            }
        }
    }

    internal void InvokeCMultiLightProxyRestoreFlashlightThinkPost(ref CMultiLightProxyRestoreFlashlightThinkPostContext ctx)
    {
        if (!DatamapsHook.CMultiLightProxyHook.CMultiLightProxyRestoreFlashlightThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CMultiLightProxyHook.CMultiLightProxyRestoreFlashlightThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CMultiLightProxy::RestoreFlashlightThink::Post.");
            }
        }
    }

    internal void InvokeCMultiSourceRegisterPre(ref CMultiSourceRegisterPreContext ctx)
    {
        if (!DatamapsHook.CMultiSourceHook.CMultiSourceRegisterHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CMultiSourceHook.CMultiSourceRegisterHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CMultiSource::Register::Pre.");
            }
        }
    }

    internal void InvokeCMultiSourceRegisterPost(ref CMultiSourceRegisterPostContext ctx)
    {
        if (!DatamapsHook.CMultiSourceHook.CMultiSourceRegisterHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CMultiSourceHook.CMultiSourceRegisterHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CMultiSource::Register::Post.");
            }
        }
    }

    internal void InvokeCParticleSystemStartParticleSystemThinkPre(ref CParticleSystemStartParticleSystemThinkPreContext ctx)
    {
        if (!DatamapsHook.CParticleSystemHook.CParticleSystemStartParticleSystemThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CParticleSystemHook.CParticleSystemStartParticleSystemThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CParticleSystem::StartParticleSystemThink::Pre.");
            }
        }
    }

    internal void InvokeCParticleSystemStartParticleSystemThinkPost(ref CParticleSystemStartParticleSystemThinkPostContext ctx)
    {
        if (!DatamapsHook.CParticleSystemHook.CParticleSystemStartParticleSystemThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CParticleSystemHook.CParticleSystemStartParticleSystemThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CParticleSystem::StartParticleSystemThink::Post.");
            }
        }
    }

    internal void InvokeCPathMoverEntitySpawnerSpawnThinkPre(ref CPathMoverEntitySpawnerSpawnThinkPreContext ctx)
    {
        if (!DatamapsHook.CPathMoverEntitySpawnerHook.CPathMoverEntitySpawnerSpawnThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPathMoverEntitySpawnerHook.CPathMoverEntitySpawnerSpawnThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPathMoverEntitySpawner::SpawnThink::Pre.");
            }
        }
    }

    internal void InvokeCPathMoverEntitySpawnerSpawnThinkPost(ref CPathMoverEntitySpawnerSpawnThinkPostContext ctx)
    {
        if (!DatamapsHook.CPathMoverEntitySpawnerHook.CPathMoverEntitySpawnerSpawnThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPathMoverEntitySpawnerHook.CPathMoverEntitySpawnerSpawnThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPathMoverEntitySpawner::SpawnThink::Post.");
            }
        }
    }

    internal void InvokeCPathNodeParentedMoveThinkPre(ref CPathNodeParentedMoveThinkPreContext ctx)
    {
        if (!DatamapsHook.CPathNodeHook.CPathNodeParentedMoveThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPathNodeHook.CPathNodeParentedMoveThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPathNode::ParentedMoveThink::Pre.");
            }
        }
    }

    internal void InvokeCPathNodeParentedMoveThinkPost(ref CPathNodeParentedMoveThinkPostContext ctx)
    {
        if (!DatamapsHook.CPathNodeHook.CPathNodeParentedMoveThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPathNodeHook.CPathNodeParentedMoveThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPathNode::ParentedMoveThink::Post.");
            }
        }
    }

    internal void InvokeCPhysForceForceOffPre(ref CPhysForceForceOffPreContext ctx)
    {
        if (!DatamapsHook.CPhysForceHook.CPhysForceForceOffHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysForceHook.CPhysForceForceOffHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysForce::ForceOff::Pre.");
            }
        }
    }

    internal void InvokeCPhysForceForceOffPost(ref CPhysForceForceOffPostContext ctx)
    {
        if (!DatamapsHook.CPhysForceHook.CPhysForceForceOffHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysForceHook.CPhysForceForceOffHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysForce::ForceOff::Post.");
            }
        }
    }

    internal void InvokeCPhysForceInitialThinkPre(ref CPhysForceInitialThinkPreContext ctx)
    {
        if (!DatamapsHook.CPhysForceHook.CPhysForceInitialThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysForceHook.CPhysForceInitialThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysForce::InitialThink::Pre.");
            }
        }
    }

    internal void InvokeCPhysForceInitialThinkPost(ref CPhysForceInitialThinkPostContext ctx)
    {
        if (!DatamapsHook.CPhysForceHook.CPhysForceInitialThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysForceHook.CPhysForceInitialThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysForce::InitialThink::Post.");
            }
        }
    }

    internal void InvokeCPhysHingeLimitThinkPre(ref CPhysHingeLimitThinkPreContext ctx)
    {
        if (!DatamapsHook.CPhysHingeHook.CPhysHingeLimitThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysHingeHook.CPhysHingeLimitThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysHinge::LimitThink::Pre.");
            }
        }
    }

    internal void InvokeCPhysHingeLimitThinkPost(ref CPhysHingeLimitThinkPostContext ctx)
    {
        if (!DatamapsHook.CPhysHingeHook.CPhysHingeLimitThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysHingeHook.CPhysHingeLimitThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysHinge::LimitThink::Post.");
            }
        }
    }

    internal void InvokeCPhysHingeMoveThinkPre(ref CPhysHingeMoveThinkPreContext ctx)
    {
        if (!DatamapsHook.CPhysHingeHook.CPhysHingeMoveThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysHingeHook.CPhysHingeMoveThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysHinge::MoveThink::Pre.");
            }
        }
    }

    internal void InvokeCPhysHingeMoveThinkPost(ref CPhysHingeMoveThinkPostContext ctx)
    {
        if (!DatamapsHook.CPhysHingeHook.CPhysHingeMoveThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysHingeHook.CPhysHingeMoveThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysHinge::MoveThink::Post.");
            }
        }
    }

    internal void InvokeCPhysHingeSoundThinkPre(ref CPhysHingeSoundThinkPreContext ctx)
    {
        if (!DatamapsHook.CPhysHingeHook.CPhysHingeSoundThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysHingeHook.CPhysHingeSoundThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysHinge::SoundThink::Pre.");
            }
        }
    }

    internal void InvokeCPhysHingeSoundThinkPost(ref CPhysHingeSoundThinkPostContext ctx)
    {
        if (!DatamapsHook.CPhysHingeHook.CPhysHingeSoundThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysHingeHook.CPhysHingeSoundThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysHinge::SoundThink::Post.");
            }
        }
    }

    internal void InvokeCPhysImpactPointAtEntityPre(ref CPhysImpactPointAtEntityPreContext ctx)
    {
        if (!DatamapsHook.CPhysImpactHook.CPhysImpactPointAtEntityHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysImpactHook.CPhysImpactPointAtEntityHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysImpact::PointAtEntity::Pre.");
            }
        }
    }

    internal void InvokeCPhysImpactPointAtEntityPost(ref CPhysImpactPointAtEntityPostContext ctx)
    {
        if (!DatamapsHook.CPhysImpactHook.CPhysImpactPointAtEntityHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysImpactHook.CPhysImpactPointAtEntityHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysImpact::PointAtEntity::Post.");
            }
        }
    }

    internal void InvokeCPhysSlideConstraintSoundThinkPre(ref CPhysSlideConstraintSoundThinkPreContext ctx)
    {
        if (!DatamapsHook.CPhysSlideConstraintHook.CPhysSlideConstraintSoundThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysSlideConstraintHook.CPhysSlideConstraintSoundThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysSlideConstraint::SoundThink::Pre.");
            }
        }
    }

    internal void InvokeCPhysSlideConstraintSoundThinkPost(ref CPhysSlideConstraintSoundThinkPostContext ctx)
    {
        if (!DatamapsHook.CPhysSlideConstraintHook.CPhysSlideConstraintSoundThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysSlideConstraintHook.CPhysSlideConstraintSoundThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysSlideConstraint::SoundThink::Post.");
            }
        }
    }

    internal void InvokeCPhysicsPropClearFlagsThinkPre(ref CPhysicsPropClearFlagsThinkPreContext ctx)
    {
        if (!DatamapsHook.CPhysicsPropHook.CPhysicsPropClearFlagsThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysicsPropHook.CPhysicsPropClearFlagsThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysicsProp::ClearFlagsThink::Pre.");
            }
        }
    }

    internal void InvokeCPhysicsPropClearFlagsThinkPost(ref CPhysicsPropClearFlagsThinkPostContext ctx)
    {
        if (!DatamapsHook.CPhysicsPropHook.CPhysicsPropClearFlagsThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysicsPropHook.CPhysicsPropClearFlagsThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysicsProp::ClearFlagsThink::Post.");
            }
        }
    }

    internal void InvokeCPhysicsPropClearThrownByPlayerThinkPre(ref CPhysicsPropClearThrownByPlayerThinkPreContext ctx)
    {
        if (!DatamapsHook.CPhysicsPropHook.CPhysicsPropClearThrownByPlayerThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysicsPropHook.CPhysicsPropClearThrownByPlayerThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysicsProp::ClearThrownByPlayerThink::Pre.");
            }
        }
    }

    internal void InvokeCPhysicsPropClearThrownByPlayerThinkPost(ref CPhysicsPropClearThrownByPlayerThinkPostContext ctx)
    {
        if (!DatamapsHook.CPhysicsPropHook.CPhysicsPropClearThrownByPlayerThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysicsPropHook.CPhysicsPropClearThrownByPlayerThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysicsProp::ClearThrownByPlayerThink::Post.");
            }
        }
    }

    internal void InvokeCPhysicsPropRespawnableMaterializePre(ref CPhysicsPropRespawnableMaterializePreContext ctx)
    {
        if (!DatamapsHook.CPhysicsPropRespawnableHook.CPhysicsPropRespawnableMaterializeHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysicsPropRespawnableHook.CPhysicsPropRespawnableMaterializeHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysicsPropRespawnable::Materialize::Pre.");
            }
        }
    }

    internal void InvokeCPhysicsPropRespawnableMaterializePost(ref CPhysicsPropRespawnableMaterializePostContext ctx)
    {
        if (!DatamapsHook.CPhysicsPropRespawnableHook.CPhysicsPropRespawnableMaterializeHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPhysicsPropRespawnableHook.CPhysicsPropRespawnableMaterializeHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPhysicsPropRespawnable::Materialize::Post.");
            }
        }
    }

    internal void InvokeCPlantedC4C4ThinkPre(ref CPlantedC4C4ThinkPreContext ctx)
    {
        if (!DatamapsHook.CPlantedC4Hook.CPlantedC4C4ThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPlantedC4Hook.CPlantedC4C4ThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPlantedC4::C4Think::Pre.");
            }
        }
    }

    internal void InvokeCPlantedC4C4ThinkPost(ref CPlantedC4C4ThinkPostContext ctx)
    {
        if (!DatamapsHook.CPlantedC4Hook.CPlantedC4C4ThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPlantedC4Hook.CPlantedC4C4ThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPlantedC4::C4Think::Post.");
            }
        }
    }

    internal void InvokeCPointCommentaryNodeAcculumatePlayTimeThinkPre(ref CPointCommentaryNodeAcculumatePlayTimeThinkPreContext ctx)
    {
        if (!DatamapsHook.CPointCommentaryNodeHook.CPointCommentaryNodeAcculumatePlayTimeThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPointCommentaryNodeHook.CPointCommentaryNodeAcculumatePlayTimeThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPointCommentaryNode::AcculumatePlayTimeThink::Pre.");
            }
        }
    }

    internal void InvokeCPointCommentaryNodeAcculumatePlayTimeThinkPost(ref CPointCommentaryNodeAcculumatePlayTimeThinkPostContext ctx)
    {
        if (!DatamapsHook.CPointCommentaryNodeHook.CPointCommentaryNodeAcculumatePlayTimeThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPointCommentaryNodeHook.CPointCommentaryNodeAcculumatePlayTimeThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPointCommentaryNode::AcculumatePlayTimeThink::Post.");
            }
        }
    }

    internal void InvokeCPointCommentaryNodeSpinThinkPre(ref CPointCommentaryNodeSpinThinkPreContext ctx)
    {
        if (!DatamapsHook.CPointCommentaryNodeHook.CPointCommentaryNodeSpinThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPointCommentaryNodeHook.CPointCommentaryNodeSpinThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPointCommentaryNode::SpinThink::Pre.");
            }
        }
    }

    internal void InvokeCPointCommentaryNodeSpinThinkPost(ref CPointCommentaryNodeSpinThinkPostContext ctx)
    {
        if (!DatamapsHook.CPointCommentaryNodeHook.CPointCommentaryNodeSpinThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPointCommentaryNodeHook.CPointCommentaryNodeSpinThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPointCommentaryNode::SpinThink::Post.");
            }
        }
    }

    internal void InvokeCPointCommentaryNodeUpdateViewPostThinkPre(ref CPointCommentaryNodeUpdateViewPostThinkPreContext ctx)
    {
        if (!DatamapsHook.CPointCommentaryNodeHook.CPointCommentaryNodeUpdateViewPostThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPointCommentaryNodeHook.CPointCommentaryNodeUpdateViewPostThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPointCommentaryNode::UpdateViewPostThink::Pre.");
            }
        }
    }

    internal void InvokeCPointCommentaryNodeUpdateViewPostThinkPost(ref CPointCommentaryNodeUpdateViewPostThinkPostContext ctx)
    {
        if (!DatamapsHook.CPointCommentaryNodeHook.CPointCommentaryNodeUpdateViewPostThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPointCommentaryNodeHook.CPointCommentaryNodeUpdateViewPostThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPointCommentaryNode::UpdateViewPostThink::Post.");
            }
        }
    }

    internal void InvokeCPointCommentaryNodeUpdateViewThinkPre(ref CPointCommentaryNodeUpdateViewThinkPreContext ctx)
    {
        if (!DatamapsHook.CPointCommentaryNodeHook.CPointCommentaryNodeUpdateViewThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPointCommentaryNodeHook.CPointCommentaryNodeUpdateViewThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPointCommentaryNode::UpdateViewThink::Pre.");
            }
        }
    }

    internal void InvokeCPointCommentaryNodeUpdateViewThinkPost(ref CPointCommentaryNodeUpdateViewThinkPostContext ctx)
    {
        if (!DatamapsHook.CPointCommentaryNodeHook.CPointCommentaryNodeUpdateViewThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPointCommentaryNodeHook.CPointCommentaryNodeUpdateViewThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPointCommentaryNode::UpdateViewThink::Post.");
            }
        }
    }

    internal void InvokeCPointHurtHurtThinkPre(ref CPointHurtHurtThinkPreContext ctx)
    {
        if (!DatamapsHook.CPointHurtHook.CPointHurtHurtThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPointHurtHook.CPointHurtHurtThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPointHurt::HurtThink::Pre.");
            }
        }
    }

    internal void InvokeCPointHurtHurtThinkPost(ref CPointHurtHurtThinkPostContext ctx)
    {
        if (!DatamapsHook.CPointHurtHook.CPointHurtHurtThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPointHurtHook.CPointHurtHurtThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPointHurt::HurtThink::Post.");
            }
        }
    }

    internal void InvokeCPointOrientReorientThinkPre(ref CPointOrientReorientThinkPreContext ctx)
    {
        if (!DatamapsHook.CPointOrientHook.CPointOrientReorientThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPointOrientHook.CPointOrientReorientThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPointOrient::ReorientThink::Pre.");
            }
        }
    }

    internal void InvokeCPointOrientReorientThinkPost(ref CPointOrientReorientThinkPostContext ctx)
    {
        if (!DatamapsHook.CPointOrientHook.CPointOrientReorientThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPointOrientHook.CPointOrientReorientThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPointOrient::ReorientThink::Post.");
            }
        }
    }

    internal void InvokeCPointPushPushThinkPre(ref CPointPushPushThinkPreContext ctx)
    {
        if (!DatamapsHook.CPointPushHook.CPointPushPushThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPointPushHook.CPointPushPushThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPointPush::PushThink::Pre.");
            }
        }
    }

    internal void InvokeCPointPushPushThinkPost(ref CPointPushPushThinkPostContext ctx)
    {
        if (!DatamapsHook.CPointPushHook.CPointPushPushThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPointPushHook.CPointPushPushThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPointPush::PushThink::Post.");
            }
        }
    }

    internal void InvokeCPointValueRemapperUpdateThinkPre(ref CPointValueRemapperUpdateThinkPreContext ctx)
    {
        if (!DatamapsHook.CPointValueRemapperHook.CPointValueRemapperUpdateThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPointValueRemapperHook.CPointValueRemapperUpdateThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPointValueRemapper::UpdateThink::Pre.");
            }
        }
    }

    internal void InvokeCPointValueRemapperUpdateThinkPost(ref CPointValueRemapperUpdateThinkPostContext ctx)
    {
        if (!DatamapsHook.CPointValueRemapperHook.CPointValueRemapperUpdateThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CPointValueRemapperHook.CPointValueRemapperUpdateThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CPointValueRemapper::UpdateThink::Post.");
            }
        }
    }

    internal void InvokeCRagdollPropAttachedItemsThinkPre(ref CRagdollPropAttachedItemsThinkPreContext ctx)
    {
        if (!DatamapsHook.CRagdollPropHook.CRagdollPropAttachedItemsThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CRagdollPropHook.CRagdollPropAttachedItemsThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CRagdollProp::AttachedItemsThink::Pre.");
            }
        }
    }

    internal void InvokeCRagdollPropAttachedItemsThinkPost(ref CRagdollPropAttachedItemsThinkPostContext ctx)
    {
        if (!DatamapsHook.CRagdollPropHook.CRagdollPropAttachedItemsThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CRagdollPropHook.CRagdollPropAttachedItemsThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CRagdollProp::AttachedItemsThink::Post.");
            }
        }
    }

    internal void InvokeCRagdollPropClearFlagsThinkPre(ref CRagdollPropClearFlagsThinkPreContext ctx)
    {
        if (!DatamapsHook.CRagdollPropHook.CRagdollPropClearFlagsThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CRagdollPropHook.CRagdollPropClearFlagsThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CRagdollProp::ClearFlagsThink::Pre.");
            }
        }
    }

    internal void InvokeCRagdollPropClearFlagsThinkPost(ref CRagdollPropClearFlagsThinkPostContext ctx)
    {
        if (!DatamapsHook.CRagdollPropHook.CRagdollPropClearFlagsThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CRagdollPropHook.CRagdollPropClearFlagsThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CRagdollProp::ClearFlagsThink::Post.");
            }
        }
    }

    internal void InvokeCRagdollPropFadeOutThinkPre(ref CRagdollPropFadeOutThinkPreContext ctx)
    {
        if (!DatamapsHook.CRagdollPropHook.CRagdollPropFadeOutThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CRagdollPropHook.CRagdollPropFadeOutThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CRagdollProp::FadeOutThink::Pre.");
            }
        }
    }

    internal void InvokeCRagdollPropFadeOutThinkPost(ref CRagdollPropFadeOutThinkPostContext ctx)
    {
        if (!DatamapsHook.CRagdollPropHook.CRagdollPropFadeOutThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CRagdollPropHook.CRagdollPropFadeOutThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CRagdollProp::FadeOutThink::Post.");
            }
        }
    }

    internal void InvokeCRagdollPropSetDebrisThinkPre(ref CRagdollPropSetDebrisThinkPreContext ctx)
    {
        if (!DatamapsHook.CRagdollPropHook.CRagdollPropSetDebrisThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CRagdollPropHook.CRagdollPropSetDebrisThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CRagdollProp::SetDebrisThink::Pre.");
            }
        }
    }

    internal void InvokeCRagdollPropSetDebrisThinkPost(ref CRagdollPropSetDebrisThinkPostContext ctx)
    {
        if (!DatamapsHook.CRagdollPropHook.CRagdollPropSetDebrisThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CRagdollPropHook.CRagdollPropSetDebrisThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CRagdollProp::SetDebrisThink::Post.");
            }
        }
    }

    internal void InvokeCRagdollPropSettleThinkPre(ref CRagdollPropSettleThinkPreContext ctx)
    {
        if (!DatamapsHook.CRagdollPropHook.CRagdollPropSettleThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CRagdollPropHook.CRagdollPropSettleThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CRagdollProp::SettleThink::Pre.");
            }
        }
    }

    internal void InvokeCRagdollPropSettleThinkPost(ref CRagdollPropSettleThinkPostContext ctx)
    {
        if (!DatamapsHook.CRagdollPropHook.CRagdollPropSettleThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CRagdollPropHook.CRagdollPropSettleThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CRagdollProp::SettleThink::Post.");
            }
        }
    }

    internal void InvokeCRevertSavedLoadThinkPre(ref CRevertSavedLoadThinkPreContext ctx)
    {
        if (!DatamapsHook.CRevertSavedHook.CRevertSavedLoadThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CRevertSavedHook.CRevertSavedLoadThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CRevertSaved::LoadThink::Pre.");
            }
        }
    }

    internal void InvokeCRevertSavedLoadThinkPost(ref CRevertSavedLoadThinkPostContext ctx)
    {
        if (!DatamapsHook.CRevertSavedHook.CRevertSavedLoadThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CRevertSavedHook.CRevertSavedLoadThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CRevertSaved::LoadThink::Post.");
            }
        }
    }

    internal void InvokeCScriptedSequenceScriptThinkPre(ref CScriptedSequenceScriptThinkPreContext ctx)
    {
        if (!DatamapsHook.CScriptedSequenceHook.CScriptedSequenceScriptThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CScriptedSequenceHook.CScriptedSequenceScriptThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CScriptedSequence::ScriptThink::Pre.");
            }
        }
    }

    internal void InvokeCScriptedSequenceScriptThinkPost(ref CScriptedSequenceScriptThinkPostContext ctx)
    {
        if (!DatamapsHook.CScriptedSequenceHook.CScriptedSequenceScriptThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CScriptedSequenceHook.CScriptedSequenceScriptThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CScriptedSequence::ScriptThink::Post.");
            }
        }
    }

    internal void InvokeCSmokeGrenadeProjectileThink_BuildingSmokeVolumePre(ref CSmokeGrenadeProjectileThink_BuildingSmokeVolumePreContext ctx)
    {
        if (!DatamapsHook.CSmokeGrenadeProjectileHook.CSmokeGrenadeProjectileThink_BuildingSmokeVolumeHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSmokeGrenadeProjectileHook.CSmokeGrenadeProjectileThink_BuildingSmokeVolumeHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSmokeGrenadeProjectile::Think_BuildingSmokeVolume::Pre.");
            }
        }
    }

    internal void InvokeCSmokeGrenadeProjectileThink_BuildingSmokeVolumePost(ref CSmokeGrenadeProjectileThink_BuildingSmokeVolumePostContext ctx)
    {
        if (!DatamapsHook.CSmokeGrenadeProjectileHook.CSmokeGrenadeProjectileThink_BuildingSmokeVolumeHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSmokeGrenadeProjectileHook.CSmokeGrenadeProjectileThink_BuildingSmokeVolumeHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSmokeGrenadeProjectile::Think_BuildingSmokeVolume::Post.");
            }
        }
    }

    internal void InvokeCSmokeGrenadeProjectileThink_DetonatePre(ref CSmokeGrenadeProjectileThink_DetonatePreContext ctx)
    {
        if (!DatamapsHook.CSmokeGrenadeProjectileHook.CSmokeGrenadeProjectileThink_DetonateHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSmokeGrenadeProjectileHook.CSmokeGrenadeProjectileThink_DetonateHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSmokeGrenadeProjectile::Think_Detonate::Pre.");
            }
        }
    }

    internal void InvokeCSmokeGrenadeProjectileThink_DetonatePost(ref CSmokeGrenadeProjectileThink_DetonatePostContext ctx)
    {
        if (!DatamapsHook.CSmokeGrenadeProjectileHook.CSmokeGrenadeProjectileThink_DetonateHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSmokeGrenadeProjectileHook.CSmokeGrenadeProjectileThink_DetonateHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSmokeGrenadeProjectile::Think_Detonate::Post.");
            }
        }
    }

    internal void InvokeCSmokeGrenadeProjectileThink_RemovePre(ref CSmokeGrenadeProjectileThink_RemovePreContext ctx)
    {
        if (!DatamapsHook.CSmokeGrenadeProjectileHook.CSmokeGrenadeProjectileThink_RemoveHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSmokeGrenadeProjectileHook.CSmokeGrenadeProjectileThink_RemoveHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSmokeGrenadeProjectile::Think_Remove::Pre.");
            }
        }
    }

    internal void InvokeCSmokeGrenadeProjectileThink_RemovePost(ref CSmokeGrenadeProjectileThink_RemovePostContext ctx)
    {
        if (!DatamapsHook.CSmokeGrenadeProjectileHook.CSmokeGrenadeProjectileThink_RemoveHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSmokeGrenadeProjectileHook.CSmokeGrenadeProjectileThink_RemoveHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSmokeGrenadeProjectile::Think_Remove::Post.");
            }
        }
    }

    internal void InvokeCSmokeGrenadeProjectileThink_UpdatePre(ref CSmokeGrenadeProjectileThink_UpdatePreContext ctx)
    {
        if (!DatamapsHook.CSmokeGrenadeProjectileHook.CSmokeGrenadeProjectileThink_UpdateHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSmokeGrenadeProjectileHook.CSmokeGrenadeProjectileThink_UpdateHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSmokeGrenadeProjectile::Think_Update::Pre.");
            }
        }
    }

    internal void InvokeCSmokeGrenadeProjectileThink_UpdatePost(ref CSmokeGrenadeProjectileThink_UpdatePostContext ctx)
    {
        if (!DatamapsHook.CSmokeGrenadeProjectileHook.CSmokeGrenadeProjectileThink_UpdateHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSmokeGrenadeProjectileHook.CSmokeGrenadeProjectileThink_UpdateHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSmokeGrenadeProjectile::Think_Update::Post.");
            }
        }
    }

    internal void InvokeCSoundEventConeEntitySoundEventConeThinkPre(ref CSoundEventConeEntitySoundEventConeThinkPreContext ctx)
    {
        if (!DatamapsHook.CSoundEventConeEntityHook.CSoundEventConeEntitySoundEventConeThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundEventConeEntityHook.CSoundEventConeEntitySoundEventConeThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundEventConeEntity::SoundEventConeThink::Pre.");
            }
        }
    }

    internal void InvokeCSoundEventConeEntitySoundEventConeThinkPost(ref CSoundEventConeEntitySoundEventConeThinkPostContext ctx)
    {
        if (!DatamapsHook.CSoundEventConeEntityHook.CSoundEventConeEntitySoundEventConeThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundEventConeEntityHook.CSoundEventConeEntitySoundEventConeThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundEventConeEntity::SoundEventConeThink::Post.");
            }
        }
    }

    internal void InvokeCSoundEventEntitySoundFinishedThinkPre(ref CSoundEventEntitySoundFinishedThinkPreContext ctx)
    {
        if (!DatamapsHook.CSoundEventEntityHook.CSoundEventEntitySoundFinishedThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundEventEntityHook.CSoundEventEntitySoundFinishedThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundEventEntity::SoundFinishedThink::Pre.");
            }
        }
    }

    internal void InvokeCSoundEventEntitySoundFinishedThinkPost(ref CSoundEventEntitySoundFinishedThinkPostContext ctx)
    {
        if (!DatamapsHook.CSoundEventEntityHook.CSoundEventEntitySoundFinishedThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundEventEntityHook.CSoundEventEntitySoundFinishedThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundEventEntity::SoundFinishedThink::Post.");
            }
        }
    }

    internal void InvokeCSoundEventOBBEntitySoundEventOBBThinkPre(ref CSoundEventOBBEntitySoundEventOBBThinkPreContext ctx)
    {
        if (!DatamapsHook.CSoundEventOBBEntityHook.CSoundEventOBBEntitySoundEventOBBThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundEventOBBEntityHook.CSoundEventOBBEntitySoundEventOBBThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundEventOBBEntity::SoundEventOBBThink::Pre.");
            }
        }
    }

    internal void InvokeCSoundEventOBBEntitySoundEventOBBThinkPost(ref CSoundEventOBBEntitySoundEventOBBThinkPostContext ctx)
    {
        if (!DatamapsHook.CSoundEventOBBEntityHook.CSoundEventOBBEntitySoundEventOBBThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundEventOBBEntityHook.CSoundEventOBBEntitySoundEventOBBThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundEventOBBEntity::SoundEventOBBThink::Post.");
            }
        }
    }

    internal void InvokeCSoundEventPathCornerEntitySoundEventPathCornerThinkPre(ref CSoundEventPathCornerEntitySoundEventPathCornerThinkPreContext ctx)
    {
        if (!DatamapsHook.CSoundEventPathCornerEntityHook.CSoundEventPathCornerEntitySoundEventPathCornerThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundEventPathCornerEntityHook.CSoundEventPathCornerEntitySoundEventPathCornerThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundEventPathCornerEntity::SoundEventPathCornerThink::Pre.");
            }
        }
    }

    internal void InvokeCSoundEventPathCornerEntitySoundEventPathCornerThinkPost(ref CSoundEventPathCornerEntitySoundEventPathCornerThinkPostContext ctx)
    {
        if (!DatamapsHook.CSoundEventPathCornerEntityHook.CSoundEventPathCornerEntitySoundEventPathCornerThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundEventPathCornerEntityHook.CSoundEventPathCornerEntitySoundEventPathCornerThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundEventPathCornerEntity::SoundEventPathCornerThink::Post.");
            }
        }
    }

    internal void InvokeCSoundEventSphereEntitySoundEventSphereThinkPre(ref CSoundEventSphereEntitySoundEventSphereThinkPreContext ctx)
    {
        if (!DatamapsHook.CSoundEventSphereEntityHook.CSoundEventSphereEntitySoundEventSphereThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundEventSphereEntityHook.CSoundEventSphereEntitySoundEventSphereThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundEventSphereEntity::SoundEventSphereThink::Pre.");
            }
        }
    }

    internal void InvokeCSoundEventSphereEntitySoundEventSphereThinkPost(ref CSoundEventSphereEntitySoundEventSphereThinkPostContext ctx)
    {
        if (!DatamapsHook.CSoundEventSphereEntityHook.CSoundEventSphereEntitySoundEventSphereThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundEventSphereEntityHook.CSoundEventSphereEntitySoundEventSphereThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundEventSphereEntity::SoundEventSphereThink::Post.");
            }
        }
    }

    internal void InvokeCSoundOpvarSetAutoRoomEntitySetOpvarThinkPre(ref CSoundOpvarSetAutoRoomEntitySetOpvarThinkPreContext ctx)
    {
        if (!DatamapsHook.CSoundOpvarSetAutoRoomEntityHook.CSoundOpvarSetAutoRoomEntitySetOpvarThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundOpvarSetAutoRoomEntityHook.CSoundOpvarSetAutoRoomEntitySetOpvarThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundOpvarSetAutoRoomEntity::SetOpvarThink::Pre.");
            }
        }
    }

    internal void InvokeCSoundOpvarSetAutoRoomEntitySetOpvarThinkPost(ref CSoundOpvarSetAutoRoomEntitySetOpvarThinkPostContext ctx)
    {
        if (!DatamapsHook.CSoundOpvarSetAutoRoomEntityHook.CSoundOpvarSetAutoRoomEntitySetOpvarThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundOpvarSetAutoRoomEntityHook.CSoundOpvarSetAutoRoomEntitySetOpvarThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundOpvarSetAutoRoomEntity::SetOpvarThink::Post.");
            }
        }
    }

    internal void InvokeCSoundOpvarSetBoxEntitySetOpvarThinkPre(ref CSoundOpvarSetBoxEntitySetOpvarThinkPreContext ctx)
    {
        if (!DatamapsHook.CSoundOpvarSetBoxEntityHook.CSoundOpvarSetBoxEntitySetOpvarThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundOpvarSetBoxEntityHook.CSoundOpvarSetBoxEntitySetOpvarThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundOpvarSetBoxEntity::SetOpvarThink::Pre.");
            }
        }
    }

    internal void InvokeCSoundOpvarSetBoxEntitySetOpvarThinkPost(ref CSoundOpvarSetBoxEntitySetOpvarThinkPostContext ctx)
    {
        if (!DatamapsHook.CSoundOpvarSetBoxEntityHook.CSoundOpvarSetBoxEntitySetOpvarThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundOpvarSetBoxEntityHook.CSoundOpvarSetBoxEntitySetOpvarThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundOpvarSetBoxEntity::SetOpvarThink::Post.");
            }
        }
    }

    internal void InvokeCSoundOpvarSetOBBWindEntitySetOpvarThinkPre(ref CSoundOpvarSetOBBWindEntitySetOpvarThinkPreContext ctx)
    {
        if (!DatamapsHook.CSoundOpvarSetOBBWindEntityHook.CSoundOpvarSetOBBWindEntitySetOpvarThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundOpvarSetOBBWindEntityHook.CSoundOpvarSetOBBWindEntitySetOpvarThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundOpvarSetOBBWindEntity::SetOpvarThink::Pre.");
            }
        }
    }

    internal void InvokeCSoundOpvarSetOBBWindEntitySetOpvarThinkPost(ref CSoundOpvarSetOBBWindEntitySetOpvarThinkPostContext ctx)
    {
        if (!DatamapsHook.CSoundOpvarSetOBBWindEntityHook.CSoundOpvarSetOBBWindEntitySetOpvarThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundOpvarSetOBBWindEntityHook.CSoundOpvarSetOBBWindEntitySetOpvarThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundOpvarSetOBBWindEntity::SetOpvarThink::Post.");
            }
        }
    }

    internal void InvokeCSoundOpvarSetPathCornerEntitySetOpvarThinkPre(ref CSoundOpvarSetPathCornerEntitySetOpvarThinkPreContext ctx)
    {
        if (!DatamapsHook.CSoundOpvarSetPathCornerEntityHook.CSoundOpvarSetPathCornerEntitySetOpvarThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundOpvarSetPathCornerEntityHook.CSoundOpvarSetPathCornerEntitySetOpvarThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundOpvarSetPathCornerEntity::SetOpvarThink::Pre.");
            }
        }
    }

    internal void InvokeCSoundOpvarSetPathCornerEntitySetOpvarThinkPost(ref CSoundOpvarSetPathCornerEntitySetOpvarThinkPostContext ctx)
    {
        if (!DatamapsHook.CSoundOpvarSetPathCornerEntityHook.CSoundOpvarSetPathCornerEntitySetOpvarThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundOpvarSetPathCornerEntityHook.CSoundOpvarSetPathCornerEntitySetOpvarThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundOpvarSetPathCornerEntity::SetOpvarThink::Post.");
            }
        }
    }

    internal void InvokeCSoundOpvarSetPointBaseSetOpvarThinkPre(ref CSoundOpvarSetPointBaseSetOpvarThinkPreContext ctx)
    {
        if (!DatamapsHook.CSoundOpvarSetPointBaseHook.CSoundOpvarSetPointBaseSetOpvarThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundOpvarSetPointBaseHook.CSoundOpvarSetPointBaseSetOpvarThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundOpvarSetPointBase::SetOpvarThink::Pre.");
            }
        }
    }

    internal void InvokeCSoundOpvarSetPointBaseSetOpvarThinkPost(ref CSoundOpvarSetPointBaseSetOpvarThinkPostContext ctx)
    {
        if (!DatamapsHook.CSoundOpvarSetPointBaseHook.CSoundOpvarSetPointBaseSetOpvarThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundOpvarSetPointBaseHook.CSoundOpvarSetPointBaseSetOpvarThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundOpvarSetPointBase::SetOpvarThink::Post.");
            }
        }
    }

    internal void InvokeCSoundOpvarSetPointEntitySetOpvarThinkPre(ref CSoundOpvarSetPointEntitySetOpvarThinkPreContext ctx)
    {
        if (!DatamapsHook.CSoundOpvarSetPointEntityHook.CSoundOpvarSetPointEntitySetOpvarThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundOpvarSetPointEntityHook.CSoundOpvarSetPointEntitySetOpvarThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundOpvarSetPointEntity::SetOpvarThink::Pre.");
            }
        }
    }

    internal void InvokeCSoundOpvarSetPointEntitySetOpvarThinkPost(ref CSoundOpvarSetPointEntitySetOpvarThinkPostContext ctx)
    {
        if (!DatamapsHook.CSoundOpvarSetPointEntityHook.CSoundOpvarSetPointEntitySetOpvarThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSoundOpvarSetPointEntityHook.CSoundOpvarSetPointEntitySetOpvarThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSoundOpvarSetPointEntity::SetOpvarThink::Post.");
            }
        }
    }

    internal void InvokeCSplineConstraintTransitionThinkPre(ref CSplineConstraintTransitionThinkPreContext ctx)
    {
        if (!DatamapsHook.CSplineConstraintHook.CSplineConstraintTransitionThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSplineConstraintHook.CSplineConstraintTransitionThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSplineConstraint::TransitionThink::Pre.");
            }
        }
    }

    internal void InvokeCSplineConstraintTransitionThinkPost(ref CSplineConstraintTransitionThinkPostContext ctx)
    {
        if (!DatamapsHook.CSplineConstraintHook.CSplineConstraintTransitionThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSplineConstraintHook.CSplineConstraintTransitionThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSplineConstraint::TransitionThink::Post.");
            }
        }
    }

    internal void InvokeCSpriteAnimateThinkPre(ref CSpriteAnimateThinkPreContext ctx)
    {
        if (!DatamapsHook.CSpriteHook.CSpriteAnimateThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSpriteHook.CSpriteAnimateThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSprite::AnimateThink::Pre.");
            }
        }
    }

    internal void InvokeCSpriteAnimateThinkPost(ref CSpriteAnimateThinkPostContext ctx)
    {
        if (!DatamapsHook.CSpriteHook.CSpriteAnimateThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSpriteHook.CSpriteAnimateThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSprite::AnimateThink::Post.");
            }
        }
    }

    internal void InvokeCSpriteAnimateUntilDeadPre(ref CSpriteAnimateUntilDeadPreContext ctx)
    {
        if (!DatamapsHook.CSpriteHook.CSpriteAnimateUntilDeadHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSpriteHook.CSpriteAnimateUntilDeadHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSprite::AnimateUntilDead::Pre.");
            }
        }
    }

    internal void InvokeCSpriteAnimateUntilDeadPost(ref CSpriteAnimateUntilDeadPostContext ctx)
    {
        if (!DatamapsHook.CSpriteHook.CSpriteAnimateUntilDeadHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSpriteHook.CSpriteAnimateUntilDeadHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSprite::AnimateUntilDead::Post.");
            }
        }
    }

    internal void InvokeCSpriteBeginFadeOutThinkPre(ref CSpriteBeginFadeOutThinkPreContext ctx)
    {
        if (!DatamapsHook.CSpriteHook.CSpriteBeginFadeOutThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSpriteHook.CSpriteBeginFadeOutThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSprite::BeginFadeOutThink::Pre.");
            }
        }
    }

    internal void InvokeCSpriteBeginFadeOutThinkPost(ref CSpriteBeginFadeOutThinkPostContext ctx)
    {
        if (!DatamapsHook.CSpriteHook.CSpriteBeginFadeOutThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSpriteHook.CSpriteBeginFadeOutThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSprite::BeginFadeOutThink::Post.");
            }
        }
    }

    internal void InvokeCSpriteExpandThinkPre(ref CSpriteExpandThinkPreContext ctx)
    {
        if (!DatamapsHook.CSpriteHook.CSpriteExpandThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSpriteHook.CSpriteExpandThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSprite::ExpandThink::Pre.");
            }
        }
    }

    internal void InvokeCSpriteExpandThinkPost(ref CSpriteExpandThinkPostContext ctx)
    {
        if (!DatamapsHook.CSpriteHook.CSpriteExpandThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CSpriteHook.CSpriteExpandThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CSprite::ExpandThink::Post.");
            }
        }
    }

    internal void InvokeCTriggerActiveWeaponDetectActiveWeaponThinkPre(ref CTriggerActiveWeaponDetectActiveWeaponThinkPreContext ctx)
    {
        if (!DatamapsHook.CTriggerActiveWeaponDetectHook.CTriggerActiveWeaponDetectActiveWeaponThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerActiveWeaponDetectHook.CTriggerActiveWeaponDetectActiveWeaponThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerActiveWeaponDetect::ActiveWeaponThink::Pre.");
            }
        }
    }

    internal void InvokeCTriggerActiveWeaponDetectActiveWeaponThinkPost(ref CTriggerActiveWeaponDetectActiveWeaponThinkPostContext ctx)
    {
        if (!DatamapsHook.CTriggerActiveWeaponDetectHook.CTriggerActiveWeaponDetectActiveWeaponThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerActiveWeaponDetectHook.CTriggerActiveWeaponDetectActiveWeaponThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerActiveWeaponDetect::ActiveWeaponThink::Post.");
            }
        }
    }

    internal void InvokeCTriggerFanPushThinkPre(ref CTriggerFanPushThinkPreContext ctx)
    {
        if (!DatamapsHook.CTriggerFanHook.CTriggerFanPushThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerFanHook.CTriggerFanPushThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerFan::PushThink::Pre.");
            }
        }
    }

    internal void InvokeCTriggerFanPushThinkPost(ref CTriggerFanPushThinkPostContext ctx)
    {
        if (!DatamapsHook.CTriggerFanHook.CTriggerFanPushThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerFanHook.CTriggerFanPushThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerFan::PushThink::Post.");
            }
        }
    }

    internal void InvokeCTriggerHurtHurtThinkPre(ref CTriggerHurtHurtThinkPreContext ctx)
    {
        if (!DatamapsHook.CTriggerHurtHook.CTriggerHurtHurtThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerHurtHook.CTriggerHurtHurtThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerHurt::HurtThink::Pre.");
            }
        }
    }

    internal void InvokeCTriggerHurtHurtThinkPost(ref CTriggerHurtHurtThinkPostContext ctx)
    {
        if (!DatamapsHook.CTriggerHurtHook.CTriggerHurtHurtThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerHurtHook.CTriggerHurtHurtThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerHurt::HurtThink::Post.");
            }
        }
    }

    internal void InvokeCTriggerHurtNavThinkPre(ref CTriggerHurtNavThinkPreContext ctx)
    {
        if (!DatamapsHook.CTriggerHurtHook.CTriggerHurtNavThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerHurtHook.CTriggerHurtNavThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerHurt::NavThink::Pre.");
            }
        }
    }

    internal void InvokeCTriggerHurtNavThinkPost(ref CTriggerHurtNavThinkPostContext ctx)
    {
        if (!DatamapsHook.CTriggerHurtHook.CTriggerHurtNavThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerHurtHook.CTriggerHurtNavThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerHurt::NavThink::Post.");
            }
        }
    }

    internal void InvokeCTriggerHurtRadiationThinkPre(ref CTriggerHurtRadiationThinkPreContext ctx)
    {
        if (!DatamapsHook.CTriggerHurtHook.CTriggerHurtRadiationThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerHurtHook.CTriggerHurtRadiationThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerHurt::RadiationThink::Pre.");
            }
        }
    }

    internal void InvokeCTriggerHurtRadiationThinkPost(ref CTriggerHurtRadiationThinkPostContext ctx)
    {
        if (!DatamapsHook.CTriggerHurtHook.CTriggerHurtRadiationThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerHurtHook.CTriggerHurtRadiationThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerHurt::RadiationThink::Post.");
            }
        }
    }

    internal void InvokeCTriggerImpactDisableThinkPre(ref CTriggerImpactDisableThinkPreContext ctx)
    {
        if (!DatamapsHook.CTriggerImpactHook.CTriggerImpactDisableThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerImpactHook.CTriggerImpactDisableThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerImpact::DisableThink::Pre.");
            }
        }
    }

    internal void InvokeCTriggerImpactDisableThinkPost(ref CTriggerImpactDisableThinkPostContext ctx)
    {
        if (!DatamapsHook.CTriggerImpactHook.CTriggerImpactDisableThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerImpactHook.CTriggerImpactDisableThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerImpact::DisableThink::Post.");
            }
        }
    }

    internal void InvokeCTriggerLerpObjectAttachedEntityThinkPre(ref CTriggerLerpObjectAttachedEntityThinkPreContext ctx)
    {
        if (!DatamapsHook.CTriggerLerpObjectHook.CTriggerLerpObjectAttachedEntityThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerLerpObjectHook.CTriggerLerpObjectAttachedEntityThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerLerpObject::AttachedEntityThink::Pre.");
            }
        }
    }

    internal void InvokeCTriggerLerpObjectAttachedEntityThinkPost(ref CTriggerLerpObjectAttachedEntityThinkPostContext ctx)
    {
        if (!DatamapsHook.CTriggerLerpObjectHook.CTriggerLerpObjectAttachedEntityThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerLerpObjectHook.CTriggerLerpObjectAttachedEntityThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerLerpObject::AttachedEntityThink::Post.");
            }
        }
    }

    internal void InvokeCTriggerLerpObjectLerpThinkPre(ref CTriggerLerpObjectLerpThinkPreContext ctx)
    {
        if (!DatamapsHook.CTriggerLerpObjectHook.CTriggerLerpObjectLerpThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerLerpObjectHook.CTriggerLerpObjectLerpThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerLerpObject::LerpThink::Pre.");
            }
        }
    }

    internal void InvokeCTriggerLerpObjectLerpThinkPost(ref CTriggerLerpObjectLerpThinkPostContext ctx)
    {
        if (!DatamapsHook.CTriggerLerpObjectHook.CTriggerLerpObjectLerpThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerLerpObjectHook.CTriggerLerpObjectLerpThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerLerpObject::LerpThink::Post.");
            }
        }
    }

    internal void InvokeCTriggerLerpObjectUnsetWaitForEntityPre(ref CTriggerLerpObjectUnsetWaitForEntityPreContext ctx)
    {
        if (!DatamapsHook.CTriggerLerpObjectHook.CTriggerLerpObjectUnsetWaitForEntityHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerLerpObjectHook.CTriggerLerpObjectUnsetWaitForEntityHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerLerpObject::UnsetWaitForEntity::Pre.");
            }
        }
    }

    internal void InvokeCTriggerLerpObjectUnsetWaitForEntityPost(ref CTriggerLerpObjectUnsetWaitForEntityPostContext ctx)
    {
        if (!DatamapsHook.CTriggerLerpObjectHook.CTriggerLerpObjectUnsetWaitForEntityHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerLerpObjectHook.CTriggerLerpObjectUnsetWaitForEntityHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerLerpObject::UnsetWaitForEntity::Post.");
            }
        }
    }

    internal void InvokeCTriggerLookTimeoutThinkPre(ref CTriggerLookTimeoutThinkPreContext ctx)
    {
        if (!DatamapsHook.CTriggerLookHook.CTriggerLookTimeoutThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerLookHook.CTriggerLookTimeoutThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerLook::TimeoutThink::Pre.");
            }
        }
    }

    internal void InvokeCTriggerLookTimeoutThinkPost(ref CTriggerLookTimeoutThinkPostContext ctx)
    {
        if (!DatamapsHook.CTriggerLookHook.CTriggerLookTimeoutThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerLookHook.CTriggerLookTimeoutThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerLook::TimeoutThink::Post.");
            }
        }
    }

    internal void InvokeCTriggerMultipleMultiTouchPre(ref CTriggerMultipleMultiTouchPreContext ctx)
    {
        if (!DatamapsHook.CTriggerMultipleHook.CTriggerMultipleMultiTouchHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerMultipleHook.CTriggerMultipleMultiTouchHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerMultiple::MultiTouch::Pre.");
            }
        }
    }

    internal void InvokeCTriggerMultipleMultiTouchPost(ref CTriggerMultipleMultiTouchPostContext ctx)
    {
        if (!DatamapsHook.CTriggerMultipleHook.CTriggerMultipleMultiTouchHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerMultipleHook.CTriggerMultipleMultiTouchHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerMultiple::MultiTouch::Post.");
            }
        }
    }

    internal void InvokeCTriggerMultipleMultiWaitOverPre(ref CTriggerMultipleMultiWaitOverPreContext ctx)
    {
        if (!DatamapsHook.CTriggerMultipleHook.CTriggerMultipleMultiWaitOverHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerMultipleHook.CTriggerMultipleMultiWaitOverHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerMultiple::MultiWaitOver::Pre.");
            }
        }
    }

    internal void InvokeCTriggerMultipleMultiWaitOverPost(ref CTriggerMultipleMultiWaitOverPostContext ctx)
    {
        if (!DatamapsHook.CTriggerMultipleHook.CTriggerMultipleMultiWaitOverHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerMultipleHook.CTriggerMultipleMultiWaitOverHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerMultiple::MultiWaitOver::Post.");
            }
        }
    }

    internal void InvokeCTriggerProximityMeasureThinkPre(ref CTriggerProximityMeasureThinkPreContext ctx)
    {
        if (!DatamapsHook.CTriggerProximityHook.CTriggerProximityMeasureThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerProximityHook.CTriggerProximityMeasureThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerProximity::MeasureThink::Pre.");
            }
        }
    }

    internal void InvokeCTriggerProximityMeasureThinkPost(ref CTriggerProximityMeasureThinkPostContext ctx)
    {
        if (!DatamapsHook.CTriggerProximityHook.CTriggerProximityMeasureThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerProximityHook.CTriggerProximityMeasureThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerProximity::MeasureThink::Post.");
            }
        }
    }

    internal void InvokeCTriggerSaveRetriggerWaitOverPre(ref CTriggerSaveRetriggerWaitOverPreContext ctx)
    {
        if (!DatamapsHook.CTriggerSaveHook.CTriggerSaveRetriggerWaitOverHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerSaveHook.CTriggerSaveRetriggerWaitOverHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerSave::RetriggerWaitOver::Pre.");
            }
        }
    }

    internal void InvokeCTriggerSaveRetriggerWaitOverPost(ref CTriggerSaveRetriggerWaitOverPostContext ctx)
    {
        if (!DatamapsHook.CTriggerSaveHook.CTriggerSaveRetriggerWaitOverHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerSaveHook.CTriggerSaveRetriggerWaitOverHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerSave::RetriggerWaitOver::Post.");
            }
        }
    }

    internal void InvokeCTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPre(ref CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPreContext ctx)
    {
        if (!DatamapsHook.CTriggerSndSosOpvarHook.CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerSndSosOpvarHook.CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerSndSosOpvar::SndSosTriggerOpvarWaitOver::Pre.");
            }
        }
    }

    internal void InvokeCTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPost(ref CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPostContext ctx)
    {
        if (!DatamapsHook.CTriggerSndSosOpvarHook.CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerSndSosOpvarHook.CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerSndSosOpvar::SndSosTriggerOpvarWaitOver::Post.");
            }
        }
    }

    internal void InvokeCTriggerSoundscapePlayerUpdateThinkPre(ref CTriggerSoundscapePlayerUpdateThinkPreContext ctx)
    {
        if (!DatamapsHook.CTriggerSoundscapeHook.CTriggerSoundscapePlayerUpdateThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerSoundscapeHook.CTriggerSoundscapePlayerUpdateThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerSoundscape::PlayerUpdateThink::Pre.");
            }
        }
    }

    internal void InvokeCTriggerSoundscapePlayerUpdateThinkPost(ref CTriggerSoundscapePlayerUpdateThinkPostContext ctx)
    {
        if (!DatamapsHook.CTriggerSoundscapeHook.CTriggerSoundscapePlayerUpdateThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CTriggerSoundscapeHook.CTriggerSoundscapePlayerUpdateThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CTriggerSoundscape::PlayerUpdateThink::Post.");
            }
        }
    }

    internal void InvokeCVoteControllerVoteControllerThinkPre(ref CVoteControllerVoteControllerThinkPreContext ctx)
    {
        if (!DatamapsHook.CVoteControllerHook.CVoteControllerVoteControllerThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CVoteControllerHook.CVoteControllerVoteControllerThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CVoteController::VoteControllerThink::Pre.");
            }
        }
    }

    internal void InvokeCVoteControllerVoteControllerThinkPost(ref CVoteControllerVoteControllerThinkPostContext ctx)
    {
        if (!DatamapsHook.CVoteControllerHook.CVoteControllerVoteControllerThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CVoteControllerHook.CVoteControllerVoteControllerThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CVoteController::VoteControllerThink::Post.");
            }
        }
    }

    internal void InvokeCWaterBulletBulletThinkPre(ref CWaterBulletBulletThinkPreContext ctx)
    {
        if (!DatamapsHook.CWaterBulletHook.CWaterBulletBulletThinkHook.HasPreListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CWaterBulletHook.CWaterBulletBulletThinkHook.InvokePre(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CWaterBullet::BulletThink::Pre.");
            }
        }
    }

    internal void InvokeCWaterBulletBulletThinkPost(ref CWaterBulletBulletThinkPostContext ctx)
    {
        if (!DatamapsHook.CWaterBulletHook.CWaterBulletBulletThinkHook.HasPostListeners)
        {
            return;
        }

        try
        {
            DatamapsHook.CWaterBulletHook.CWaterBulletBulletThinkHook.InvokePost(ref ctx);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                logger.LogError(e, "Error invoking GameHooks::Datamaps::CWaterBullet::BulletThink::Post.");
            }
        }
    }

}