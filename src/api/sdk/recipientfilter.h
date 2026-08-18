
#ifndef _recipient_filter_h
#define _recipient_filter_h

#include <public/irecipientfilter.h>
#include <api/interfaces/interfaces.h>

#define INVALID_PLAYER_SLOT_INDEX -1
#define INVALID_PLAYER_SLOT CPlayerSlot( INVALID_PLAYER_SLOT_INDEX )

class CRecipientFilter : public IRecipientFilter
{
public:
    CRecipientFilter(NetChannelBufType_t nBufType = BUF_RELIABLE, bool bInitMessage = false) :
        m_nBufType(nBufType), m_bInitMessage(bInitMessage) {
    }

    CRecipientFilter(IRecipientFilter* source, CPlayerSlot exceptSlot = -1)
    {
        m_Recipients = source->GetRecipients();
        m_nBufType = source->GetNetworkBufType();
        m_bInitMessage = source->IsInitMessage();

        if (exceptSlot != -1)
            m_Recipients.Clear(exceptSlot.Get());
    }

    ~CRecipientFilter() override {}

    NetChannelBufType_t GetNetworkBufType(void) const override { return m_nBufType; }
    bool IsInitMessage(void) const override { return m_bInitMessage; }
    const CPlayerBitVec& GetRecipients(void) const override { return m_Recipients; }
    CPlayerSlot GetPredictedPlayerSlot(void) const override { return m_PredictedSlot; }

    void AddAllPlayers(void)
    {
        m_Recipients.ClearAll();

        for (int i = 0; i < ABSOLUTE_PLAYER_LIMIT; i++)
            if (g_pGameEngine->GetClientSteamID(i) != nullptr)
                AddRecipient(i);
    }

    void RemoveAllPlayers(void)
    {
        m_Recipients.ClearAll();
    }

    void AddRecipient(CPlayerSlot slot)
    {
        if (slot.Get() >= 0 && slot.Get() < ABSOLUTE_PLAYER_LIMIT)
            m_Recipients.Set(slot.Get());
    }

    void RemoveRecipient(CPlayerSlot slot)
    {
        if (slot.Get() >= 0 && slot.Get() < ABSOLUTE_PLAYER_LIMIT)
            m_Recipients.Clear(slot.Get());
    }

    int GetRecipientCount()
    {
        const uint64_t bits = *reinterpret_cast<const uint64_t*>(GetRecipients().Base());

        int count = 0;
        for (int i = 0; i < 64; i++)
            if ((bits & (1ULL << i)))
                ++count;

        return count;
    }

protected:
    CPlayerBitVec m_Recipients;
    CPlayerSlot m_PredictedSlot = INVALID_PLAYER_SLOT;
    NetChannelBufType_t m_nBufType;
    bool m_bInitMessage;
    bool m_bDisabledPrediction;
};

class CBroadcastRecipientFilter : public CRecipientFilter
{
public:
    CBroadcastRecipientFilter(void)
    {
        AddAllPlayers();
    }
};

class CSingleRecipientFilter : public CRecipientFilter
{
public:
    CSingleRecipientFilter(CPlayerSlot nRecipientSlot, NetChannelBufType_t nBufType = BUF_RELIABLE, bool bInitMessage = false) :
        CRecipientFilter(nBufType, bInitMessage)
    {
        if (nRecipientSlot.Get() >= 0 && nRecipientSlot.Get() < ABSOLUTE_PLAYER_LIMIT)
            m_Recipients.Set(nRecipientSlot.Get());
    }
};

#endif